using Enhanced_Rings;
using HarmonyLib;
using StardewValley;
using StardewValley.Locations;

public static class MonsterSpawnHandler
{
    /// <summary>
    /// 矿洞和骷髅洞穴的怪物生成调整
    /// </summary>
    [HarmonyPostfix]
    public static void AdjustMineShaftMonsterChance_Postfix(MineShaft __instance, ref double stoneChance,
        ref double monsterChance, ref double itemChance, ref double gemStoneChance)
    {
        if (!IsInValidMineArea(__instance))
            return;

        double multiplier = GetMonsterSpawnMultiplier();
        if (multiplier > 1.0)
        {
            var config = ModEntry.GetInstance().Config;
            double newChance = monsterChance * multiplier;
            monsterChance = System.Math.Min(newChance, config.MaxMonsterSpawnChance);
        }
    }

    /// <summary>
    /// 火山地牢的怪物生成调整
    /// </summary>
    [HarmonyPostfix]
    public static void AdjustVolcanoDungeonMonsterChance_Postfix(VolcanoDungeon __instance, ref double stoneChance,
        ref double monsterChance, ref double itemChance, ref double gemStoneChance)
    {
        var config = ModEntry.GetInstance().Config;

        if (!config.AffectVolcanoDungeon)
            return;

        double multiplier = GetMonsterSpawnMultiplier();
        if (multiplier > 1.0)
        {
            double newChance = monsterChance * multiplier;
            monsterChance = System.Math.Min(newChance, config.MaxMonsterSpawnChance);
        }
    }

    /// <summary>
    /// 检查是否在有效的矿洞区域
    /// </summary>
    private static bool IsInValidMineArea(MineShaft mine)
    {
        var config = ModEntry.GetInstance().Config;

        if (mine.mineLevel <= 0 || mine.mineLevel == 77377)
            return false;

        if (mine.mineLevel % 5 != 0 && mine.getMineArea(-1) != 121)
            return true;

        if (mine.getMineArea(-1) == 121)
            return config.AffectSkullCavern;

        return false;
    }

    /// <summary>
    /// 获取怪物生成倍率（保持不变）
    /// </summary>
    private static double GetMonsterSpawnMultiplier()
    {
        double multiplier = 1.0;
        var config = ModEntry.GetInstance().Config;

        foreach (var farmer in Game1.getAllFarmers())
        {
            if (farmer == null || !farmer.IsLocalPlayer)
                continue;

            if (farmer.isWearingRing(RingIds.MonsterSpawnRing3))
            {
                multiplier = System.Math.Max(multiplier, config.MonsterSpawnRing3Multiplier);
            }
            else if (farmer.isWearingRing(RingIds.MonsterSpawnRing2))
            {
                multiplier = System.Math.Max(multiplier, config.MonsterSpawnRing2Multiplier);
            }
            else if (farmer.isWearingRing(RingIds.MonsterSpawnRing))
            {
                multiplier = System.Math.Max(multiplier, config.MonsterSpawnRingMultiplier);
            }
        }

        return multiplier;
    }
}