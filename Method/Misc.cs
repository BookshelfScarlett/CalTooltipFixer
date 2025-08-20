using Terraria;
using Terraria.ModLoader;

namespace CalTooltipFixer.Method
{
    public static partial class MethodList
    {
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
        public static bool AnyNPCs<T>() where T : ModNPC => NPC.AnyNPCs(ModContent.NPCType<T>());
    }
}