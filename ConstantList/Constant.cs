using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalTooltipFixer.Method;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;

namespace CalTooltipFixer.ConstantList
{
    public class TooltipConstants
    {
        #region 盾冲的伤害
        public static readonly int AegisDashDamage = AsgardianAegis.ShieldSlamDamage;
        public static readonly int ValorDashDamage = AsgardsValor.ShieldSlamDamage;
        public static readonly int PlagueSummonDashDamage = 50;
        public static readonly int OrinateDashDamage = OrnateShield.ShieldSlamDamage;
        //斯塔提斯镰刀
        public static readonly int StatisScytheDamage = 250;
        //软壳仆从
        public static readonly int MolluskMinion = 140;
        public static readonly int NebulousCoreDamage = 250;
        public static readonly int SulphDoubleJumpDamage = 20;
        #endregion
        #region 常规文本颜色
        public static Color CalTooltipExtraColor => new(248, 240, 166);
        public static Color CalamityModifyColor => new(220, 20, 60);
        public static Color CatalystModifyColor => new(250, 133, 243);
        public static Color GoozmaModifyColor => new(250, 239, 133);
        #endregion
        #region 必要字符串
        public static string CatalystSuperBoss => "Astrageldon";
        public static string GoozmaSuperBoss => "Goozma";
        #endregion
        #region 必要地址串
        public static string ItemPath => MethodList.GetLocalText("ItemTooltip.");
        public static string WeaponPath => MethodList.GetLocalText("WeaponTooltip.");
        public static string GetCanOldFashionedText => (ItemPath + "CanOldFashionedBounes");
        public static string CalamityModifyText => MethodList.GetLocalText("CalamityModifyPingText");
        public static string CalamityResistanceText => MethodList.GetLocalText("CalamityResistance.PingText");
        public static string CalamityResistanceValue => WeaponPath + "CalamityResistanceValue.";
        public static string CalExtraText => MethodList.GetLocalText("CalTooltipFixerExtra");
        public static string CatalystModifyText => MethodList.GetLocalText("CatalystModName");
        public static string GoozmaModifyText => MethodList.GetLocalText("GoozmaModName");

        internal static string BossResisPath => WeaponPath + "BossResistanceConstant";
        internal static string GeneralString => "General";
        internal static string TrueMeleeString => "TrueMelee";
        internal static string SpecialDRString => "SpecialDR";

        public static string BossResistance => BossResisPath + "." + GeneralString;
        public static string BossResistanceTrueMelee => BossResisPath + "." + TrueMeleeString;
        public static string BossResistanceSpecial => BossResisPath + "." + SpecialDRString;
        #endregion
        //职业们
        static string IsClassName => "ClassNameReal";
        // #ffc67bff
        // #8bffe2ff
        // #ffa2f3ff
        // #81a2fcff
        // #b278ffff
        public static string MeleeClassName => MethodList.StringNameHandler($"{IsClassName}.Melee");
        public static string RangedClassName => MethodList.StringNameHandler($"{IsClassName}.Ranged");
        public static string MagicClassName => MethodList.StringNameHandler($"{IsClassName}.Magic");
        public static string SummonClassName => MethodList.StringNameHandler($"{IsClassName}.Summon");
        public static string RogueClassName => MethodList.StringNameHandler($"{IsClassName}.Rogue");
        public static string GenericClassName => MethodList.StringNameHandler($"{IsClassName}.Generic");
        
    }
}