using System.Collections.Generic;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public abstract class BaseFixedArmor : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public virtual int HeadType { get; set; }
        public virtual int BodyType { get; set; }
        public virtual int LegsType { get; set; }
        public virtual string ArmorTextPath { get; set; }
        public virtual Mod TheMod => ModLoader.GetMod("CalamityMod");
        public static Player Owner => Main.LocalPlayer;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (TheMod is null)
                return;
            ArmorFixedTooltip(item, tooltips);
        }
 
        public virtual void ArmorFixedTooltip(Item item, List<TooltipLine> tooltips)
        {

        }
        public static bool IsFullSet(Player player, int headType, int bodyType, int legsType)
        {
            return player.ThisBodyPart("Head", headType) && player.ThisBodyPart("Body", bodyType) && player.ThisBodyPart("Legs", legsType);
        }
    }
}