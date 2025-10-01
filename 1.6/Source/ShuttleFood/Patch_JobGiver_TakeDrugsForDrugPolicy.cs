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
    [HarmonyPatch(typeof(JobGiver_TakeDrugsForDrugPolicy))]
    [HarmonyPatch("TryGiveJob")]
    public static class Patch_JobGiver_TakeDrugsForDrugPolicy_TryGiveJob
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.opcode == OpCodes.Call && i.operand is MethodInfo m && m == typeof(DrugAIUtility).Method(nameof(DrugAIUtility.IngestAndTakeToInventoryJob)));
            List<Label> labels = instructionsList[index - 3].labels.ListFullCopy();
            LocalBuilder jobLocal = il.DeclareLocal(typeof(Job));
            Label ingestJobLabel = il.DefineLabel();
            instructionsList[index - 3].labels.Clear();
            instructionsList[index - 3].labels.Add(ingestJobLabel);
            instructionsList.InsertRange(index - 3, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_2) { labels = labels },
                new CodeInstruction(OpCodes.Call, typeof(Patch_JobGiver_TakeDrugsForDrugPolicy_TryGiveJob).Method(nameof(GetTakeFromShuttleJob))),
                new CodeInstruction(OpCodes.Dup),
                new CodeInstruction(OpCodes.Stloc_S, jobLocal),
                new CodeInstruction(OpCodes.Brfalse_S, ingestJobLabel),
                new CodeInstruction(OpCodes.Ldloc_S, jobLocal),
                new CodeInstruction(OpCodes.Ret)
            });
            return instructionsList;
        }

        private static Job GetTakeFromShuttleJob(Thing drug)
        {
            if (drug.ParentHolder is CompTransporter comp && comp.parent is Building_PassengerShuttle shuttle)
            {
                Job job = JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("OdysseyPatch_TakeFromShuttle"), drug, shuttle);
                job.count = 1;
                return job;
            }
            else
            {
                return null;
            }
        }
    }

    [HarmonyPatch(typeof(JobGiver_TakeDrugsForDrugPolicy))]
    [HarmonyPatch("FindDrugFor")]
    public static class Patch_JobGiver_TakeDrugsForDrugPolicy_FindDrugFor
    {
        public static void Postfix(JobGiver_TakeDrugsForDrugPolicy __instance, Pawn pawn, ThingDef drugDef, ref Thing __result)
        {
            if (OdysseyPatchSettings.ShuttleFood && __result == null)
            {
                if (pawn.IsColonist && pawn.Map != null)
                {
                    foreach (Building_PassengerShuttle shuttle in pawn.Map.listerThings.GetThingsOfType<Building_PassengerShuttle>().Where(s => s.Faction.IsPlayer && s.GetGrabbableSupplies().enabled))
                    {
                        if (!shuttle.IsForbidden(pawn) && pawn.CanReach(shuttle, PathEndMode.InteractionCell, Danger.Some))
                        {
                            foreach (Thing thing in shuttle.TransporterComp.innerContainer)
                            {
                                if (thing.def == drugDef && DrugValidator(__instance, pawn, thing))
                                {
                                    __result = thing;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool DrugValidator(JobGiver_TakeDrugsForDrugPolicy instance, Pawn pawn, Thing drug) => (bool)typeof(JobGiver_TakeDrugsForDrugPolicy).Method("DrugValidator").Invoke(instance, new object[] { pawn, drug });
    }
}
