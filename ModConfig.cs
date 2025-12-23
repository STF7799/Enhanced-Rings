using StardewModdingAPI;

namespace Enhanced_Rings
{
    public class ModConfig
    {
        // ========== 强盗戒指配置 ==========
        public int BurglarRingPlusRounds { get; set; } = 5;
        public int BurglarRingPlus2Rounds { get; set; } = 10;
        public int BurglarRingPlus3Rounds { get; set; } = 15;

        // ========== 怪物生成戒指配置 ==========
        public double MonsterSpawnRingMultiplier { get; set; } = 1.5;
        public double MonsterSpawnRing2Multiplier { get; set; } = 2.0;
        public double MonsterSpawnRing3Multiplier { get; set; } = 3.0;
        public double MaxMonsterSpawnChance { get; set; } = 0.5;
        public bool AffectSkullCavern { get; set; } = true;
        public bool AffectVolcanoDungeon { get; set; } = true;

        // ========== 组合戒指配置 ==========
        public float RubyRingCombinedMultiplier { get; set; } = 0.2f;
        public float RubyRingCombinedPlusMultiplier { get; set; } = 0.4f;
        public float EmeraldRingCombinedMultiplier { get; set; } = 0.2f;
        public float EmeraldRingCombinedPlusMultiplier { get; set; } = 0.4f;
        public float JadeRingCombinedMultiplier { get; set; } = 0.2f;
        public float JadeRingCombinedPlusMultiplier { get; set; } = 0.4f;
        public float AquamarineRingCombinedMultiplier { get; set; } = 0.2f;
        public float AquamarineRingCombinedPlusMultiplier { get; set; } = 0.4f;

        // ========== 铱环Alpha配置 ==========
        public float IridiumBandAlphaAttackMultiplier { get; set; } = 0.1f;
        public float IridiumBandAlphaSpeedMultiplier { get; set; } = 0.1f;
        public float IridiumBandAlphaCritPowerMultiplier { get; set; } = 0.1f;
        public float IridiumBandAlphaCritChanceMultiplier { get; set; } = 0.1f;

        // ========== 凝固汽油戒指配置 ==========
        public int NapalmRingPlusRadius { get; set; } = 3;
        public int NapalmRingPlus2Radius { get; set; } = 5;
        public int NapalmRingPlus3Radius { get; set; } = 7;

        // ========== 幸运戒指配置 ==========
        public int LuckyRingPlusLuck { get; set; } = 3;
        public int LuckyRingPlus2Luck { get; set; } = 5;

        // ========== 钓鱼戒指配置 ==========
        public int ExtraFishNum1 { get; set; } = 1;
        public int ExtraFishNum2 { get; set; } = 3;
        public int ExtraFishNum3 { get; set; } = 5;

        // ========== 其他配置 ==========
        public bool EnableNotfishAvailable { get; set; } = false;

        // ========== 配方开关配置 ==========
        public bool EnableLuckyRingRecipes { get; set; } = true;
        public bool EnableCombinedRingRecipes { get; set; } = true;
        public bool EnableIridiumBandRecipe { get; set; } = true;
        public bool EnableBurglarRingRecipes { get; set; } = true;
        public bool EnableMonsterSpawnRingRecipes { get; set; } = true;
        public bool EnableNapalmRingRecipes { get; set; } = true;
        public bool EnableChefRingRecipes { get; set; } = true;
        public bool EnableLFishRingRecipe { get; set; } = true;
        public bool EnableFishRingRecipes { get; set; } = true;
        public bool EnableQualityRingRecipes { get; set; } = true;

        // ========== 补丁开关配置 ==========
        public bool EnableChefRingPatch { get; set; } = true;
        public bool EnableLFishRingPatch { get; set; } = true;
        public bool EnableFishRingPatch { get; set; } = true;
        public bool EnableCropQualityRingPatch { get; set; } = true;
    }
}