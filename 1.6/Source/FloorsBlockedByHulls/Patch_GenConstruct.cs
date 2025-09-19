using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.FloorsBlockedByHulls
{
    [HarmonyPatch(typeof(GenConstruct))]
    [HarmonyPatch(nameof(GenConstruct.CanPlaceBlueprintAt))]
    public static class Patch_GenConstruct
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FirstIndexOf(i => i.opcode == OpCodes.Ldfld && i.operand is FieldInfo f && f == typeof(ThingDef).Field(nameof(ThingDef.category)));
            instructionsList.InsertRange(index - 2, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_S, 21),
                new CodeInstruction(OpCodes.Call, typeof(Patch_GenConstruct).Method(nameof(ShouldIgnoreForFoundation))),
                new CodeInstruction(OpCodes.Brfalse_S, instructionsList[index - 3].operand)
            });
            return instructionsList;
        }

        private static bool ShouldIgnoreForFoundation(TerrainDef terrain) => !OdysseyPatchSettings.FloorsBlockedByHulls || terrain.isFoundation;
    }
}
