using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.Weapons.Rogue;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Content.Players;
using CalTooltipFixer.Core;
using CalTooltipFixer.Method;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items.FixedItems
{
    public partial class FixedItem : GlobalItem 
    {
        public override bool InstancePerEntity => true;
        public static Player LocalPlayer => Main.LocalPlayer;
        public static TooltipPlayer ModPlayer => LocalPlayer.ThisMod();
        #region 字符串挂件门
        public static string ProjectileString => MethodList.StringNameHandler("ProjectileName.");
        public static string BuffIconName => Language.GetTextValue(MethodList.StringNameHandler("BuffPlaceholder."));
        public static string OldFashionedDamageShown => Language.GetTextValue(MethodList.GetLocalText("OldFashionedSupport"));
        #endregion
        #region 鞭子数据
        //万花筒
        public double RainbowWhipMul = 1.12;
        //晨星
        public double MaceWhipMul = 1.11;
        //迪朗达尔
        public double SwordWhipMul = 1.09;
        //皮革鞭
        public double LeatherWhipMul = 1.08;
        //荆鞭
        public double ThornWhipMul = 1.04;
        //骨
        public double BoneWhipMul = 1.08;
        //冷
        public double CoolWhipMul = 1.08;
        //鞭炮
        public int FireWhipMul = 200;
        #endregion
        #region 其他
        public int PlagueSMGCrtisChance = 75;
        public int NanoBladeCap = 5;
        public int WarbannerTiles = 30;
        public int NorfleetAntiCheeseTime = 3;
        #endregion
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            //根据表单与类型给予不同的提示字符串
            foreach (var itemID in CalFixerList.GiveExtraTooltipList)
            {
                if (item.ThisItem(itemID))
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalExtraText, TooltipConstants.CalExtraColor);
            }

            foreach (var giveCalItemID in CalFixerList.GiveCalTooltipList)
            {
                if (item.ThisItem(giveCalItemID))
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityModifyText, TooltipConstants.CalamityModifyColor);
            }
            //鞭子
            
            WhipsCases(item,tooltips);
            //处理盗贼潜伏倍率
            ModifyRogueDamageTooltip(item, tooltips);
            //打表每一个可能需额外工具提的的武器
            WeaponsCases(item, tooltips);
            //打表饰品
            AccessoriesAndMiscCases(item, tooltips);
            //处理古典酒
            ApplyOldFashioned(item, tooltips);
            //灾厄也干了：免伤
            CalamityDamageResisitance(item, tooltips);
            DoGCalDR(item, tooltips);
            SentinalsCalDR(item, tooltips);
            ProfanedBossesCalDR(item, tooltips);
            //Hunt联动
            FuckHunt(item, tooltips);
            //遗产联动
            Inheritance(item, tooltips);
            //灾劫联动
            FuckCatalyst(item, tooltips);

        }
        
        #region 古典酒，只有标记作用
        public void ApplyOldFashioned(Item item, List<TooltipLine> tooltips)
        {
            //如果玩家身上当前没有古典酒效果，则做掉
            if (!LocalPlayer.Calamity().oldFashioned)
                return;
            foreach (var itemID in CalFixerList.OldFashionedList)
            {
                if (item.ThisItem(itemID))
                    tooltips.ApplyOldFashionedPing(Mod);
            }
            //盾冲饰品需单独打表
        }

        #endregion

        private void Inheritance(Item item, List<TooltipLine> tooltips)
        {
            Mod inheritance = CalTooltipFixer.Inheritance;
            if (inheritance is null)
                return;
            //使其支持武士纹章
            if (inheritance.TryFind("SamuraiBadge", out ModItem samuraiBadge))
            {
                if (item.ThisItem(samuraiBadge.Type))
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalExtraText, TooltipConstants.CalExtraColor);
                    ModifyWarbannerTooltip(tooltips, TooltipConstants.ItemPath + "WarbannerBonus", 0.3f);
                }
            }
        }
        
        #region 处理阳炎战旗
        public void ModifyWarbannerTooltip(List<TooltipLine> tooltips, string path, float bonus = 0.2f)
        {
            float multipler = GetWarbannerDamage(bonus);
            int floatToInt = (int)(multipler * 100f);
            tooltips.QuickNewLineNoColor(Mod, path, WarbannerTiles, floatToInt.ToString());
        }
        public static float GetWarbannerDamage(float MaxBonus)
        {
            Player player = Main.LocalPlayer;
            float bonus = 0f;
            float MaxDistance = 480f;

            int closestNPC = -1;
            foreach (NPC nPC in Main.ActiveNPCs)
            {
                if (nPC.IsAnEnemy() && !nPC.dontTakeDamage)
                {
                    closestNPC = nPC.whoAmI;
                    break;
                }
            }
            float distance = -1f;
            foreach (NPC nPC in Main.ActiveNPCs)
            {
                if (nPC.IsAnEnemy() && !nPC.dontTakeDamage)
                {
                    float distance2 = Math.Abs(nPC.position.X + (float)(nPC.width / 2) - (player.position.X + (float)(player.width / 2))) + Math.Abs(nPC.position.Y + (float)(nPC.height / 2) - (player.position.Y + (float)(player.height / 2)));
                    if (distance == -1f || distance2 < distance)
                    {
                        distance = distance2;
                        closestNPC = nPC.whoAmI;
                    }
                }
            }

            if (closestNPC != -1)
            {
                NPC actualClosestNPC = Main.npc[closestNPC];

                float generousHitboxWidth = Math.Max(actualClosestNPC.Hitbox.Width / 2f, actualClosestNPC.Hitbox.Height / 2f);
                float hitboxEdgeDist = actualClosestNPC.Distance(player.Center) - generousHitboxWidth;

                if (hitboxEdgeDist < 0)
                    hitboxEdgeDist = 0;

                if (hitboxEdgeDist < MaxDistance)
                {
                    bonus = MathHelper.Lerp(0f, MaxBonus, 1f - (hitboxEdgeDist / MaxDistance));

                    if (bonus > MaxBonus)
                        bonus = MaxBonus;
                }
            }

            return bonus;
        }
        #endregion
        public void ModifyRogueDamageTooltip(Item item, List<TooltipLine> tooltips)
        {
            if (item.ModItem is RogueWeapon rogueWeapon)
            {
                float multipler = rogueWeapon.StealthDamageMultiplier;
                //如果倍率为1f则返回，没什么必要
                if (multipler == 1f)
                    return;
                //获得已经带有盗贼伤害倍率占位的的文本：
                string baseTextValue = Language.GetTextValue(TooltipConstants.WeaponPath + "RogueStealthMul");

                if (!string.IsNullOrEmpty(baseTextValue))
                {
                    int floatToInt = (int)(multipler * 100f);
                    string trueLine = string.Format(baseTextValue, floatToInt.ToString());
                    tooltips.Add(new TooltipLine(Mod, "Name", trueLine));
                }
            }
        }
    }
    public static class FixedMethod
    {
        public static bool ThisItem(this Item item, int type) => item.type == type;
        public static bool ThisItem<T>(this Item itme) where T : ModItem => ThisItem(itme, ModContent.ItemType<T>());
        public static int Item<T>() where T : ModItem => ModContent.ItemType<T>();
        public static void ApplyOldFashionedPing(this List<TooltipLine> tooltip, Mod mod)
        {
            string pingPath = MethodList.GetLocalText("ItemTooltip.CanOldFashionedBounes");
            string getOldFashionedName = "OldFashioned";
            string thisItemName = $"[i:CalamityMod/{getOldFashionedName}]";
            tooltip.QuickNewLineNoColor(mod, pingPath, thisItemName);
        }

    }
}