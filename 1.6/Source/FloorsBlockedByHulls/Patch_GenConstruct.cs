using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.FloorsBlockedByHulls
{
    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.FLOORS_BLOCKED_BY_HULLS)]
    [HarmonyPatch(typeof(GenConstruct))]
    [HarmonyPatch(nameof(GenConstruct.CanPlaceBlueprintAt_NewTemp))]
    public static class Patch_GenConstruct
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FirstIndexOf(i => i.opcode == OpCodes.Ldfld && i.operand is FieldInfo f && f == typeof(ThingDef).Field(nameof(ThingDef.category)));
            instructionsList.InsertRange(index - 2, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_S, 20),
                new CodeInstruction(OpCodes.Ldloc_S, 21),
                new CodeInstruction(OpCodes.Call, typeof(Patch_GenConstruct).Method(nameof(ShouldIgnoreForFoundation))),
                new CodeInstruction(OpCodes.Brfalse_S, instructionsList[index - 3].operand)
            });
            return instructionsList;
        }

        private static bool ShouldIgnoreForFoundation(Building building, TerrainDef terrain) => !Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.FLOORS_BLOCKED_BY_HULLS) || terrain.isFoundation || building == null || !building.def.coversFloor;
    }
}
