using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Tools;

namespace Enhanced_Rings
{
    internal static class FishCaughtHandler
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(FishingRod), nameof(FishingRod.pullFishFromWater))]
        public static bool PullFishFromWater_Prefix(
            ref int fishQuality,
            int fishSize,
            ref bool fromFishPond,
            ref int numCaught)
        {
            var modEntry = ModEntry.GetInstance();
            var config = modEntry.Config;

            try
            {
                if (!config.EnableFishRingPatch)
                    return true;

                if (!fromFishPond) // Dont even think about it
                {
                    if (Game1.player.isWearingRing("ExtraFishRing3"))
                    {
                        numCaught += config.ExtraFishNum3;
                    }
                    else if (Game1.player.isWearingRing("ExtraFishRing2"))
                    {
                        numCaught += config.ExtraFishNum2;
                    }
                    else if (Game1.player.isWearingRing("ExtraFishRing1"))
                    {
                        numCaught += config.ExtraFishNum1;
                    }
                }
                bool shouldApplyQualityRings = false;
                if (fishSize > 0)
                {
                    shouldApplyQualityRings = true;
                }
                else if (config.EnableNotfishAvailable)
                {
                    shouldApplyQualityRings = true;
                }

                if (shouldApplyQualityRings)
                {
                    if (Game1.player.isWearingRing("QualityRing3"))
                    {
                        if (fishQuality < 4) fishQuality = 4;
                    }
                    else if (Game1.player.isWearingRing("QualityRing2"))
                    {
                        if (fishQuality < 2) fishQuality = 2;
                    }
                    else if (Game1.player.isWearingRing("QualityRing1"))
                    {
                        if (fishQuality < 1) fishQuality = 1;
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                ModEntry.GetInstance().Monitor.Log($"Err fish get: {ex.Message}", LogLevel.Error);
                return true;
            }
        }
    }
}