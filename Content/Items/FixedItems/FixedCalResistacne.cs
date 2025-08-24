using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Content.Items.BossesResistances;
using CalTooltipFixer.Core;
using CalTooltipFixer.Method;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalTooltipFixer.Content.Items.FixedItems
{
    public partial class FixedItem : GlobalItem
    {
        private void ProfanedBossesCalDR(Item item, List<TooltipLine> tooltips)
        {
            bool anyProfaned = ModPlayer.ResistanceProviAndGuardsBool;
            if (!anyProfaned)
                return;
            string ProfanedSpecial = $"{TooltipConstants.ResistanceItemText}{nameof(ResistanceProviAndGuards)}.Special";
            string generalDRText = TooltipConstants.BossResistance;
            string provi = "ProviName".GetBossName();
            if (item.CountsAsClass<TrueMeleeDamageClass>()||item.CountsAsClass<TrueMeleeNoSpeedDamageClass>())
            {
                tooltips.QuickNewLineWithColor("CalDR", Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, ProfanedSpecial, TooltipConstants.TrueMeleeName.ToLangValue(), 50);
            }
            if (item.ThisItem<HellsSun>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, generalDRText, provi, 20);
            }
            if (item.ThisItem<ElementalLance>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, generalDRText, provi, 65);
            }
        }
        private void DoGCalDR(Item item, List<TooltipLine> tooltips)
        {
            bool anyDoG = ModPlayer.ResistanceDoGBool;
            if (!anyDoG)
                return;
            string DoGSpecial = $"{TooltipConstants.ResistanceItemText}{nameof(ResistanceDoG)}.";
            string generalDRText = $"{TooltipConstants.BossResistance}";
            string bossName = "DoGName".GetBossName();
            //震波：20
            if (item.ThisItem<WavePounder>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, generalDRText, bossName, 20);
            }
            if (item.ThisItem<TimeBolt>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, DoGSpecial+"TimeBolt", bossName, 115);
            }
            if (item.ThisItem<EidolicWail>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, generalDRText, bossName, 50);
            }
            if (item.ThisItem<VenusianTrident>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, generalDRText, bossName, 50);
            }
            if (item.ThisItem<NuclearFury>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, generalDRText, bossName, 80);
            }
            if (item.ThisItem<Valediction>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, DoGSpecial+"Valediction", bossName, 80);
            }


        }
        private void SentinalsCalDR(Item item, List<TooltipLine> tooltips)
        {
            bool anySentinels = ModPlayer.ResistanceSentinelsBool;
            if (!anySentinels)
                return;

            string sentinalSpecialBase = $"{TooltipConstants.ResistanceItemText}{nameof(ResistanceSentinels)}.";
            string generalDRText = $"{TooltipConstants.BossResistance}";
            string ceaselessBoss = "CeaselessName".GetBossName();
            string stormWeaverBoss = "StormWeaverName".GetBossName();
            //全体真近战：50
            if (item.CountsAsClass<TrueMeleeDamageClass>() || item.CountsAsClass<TrueMeleeNoSpeedDamageClass>())
            {
                const int trueMeleeResis = 50;
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, $"{sentinalSpecialBase}Special", stormWeaverBoss, ceaselessBoss, TooltipConstants.TrueMeleeName.ToLangValue(), trueMeleeResis);
            }
            //亵渎小刀：50
            //50DR
            const int dr50 = 50;
            if (item.ThisItem<DazzlingStabberStaff>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, generalDRText, stormWeaverBoss, dr50);
            }
            if (item.ThisItem<ElementalAxe>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, generalDRText, stormWeaverBoss, dr50);
            }
            if (item.ThisItem<PristineFury>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, $"{sentinalSpecialBase}PristineFury", stormWeaverBoss, dr50);   
            }
            if (item.ThisItem<TacticiansTrumpCard>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod, $"{sentinalSpecialBase}{"TacticiansTrumpCardBoom"}", stormWeaverBoss, dr50);
            }
            //熔岩切碎,75伤害
            if (item.ThisItem<MoltenAmputator>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod,  generalDRText, stormWeaverBoss, 75);
            }
            //暗温能量：星环杖60
            if (item.ThisItem<StellarTorusStaff>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, TooltipConstants.CalamityModifyColor);
                tooltips.QuickNewLineNoColor(Mod,  generalDRText, ceaselessBoss, 60);
            }
        }
        private void CalamityDamageResisitance(Item item, List<TooltipLine> tooltips)
        {
            
            #region 字符值所在的地址
            string resistanceSpecial = $"{TooltipConstants.WeaponPath}CalamityResistanceValue.Special";
            #endregion
            //为了避免硬编出现的问题，这里需要间接通过本地化转过去
            string aresName = Language.GetTextValue(MethodList.StringNameHandler("NPCName.AresName"));
            string artemisAndApolloName = Language.GetTextValue(MethodList.StringNameHandler("NPCName.ExoTwinsName"));
            string thanatosName = Language.GetTextValue(MethodList.StringNameHandler("NPCName.ThanatosName")); 
            Color colorValue = new(132,255,255);
            bool anyAres = ModPlayer.ResistanceAresBool;
            bool anyExoTwins = ModPlayer.ResistanceExoTwinsBool;
            bool anyThanatos = ModPlayer.ResistanceThanatosBool;
            #region 免伤表比我命都长
            int resis90 = 90;
            int resis85 = 85;
            int resis80 = 80;
            int resis75 = 75;
            int resis70 = 70;
            int resis65 = 65;
            int resis55 = 55;
            int resis50 = 50;
            int resis40 = 40;
            int resis35 = 35;
            #endregion
            //先给真近战都上一个
            if (item.ThisItem<TheFinalDawn>())
            {
                //终曲黎明对塔有两套不同免伤
                if (anyThanatos)
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".TheFinalDawn.Lingering", thanatosName, resis85.ToString());
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".TheFinalDawn.Lunge", thanatosName, resis35.ToString());
                }
            }
            //日蚀投矛
            if (item.ThisItem<EclipsesFall>())
            {
                if (anyAres || anyThanatos || anyExoTwins)
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                if (anyAres)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, aresName, resis80.ToString());
                if (anyThanatos)
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".EclipsesFall.Thanatos", thanatosName, resis70.ToString());
                if (anyExoTwins)
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".EclipsesFall.ExoTwins", artemisAndApolloName, resis90.ToString());
            }
            //鸿蒙方舟
            if (item.ThisItem<ArkoftheCosmos>())
            {
                if (anyAres || anyThanatos)
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                if (anyAres)
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".ArkoftheCosmosThrown", args:[aresName, resis90.ToString()]);
                if (anyThanatos)
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".ArkoftheCosmosThrown", args:[thanatosName, resis75.ToString()]);
            }
            //黄炮
            if (item.ThisItem<DynamicPursuer>())
            {
                if (anyAres || anyThanatos)
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                if (anyAres)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, aresName, resis70.ToString());

                //皇天御流对塔塔纳托斯40%免伤与右键爆炸75%免伤
                if (anyThanatos)
                {
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, thanatosName, resis40.ToString());
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".DynamicPursuer", thanatosName, resis80.ToString());
                }
            }
            //龙怒
            if (item.ThisItem<DragonRage>())
            {
                if (anyAres || anyThanatos)
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                if (anyAres)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, aresName, resis80.ToString());
                if (anyThanatos)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, thanatosName, resis75.ToString());
            }
            //天顶
            if (item.ThisItem(ItemID.Zenith))
            {
                if (anyAres || anyThanatos)
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                if (anyAres)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, aresName, resis80.ToString());
                if (anyThanatos)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, thanatosName, resis75.ToString());
            }
            //魔君水晶
            //你的意思是这东西在50面板的情况下吃50%塔纳托斯免伤？
            if (item.ThisItem<YharimsCrystal>())
            {
                if (anyAres || anyThanatos)
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                if (anyAres)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, aresName, resis80.ToString());
                if (anyThanatos)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, thanatosName, resis50.ToString());
            }
            //Rancor:
            if (item.ThisItem<Rancor>())
            {
                if (anyAres || anyThanatos)
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                if (anyAres)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, aresName, resis80.ToString());
                if (anyThanatos)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, thanatosName, resis50.ToString());
            }

            //j接下来Boss划分免伤
            if (anyThanatos)
            {
                if (item.ThisItem<PrismaticBreaker>())
                {

                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".PrismaticBreaker", thanatosName, resis65.ToString());
                }

                if (item.ThisItem<Sirius>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".Sirius", thanatosName, resis70.ToString());
                }

                if (item.ThisItem<Ultima>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".Ultima", thanatosName, resis70.ToString());
                }

                if (item.ThisItem<TarragonThrowingDart>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".TarragonThrowingDart", thanatosName, resis50.ToString());
                }

                if (item.ThisItem<TheEnforcer>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".TheEnforcer", thanatosName, resis65.ToString());
                }
                //小鸡大炮、Omicron右键
                if (item.ThisItem<Omicron>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".Omicron", thanatosName, resis65.ToString());
                }

                if (item.ThisItem<ChickenCannon>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, thanatosName, resis50.ToString());
                }
                //血喷火器、虚空漩涡、几点光伏、等离子雷
                if (item.ThisItem<BloodBoiler>() || item.ThisItem<VoidVortex>() || item.ThisItem<VoltaicClimax>() || item.ThisItem<PlasmaGrenade>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, thanatosName, resis80.ToString());
                }

                //盖尔大剑、卡兰德
                if (item.ThisItem<MirrorofKalandra>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".Kalander", thanatosName, resis75.ToString());
                }

                if (item.ThisItem<GaelsGreatsword>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".GaelsGreatsword", thanatosName, resis75.ToString());
                }

                if (item.ThisItem<StellarTorusStaff>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, thanatosName, resis35.ToString());
                }

                if (item.ThisItem<Wrathwing>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, resistanceSpecial + ".Wrathwing", thanatosName, resis55.ToString());
                }
            }
            if (MethodList.AnyNPCs<AresBody>())
            {
                if (item.ThisItem<AetherfluxCannon>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, aresName, resis75.ToString());
                }
                if (item.ThisItem<Murasama>())
                {
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityResistanceText, colorValue);
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistance, aresName, resis85.ToString());
                }
            }
            if (item.CountsAsClass<TrueMeleeDamageClass>())
            {
                if (anyAres)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistanceTrueMelee, args:[aresName, resis50.ToString()]);
                if (anyThanatos)
                    tooltips.QuickNewLineNoColor(Mod, TooltipConstants.BossResistanceTrueMelee, args:[thanatosName, resis35.ToString()]);
            }
        }
    }
}