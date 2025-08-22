using System;
using System.Collections.Generic;
using CalamityMod.Items.VanillaArmorChanges;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedFrostArmor : BaseFixedArmor
    {
        public const double MaxBounsDamage = 1.15;
        public const float ProymixtedDamage = 0.15f;
        public const int MinDistanceTiles = (int)(FrostArmorSetChange.MinDistance / 16f);
        public const int MaxDistanceTiles = (int)(FrostArmorSetChange.MaxDistance / 16f);
        internal string GeneralTextPath => ThisArmorFixedTextValue + ".GeneralText";
        internal static string MeleeClass => TooltipConstants.MeleeClassName.ToLangValue();
        internal static string RangedClass => TooltipConstants.RangedClassName.ToLangValue();
        public override int HeadType => ItemID.FrostHelmet;
        public override int BodyType => ItemID.FrostBreastplate;
        public override int LegsType => ItemID.FrostLeggings;
        public override int ShouldApplyTo() => HeadType;
        //常规静态变量最好在外部就进行赋值，避免重复局部定义
        internal static string HasColoredText = GetColoredText(TooltipConstants.CalExtraColor, TooltipConstants.CalExtraText);
        internal static string Header => "\n" + HasColoredText + "\n";
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, bool isFull, TooltipLine setBonusLine)
        {
            //搜索距离玩家屏幕范围内的怪。
            //why？
            NPC closestNPC = owner.Center.GetClosestTarget(Main.screenWidth / 2);
            ref float MeleeBoost = ref owner.ThisMod().FrostArmorMeleeBoost;
            ref float RangedBoost = ref owner.ThisMod().FrostArmorRangedBoost;
            ref bool cacheNPC = ref owner.ThisMod()._cacheForstArmorNPC;
            string GeneralText = GeneralTextPath.ToLangValue().GetFormatString(MaxBounsDamage, MinDistanceTiles, MaxDistanceTiles);

            string BoostingTextPath = ThisArmorFixedTextValue + ".BoostingText";
            if (closestNPC is not null)
            {
                float getDistance = closestNPC.Distance(owner.Center);
                //0f = 最近距离, 1f = 最大距离极更远
                float distanceInter = Utils.GetLerpValue(FrostArmorSetChange.MinDistance, FrostArmorSetChange.MaxDistance, getDistance, true);
                //获取加成。    
                MeleeBoost = MathHelper.Lerp(0f, ProymixtedDamage, 1 - distanceInter);
                RangedBoost = MathHelper.Lerp(0f, ProymixtedDamage, distanceInter);
                //已经找到NPC了,将其设置为true
                cacheNPC = true;
            }
            else
            {
                //附近没有敌人重置下方数值
                MeleeBoost = 0f;
                RangedBoost = 0f;
            }
            if (setBonusLine is not null)
            {
                //只有实际有加成时才会显示这个东西……什么的。
                if (MeleeBoost == 0f && RangedBoost == 0f)
                {
                    setBonusLine.Text += Header + GeneralText;
                }
                else if (closestNPC is not null || cacheNPC)
                {
                    string formatedText = BoostingTextPath.ToLangValue().GetFormatString(MeleeClass, MeleeBoost.ToIntSingle().ToString(), RangedClass, RangedBoost.ToIntSingle().ToString());
                    setBonusLine.Text += Header + GeneralText + "\n" + formatedText;
                }
            }
        }
    }
}