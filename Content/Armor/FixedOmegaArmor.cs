using CalamityMod.Items.Armor.OmegaBlue;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedOmegaArmor : BaseFixedArmor
    {
        public override int HeadType => ArmorType<OmegaBlueHelmet>();
        public override int BodyType => ArmorType<OmegaBlueChestplate>();
        public override int LegsType => ArmorType<OmegaBlueTentacles>();
        public override bool ShouldPingOldFashioned => true;
    }
}