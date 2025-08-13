using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace OdysseyPatch.LavaPathAvoidance
{
    [HarmonyPatch(typeof(Pawn_PathFollower))]
    [HarmonyPatch(nameof(Pawn_PathFollower.PatherTick))]
    public static class Patch_Pawn_PathFollower_PatherTick
    {
        public static void Postfix(Pawn ___pawn)
        {
            if (OdysseyPatchSettings.LavaPathAvoidance && ___pawn.IsHashIntervalTick(300) && LavaPathAvoidanceUtility.HasDirtyPath(___pawn))
            {
                LavaPathAvoidanceUtility.RepathIfNecessary(___pawn);
            }
        }
    }

    [HarmonyPatch(typeof(Pawn_PathFollower))]
    [HarmonyPatch(nameof(Pawn_PathFollower.StartPath))]
    public static class Patch_Pawn_PathFollower_StartPath
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.opcode == OpCodes.Stfld && i.operand is FieldInfo f && f == typeof(Pawn_PathFollower).Field("destination"));
            instructionsList.InsertRange(index + 1, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, typeof(Pawn_PathFollower).Field("pawn")),
                new CodeInstruction(OpCodes.Call, typeof(LavaPathAvoidanceUtility).Method(nameof(LavaPathAvoidanceUtility.SetPawnStartedMoving)))
            });
            return instructionsList;
        }
    }
}
