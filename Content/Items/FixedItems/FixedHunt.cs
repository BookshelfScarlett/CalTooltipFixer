using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items.FixedItems
{
    public partial class FixedItem : GlobalItem
    {
        private void FuckHunt(Item item, List<TooltipLine> tooltips)
        {
            string basePath = TooltipConstants.WeaponPath + "GoozmaResis.";
            string path = basePath + "General";
            Color huntColor = TooltipConstants.GoozmaModifyColor;
            string goozmaName = TooltipConstants.HuntResistanceText;
            Mod hunt = CalTooltipFixer.Hunt;
            if (hunt is null)
                return;
            if (hunt.TryFind(TooltipConstants.GoozmaSuperBoss, out ModNPC boss))
            {
                if (!ModPlayer.ResistanceGoozmaBool)
                    return;
                //炽天使1%
                if (item.ThisItem<Seraphim>())
                {
                    int seraphimResis = 1;
                    tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                    tooltips.QuickNewLineNoColor(Mod, path, seraphimResis);
                }
                //终曲黎明20%
                if (item.ThisItem<TheFinalDawn>())
                {
                    int finalDawnResis = 20;
                    tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                    tooltips.QuickNewLineNoColor(Mod, path, finalDawnResis);
                }
                //树枝30%
                if (item.ThisItem<TheWand>())
                {
                    int wandResis = 30;
                    tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                    tooltips.QuickNewLineNoColor(Mod, path, wandResis);
                }
                //龙魂破、宙能50%
                if (item.ThisItem<DragonPow>() || item.ThisItem<Excelsus>())
                {
                    int resis = 50;
                    tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                    tooltips.QuickNewLineNoColor(Mod, path, resis);
                }
                //龙怒56%
                if (item.ThisItem<DragonRage>())
                {
                    int rageResis = 56;
                    tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                    tooltips.QuickNewLineNoColor(Mod, path, rageResis);
                }
                if (item.ThisItem<DynamicPursuer>() || item.ThisItem<TheJailor>())
                {
                    int resis = 65;
                    tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                    tooltips.QuickNewLineNoColor(Mod, path, resis);
                }
                if (item.ThisItem<Condemnation>())
                {
                    int resis = 70;
                    tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                    tooltips.QuickNewLineNoColor(Mod, path, resis);
                }
                if (item.ThisItem<PrimordialAncient>())
                {
                    int resis = 75;
                    tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                    tooltips.QuickNewLineNoColor(Mod, path, resis);
                }
                //85%免伤
                if (item.ThisItem<AtlasMunitionsBeacon>()
                    || item.ThisItem<GodSlayerSlug>()
                    || item.ThisItem<HolyFireBullet>()
                    || item.ThisItem<Perdition>()
                    || item.ThisItem<PridefulHuntersPlanarRipper>()
                    || item.ThisItem<Vehemence>()
                    || item.ThisItem<Wrathwing>())
                {
                    int minionResis = 85;
                    tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                    tooltips.QuickNewLineNoColor(Mod, path, minionResis);
                }
                //90%
                if (item.ThisItem<ArkoftheCosmos>() || item.ThisItem<PhotonRipper>() || item.ThisItem<SpineOfThanatos>())
                {
                    int resis = 90;
                    tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                    tooltips.QuickNewLineNoColor(Mod, path, resis);
                }
                //真近战特殊
                if (item.CountsAsClass<TrueMeleeDamageClass>())
                {
                    string trueMelee = basePath + "TrueMelee";
                    int trueMeleeResis = 70;
                    tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                    tooltips.QuickNewLineNoColor(Mod, trueMelee, trueMeleeResis);
                }
                //召唤伤害特殊
                if (item.CountsAsClass<SummonDamageClass>())
                {
                    //goozma对仆从有免伤但对哨兵除外
                    Projectile minionProj = MethodList.ThisProjMod(item.shoot);
                    if (minionProj is not null && !minionProj.sentry && minionProj.minionSlots > 0f)
                    {
                        string minion = basePath + "Minion";
                        int minionResis = 85;
                        tooltips.QuickNewLineWithColor("HuntDR", Mod, goozmaName, huntColor);
                        tooltips.QuickNewLineNoColor(Mod, minion, minionResis);
                    }
                }
                
            }
        }
    }
}