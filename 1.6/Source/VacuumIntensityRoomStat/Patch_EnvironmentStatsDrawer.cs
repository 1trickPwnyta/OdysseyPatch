using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.VacuumIntensityRoomStat
{
    [HarmonyPatch(typeof(EnvironmentStatsDrawer))]
    [HarmonyPatch(nameof(EnvironmentStatsDrawer.DoRoomInfo))]
    public static class Patch_EnvironmentStatsDrawer
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.LoadsField(typeof(RoomStatDef).Field(nameof(RoomStatDef.isHidden))));
            instructionsList.InsertRange(index + 1, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_S, 4),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, typeof(VacuumUtility).Method(nameof(VacuumUtility.HiddenOrHiddenVacuum)))
            });
            return instructionsList;
        }
    }
}
