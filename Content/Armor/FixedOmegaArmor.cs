using System.Collections.Generic;
using CalamityMod.Items.Armor.OmegaBlue;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedOmegaArmor : BaseFixedArmor
    {
        #region Args:
        public const int TentacleDamage = 390;
        public const int ArmorsetBounesDamage = 10;
        public static string TentacleName => MethodList.StringNameHandler("ProjectileName.Tentacle").ToLangValue();
        public string BestClass = TooltipConstants.BestClassName.ToLangValue();
        #endregion
        public override int HeadType => ArmorType<OmegaBlueHelmet>();
        public override int BodyType => ArmorType<OmegaBlueChestplate>();
        public override int LegsType => ArmorType<OmegaBlueTentacles>();
        public override bool ShouldPingOldFashioned => true;
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine)
        {
            string oldFashionedLine = TooltipConstants.GetOldFashionedSupportValue.GetFormatString(TentacleName, TentacleDamage, BestClass);
            string basicText = ThisArmorFixedText.ToLangValue().GetFormatString();
            if (!string.IsNullOrEmpty(basicText))
            {
                string realText = oldFashionedLine + "\n" + basicText;
                if (setBonusLine is not null)
                    setBonusLine.Text += Header + realText;
            }
        }
    }
}