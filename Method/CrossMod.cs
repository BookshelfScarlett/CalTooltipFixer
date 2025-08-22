using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net.Repository.Hierarchy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalTooltipFixer.Method
{
    public static class CrossMod
    {
        public static string HuntOfTheGodName => "CalamityHunt";
        public static string WrathOfTheGodName => "NoxusBoss";
        public static string Catalyst => "CatalystMod";
        public static string Inheritance => "CalamityInheritance";
        public static string Infernum => "InfernumMode";
        public static void SetupModLine(this List<TooltipLine> tooltips, Mod mod, string modName, Color color)
        {
            string textPath = MethodList.GetLocalText(modName);
            if (string.IsNullOrEmpty(textPath))
                return;
            tooltips.QuickNewLineWithColor(mod, textPath, color);
        }
        public static bool? GetInheritancePlayerFieldBoolen(this Player player, string boolField)
        {
            return GetModPlayerField(player, Inheritance, "CIPlayer", "CalamityInheritancePlayer", boolField);
        }
        #region 反射获取玩家类内部字段名
        private static Type _cachedTargetType;
        private static FieldInfo _cachedBoolenField;
        private static string _lastModName;
        private static string _lastClassName;
        private static string _lastFieldName;
        private static string _lastNamespaceName;
        /// <summary>
        /// 获取特定模组玩家类里的boolen类型字段名，弱引用
        /// </summary>
        /// <param name="modName">模组名</param>
        /// <param name="namespaceName">命名空间</param>
        /// <param name="className">玩家类名，需要包括命名空间(如CalamityInheritance.(命名空间).className</param>
        /// <param name="fieldName">字段名</param>
        /// <returns></returns>
        public static bool? GetModPlayerField(this Player player, string modName, string namespaceName, string className, string fieldName)
        {
            try
            {
                //查阅模组加载情况
                Mod targetMod = ModLoader.GetMod(modName);
                if (targetMod is null)
                {
                    ClearCacheIfNotCorrectMod(modName, namespaceName, className, fieldName);
                    return null;
                }
                //获取目标玩家类类型
                Type targetType = GetCachedType(targetMod, modName, namespaceName, className, fieldName);
                if (targetType is null)
                    return null;
                //限定GetModPlayer的方法
                MethodInfo getModPlayerMethod = typeof(Player).GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(m => m.Name == "GetModPlayer" && m.IsGenericMethod && m.GetParameters().Length == 0).FirstOrDefault();
                if (getModPlayerMethod is null)
                {
                    CalTooltipFixer.Instance.Logger.Error("未找到GetModPlayer<T>()方法");
                    return null;
                }
                //绑定泛型类泛型且调用。
                MethodInfo genericMethod = getModPlayerMethod.MakeGenericMethod(targetType);
                //获取ModPlayer实例
                ModPlayer targetModPlayer = (ModPlayer)genericMethod.Invoke(player,null);
                if (targetModPlayer is null)
                    return null;
                //获取字段名
                FieldInfo boolField = GetCachedField(targetType, modName, namespaceName,className, fieldName);
                if (boolField is null)
                    return null;
                //最后获取当前值
                return (bool)boolField.GetValue(targetModPlayer);
            }
            catch (Exception ex)
            {
                CalTooltipFixer.Instance.Logger.Error($"获取字段失败: {ex.Message}");
                return null;
            }
        }

        private static FieldInfo GetCachedField(Type targetType, string modName, string namespaceName, string className, string fieldName)
        {
            var send = CalTooltipFixer.Instance.Logger;
            if (_cachedBoolenField is null || !IsCacheValid(modName, namespaceName, className, fieldName))
            {
                //查找实例字段
                _cachedBoolenField = targetType.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (_cachedBoolenField is null)
                    send.Warn($"{targetType.Name} 中无public字段 {fieldName}");

                if (_cachedBoolenField.FieldType != typeof(bool))
                {
                    send.Warn($"{fieldName} is not boolen type, actual type:{_cachedBoolenField.FieldType.Name}");
                    return null;
                }
            }
            return _cachedBoolenField;
        }


        //缓存目标类型主要是为了减少反射情况
        private static Type GetCachedType(Mod targetMod, string modName, string namespaceName,string className, string fieldName)
        {
            var send = CalTooltipFixer.Instance.Logger;
            //参数发生变化，清空旧的缓存
            if (_cachedTargetType is null || !IsCacheValid(modName, namespaceName,className, fieldName))
            {
                string fullTypeName = $"{targetMod.Name}.{namespaceName}.{className}";
                _cachedTargetType = targetMod.Code.GetType(fullTypeName);
                //验证类型是否有效
                if (_cachedTargetType is null)
                {
                    send.Warn($"No corrected class: {fullTypeName}, checked if is corrected");
                    return null;
                }
                //子类不对，返回
                if (!typeof(ModPlayer).IsAssignableFrom(_cachedTargetType))
                {
                    send.Warn($"{fullTypeName} is not ModPlayer class");
                    return null;
                }
                //更新类名缓存与字段缓存
                UpdateCacheInfo(modName, namespaceName,className, fieldName);
            }
            return _cachedTargetType;
        }

        //参数发生变化时清空缓存
        private static void ClearCacheIfNotCorrectMod(string modName, string namespaceName, string className, string fieldName)
        {
            if (!IsCacheValid(modName, namespaceName,className, fieldName))
            {
                _cachedTargetType = null;
                _cachedBoolenField = null;
            }
        }
        //更新缓存
        private static void UpdateCacheInfo(string modName, string namespaceName, string className, string fieldName)
        {
            _lastModName = modName;
            _lastClassName = className;
            _lastFieldName = fieldName;
            _lastNamespaceName = namespaceName;
        }
        //判断缓存是否有效
        private static bool IsCacheValid(string modName, string namespaceName,string className, string fieldName)
        {
            return _lastModName == modName
                && _lastClassName == className
                && _lastFieldName == fieldName
                && _lastNamespaceName == namespaceName;
        }
        #endregion
    }
}