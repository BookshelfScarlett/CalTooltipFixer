using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items.FixedItems
{
    public partial class FixedItem : GlobalItem
    {
        private void FuckCatalyst(Item item, List<TooltipLine> tooltips)
        {
            //真他妈绝了
            Mod catalyst = CalTooltipFixer.Catalyst;
            if (catalyst is null)
                return;
            //星史的免伤表又臭又长又复杂，只能这么打表了
            //对不起，灾厄的免伤表更长
            string basePath = $"{TooltipConstants.WeaponPath}AstralSlimeResis.";
            string thisPath = $"{basePath}General";

            if (catalyst.TryFind(TooltipConstants.CatalystSuperBoss, out ModNPC boss) && ModPlayer.ResistanceCatalystSuperbossBool)
            {
                string specialDRPath = $"{basePath}SpecialDR";
                //1：遍历所有武器而非遍历弹幕，将免伤打入
                if (item.CountsAsClass<MeleeDamageClass>())
                {
                    int meleeDR = 90;
                    tooltips.QuickNewLineWithColor("CatalystDR", Mod,TooltipConstants.CatalystResistanceText, TooltipConstants.CatalystModifyColor);
                    tooltips.QuickNewLineNoColor(Mod, specialDRPath, TooltipConstants.MeleeClassName.ToLangValue(), meleeDR);
                }
                else if (item.CountsAsClass<MagicDamageClass>())
                {
                    int magicDR = 55;
                    tooltips.QuickNewLineWithColor("CatalystDR", Mod,TooltipConstants.CatalystResistanceText, TooltipConstants.CatalystModifyColor);
                    tooltips.QuickNewLineNoColor(Mod, specialDRPath, TooltipConstants.MagicClassName.ToLangValue(), magicDR);
                }
                else if (item.CountsAsClass<TrueMeleeDamageClass>()) 
                {
                    int trueMeleeDR = 80;
                    string trueMeleeDRPath = $"{basePath}TrueMelee";
                    tooltips.QuickNewLineWithColor("CatalystDR", Mod,TooltipConstants.CatalystResistanceText, TooltipConstants.CatalystModifyColor);
                    tooltips.QuickNewLineNoColor(Mod, trueMeleeDRPath, TooltipConstants.TrueMeleeName.ToLangValue(), trueMeleeDR);
                }
                //潜伏攻击需要给每个盗贼武器单独写一个显示
                else if (item.CountsAsClass<RogueDamageClass>())
                {
                    int stealthDR = 65;
                    string stealthItem = $"[i:CalamityMod/{nameof(SilencingSheath)}]";
                    tooltips.QuickNewLineWithColor("CatalystDR", Mod,TooltipConstants.CatalystResistanceText, TooltipConstants.CatalystModifyColor);
                    string rogueStealthSpecialDRPath = $"{basePath}RogueStealthDRSpecial";
                    tooltips.QuickNewLineNoColor(Mod, rogueStealthSpecialDRPath, stealthItem, stealthDR);
                }
                //将其他的伤害打入进去，单独打表。
                else if (item.CountsAsClass<RangedDamageClass>())
                {
                    int otherDR = 75;
                    tooltips.QuickNewLineWithColor("CatalystDR", Mod,TooltipConstants.CatalystResistanceText, TooltipConstants.CatalystModifyColor);
                    tooltips.QuickNewLineNoColor(Mod, specialDRPath, TooltipConstants.MagicClassName.ToLangValue(), otherDR);
                }
                else if (item.CountsAsClass<SummonDamageClass>() || item.CountsAsClass<SummonMeleeSpeedDamageClass>())
                {
                    int otherDR = 75;
                    tooltips.QuickNewLineWithColor("CatalystDR", Mod,TooltipConstants.CatalystResistanceText, TooltipConstants.CatalystModifyColor);
                    tooltips.QuickNewLineNoColor(Mod, specialDRPath, TooltipConstants.SummonClassName.ToLangValue(), otherDR);
                }
                //2.每个武器单独打表，不用注册额外的行
                //TODO:动态修改
                if (item.ThisItem<TheSwarmer>())
                {
                    int dr = 85;
                    tooltips.QuickNewLineNoColor(Mod, thisPath, dr);
                }
                if (item.ThisItem(ItemID.NebulaArcanum))
                {
                    int dr = 75;
                    tooltips.QuickNewLineNoColor(Mod, thisPath, dr);
                }
                if (item.ThisItem<HivePod>())
                {
                    int dr = 80;
                    tooltips.QuickNewLineNoColor(Mod, thisPath, dr);
                }
                string rogueStealthDRPath = $"{basePath}RogueStealthDR";
                if (item.ThisItem<FantasyTalisman>() || item.ThisItem<RegulusRiot>())
                {
                    int stealthDR = 80;
                    tooltips.QuickNewLineNoColor(Mod, rogueStealthDRPath, stealthDR);
                }
                if (item.ThisItem<Malachite>())
                {
                    int stealthDR = 70;
                    tooltips.QuickNewLineNoColor(Mod, rogueStealthDRPath, stealthDR);
                }
                if (item.ThisItem<DukesDecapitator>())
                {
                    int dr = 40;
                    tooltips.QuickNewLineNoColor(Mod, thisPath, dr);
                }
                if (item.ThisItem<Vortexpopper>())
                {
                    int combinedWithHybrus = 90;
                    int combinedWithPlague = 80;

                    string vortexpopperPath = $"{basePath}VortexpopperDR";
                    tooltips.QuickNewLineNoColor(Mod, vortexpopperPath, combinedWithHybrus, combinedWithPlague);
                }
                //狮源流星单独打表
                if (item.ThisItem<LeonidProgenitor>())
                {
                    int smallAndStarDR = 90;
                    int bigAndSelfDR = 80;

                    string lionMetoerSpecialPath = $"{basePath}LeonidProgenitorSpecialDR";
                    tooltips.QuickNewLineNoColor(Mod, lionMetoerSpecialPath, smallAndStarDR, bigAndSelfDR);
                }

            }
        }
    }
}