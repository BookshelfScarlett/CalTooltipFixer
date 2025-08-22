using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Accessories;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Content.Items.BuffPlaceholder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
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
            string formattedValue = textValue.GetFormatString(args);
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
            string realValue = textValue.GetFormatString(args);
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
                string trueValue = GetFormatString(baseTextValue, args);
                getTooltip.Text = trueValue;
            }
        }
        /// <summary>
        /// 将Xna颜色转化为16进制编码
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHexStringColor(this Color color) => $"{color.R:X2}{color.G:X2}{color.B:X2}";
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
        public static void HoldingShiftToReplace(this List<TooltipLine> tooltips, Mod mod, string replacedPath, string pingText, Color pingColor)
        {
            if (Main.keyState.IsKeyDown(Keys.LeftShift))
                tooltips.FuckThisTooltipAndReplace(replacedPath);
            else
            {
                tooltips.QuickNewLineWithColor(mod, pingText, pingColor);
                tooltips.QuickNewLineNoColor(mod, TooltipConstants.ItemPath + nameof(AngelicAlliance) + ".HoldShiftTo");
            }
        }

        public static string ToLangValue(this string path) => Language.GetTextValue(path);
        public static string ToLangValue(this string path, string wantedText) => Language.GetTextValue(path + wantedText);
        /// <summary>
        /// 获取格式化文本的扩展方法
        /// </summary>
        /// <param name="basicString">可编入字段的基本字符串</param>
        /// <param name="objects">键入参</param>
        /// <returns>一个带有键入参的格式化字符串</returns>
        public static string GetFormatString(this string basicString, params object[] objects)
        {
            try
            {
                return string.Format(basicString, objects);
            }
            catch
            {
                return basicString + "，格式化出错";
            }
        }
    }
}