
using CalamityMod.Items.Armor.GemTech;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedGemTechArmor : BaseFixedArmor
    {
        public override int HeadType => ArmorType<GemTechHeadgear>();
        public override int BodyType => ArmorType<GemTechBodyArmor>();
        public override int LegsType => ArmorType<GemTechSchynbaulds>();
        public override bool ShouldPingOldFashioned => true;
    }
}