using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.StormWeaver;
using CalTooltipFixer.Content.Players;
using CalTooltipFixer.Core;
using Microsoft.Xna.Framework;

namespace CalTooltipFixer.Content.Items.BossesResistances
{
    public class ResistanceSentinels : BaseResistance
    {
        public override int[] BossType => [NPCID<StormWeaverHead>(), NPCID<CeaselessVoid>()];
        public override string[] WeaponType => [.. CalFixerList.SentinalsResistanceList];
        public override Vector2 BossHeadBox => new(76, 63);
        public override void SetResistanceBoolen(TooltipPlayer set) => set.ResistanceSentinelsBool = true;
    }
}