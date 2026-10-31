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

            // 尝试找到 MathHelper.Clamp 调用后的第一个存储指令（品质变量）
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Call &&
                    codes[i].operand is MethodInfo methodInfo &&
                    methodInfo.Name == "Clamp" &&
                    methodInfo.DeclaringType == typeof(Microsoft.Xna.Framework.MathHelper))
                {
                    // 从 Clamp 之后开始搜索第一个 stloc 或 stloc.s
                    for (int j = i + 1; j < codes.Count; j++)
                    {
                        if (codes[j].opcode == OpCodes.Stloc_S || codes[j].opcode == OpCodes.Stloc)
                        {
                            if (codes[j].operand is LocalBuilder localBuilder)
                            {
                                cropQualityLocalIndex = localBuilder.LocalIndex;
                                break;
                            }
                            else if (codes[j].operand is int localIndex)
                            {
                                cropQualityLocalIndex = localIndex;
                                break;
                            }
                        }
                    }
                    if (cropQualityLocalIndex != -1)
                        break;
                }
            }

            if (cropQualityLocalIndex == -1)
            {
                ModEntry.GetInstance().Monitor.Log("Could not locate crop quality local variable. Crop quality ring effect will not work.", LogLevel.Warn);
                // 返回原始指令，避免破坏游戏
                foreach (var code in codes)
                    yield return code;
                yield break;
            }

            // 遍历所有指令，在每次存储到目标变量后插入调整代码
            foreach (var code in codes)
            {
                yield return code;

                // 检查当前指令是否存储到目标变量
                if ((code.opcode == OpCodes.Stloc_S || code.opcode == OpCodes.Stloc) &&
                    ((code.operand is LocalBuilder lb && lb.LocalIndex == cropQualityLocalIndex) ||
                     (code.operand is int idx && idx == cropQualityLocalIndex)))
                {
                    // 插入调整代码：加载变量 → 调用调整方法 → 存回变量
                    yield return new CodeInstruction(OpCodes.Ldloc_S, cropQualityLocalIndex);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CropQualityPatcher), "AdjustCropQuality"));
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