using Terraria;
using Terraria.ModLoader;

namespace CalTooltipFixer.Method
{
    public static class SimpleMethod
    {
        public static bool ThisBodyPart(this Player player, string slot, int itemID)
        {
            if (slot == "Head")
                return player.armor[0].type == itemID;
            if (slot == "Body")
                return player.armor[1].type == itemID;
            if (slot == "Legs")
                return player.armor[2].type == itemID;
            return false;
        }
        public static bool ThisBodyPart<T>(this Player player, string slot) where T : ModItem => ThisBodyPart(player, slot, ModContent.ItemType<T>());
    }
}