using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items
{
    public class Dagger : ModItem
    {
        public static int TossCounts = 0;
        public bool DoTripleShots = false;
        public int DoTripleShotsCounts = 0;
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 40;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.value = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 18f;
            //干掉，使用自定义挥舞
            Item.noUseGraphic = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (DoTripleShots)
            {
                Item.useTime = 3;
                Item.useAnimation = 9;
            }
            else
            {
                Item.useTime = 15;
                Item.useAnimation = 15;
            }
            return base.CanUseItem(player);
        }
        public override bool? UseItem(Player player)
        {
            if (!DoTripleShots)
            {
                TossCounts++;
                if (TossCounts >= 3)
                {
                    DoTripleShots = true;
                    TossCounts = 0;
                    DoTripleShotsCounts = 0;
                }

            }
            else
            {
                DoTripleShotsCounts++;
                if (DoTripleShotsCounts >= 3)
                    DoTripleShots = false;
            }
            return base.UseItem(player);
        }
        //重写使用动画
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            float itemAnimationProgress = player.itemAnimation / (float)player.itemTime;
            player.itemRotation = player.direction * 0.8f * (0.5f - itemAnimationProgress);
            //连发时加速手臂挥舞
            float swingSpeed = DoTripleShots ? 2f : 1f;
            float swingRange = DoTripleShots ? 12f : 10f;
            //摆动动画，使用正弦函数
            player.itemLocation.Y += (float)Math.Sin(itemAnimationProgress * MathHelper.Pi * 2 * swingSpeed) * swingRange * player.gravDir;
            //将刀光特效作为手持动画
        }
    }
}