using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Tools;
using System.Reflection;

namespace Enhanced_Rings
{
    internal static class FishingOverhaulCompatibility
    {
        private static IMonitor Monitor => ModEntry.GetInstance().Monitor;
        private static ModConfig Config => ModEntry.GetInstance().Config;

        public static void Apply(Harmony harmony)
        {
            try
            {
                // 获取 FishingOverhaul 的 patcher 类型
                var patcherType = Type.GetType("TehPers.FishingOverhaul.Services.Setup.FishingRodPatcher, TehPers.FishingOverhaul");
                if (patcherType == null)
                {
                    Monitor.Log("FishingRodPatcher type not found. Fishing Overhaul compatibility disabled.", LogLevel.Warn);
                    return;
                }

                // 获取 CatchItem 方法
                var catchItemMethod = AccessTools.Method(patcherType, "CatchItem");
                if (catchItemMethod == null)
                {
                    Monitor.Log("CatchItem method not found. Fishing Overhaul compatibility disabled.", LogLevel.Warn);
                    return;
                }

                // 应用前缀补丁
                harmony.Patch(catchItemMethod,
                    prefix: new HarmonyMethod(typeof(FishingOverhaulCompatibility), nameof(CatchItem_Prefix)));

                Monitor.Log("Applied Fishing Overhaul compatibility patches.", LogLevel.Info);
            }
            catch (Exception ex)
            {
                Monitor.Log($"Failed to apply Fishing Overhaul compatibility patches: {ex}", LogLevel.Error);
            }
        }

        private static void CatchItem_Prefix(FishingRod rod, object info) // info 是 CatchInfo 类型
        {
            if (rod.fromFishPond || Game1.isFestival()) return;

            // 反射获取 info 的具体类型和内部字段
            Type infoType = info.GetType();
            // 检查是否是 FishCatch 类型（嵌套类）
            if (infoType.Name == "FishCatch")
            {
                // 获取字段（使用反射，避免强类型）
                var numField = infoType.GetField("<NumberOfFishCaught>k__BackingField",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                var qualField = infoType.GetField("<FishQuality>k__BackingField",
                    BindingFlags.NonPublic | BindingFlags.Instance);

                int extraFish = 0;
                int qualityBoost = 0;

                var player = Game1.player;
                if (player.isWearingRing(RingIds.ExtraFishRing3))
                {
                    extraFish = Config.ExtraFishNum3;
                }
                else if (player.isWearingRing(RingIds.ExtraFishRing2))
                {
                    extraFish = Config.ExtraFishNum2;
                }
                else if (player.isWearingRing(RingIds.ExtraFishRing1))
                {
                    extraFish = Config.ExtraFishNum1;
                }

                if (player.isWearingRing(RingIds.QualityRing3))
                {
                    qualityBoost = 4;
                }
                else if (player.isWearingRing(RingIds.QualityRing2))
                {
                    qualityBoost = 2;
                }
                else if (player.isWearingRing(RingIds.QualityRing1))
                {
                    qualityBoost = 1;
                }

                if (extraFish > 0 && numField != null)
                {
                    int currentNum = (int)numField.GetValue(info);
                    numField.SetValue(info, currentNum + extraFish);
                }

                if (qualityBoost > 0 && qualField != null)
                {
                    int currentQual = (int)qualField.GetValue(info);
                    int newQuality = Math.Max(currentQual, qualityBoost);
                    if (newQuality > 4) newQuality = 4;
                    qualField.SetValue(info, newQuality);
                }
            }
        }
    }
}