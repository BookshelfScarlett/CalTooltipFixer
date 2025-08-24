using System.Collections.Generic;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedSpectreMask : BaseFixedArmor
    {
        public const int GhostDamageNerfed = 75;
        public override int HeadType => ItemID.SpectreMask;
        public override int BodyType => ItemID.SpectreRobe;
        public override int LegsType => ItemID.SpectreBoots;
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine)
        {
            string fixedText = ThisArmorFixedText.ToLangValue();
            if (string.IsNullOrEmpty(fixedText))
                return;
            
            string realText = fixedText.GetFormatString(GhostDamageNerfed);
            string hasColoredText = GetColoredText(TooltipConstants.CalExtraColor, TooltipConstants.CalExtraText);
            setBonusLine.Text += "\n" + hasColoredText + "\n" + realText;
        }
    }
}