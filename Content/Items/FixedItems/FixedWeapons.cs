using System.Collections.Generic;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Ranged;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items.FixedItems
{
    public partial class FixedItem : GlobalItem
    {
        private void WeaponsCases(Item item, List<TooltipLine> tooltips)
        {
            if (item.ThisItem<PlagueTaintedSMG>())
            {
                bool exoLore = LocalPlayer.GetModPlayerField(CrossMod.Inheritance, "CIPlayer", "CalamityInheritancePlayer", "LoreExo").Value;
                if (exoLore)
                {
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.WeaponPath + "PlagueSMG.Crits", PlagueSMGCrtisChance);
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.WeaponPath + "PlagueSMG.Special");
                }
            }
            
            //诺法雷：反作弊
            if (item.ThisItem<Norfleet>())
            {
                Color norfleetColor = new(220, 20, 60);
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.WeaponPath + "NorfleetAntiCheese", norfleetColor, NorfleetAntiCheeseTime);
            }
            //寒冰弹幕：显示最近的距大小
            if (item.ThisItem<IceBarrage>())
            {
                int tiles = 31;
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.WeaponPath + "IceBarrageHomingDist", tiles);
            }
            // //归墟：显示实际使用条件
            // if (item.ThisItem<CosmicImmaterializer>())
            // {
            //     int slot10 = 10;
            //     tooltips.QuickNewLineWithColor(Mod, TooltipConstants.WeaponPath + "CosmicActualRequirement", Color.White, slot10);
            // }
        }

        private void WhipsCases(Item item, List<TooltipLine> tooltips)
        {
            if (item.ThisItem(ItemID.RainbowWhip))
                tooltips.FuckThisTooltipAndReplace($"{TooltipConstants.WeaponPath}RainbowWhip", RainbowWhipMul);

            if (item.ThisItem(ItemID.BoneWhip))
                tooltips.FuckThisTooltipAndReplace($"{TooltipConstants.WeaponPath}BoneWhip", BoneWhipMul);

            if (item.ThisItem(ItemID.FireWhip))
                tooltips.FuckThisTooltipAndReplace($"{TooltipConstants.WeaponPath}FireWhip", FireWhipMul);

            if (item.ThisItem(ItemID.CoolWhip))
                tooltips.FuckThisTooltipAndReplace($"{TooltipConstants.WeaponPath}CoolWhip", CoolWhipMul);

            if (item.ThisItem(ItemID.MaceWhip))
                tooltips.FuckThisTooltipAndReplace($"{TooltipConstants.WeaponPath}MaceWhip", MaceWhipMul);

            if (item.ThisItem(ItemID.SwordWhip))
                tooltips.FuckThisTooltipAndReplace($"{TooltipConstants.WeaponPath}SwordWhip", SwordWhipMul);
            //皮鞭
            if (item.ThisItem(ItemID.BlandWhip))
                tooltips.FuckThisTooltipAndReplace($"{TooltipConstants.WeaponPath}LeatherWhip", LeatherWhipMul);
            
            if (item.ThisItem(ItemID.ThornWhip))
            {
                //荆鞭需要单独处理灾劫的情况
                tooltips.FuckThisTooltipAndReplace($"{TooltipConstants.WeaponPath}ThornWhip", ThornWhipMul);
                if (CalTooltipFixer.Catalyst is not null)
                {
                    int thronDamage = 14;
                    int thronUseTime = 40;
                    int kbIndex = tooltips.FindIndex(line => line.Name == "Material");
                    string fuckCatalyst = Language.GetTextValue($"{TooltipConstants.WeaponPath}ThornWhipCatalystNerfed");
                    if (!string.IsNullOrEmpty(fuckCatalyst))
                    {
                        string realLine = string.Format(fuckCatalyst, thronDamage.ToString(), thronUseTime.ToString());
                        tooltips.Insert(kbIndex + 1, new TooltipLine(Mod, "Insert", realLine));
                    }
                }
            }
            //瑞德，我好爱你，cnm！
            if (item.ThisItem(ItemID.IvyWhip))
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "IvyWhip");
        }   
    }
}