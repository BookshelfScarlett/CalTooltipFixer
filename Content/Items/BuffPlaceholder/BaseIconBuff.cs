using CalTooltipFixer.Method;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items.BuffPlaceholder
{
    public abstract class IconBuffBase : ModItem
    {
        public static string BuffIconPath => "Buffs.Placeholder"; 
        public static string DoTPath => "CalamityMod/Buffs/DamageOverTime/";
        public static string StatDebuffPath => "CalamityMod/Buffs/StatDebuffs/";
        public virtual string GetBuffIcon => (GetType().Namespace + "." + GetType().Name).Replace('.', '/'); 
        public override string Texture => GetBuffIcon;
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatShouldNotBeInInventory[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = Item.height = 32;
            Item.noMelee = true;
        }
    }
}