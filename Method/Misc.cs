using CalamityMod;
using CalamityMod.Items.Potions.Alcohol;
using CalTooltipFixer.Content.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalTooltipFixer.Method
{
    public static partial class MethodList
    {
        public static TooltipPlayer ThisMod(this Player player) => player.GetModPlayer<TooltipPlayer>();
        public static Projectile ThisProjMod(int type)
        {
            //遍历几乎所有射弹
            foreach (Mod mod in ModLoader.Mods)
            {
                foreach (var proj in mod.GetContent<ModProjectile>())
                {
                    //然后找寻对应的Type
                    if (proj.Type == type)
                    {
                        //实例化并返回这个proj
                        return proj.Projectile;
                    }
                }
            }
            //其余状态返回null
            return null;
        }
        /// <summary>
        /// 我在Tooltips模组写寻找最近敌人的方法？真的假的？
        /// </summary>
        /// <param name="orginalPosition">原始位置</param>
        /// <returns></returns>
        public static NPC GetClosestTarget(this Vector2 orginalPosition, float maxDistance)
        {
            int closestNPC = -1;
            float distance = maxDistance;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.IsAnEnemy() && !npc.dontTakeDamage)
                {
                    float closestDistacne = (npc.Center - orginalPosition).Length();
                    if (closestDistacne < distance)
                    {
                        distance = closestDistacne;
                        closestNPC = npc.whoAmI;
                    }
                }
            }
            if (closestNPC != -1)
            {
                return Main.npc[closestNPC];
            }
            return null;
        }
        public static bool AnyNPCs<T>() where T : ModNPC => NPC.AnyNPCs(ModContent.NPCType<T>());
        public static int ToIntSingle(this float single) => (int)(single * 100f);
        public static int ToOldFashioned(this Player player, int baseDamage)
        {
            bool wtf = player.Calamity().oldFashioned;
            return wtf ? baseDamage : (int)(baseDamage * OldFashioned.AccessoryAndSetBonusDamageMultiplier);
        }
    }
}