using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalTooltipFixer.Content.Players;
using CalTooltipFixer.Core;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items.BossesResistances
{
    public class ResistanceProviAndGuards : BaseResistance
    {
        public override int[] BossType => [NPCID<Providence>(), NPCID<ProfanedGuardianHealer>(), NPCID<ProfanedGuardianCommander>(), NPCID<ProfanedGuardianDefender>()];
        public override string[] WeaponType => [..CalFixerList.ProfanedBossesResistanceList];
        public override Vector2 BossHeadBox => new(62, 44);
        public override void SetResistanceBoolen(TooltipPlayer set) => set.ResistanceProviAndGuardsBool = true;
    }
}