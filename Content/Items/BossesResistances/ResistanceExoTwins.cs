using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalTooltipFixer.Content.Players;
using CalTooltipFixer.Core;
using Microsoft.Xna.Framework;

namespace CalTooltipFixer.Content.Items.BossesResistances
{
    public class ResistanceExoTwins : BaseResistance
    {
        public override int[] BossType => [NPCID<Apollo>(), NPCID<Artemis>()];
        public override string[] WeaponType => [nameof(EclipsesFall)];
        public override Vector2 BossHeadBox => new(64, 43);
        public override void SetResistanceBoolen(TooltipPlayer set) => set.ResistanceExoTwinsBool= true;
    }
}