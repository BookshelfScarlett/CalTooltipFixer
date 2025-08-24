using System;
using CalTooltipFixer.Core;
using CalTooltipFixer.Method;
using Terraria.ModLoader;

namespace CalTooltipFixer
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class CalTooltipFixer : Mod
	{
		public static CalTooltipFixer Instance;
		public static Mod Hunt;
		public static Mod WrathOfTheGod;
		public static Mod Catalyst;
		public static Mod Inheritance;
		public static Mod Infernum;
		public override void Load()
		{
			Instance = this;
            LoadCrossMod();
			CalFixerList.LoadList();
        }
		public static void LoadCrossMod()
		{
			Mod[] mods =
			[
                Hunt,
                WrathOfTheGod,
                Catalyst,
                Inheritance,
				Infernum
			];
			for (int i = 0; i < mods.Length; i++)
			{
				mods[i] = null;
			}
			ModLoader.TryGetMod(CrossMod.HuntOfTheGodName, out Hunt);
			ModLoader.TryGetMod(CrossMod.WrathOfTheGodName, out WrathOfTheGod);
			ModLoader.TryGetMod(CrossMod.Catalyst, out Catalyst);
			ModLoader.TryGetMod(CrossMod.Inheritance, out Inheritance);
			ModLoader.TryGetMod(CrossMod.Infernum, out Infernum);
        }
        public override void Unload()
        {
            UnloadCrossMod();
			CalFixerList.UnLoadList();
        }

        public static void UnloadCrossMod()
        {
			Mod[] mods =
			[
				Hunt,
				WrathOfTheGod,
				Catalyst,
				Inheritance,
				Infernum
			];
			for (int i = 0; i < mods.Length; i++)
			{
				mods[i] = null;
			}
        }
    }
}
