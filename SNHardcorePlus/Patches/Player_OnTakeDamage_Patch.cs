﻿using Harmony;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace SNHardcorePlus.Patches
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("OnTakeDamage")]
    class Player_OnTakeDamage_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(MethodBase original, IEnumerable<CodeInstruction> instructions)
        {
            bool injected = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode.Equals(OpCodes.Ldc_R4) && instruction.operand.Equals(100f) && !injected)
                {
                    injected = true;
                    var newInstruction = new CodeInstruction(OpCodes.Ldc_R4, HCPSettings.Instance.HealthMax);
                    newInstruction.labels = instruction.labels;
                    yield return newInstruction;
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }
}
