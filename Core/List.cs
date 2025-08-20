using System.Collections.Generic;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Ranged;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Core
{
    public static class CalFixerList
    {
        private static List<int> oldFashionedList = [];
        private static List<int> giveExtraTooltipList = [];
        private static List<int> giveCalTooltipList = [];

        public static List<int> OldFashionedList { get => oldFashionedList; set => oldFashionedList = value; }
        public static List<int> GiveExtraTooltipList { get => giveExtraTooltipList; set => giveExtraTooltipList = value; }
        public static List<int> GiveCalTooltipList { get => giveCalTooltipList; set => giveCalTooltipList = value; }

        public static void LoadList()
        {
            SetOldFasionList();
            SetShouldGiveExtraTooltipList();
        }

        private static void SetShouldGiveExtraTooltipList()
        {
            GiveExtraTooltipList =
            [
                ModContent.ItemType<Nanotech>(),
                ModContent.ItemType<VeneratedLocket>(),

                ModContent.ItemType<Nucleogenesis>(),
                ModContent.ItemType<StarTaintedGenerator>(),

                ModContent.ItemType<PlagueTaintedSMG>(),
                ModContent.ItemType<Norfleet>(),

                ModContent.ItemType<IceBarrage>(),

                // ModContent.ItemType<CosmicImmaterializer>(),

                ModContent.ItemType<BloodflareCore>(),
                ModContent.ItemType<AeroStone>(),
                ModContent.ItemType<WarbanneroftheSun>(),
                ModContent.ItemType<AscendantInsignia>()
            ];
            GiveCalTooltipList =
            [
                ItemID.SlimySaddle,
                ItemID.PogoStick,
                ItemID.QueenSlimeMountSaddle
            ];
        }

        public static void UnLoadList()
        {
            OldFashionedList = null;
            GiveExtraTooltipList = null;
            GiveCalTooltipList = null;
        }
        public static void SetOldFasionList()
        {
            OldFashionedList =
            [
                ModContent.ItemType<Nanotech>(),
                ModContent.ItemType<MoonstoneCrown>(),
                ModContent.ItemType<FeatherCrown>(),
                ModContent.ItemType<ElectriciansGlove>(),
                ModContent.ItemType<AbyssalMirror>(),
                ModContent.ItemType<EclipseMirror>(),

                ModContent.ItemType<Nucleogenesis>(),
                ModContent.ItemType<StarTaintedGenerator>(),
                ModContent.ItemType<PhantomicArtifact>(),
                ModContent.ItemType<HallowedRune>(),
                ModContent.ItemType<ProfanedSoulCrystal>(),
                ModContent.ItemType<ProfanedSoulArtifact>(),
                ModContent.ItemType<PearlofEnthrallment>(),
                ModContent.ItemType<EyeoftheStorm>(),
                ModContent.ItemType<RoseStone>(),
                ModContent.ItemType<HeartoftheElements>(),

                ModContent.ItemType<HideofAstrumDeus>(),
                ModContent.ItemType<NebulousCore>(),
                ModContent.ItemType<InkBomb>(),

                ModContent.ItemType<FungalClump>(),
                ModContent.ItemType<RottenBrain>(),
                ModContent.ItemType<LuxorsGift>()
            ];
        }
        
    }
}