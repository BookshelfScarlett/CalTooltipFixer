using System.Collections.Generic;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using Terraria.ModLoader;

namespace CalTooltipFixer.Core
{
    public partial class CalFixerList
    {
        private static List<string> aresResistanceList = [];
        private static List<string> thanathosResistanceList = [];
        private static List<string> oldDukeResistanceList = [];
        private static List<int> aresResistanceListID = [];
        private static List<string> sentinalsResistanceList = [];
        private static List<string> profanedBossesResistanceList = [];
        private static List<string> deusResistanceList = [];
        private static List<string> ravagerResistanceList = [];
        private static List<string> doGResistanceList = [];


        public static List<string> AresResistanceList { get => aresResistanceList; set => aresResistanceList = value; }
        public static List<int> AresResistanceListID { get => aresResistanceListID; set => aresResistanceListID = value; }
        public static List<string> ThanathosResistanceList { get => thanathosResistanceList; set => thanathosResistanceList = value; }
        public static List<string> OldDukeResistanceList { get => oldDukeResistanceList; set => oldDukeResistanceList = value; }
        public static List<string> SentinalsResistanceList { get => sentinalsResistanceList; set => sentinalsResistanceList = value; }
        public static List<string> ProfanedBossesResistanceList { get => profanedBossesResistanceList; set => profanedBossesResistanceList = value; }
        public static List<string> DeusResistanceList { get => deusResistanceList; set => deusResistanceList = value; }
        public static List<string> RavagerResistanceList { get => ravagerResistanceList; set => ravagerResistanceList = value; }
        public static List<string> DoGResistanceList { get => doGResistanceList; set => doGResistanceList = value; }

        public static void LoadResistance()
        {
            SetCalResistanceList();
        }
        public static void UnLoadResistance()
        {
            List<string>[] list =
            [
                AresResistanceList,
                ThanathosResistanceList,
                OldDukeResistanceList,
                SentinalsResistanceList,
                DeusResistanceList,
                SentinalsResistanceList,
                ProfanedBossesResistanceList,
                RavagerResistanceList,
                DoGResistanceList
            ];
            for (int i = 0; i < list.Length; i++)
                list[i] = null;

        }
        public static void SetCalResistanceList()
        {
            AresResistanceList =
            [
                nameof(ArkoftheCosmos),
                nameof(Murasama),
                nameof(DragonRage),

                nameof(AetherfluxCannon),
                nameof(YharimsCrystal),
                nameof(Rancor),

                nameof(EclipsesFall),
                nameof(DynamicPursuer),
            ];
            
            ThanathosResistanceList =
            [
                nameof(ArkoftheCosmos),
                nameof(DragonRage),
                nameof(TheEnforcer),
                nameof(GaelsGreatsword),

                nameof(PrismaticBreaker),
                nameof(TheAnomalysNanogun),
                nameof(Ultima),
                nameof(ChickenCannon),
                nameof(BloodBoiler),

                nameof(YharimsCrystal),
                nameof(Rancor),
                nameof(Omicron),
                nameof(VoidVortex),
                nameof(VoltaicClimax),
                nameof(Vehemence),
                nameof(TheWand),
                nameof(GruesomeEminence),

                nameof(StellarTorusStaff),
                nameof(Sirius),
                nameof(LiliesOfFinality),
                nameof(MirrorofKalandra),

                nameof(TheFinalDawn),
                nameof(TarragonThrowingDart),
                nameof(PlasmaGrenade),
                nameof(Eradicator),
                nameof(DynamicPursuer),

                nameof(GodSlayerSlug),
                nameof(DragonScales)
            ];
            DoGResistanceList =
            [
                nameof(WavePounder),
                nameof(TimeBolt),
                nameof(EidolicWail),
                nameof(VenusianTrident),
                nameof(Valediction),
                nameof(NuclearFury),
            ];
            SentinalsResistanceList =
            [
                nameof(DazzlingStabberStaff),
                nameof(ElementalAxe),
                nameof(PristineFury),
                nameof(TacticiansTrumpCard),
                nameof(MoltenAmputator),

                nameof(StellarTorusStaff)
            ];
            ProfanedBossesResistanceList =
            [
                nameof(HellsSun),
                nameof(ElementalLance)
            ];
            //TODO:应该有什么不用打两次表的方法的
            AresResistanceListID =
            [
                ModContent.ItemType<EclipsesFall>(),
                ModContent.ItemType<TheFinalDawn>(),
                ModContent.ItemType<ArkoftheCosmos>(),
                ModContent.ItemType<DynamicPursuer>(),
                ModContent.ItemType<DragonRage>(),
                ModContent.ItemType<YharimsCrystal>(),
                ModContent.ItemType<Rancor>(),
                ModContent.ItemType<DynamicPursuer>(),
                ModContent.ItemType<EclipsesFall>()
            ];
        }
    }
}