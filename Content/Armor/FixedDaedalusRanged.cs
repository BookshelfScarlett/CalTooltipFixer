using System.Collections.Generic;
using CalamityMod.Items.Armor.Daedalus;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedDaedalusRagned : BaseFixedArmor
    {
        internal const int DamageMul = 33;
        public override int HeadType => ArmorType<DaedalusHeadRanged>();
        public override int BodyType => ArmorType<DaedalusBreastplate>();
        public override int LegsType => ArmorType<DaedalusLeggings>();
        public override string ArmorTextPath => "FixedDaedalus.Ranged";
        public override bool ShouldPingOldFashioned => true;
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine)
        {
            bool ifIsFull = owner.ThisBodyPart("Head", HeadType) && owner.ThisBodyPart("Body", BodyType) && owner.ThisBodyPart("Legs", LegsType);
            if (!ifIsFull)
                return;
            string formatedBaseText = MethodList.GetLocalText("ArmorTooltip.FixedDaedalus.General").ToLangValue().GetFormatString(FixedDaedalusRogue.CrystalBulletName);
            string fixedText = ThisArmorFixedText.ToLangValue();
            if (string.IsNullOrEmpty(fixedText))
                return;
            string formatText = fixedText.GetFormatString(DamageMul, TooltipConstants.RangedClassName.ToLangValue());
            setBonusLine.Text += Header + formatText;
        }
        public override bool ExtraOldFashion(Player owner)
        {
            return owner.ThisBodyPart("Head", HeadType) && owner.ThisBodyPart("Body", BodyType) && owner.ThisBodyPart("Legs", LegsType);
        }
    }
}