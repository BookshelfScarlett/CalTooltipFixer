using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.Armor.Demonshade;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Content.Items;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedDemonshadeArmor : BaseFixedArmor
    {
        #region 众神削弱
        internal double CalMulToTarget = 2.5;
        internal double WarthMulToTarget = 1.5;
        internal double CalMulToPlayer = 1.25;
        internal double WrathMulToPlayer = 1.5;
        #endregion
        #region 
        internal const int RedDevilDmage = 10000;
        internal static string RedDevilName => MethodList.StringNameHandler("ProjectileName.RedDevil").ToLangValue();
        #endregion
        public override int HeadType => ArmorType<DemonshadeHelm>();
        public override int BodyType => ArmorType<DemonshadeBreastplate>();
        
        public override int LegsType => ArmorType<DemonshadeGreaves>();
        public override bool ShouldPingOldFashioned => true; 
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine)
        {
            //先安装Header
            //而后众神判定先跑
            WrathoftheGodsNerf(setBonusLine);
            //而后我们再跑其他的判定
            string fixedText = TooltipConstants.GetOldFashionedSupportValue.GetFormatString(RedDevilName, RedDevilDmage, TooltipConstants.BestClassName.ToLangValue());
            if (string.IsNullOrEmpty(fixedText))
                return;
            string header = GetPingText + "\n"; 
            setBonusLine.Text += header + fixedText;
        }

        private void WrathoftheGodsNerf(TooltipLine setBonusLine)
        {
            //没开启众神之怒时不要做任何事
            if (CalTooltipFixer.WrathOfTheGod is null)
                return;
            string wotgColorText = GetColoredText(TooltipConstants.WrathoftheGodsColor, TooltipConstants.WrathoftheGodsText);
            //将众神标记削弱打入
            string fixedText = ThisArmorFixedText.ToLangValue();
            if (string.IsNullOrEmpty(fixedText))
                return;
            string formatText = fixedText.GetFormatString(CalMulToTarget.ToMulString(), WarthMulToTarget.ToMulString(), CalMulToPlayer.ToMulString(), WrathMulToPlayer.ToMulString());
            string realText = $"\n{wotgColorText}\n{formatText}\n";
            //下一个换行的预处理
            setBonusLine.Text += realText;
        }
    }
}