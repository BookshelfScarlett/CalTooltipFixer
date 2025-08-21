using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalTooltipFixer.Content.Players;
using CalTooltipFixer.Core;
using Microsoft.Xna.Framework;

namespace CalTooltipFixer.Content.Items.BossesResistances
{
    public class ResistanceThanatos : BaseResistance
    {
        public override int[] BossType => [NPCID<ThanatosHead>()];
        public override string[] WeaponType => [.. CalFixerList.ThanathosResistanceList];
        public override string[] WeaponTypeVanilla => ["Zenith"];
        public override Vector2 BossHeadBox => new(38, 42);
        public override void SetResistanceBoolen(TooltipPlayer set) => set.ResistanceThanatosBool= true;
    }
}