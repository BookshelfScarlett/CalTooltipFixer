using CalTooltipFixer.Method;
using Terraria;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Players
{
    public partial class TooltipPlayer : ModPlayer
    {
        //h缓存表单null"
        public string _cacheResistanceList = null;
        public bool _cacheForstArmorNPC = false;
        #region 抗性表单的Boolen，这个必须得打表了。起名规则为对应物品的类名+Bool
        public bool ResistanceAresBool = false;
        public bool ResistanceThanatosBool = false;
        public bool ResistanceExoTwinsBool = false;
        public bool ResistanceDoGBool = false;
        public bool ResistanceProviAndGuardsBool = false;
        public bool ResistanceSentinelsBool = false;
        public bool ResistanceCatalystSuperbossBool = false;
        public bool ResistanceGoozmaBool = false;
        #endregion
        //寒霜套存player类
        public float FrostArmorMeleeBoost = 0f;
        public float FrostArmorRangedBoost = 0f;
        public override void UpdateDead()
        {
            _cacheForstArmorNPC = false;
        }
    }
}