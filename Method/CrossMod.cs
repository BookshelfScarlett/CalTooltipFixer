using System.Collections.Generic;
using Terraria.ModLoader;

namespace CalTooltipFixer.Method
{
    public static class CrossMod
    {
        public static string HuntOfTheGodName => "CalamityHunt";
        public static string WrathOfTheGodName => "NoxusBoss";
        public static string Catalyst => "CatalystMod";
        public static string Inheritance => "CalamityInheritance";
        public static void SetupModLine(this List<TooltipLine> tooltips, Mod mod, string modName, string color)
        {
            string textPath = MethodList.GetLocalText(modName);
            if (string.IsNullOrEmpty(textPath))
                return;
            tooltips.QuickNewLine(mod, textPath, color);
        }
    }
}