using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace OdysseyPatch.ShuttleFood
{
    [HarmonyPatch(typeof(JobGiver_GetFood))]
    [HarmonyPatch("TryGiveJob")]
    public static class Patch_JobGiver_GetFood
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.opcode == OpCodes.Ldsfld && i.operand is FieldInfo f && f == typeof(JobDefOf).Field(nameof(JobDefOf.Ingest)));
            Label shuttleJobLabel = instructionsList[index].labels[0];
            Label ingestJobLabel = il.DefineLabel();
            LocalBuilder jobLocal = il.DeclareLocal(typeof(Job));
            instructionsList[index].labels[0] = ingestJobLabel;
            instructionsList.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_1) { labels = new List<Label> { shuttleJobLabel } },
                new CodeInstruction(OpCodes.Ldloc_S, 4),
                new CodeInstruction(OpCodes.Ldloc_S, 5),
                new CodeInstruction(OpCodes.Ldloc_S, 8),
                new CodeInstruction(OpCodes.Call, typeof(Patch_JobGiver_GetFood).Method(nameof(GetTakeFromShuttleJob))),
                new CodeInstruction(OpCodes.Dup),
                new CodeInstruction(OpCodes.Stloc_S, jobLocal),
                new CodeInstruction(OpCodes.Brfalse_S, ingestJobLabel),
                new CodeInstruction(OpCodes.Ldloc_S, jobLocal),
                new CodeInstruction(OpCodes.Ret)
            });
            return instructionsList;
        }

        private static Job GetTakeFromShuttleJob(Pawn pawn, Thing foodSource, ThingDef foodDef, float nutrition)
        {
            if (foodSource.ParentHolder is CompTransporter comp && comp.parent is Building_PassengerShuttle shuttle)
            {
                Job job = JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("OdysseyPatch_TakeFromShuttle"), foodSource, shuttle);
                job.count = FoodUtility.WillIngestStackCountOf(pawn, foodDef, nutrition);
                return job;
            }
            else
            {
                return null;
            }
        }
    }
}
