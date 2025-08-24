using CalamityMod.Items.Accessories;
using CalTooltipFixer.Method;
using Microsoft.Xna.Framework;

namespace CalTooltipFixer.ConstantList
{
    public partial class TooltipConstants
    {
        #region 盾冲的伤害
        public static readonly int AegisDashDamage = AsgardianAegis.ShieldSlamDamage;
        public static readonly int ValorDashDamage = AsgardsValor.ShieldSlamDamage;
        public static readonly int PlagueSummonDashDamage = 50;
        public static readonly int OrinateDashDamage = OrnateShield.ShieldSlamDamage;
        public const int SolarFlareSlam = 1200;
        //斯塔提斯镰刀
        public static readonly int StatisScytheDamage = 250;
        //软壳仆从
        public static readonly int MolluskMinion = 140;
        public static readonly int NebulousCoreDamage = 250;
        public static readonly int SulphDoubleJumpDamage = 20;
        #endregion
        #region 常规文本颜色
        //# rgba(248,240,166,1);
        //# rgba(220,20,60,1);
        //# rgba(250,133,243,1);
        //# rgba(250,239,133,1);
        //# rgba(255,105,180,1);
        public static Color CalExtraColor => new(248, 240, 166);
        public static Color CalamityModifyColor => new(220, 20, 60);
        public static Color CatalystModifyColor => new(250, 133, 243);
        public static Color GoozmaModifyColor => new(250, 239, 133);
        public static Color WrathoftheGodsColor => new(255, 105, 180);
        #endregion
        #region 必要字符串
        public static string CatalystSuperBoss => "Astrageldon";
        public static string GoozmaSuperBoss => "Goozma";
        #endregion
        #region 必要地址串
        
        public static string ItemPath => MethodList.GetLocalText("ItemTooltip.");
        public static string WeaponPath => MethodList.GetLocalText("WeaponTooltip.");
        public static string GetCanOldFashionedText => ItemPath + "CanOldFashionedBounes";
        public static string GetOldFashionedSupportValue => MethodList.GetLocalText("OldFashionedSupport").ToLangValue();
        public static string ResistanceHeaderText => MethodList.GetLocalText("ResistanceItem.GenericHeader");
        public static string ResistanceItemText => MethodList.GetLocalText("ResistanceItem.");
        internal static string BossResisPath => WeaponPath + "BossResistanceConstant";
        internal static string GeneralString => "General";
        internal static string TrueMeleeString => "TrueMelee";
        internal static string SpecialDRString => "SpecialDR";
        internal static string ExtraDamageString => "ExtraDamage";
        /// <summary>
        /// 通用Boss减伤的Text，需要键入boss名与对应的减伤值
        /// </summary>
        public static string BossResistance => BossResisPath + "." + GeneralString;
        public static string BossResistanceTrueMelee => BossResisPath + "." + TrueMeleeString;
        /// <summary>
        /// 特殊的Boss减伤Text，用于表示特定职业的减伤。非武器，特定武器弹幕减伤需打表
        /// </summary>
        public static string BossResistanceSpecial => BossResisPath + "." + SpecialDRString;
        #endregion
        //职业们
        public static string IsClassName => "ClassNameReal";
        // #ffc67bff
        // #8bffe2ff
        // #ffa2f3ff
        // #81a2fcff
        // #b278ffff
        // #ff7b7bff
        public static string MeleeClassName => MethodList.StringNameHandler($"{IsClassName}.Melee");
        public static string RangedClassName => MethodList.StringNameHandler($"{IsClassName}.Ranged");
        public static string MagicClassName => MethodList.StringNameHandler($"{IsClassName}.Magic");
        public static string SummonClassName => MethodList.StringNameHandler($"{IsClassName}.Summon");
        public static string RogueClassName => MethodList.StringNameHandler($"{IsClassName}.Rogue");
        public static string GenericClassName => MethodList.StringNameHandler($"{IsClassName}.Generic");
        public static string BestClassName => MethodList.StringNameHandler($"{IsClassName}.Best");
        public static string TrueMeleeName => MethodList.StringNameHandler($"{IsClassName}.TrueMelee"); 
    }
}