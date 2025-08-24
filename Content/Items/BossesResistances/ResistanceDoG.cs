using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.NPCs.DevourerofGods;
using CalTooltipFixer.Content.Players;
using CalTooltipFixer.Core;
using Microsoft.Xna.Framework;

namespace CalTooltipFixer.Content.Items.BossesResistances
{
    public class ResistanceDoG : BaseResistance
    {
        public override int[] BossType => [NPCID<DevourerofGodsHead>()];
        public override string[] WeaponType => [..CalFixerList.DoGResistanceList];
        public override Vector2 BossHeadBox => new(30, 32);
        public override void SetResistanceBoolen(TooltipPlayer set) => set.ResistanceDoGBool = true;
    }
}