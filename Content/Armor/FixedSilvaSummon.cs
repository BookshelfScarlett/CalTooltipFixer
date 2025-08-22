using System.Collections.Generic;
using CalamityMod.Items.Armor.Silva;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedSilvaSummon : BaseFixedArmor
    {
        public const int CrystalDamage = 600;
        public override int HeadType => ArmorType<SilvaHeadSummon>();
        public override int BodyType => ArmorType<SilvaArmor>();
        public override int LegsType => ArmorType<SilvaLeggings>();
        public override bool ShouldPingOldFashioned => true;
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, bool isFull, TooltipLine setBonusLine)
        {
            string fixedText = Language.GetTextValue(ThisArmorFixedTextValue);
            if (!string.IsNullOrEmpty(fixedText))
            {
                string crystalName = MethodList.GetStringValueFromHandler("ProjectileName.SilvaCrystal").ToLangValue();
                int realDamage = owner.ToOldFashioned(CrystalDamage);
                string realText = fixedText.GetFormatString(crystalName, realDamage, TooltipConstants.SummonClassName.ToLangValue());
                if (setBonusLine is not null)
                {
                    string hasColoredText = GetColoredText(TooltipConstants.CalExtraColor, TooltipConstants.CalExtraText);
                    setBonusLine.Text += "\n" + hasColoredText + "\n" + realText;
                }
            }
        }
    }
}