using CalamityMod.NPCs.ExoMechs.Ares;
using CalTooltipFixer.Content.Players;
using CalTooltipFixer.Core;
using Microsoft.Xna.Framework;

namespace CalTooltipFixer.Content.Items.BossesResistances
{
    public class ResistanceAres : BaseResistance
    {
        public override int[] BossType => [NPCID<AresBody>()];
        public override string[] WeaponType => [.. CalFixerList.AresResistanceList];
        public override string[] WeaponTypeVanilla => ["Zenith"];
        public override Vector2 BossHeadBox => new(50, 50);
        public override void SetResistanceBoolen(TooltipPlayer player) => player.ResistanceAresBool = true;
    }
}