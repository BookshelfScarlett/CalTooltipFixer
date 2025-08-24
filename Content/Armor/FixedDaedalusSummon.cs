using System.Collections.Generic;
using CalamityMod.Items.Armor.Daedalus;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedDaedalusSummon : BaseFixedArmor
    {
        internal const int CrystalDamage = 95;
        public override int HeadType => ArmorType<DaedalusHeadSummon>();
        public override int BodyType => ArmorType<DaedalusBreastplate>();
        public override int LegsType => ArmorType<DaedalusLeggings>();
        public override string ArmorTextPath => "FixedDaedalus.Summon";
        public override bool ShouldPingOldFashioned => true;
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine)
        {
            bool ifIsFull = owner.ThisBodyPart("Head", HeadType) && owner.ThisBodyPart("Body", BodyType) && owner.ThisBodyPart("Legs", LegsType);
            if (!ifIsFull)
                return;
            string fixedText = ThisArmorFixedText.ToLangValue();
            if (string.IsNullOrEmpty(fixedText))
                return;

            string formatText = fixedText.GetFormatString(CrystalDamage, TooltipConstants.SummonClassName.ToLangValue());
            setBonusLine.Text += Header + formatText;
        }
    }
}