using CalTooltipFixer.Method;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items
{
    public class TesterSword : ModItem, ILocalizedModType
    {
        public override void SetDefaults()
        {
            Item.width = Item.height = 80;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool? UseItem(Player player)
        {
            #region 自动合成
            /*
            int count = 5;
            int removedType = ModContent.ItemType<AuricOre>();
            bool removedDone = false;
            bool isCorretcd = SoulMethod.FindInventoryItem(ref player, removedType, count);
            if (isCorretcd)
            {
                for (int i = 0; i < count; i++)
                {
                    player.ConsumeItem(removedType, false, true);
                }
                removedDone = true;
            }
            if (removedDone)
            {
                player.QuickSpawnItem(player.GetSource_FromThis(), ModContent.ItemType<AuricBar>(), 1);
                Main.NewText("合成一个金源锭");
            }
            */
            #endregion
            bool exoLore = player.GetInheritancePlayerFieldBoolen("LoreExo").Value || player.GetInheritancePlayerFieldBoolen("PanelsExo").Value;
            if (exoLore)
                Main.NewText("thisText");
            return base.UseItem(player);
        }
    }
}