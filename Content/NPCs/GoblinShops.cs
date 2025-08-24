using CalamityMod;
using CalamityMod.Items.Accessories;
using CalTooltipFixer.Content.Items.BossesResistances;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.NPCs
{
    public partial class CalTooltipNPCs : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            int type = shop.NpcType;
            if (type == NPCID.GoblinTinkerer)
            {
                shop.QuickShop<ResistanceAres>(CalamityConditions.DownedYharon, 50);
                shop.QuickShop<ResistanceExoTwins>(CalamityConditions.DownedYharon, 50);
                shop.QuickShop<ResistanceThanatos>(CalamityConditions.DownedYharon, 50);
                shop.QuickShop<ResistanceDoG>(Condition.DownedMoonLord, 25);
                shop.QuickShop<ResistanceProviAndGuards>(Condition.DownedMoonLord, 25);
                shop.QuickShop<ResistanceSentinels>(Condition.DownedMoonLord, 25);
                if (CalTooltipFixer.Hunt != null)
                    shop.QuickShop<ResistanceGoozma>(CalamityConditions.DownedYharon, 50);
                if (CalTooltipFixer.Catalyst != null)
                    shop.QuickShop<ResistanceCatalystSuperboss>(Condition.DownedCultist, 15);
            }
        }
    }
    public static class ShopHelper
    {
        //     npcShop.Add(new Item(ItemID.MagicDagger) {
        //     	shopCustomPrice = 2,
        //     	shopSpecialCurrency = ExampleMod.ExampleCustomCurrencyId
        //     }, Condition.RemixWorld);
        public static void QuickShop<T>(this NPCShop shop, Condition downedBoss, int goldCoins) where T : ModItem
        {
            int convertToGolds = Item.buyPrice(gold: goldCoins);
            var item = new Item(ModContent.ItemType<T>())
            {
                shopCustomPrice = convertToGolds,
            };
            shop.Add(item, downedBoss);
        }
    }
}