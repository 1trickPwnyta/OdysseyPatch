using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.VacuumIntensityRoomStat
{
    [HarmonyPatch(typeof(CellInspectorDrawer))]
    [HarmonyPatch("DrawMapInspector")]
    public static class Patch_CellInspectorDrawer
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.LoadsField(typeof(RoomStatDef).Field(nameof(RoomStatDef.isHidden))));
            instructionsList.InsertRange(index + 1, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_S, 21),
                new CodeInstruction(OpCodes.Ldloc_3),
                new CodeInstruction(OpCodes.Call, typeof(VacuumUtility).Method(nameof(VacuumUtility.HiddenOrHiddenVacuum)))
            });
            return instructionsList;
        }
    }
}
