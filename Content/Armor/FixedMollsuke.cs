using System.Collections.Generic;
using CalamityMod.Items.Armor.Mollusk;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedMollsuke : BaseFixedArmor
    {
        public int SlamsDamage = 140;
        public override int HeadType => ArmorType<MolluskShellmet>();
        public override int BodyType => ArmorType<MolluskShellplate>();
        public override int LegsType => ArmorType<MolluskShelleggings>();
        public override string ArmorTextPath => "MolluskArmor";
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine)
        {
            string fixedText = Language.GetTextValue(ThisArmorFixedText);
            if (string.IsNullOrEmpty(fixedText))
                return;
            string realText = fixedText.GetFormatString(SlamsDamage, TooltipConstants.SummonClassName.ToLangValue());
            string hasColoredText = $"[c/{TooltipConstants.CalExtraColor.ToHexStringColor()}:{TooltipConstants.CalExtraText.ToLangValue()}]";
            setBonusLine.Text += "\n" + hasColoredText + "\n" + realText;
        }
    }
}