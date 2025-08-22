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
        public const int NerfedDR = 12;
        public override int HeadType => ItemID.SolarFlareHelmet;
        public override int BodyType => ItemID.SolarFlareBreastplate;
        public override int LegsType => ItemID.SolarFlareLeggings;
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, bool isFull, TooltipLine setBonusLine)
        {
            string fixedText = ThisArmorFixedTextValue.ToLangValue();
            if (!string.IsNullOrEmpty(fixedText))
            {
                string realText = fixedText.GetFormatString(SolarShieldDamage, NerfedDR);
                if (setBonusLine is not null)
                {
                    string hasColoredText = GetColoredText(TooltipConstants.CalamityModifyColor, TooltipConstants.CalamityModifyText);
                    setBonusLine.Text += "\n" + hasColoredText + "\n" + realText;
                }
            }
        }
    }
}