using System.Collections.Generic;
using CalamityMod.Items.Armor.Prismatic;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedPrismaticArmor : BaseFixedArmor
    {
        internal const int LasersDamage = 30;
        internal const int RocketDamage = 25;
        public override int HeadType => ArmorType<PrismaticHelmet>();
        public override int BodyType => ArmorType<PrismaticRegalia>();
        public override int LegsType => ArmorType<PrismaticGreaves>();
        public override bool ShouldPingOldFashioned => true;
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, bool isFull, TooltipLine setBonusLine)
        {
            string fixedText = Language.GetTextValue(ThisArmorFixedTextValue);
            if (!string.IsNullOrEmpty(fixedText))
            {
                string laserName = MethodList.GetStringValueFromHandler("ProjectileName.Laser").ToLangValue();
                int laserRealDamage = owner.ToOldFashioned(LasersDamage);
                string realText = fixedText.GetFormatString(laserName, laserRealDamage, RocketDamage, TooltipConstants.MagicClassName.ToLangValue());
                setBonusLine.Text += Header + realText;
            }
        }
    }
}