using System;
using System.Collections.Generic;
using System.Linq;
using CalTooltipFixer.Content.Items.BuffPlaceholder;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Method
{
    public static partial class MethodList
    {
        public static string GetLocalText(string text) => "Mods.CalTooltipFixer." + text;
        public static string StringNameHandler(string text) => GetLocalText("StringHandler." + text);
        public static string GetStringValueFromHandler(string text) => StringNameHandler(text);
        #region 快速创建新TooltipLine的多个重载方法
        /// <summary>
        /// 直接创建一个新的Tooltip，会在最底下显示
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="mod"></param>
        /// <param name="path"></param>
        [Obsolete("弃用")]
        public static void QuickNewLine(this List<TooltipLine> tooltips, Mod mod, string path)
        {
            string textValue = Language.GetTextValue(path);
            if (string.IsNullOrEmpty(textValue))
                return;
            
            if (textValue is not null)
            {
                tooltips.Add(new TooltipLine(mod, "Name", textValue));
            }
            
        }
        /// <summary>
        /// 直接创建一个新的Tooltip，输入16进制字符串以修改显示颜色
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="mod"></param>
        /// <param name="path"></param>
        /// <param name="colorValue">16进制颜色字符串</param>
        [Obsolete("已废弃，使用QuickNewLineWithColor替代，原因：直接输入16进制字符串更难使用且无法与键入参做出区分")]
        public static void QuickNewLine(this List<TooltipLine> tooltips, Mod mod, string path, string colorValue, bool abaondoned = false)
        {
            string textValue = Language.GetTextValue(path);
            if (string.IsNullOrEmpty(textValue))
                return;
            //处理染色换行
            string[] lines = textValue.Split(['\n'], StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = $"[c/{colorValue}:{lines[i]}]";
            }
            string realValue = string.Join("\n", lines);
            //创建即可
            tooltips.Add(new TooltipLine(mod, "name", realValue));
        }
        /// <summary>
        /// 直接创建一个新的Tooltip，附带键入参数
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="mod"></param>
        /// <param name="path"></param>
        /// <param name="args">键入参</param>
        [Obsolete("弃用")]
        public static void QuickNewLine(this List<TooltipLine> tooltips, Mod mod, string path, params object[] args)
        {
            string textValue = Language.GetTextValue(path);
            //空文本返回
            if (string.IsNullOrEmpty(textValue))
                return;
            //格式化文本

            string realValue;
            try
            {
                realValue = string.Format(textValue, args);
            }
            catch
            {
                //格式化出错则返回初始文本
                realValue = textValue + "，格式化出错";
            }
            //创建即可
            tooltips.Add(new TooltipLine(mod, "name", realValue));
        }
        /// <summary>
        /// 直接创建一个新的Tooltip，输入16进制字符串以修改显示颜色，附带键入参数
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="mod"></param>
        /// <param name="path"></param>
        /// <param name="colorValue">16进制颜色字符串</param>
        /// <param name="args">键入参</param>
        [Obsolete("已废弃，使用QuickNewLineWithColor替代，原因：直接输入16进制字符串更难使用且无法与键入参做出区分")]
        public static void QuickNewLine(this List<TooltipLine> tooltips, Mod mod, string path, string colorValue, bool abaondoned = false, params object[] args)
        {
            string textValue = Language.GetTextValue(path);
            //空文本返回
            if (string.IsNullOrEmpty(textValue))
                return;
            //格式化文本
            string formattedValue;
            try
            {
                formattedValue = string.Format(textValue, args);
            }
            catch
            {
                //格式化出错则返回初始文本
                formattedValue = textValue + "，格式化出错";
            }
            //处理染色换行
            string[] lines = formattedValue.Split(['\n'], StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = $"[c/{colorValue}:{lines[i]}]";
            }
            string realValue = string.Join("\n", lines);
            //创建即可
            tooltips.Add(new TooltipLine(mod, "name", realValue));
        }
        /// <summary>
        /// 直接创建一个新的Tooltip，会自动将xna转化为16进制字符串，附带键入参数
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="mod"></param>
        /// <param name="path"></param>
        /// <param name="color">颜色</param>
        /// <param name="args">键入参</param>
        public static void QuickNewLineWithColor(this List<TooltipLine> tooltips, Mod mod, string path, Color color, params object[] args)
        {
            string textValue = Language.GetTextValue(path);
            string colorValue = color.ToHexStringColor();
            //空文本返回
            if (string.IsNullOrEmpty(textValue))
                return;
            //格式化文本
            string formattedValue;
            try
            {
                formattedValue = string.Format(textValue, args);
            }
            catch
            {
                //格式化出错则返回初始文本
                formattedValue = textValue + "，格式化出错";
            }
            //处理染色换行
            string[] lines = formattedValue.Split(['\n'], StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = $"[c/{colorValue}:{lines[i]}]";
            }
            string realValue = string.Join("\n", lines);
            //创建即可
            tooltips.Add(new TooltipLine(mod, "name", realValue));
        }
        /// <summary>
        /// 直接创建一个新的Tooltip，会自动将xna转化为16进制字符串
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="mod"></param>
        /// <param name="path"></param>
        /// <param name="color">y颜色/param>
        public static void QuickNewLineWithColor(this List<TooltipLine> tooltips, Mod mod, string path, Color color)
        {
            string textValue = Language.GetTextValue(path);
            string colorValue = color.ToHexStringColor();
            if (string.IsNullOrEmpty(textValue))
                return;
            //处理染色换行
            string[] lines = textValue.Split(['\n'], StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = $"[c/{colorValue}:{lines[i]}]";
            }
            string realValue = string.Join("\n", lines);
            //创建即可
            tooltips.Add(new TooltipLine(mod, "name", realValue));
        }
        /// <summary>
        /// 直接创建一个新的Tooltip，附带键入参数
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="mod"></param>
        /// <param name="path"></param>
        /// <param name="args">键入参</param>
        public static void QuickNewLineNoColor(this List<TooltipLine> tooltips, Mod mod, string path, params object[] args)
        {
            string textValue = Language.GetTextValue(path);
            //空文本返回
            if (string.IsNullOrEmpty(textValue))
                return;
            //格式化文本

            string realValue;
            try
            {
                realValue = string.Format(textValue, args);
            }
            catch
            {
                //格式化出错则返回初始文本
                realValue = textValue + "，格式化出错";
            }
            //创建即可
            tooltips.Add(new TooltipLine(mod, "name", realValue));
        }
        /// <summary>
        /// 直接创建一个新的Tooltip，会在最底下显示
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="mod"></param>
        /// <param name="path"></param>
        public static void QuickNewLineNoColor(this List<TooltipLine> tooltips, Mod mod, string path)
        {
            string textValue = Language.GetTextValue(path);
            if (string.IsNullOrEmpty(textValue))
                return;
            
            if (textValue is not null)
            {
                tooltips.Add(new TooltipLine(mod, "Name", textValue));
            }
            
        }
        #endregion
        /// <summary>
        /// 用替换的方法完全重写Tooltip
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="replacedTextPath"></param>
        public static void FuckThisTooltipAndReplace(this List<TooltipLine> tooltips, string replacedTextPath)
        {
            tooltips.RemoveAll((line) => line.Mod == "Terraria" && line.Name != "Tooltip0" && line.Name.StartsWith("Tooltip"));
            TooltipLine getTooltip = tooltips.FirstOrDefault((x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
            if (getTooltip is not null)
                getTooltip.Text = Language.GetTextValue(replacedTextPath);
        }
        /// <summary>
        /// 用替换的方法完全重写Tooltip，附带键入参
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="replacedTextPath"></param>
        /// <param name="args">键入参</param>
        public static void FuckThisTooltipAndReplace(this List<TooltipLine> tooltips, string replacedTextPath, params object[] args)
        {
            tooltips.RemoveAll((line) => line.Mod == "Terraria" && line.Name != "Tooltip0" && line.Name.StartsWith("Tooltip"));
            TooltipLine getTooltip = tooltips.FirstOrDefault((x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
            if (getTooltip is not null)
            {
                string baseTextValue = Language.GetTextValue(replacedTextPath);
                string trueValue;
                try
                {
                    trueValue = string.Format(baseTextValue, args);
                }
                catch
                {
                    trueValue = baseTextValue + "，格式化出错";
                }
                getTooltip.Text = trueValue;
            }
        }
        /// <summary>
        /// 将Xna颜色转化为16进制编码
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHexStringColor(this Color color)
        {
            return $"{color.R:X2}{color.G:X2}{color.B:X2}";
        }
        /// <summary>
        /// 获取物品形式的Buff名字，这些都注册在本mod里面了
        /// </summary>
        /// <param name="interName"></param>
        /// <param name="buffName"></param>
        /// <returns></returns>
        public static string GetBuffIcon(string interName)
        {
            string path = GetLocalText($"{IconBuffBase.BuffIconPath}.{interName}.DisplayName");
            string actualName = Language.GetTextValue(path);
            return $"[i:CalTooltipFixer/{interName}]{actualName}";
        }
        public static string GetLangValue(this string path)
        {
            return Language.GetTextValue(path);
        }
        public static string GetLangValue(this string path, string wantedText)
        {
            return Language.GetTextValue(path + wantedText);
        }
    }
}