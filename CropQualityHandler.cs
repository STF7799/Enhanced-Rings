using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Enhanced_Rings
{
    internal class CropQualityPatcher
    {
        public static void ApplyPatch(Harmony harmony)
        {
            try
            {
                var original = typeof(Crop).GetMethod("harvest", BindingFlags.Public | BindingFlags.Instance);
                if (original == null)
                {
                    ModEntry.GetInstance().Monitor.Log("Could not find Crop.harvest method!", LogLevel.Error);
                    return;
                }

                var transpiler = typeof(CropQualityPatcher).GetMethod("Transpiler_Harvest",
                    BindingFlags.Static | BindingFlags.Public);

                if (transpiler == null)
                {
                    ModEntry.GetInstance().Monitor.Log("Could not find Transpiler_Harvest method!", LogLevel.Error);
                    return;
                }

                harmony.Patch(original, transpiler: new HarmonyMethod(transpiler));
            }
            catch (Exception ex)
            {
                ModEntry.GetInstance().Monitor.Log($"Failed to apply crop quality patch: {ex}", LogLevel.Error);
            }
        }

        public static IEnumerable<CodeInstruction> Transpiler_Harvest(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            int cropQualityLocalIndex = -1;

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Call &&
                    codes[i].operand is MethodInfo methodInfo &&
                    methodInfo.Name == "Clamp" &&
                    methodInfo.DeclaringType == typeof(Microsoft.Xna.Framework.MathHelper))
                {
                    if (i + 1 < codes.Count)
                    {
                        var nextInstruction = codes[i + 1];
                        if (nextInstruction.opcode == OpCodes.Stloc_S)
                        {
                            if (nextInstruction.operand is LocalBuilder localBuilder)
                            {
                                cropQualityLocalIndex = localBuilder.LocalIndex;
                            }
                            else if (nextInstruction.operand is int localIndex)
                            {
                                cropQualityLocalIndex = localIndex;
                            }
                        }
                    }
                    break;
                }
            }

            for (int i = 0; i < codes.Count; i++)
            {
                yield return codes[i];

                if (codes[i].opcode == OpCodes.Stloc_S &&
                    ((codes[i].operand is LocalBuilder lb && lb.LocalIndex == cropQualityLocalIndex) ||
                     (codes[i].operand is int idx && idx == cropQualityLocalIndex)))
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_S, cropQualityLocalIndex);

                    // 调用调整方法
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(CropQualityPatcher), "AdjustCropQuality"));

                    // 存储调整后的值
                    yield return new CodeInstruction(OpCodes.Stloc_S, cropQualityLocalIndex);
                }
            }
        }
        public static int AdjustCropQuality(int cropQuality)
        {
            try
            {
                if (Game1.player == null)
                    return cropQuality;

                int originalQuality = cropQuality;
                if (Game1.player.isWearingRing("QualityRing3"))
                {
                    cropQuality = 4;
                }
                else if (Game1.player.isWearingRing("QualityRing2"))
                {
                    if (cropQuality < 2)
                    {
                        cropQuality = 2;
                    }
                }
                else if (Game1.player.isWearingRing("QualityRing1"))
                {
                    if (cropQuality < 1)
                    {
                        cropQuality = 1;
                    }
                }

                if (originalQuality != cropQuality)
                {
                    ModEntry.GetInstance().Monitor.Log($"Adjusted crop quality from {originalQuality} to {cropQuality}", LogLevel.Trace);
                }

                return cropQuality;
            }
            catch (Exception ex)
            {
                ModEntry.GetInstance().Monitor.Log($"Error in AdjustCropQuality: {ex}", LogLevel.Error);
                return cropQuality;
            }
        }
    }
}