using System;
using System.Collections.Generic;
using System.Threading;
using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Content.Items.BuffPlaceholder;
using CalTooltipFixer.Content.Players;
using CalTooltipFixer.Core;
using CalTooltipFixer.Method;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items
{
    public class FixedItem : GlobalItem 
    {
        public override bool InstancePerEntity => true;
        public static Player LocalPlayer => Main.LocalPlayer;
        public static TooltipPlayer ModPlayer => LocalPlayer.ThisMod();
        #region 字符串挂件门
        public static string MeleeClass => Language.GetTextValue(MethodList.StringNameHandler("ClassName.Melee"));
        public static string RangedClass => Language.GetTextValue(MethodList.StringNameHandler("ClassName.Ranged"));
        public static string MagicClass => Language.GetTextValue(MethodList.StringNameHandler("ClassName.Magic"));
        public static string SummonClass => Language.GetTextValue(MethodList.StringNameHandler("ClassName.Summon"));
        public static string RogueClass => Language.GetTextValue(MethodList.StringNameHandler("ClassName.Rogue"));
        public static string BestClass => Language.GetTextValue(MethodList.StringNameHandler("ClassName.Best"));
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
        public int NanoBladeHomingDist = 12;
        public double NanoDamageMul = 1.05;
        public int NanoBeamDamage = 120;
        public int NanoBladeDamage = 60;
        public int BloodflareCoreRegenSpeed = 10;
        public int WarbannerTiles = 30;
        public int NorfleetAntiCheeseTime = 3;
        #endregion
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            //鞭子
            WhipsCases(item,tooltips);
            //处理盗贼潜伏倍率
            ModifyRogueDamageTooltip(item, tooltips);
            //根据表单与类型给予不同的提示字符串
            foreach (var itemID in CalFixerList.GiveExtraTooltipList)
            {
                if (item.ThisItem(itemID))
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalExtraText, TooltipConstants.CalTooltipExtraColor);
            }

            foreach (var giveCalItemID in CalFixerList.GiveCalTooltipList)
            {
                if (item.ThisItem(giveCalItemID))
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalamityModifyText, TooltipConstants.CalamityModifyColor);
            }

            //打表每一个可能需额外工具提的的武器
            WeaponsCases(item, tooltips);
            //打表饰品
            AccessoriesAndMiscCases(item, tooltips);
            //处理古典酒
            ApplyOldFashioned(item, tooltips);
            //灾厄也干了：免伤
            CalamityDamageResisitance(item, tooltips);
            //Hunt联动
            FuckHunt(item, tooltips);
            //遗产联动
            Inheritance(item, tooltips);
            //灾劫联动
            FuckCatalyst(item, tooltips);

        }

        private void AccessoriesAndMiscCases(Item item, List<TooltipLine> tooltips)
        {
            if (item.ThisItem<Nanotech>())
            {

                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "NanotechFixed.BladeCap", NanoBladeCap, NanoBladeHomingDist);
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "NanotechFixed.RealDamage", NanoDamageMul);
                tooltips.QuickNewLineNoColor(Mod, OldFashionedDamageShown, ProjectileString.GetLangValue("Nanoblade"), NanoBladeDamage,TooltipConstants.RogueClassName.GetLangValue());
                tooltips.QuickNewLineNoColor(Mod, OldFashionedDamageShown, ProjectileString.GetLangValue("Nanobeam"), NanoBeamDamage,TooltipConstants.RogueClassName.GetLangValue());
            }
            if (item.ThisItem<Nucleogenesis>())
            {
                int sparkDamage = 60;
                string immnueDebuff = MethodList.GetBuffIcon("IconIrradiated")
                                    + MethodList.GetBuffIcon("IconAstralInfection")
                                    + MethodList.GetBuffIcon("IconShadowFlames")
                                    + MethodList.GetBuffIcon("IconElectric");

                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "Nucleogenesis.DebuffList", immnueDebuff);
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "Nucleogenesis.General", ProjectileString.GetLangValue("ElectricSpark"), sparkDamage, TooltipConstants.SummonClassName.GetLangValue());

            }
            if (item.ThisItem<StarTaintedGenerator>())
            {
                int sparkDamage = 40;
                string immnueDebuff = MethodList.GetBuffIcon("IconIrradiated")
                                    + MethodList.GetBuffIcon("IconAstralInfection")
                                    + MethodList.GetBuffIcon("IconElectric");
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "Nucleogenesis.DebuffList", immnueDebuff);
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "Nucleogenesis.General", ProjectileString.GetLangValue("ElectricSpark"), sparkDamage, TooltipConstants.SummonClassName.GetLangValue());
            }
            //血炎晶核：实际恢复速度
            if (item.ThisItem<BloodflareCore>())
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "BloodCoreRealRegen", BloodflareCoreRegenSpeed);
            }
            //天蓝石：不会与羽落生效
            if (item.ThisItem<AeroStone>())
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "AeroStoneNoFeather");
            }
            //阳炎战旗：显示多少距离的加成和实际加成
            if (item.ThisItem<WarbanneroftheSun>())
            {
                ModifyWarbannerTooltip(tooltips, TooltipConstants.ItemPath + "WarbannerBonus");
            }
            //惊天神盗盒
            if (item.ThisItem<VeneratedLocket>())
            {
                int locketDamage = 7;
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "VLocket", locketDamage);
            }
            //翱翔证章：无法叠加
            if (item.ThisItem<AscendantInsignia>())
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "AscendantInsignia", "[i:EmpressFlightBooster]");
            }
            //潜水服：提示这个东西伤害减免和移动速度
            if (item.ThisItem<AbyssalDivingSuit>())
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "AbyssGear.MoveSpeed");
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "AbyssGear.DR");
            }
            //聚合脑：延长增益的效果与保留效果实际
            if (item.ThisItem<TheAmalgam>())
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "AmalgamBuffsSave");
            }
            if (item.ThisItem(ItemID.SlimySaddle) || item.ThisItem(ItemID.PogoStick) || item.ThisItem(ItemID.QueenSlimeMountSaddle))
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "SlimeSaddleNerfed");
            }
            if (item.ThisItem<OldFashioned>())
            {
                tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalExtraText, TooltipConstants.CalTooltipExtraColor);
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "OldFashionedBounesText");
            }
        }
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
                string norfleetColor = "DC143C";
                tooltips.QuickNewLine(Mod, TooltipConstants.WeaponPath + "NorfleetAntiCheese", norfleetColor, NorfleetAntiCheeseTime);
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
                tooltips.FuckThisTooltipAndReplace(TooltipConstants.WeaponPath + "RainbowWhip", RainbowWhipMul);

            if (item.ThisItem(ItemID.BoneWhip))
                tooltips.FuckThisTooltipAndReplace(TooltipConstants.WeaponPath + "BoneWhip", BoneWhipMul);

            if (item.ThisItem(ItemID.FireWhip))
                tooltips.FuckThisTooltipAndReplace(TooltipConstants.WeaponPath + "FireWhip", FireWhipMul);

            if (item.ThisItem(ItemID.CoolWhip))
                tooltips.FuckThisTooltipAndReplace(TooltipConstants.WeaponPath + "CoolWhip", CoolWhipMul);

            if (item.ThisItem(ItemID.MaceWhip))
                tooltips.FuckThisTooltipAndReplace(TooltipConstants.WeaponPath + "MaceWhip", MaceWhipMul);

            if (item.ThisItem(ItemID.SwordWhip))
                tooltips.FuckThisTooltipAndReplace(TooltipConstants.WeaponPath + "SwordWhip", SwordWhipMul);
            //皮鞭
            if (item.ThisItem(ItemID.BlandWhip))
                tooltips.FuckThisTooltipAndReplace(TooltipConstants.WeaponPath + "LeatherWhip", LeatherWhipMul);
            
            if (item.ThisItem(ItemID.ThornWhip))
            {
                //荆鞭需要单独处理灾劫的情况
                tooltips.FuckThisTooltipAndReplace(TooltipConstants.WeaponPath + "ThornWhip", ThornWhipMul);
                if (CalTooltipFixer.Catalyst is not null)
                {
                    int thronDamage = 14;
                    int thronUseTime = 40;
                    int kbIndex = tooltips.FindIndex(line => line.Name == "Material");
                    string fuckCatalyst = Language.GetTextValue(TooltipConstants.WeaponPath + "ThornWhipCatalystNerfed");
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
        #region 处理灾厄免伤
        private void CalamityDamageResisitance(Item item, List<TooltipLine> tooltips)
        {
            
            #region 字符值所在的地址
            string resistanceSpecial = TooltipConstants.WeaponPath + "CalamityResistanceValue.Special";
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
                    tooltips.QuickNewLineWithColor(Mod, TooltipConstants.CalExtraText, TooltipConstants.CalTooltipExtraColor);
                    ModifyWarbannerTooltip(tooltips, TooltipConstants.ItemPath + "WarbannerBonus", 0.3f);
                }
            }
        }
        /// <summary>
        /// 灾劫我杀了你妈妈
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tooltips"></param>
        /// <param name="weaponTip"></param>
        private void FuckCatalyst(Item item, List<TooltipLine> tooltips)
        {
            //真他妈绝了
            Mod catalyst = CalTooltipFixer.Catalyst;
            if (catalyst is null)
                return;
            //星史的免伤表又臭又长又复杂，只能这么打表了
            //对不起，灾厄的免伤表更长
            string catalystColor = "FA85F3";
            string basePath = TooltipConstants.WeaponPath + "AstralSlimeResis.";
            string thisPath = basePath + "General";
            string specialDRPath = basePath + "SpecialDR";
            string trueMeleeDRPath = basePath + "TrueMelee";
            string rogueStealthDRPath = basePath + "RogueStealthDR";
            string rogueStealthSpecialDRPath = basePath + "RogueStealthDRSpecial";
            string lionMetoerSpecialPath = basePath + "LeonidProgenitorSpecialDR";
            string vortexpopperPath = basePath + "VortexpopperDR";
            string catalystName = "CatalystModName";
            
            if (catalyst.TryFind(TooltipConstants.CatalystSuperBoss, out ModNPC boss))
            {
                if (!NPC.AnyNPCs(boss.Type))
                    return;
                //1：遍历所有武器而非遍历弹幕，将免伤打入
                if (item.CountsAsClass<MeleeDamageClass>())
                {
                    int meleeDR = 90;
                    tooltips.SetupModLine(Mod, catalystName, catalystColor);
                    tooltips.QuickNewLine(Mod, specialDRPath, meleeDR);
                }
                else if (item.CountsAsClass<MagicDamageClass>())
                {
                    int magicDR = 55;
                    tooltips.SetupModLine(Mod, catalystName, catalystColor);
                    tooltips.QuickNewLine(Mod, specialDRPath, magicDR);
                }
                else if (item.CountsAsClass<TrueMeleeDamageClass>())
                {
                    int trueMeleeDR = 80;
                    tooltips.SetupModLine(Mod, catalystName, catalystColor);
                    tooltips.QuickNewLine(Mod, trueMeleeDRPath, trueMeleeDR);
                }
                //潜伏攻击需要给每个盗贼武器单独写一个显示
                else if (item.CountsAsClass<RogueDamageClass>())
                {
                    int stealthDR = 65;
                    tooltips.SetupModLine(Mod, catalystName, catalystColor);
                    tooltips.QuickNewLine(Mod, rogueStealthSpecialDRPath, stealthDR);
                }
                //将其他的伤害打入进去，单独打表。
                else if (item.CountsAsClass<RangedDamageClass>() || item.CountsAsClass<SummonDamageClass>() || item.CountsAsClass<SummonMeleeSpeedDamageClass>())
                {
                    int otherDR = 75;
                    tooltips.SetupModLine(Mod, catalystName, catalystColor);
                    tooltips.QuickNewLine(Mod, specialDRPath, otherDR);
                }
                //2.每个武器单独打表，不用注册额外的行
                //TODO:动态修改
                if (item.ThisItem<TheSwarmer>())
                {
                    int dr = 85;

                    tooltips.QuickNewLine(Mod, thisPath, dr);
                }
                if (item.ThisItem(ItemID.NebulaArcanum))
                {
                    int dr = 75;
                    tooltips.QuickNewLine(Mod, thisPath, dr);
                }
                if (item.ThisItem<HivePod>())
                {
                    int dr = 80;
                    tooltips.QuickNewLine(Mod, thisPath, dr);
                }
                if (item.ThisItem<FantasyTalisman>() || item.ThisItem<RegulusRiot>())
                {
                    int stealthDR = 80;
                    tooltips.QuickNewLine(Mod, rogueStealthDRPath, stealthDR);
                }
                if (item.ThisItem<Malachite>())
                {
                    int stealthDR = 70;
                    tooltips.QuickNewLine(Mod, rogueStealthDRPath, stealthDR);
                }
                if (item.ThisItem<DukesDecapitator>())
                {
                    int dr = 40;
                    tooltips.QuickNewLine(Mod, thisPath, dr);
                }
                if (item.ThisItem<Vortexpopper>())
                {
                    int combinedWithHybrus = 90;
                    int combinedWithPlague = 80;

                    tooltips.QuickNewLine(Mod, vortexpopperPath, combinedWithHybrus, combinedWithPlague);
                }
                //狮源流星单独打表
                if (item.ThisItem<LeonidProgenitor>())
                {
                    int smallAndStarDR = 90;
                    int bigAndSelfDR = 80;

                    tooltips.SetupModLine(Mod, catalystName, catalystColor);
                    tooltips.QuickNewLine(Mod, lionMetoerSpecialPath, smallAndStarDR, bigAndSelfDR);
                }

            }
        }
        /// <summary>
        /// 真他妈绝了，谁会他妈的打一堆免伤表？    
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tooltips"></param>
        /// <param name="weaponTip"></param>
        private void FuckHunt(Item item, List<TooltipLine> tooltips)
        {
            string basePath = TooltipConstants.WeaponPath + "GoozmaResis.";
            string path = basePath + "General";
            string huntColor = "FAEF85";
            string goozmaName = MethodList.GetLocalText("GoozmaModName");
            Mod hunt = CalTooltipFixer.Hunt;
            if (hunt is null)
                return;
            if (hunt.TryFind("Goozma", out ModNPC boss))
            {
                if (!NPC.AnyNPCs(boss.Type))
                    return;
                //炽天使1%
                if (item.ThisItem<Seraphim>())
                {
                    int seraphimResis = 1;
                    tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                    tooltips.QuickNewLine(Mod, path, seraphimResis);
                }
                //终曲黎明20%
                if (item.ThisItem<TheFinalDawn>())
                {
                    int finalDawnResis = 20;
                    tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                    tooltips.QuickNewLine(Mod, path, finalDawnResis);
                }
                //树枝30%
                if (item.ThisItem<TheWand>())
                {
                    int wandResis = 30;
                    tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                    tooltips.QuickNewLine(Mod, path, wandResis);
                }
                //龙魂破、宙能50%
                if (item.ThisItem<DragonPow>() || item.ThisItem<Excelsus>())
                {
                    int resis = 50;
                    tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                    tooltips.QuickNewLine(Mod, path, resis);
                }
                //龙怒56%
                if (item.ThisItem<DragonRage>())
                {
                    int rageResis = 56;
                    tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                    tooltips.QuickNewLine(Mod, path, rageResis);
                }
                if (item.ThisItem<DynamicPursuer>() || item.ThisItem<TheJailor>())
                {
                    int resis = 65;
                    tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                    tooltips.QuickNewLine(Mod, path, resis);
                }
                if (item.ThisItem<Condemnation>())
                {
                    int resis = 70;
                    tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                    tooltips.QuickNewLine(Mod, path, resis);
                }
                if (item.ThisItem<PrimordialAncient>())
                {
                    int resis = 75;
                    tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                    tooltips.QuickNewLine(Mod, path, resis);
                }
                //85%免伤
                if (item.ThisItem<AtlasMunitionsBeacon>()
                    || item.ThisItem<GodSlayerSlug>()
                    || item.ThisItem<HolyFireBullet>()
                    || item.ThisItem<Perdition>()
                    || item.ThisItem<PridefulHuntersPlanarRipper>()
                    || item.ThisItem<Vehemence>()
                    || item.ThisItem<Wrathwing>())
                {
                    int minionResis = 85;
                    tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                    tooltips.QuickNewLine(Mod, path, minionResis);
                }
                //90%
                if (item.ThisItem<ArkoftheCosmos>() || item.ThisItem<PhotonRipper>() || item.ThisItem<SpineOfThanatos>())
                {
                    int resis = 90;
                    tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                    tooltips.QuickNewLine(Mod, path, resis);
                }
                //真近战特殊
                if (item.CountsAsClass<TrueMeleeDamageClass>())
                {
                    string trueMelee = basePath + "TrueMelee";
                    int trueMeleeResis = 70;
                    tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                    tooltips.QuickNewLine(Mod, trueMelee, trueMeleeResis);
                }
                //召唤伤害特殊
                if (item.CountsAsClass<SummonDamageClass>())
                {
                    //goozma对仆从有免伤但对哨兵除外
                    Projectile minionProj = MethodList.ThisProjMod(item.shoot);
                    if (minionProj is not null && !minionProj.sentry && minionProj.minionSlots > 0f)
                    {
                        string minion = basePath + "Minion";
                        int minionResis = 85;
                        tooltips.QuickNewLine(Mod, goozmaName, huntColor);
                        tooltips.QuickNewLine(Mod, minion, minionResis);
                    }
                }
                
            }
        }
        #region 处理阳炎战旗
        public void ModifyWarbannerTooltip(List<TooltipLine> tooltips, string path, float bonus = 0.2f)
        {
            float multipler = GetWarbannerDamage(bonus);
            int floatToInt = (int)(multipler * 100f);
            tooltips.QuickNewLine(Mod, path, WarbannerTiles, floatToInt.ToString());
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