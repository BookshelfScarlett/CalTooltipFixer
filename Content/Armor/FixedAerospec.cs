using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Armor.Aerospec;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedAerospec : BaseFixedArmor
    {
        internal const int FeatherDamage = 35;
        internal const int FeatherCounts = 4;
        internal const int FeatherHomingDistTile = 9;
        internal const int ValDamage = 20;
        internal static string ValName => "Val".GetProjName();
        public override int BodyType => ArmorType<AerospecBreastplate>();
        public override int LegsType => ArmorType<AerospecLeggings>();
        public override bool ShouldPingOldFashioned => true;
        //下面会重写多个方法的。
        public int[] AeroHead =
        [
            ArmorType<AerospecHelmet>(),
            ArmorType<AerospecHat>(),
            ArmorType<AerospecHeadgear>(),
            ArmorType<AerospecHood>(),
            ArmorType<AerospecHelm>()
        ];
        //重写原本Applies方法
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            int head = entity.type;
            for (int i = 0; i < AeroHead.Length; i++)
            {
                if(head == AeroHead[i])
                    return entity.type == head || entity.type == BodyType || entity.type == LegsType;
            }
            return false;
        }

        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine)
        {
            bool isWearingSummonHead = owner.ThisBodyPart("Head", ArmorType<AerospecHelmet>());

            string fixedText = ThisArmorFixedText.ToLangValue();
            if (string.IsNullOrEmpty(fixedText))
                return;
            
            string formatText = fixedText.GetFormatString(FeatherCounts, FeatherDamage, TooltipConstants.BestClassName.ToLangValue(), FeatherHomingDistTile);
            setBonusLine.Text += Header + formatText;
            //假定玩家佩戴召唤头盔，追加女武神伤害显示
            if (isWearingSummonHead)
            {
                string valText = TooltipConstants.GetOldFashionedSupportValue.ToLangValue().GetFormatString(ValName,ValDamage,TooltipConstants.SummonClassName);
                //换行
                setBonusLine.Text += "\n" + valText;
            }
        }
    }
}