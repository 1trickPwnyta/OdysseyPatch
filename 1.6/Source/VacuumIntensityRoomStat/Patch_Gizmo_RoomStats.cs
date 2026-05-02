using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.VacuumIntensityRoomStat
{
    [HarmonyPatch(typeof(Gizmo_RoomStats))]
    [HarmonyPatch(nameof(Gizmo_RoomStats.GetRoomToShowStatsFor))]
    public static class Patch_Gizmo_RoomStats_GetRoomToShowStatsFor
    {
        public static void Postfix(Room __result)
        {
            VacuumUtility.statsRoom = __result;
        }
    }

    [HarmonyPatch(typeof(Gizmo_RoomStats))]
    [HarmonyPatch(nameof(Gizmo_RoomStats.GizmoOnGUI))]
    public static class Patch_Gizmo_RoomStats_GizmoOnGUI
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            CodeInstruction instruction = instructionsList.Find(i => i.LoadsField(typeof(RoomStatDef).Field(nameof(RoomStatDef.isHidden))));
            instruction.opcode = OpCodes.Call;
            instruction.operand = typeof(Patch_Gizmo_RoomStats_GizmoOnGUI).Method(nameof(IsHidden));
            return instructionsList;
        }

        private static bool IsHidden(RoomStatDef def) => def.isHidden || def.defName == "VacuumIntensity";
    }
}
