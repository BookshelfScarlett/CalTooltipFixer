using System.Collections.Generic;
using CalamityMod.Items.Armor.Astral;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedAstralArmor : BaseFixedArmor
    {
        #region 数值
        public const int FallenStarDamage = 120;
        public const double HitDamageReduction = 6.25;
        #endregion
        public override int HeadType => ArmorType<AstralHelm>();
        public override int BodyType => ArmorType<AstralBreastplate>();
        public override int LegsType => ArmorType<AstralLeggings>();
        public override bool ShouldPingOldFashioned => true;
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine)
        {
            string fixedText = ThisArmorFixedText.ToLangValue();
            if (string.IsNullOrEmpty(fixedText))
                return;

            string realText = fixedText.GetFormatString(FallenStarDamage, HitDamageReduction);
            setBonusLine.Text += Header + realText;
        }
    }
}
