using System.Collections.Generic;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedSolarArmor : BaseFixedArmor
    {
        public const int SolarShieldDamage = 1200;
        public const int NerfedDR = 20;
        public override int HeadType => ItemID.SolarFlareHelmet;
        public override int BodyType => ItemID.SolarFlareBreastplate;
        public override int LegsType => ItemID.SolarFlareLeggings;
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine)
        {
            string fixedText = ThisArmorFixedText.ToLangValue();
            if (string.IsNullOrEmpty(fixedText))
                return;

            string realText = fixedText.GetFormatString(SolarShieldDamage, NerfedDR);
            string hasColoredText = GetColoredText(TooltipConstants.CalamityModifyColor, TooltipConstants.CalamityModifyText);
            setBonusLine.Text += "\n" + hasColoredText + "\n" + realText;
        }
    }
}