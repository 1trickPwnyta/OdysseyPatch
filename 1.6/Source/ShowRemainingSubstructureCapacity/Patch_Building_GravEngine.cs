using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.ShowRemainingSubstructureCapacity
{
    [HarmonyPatch(typeof(Building_GravEngine))]
    [HarmonyPatch(nameof(Building_GravEngine.GetInspectString))]
    public static class Patch_Building_GravEngine
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundConnectedSubstructure = false;
            bool done = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.LoadsConstant("ConnectedSubstructure"))
                {
                    foundConnectedSubstructure = true;
                }
                if (foundConnectedSubstructure && !done && instruction.opcode == OpCodes.Stloc_0)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Building_GravEngine).PropertyGetter(nameof(Building_GravEngine.AllConnectedSubstructure)));
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_Building_GravEngine).Method(nameof(GetNewInspectString)));
                    done = true;
                }

                yield return instruction;
            }
        }

        private static string GetNewInspectString(string oldInspectString, HashSet<IntVec3> allConnectedSubstructure, Building_GravEngine engine)
        {
            if (OdysseyPatchSettings.ShowRemainingSubstructureCapacity)
            {
                return oldInspectString + $" ({(int)engine.GetStatValue(StatDefOf.SubstructureSupport) - allConnectedSubstructure.Count} {"OdysseyPatch_Remaining".Translate()})";
            }
            else
            {
                return oldInspectString;
            }
        }
    }
}
