using CalamityMod.Items.Armor.Astral;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedAstralArmor : BaseFixedArmor
    {
        #region 数值
        public const int FallenStarDamage = 120;
        public const double HitDamageReduction = 6.25
        #endregion
        public override int HeadType => ArmorType<AstralHelm>();
        public override int BodyType => ArmorType<AstralBreastplate>();
        public override int LegsType => ArmorType<AstralLeggings>();
        public override bool ShouldPingOldFashioned => true;
    }
}
