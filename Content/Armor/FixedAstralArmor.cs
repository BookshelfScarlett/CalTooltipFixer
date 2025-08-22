using CalamityMod.Items.Armor.Astral;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedAstralArmor : BaseFixedArmor
    {
        public override int HeadType => ArmorType<AstralHelm>();
        public override int BodyType => ArmorType<AstralBreastplate>();
        public override int LegsType => ArmorType<AstralLeggings>();
        public override bool ShouldPingOldFashioned => true;
    }
}