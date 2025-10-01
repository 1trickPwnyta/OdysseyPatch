using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace OdysseyPatch.ShuttleFood
{
    [HarmonyPatch(typeof(JobDriver_FoodFeedPatient))]
    [HarmonyPatch("MakeNewToils")]
    public static class Patch_JobDriver_FoodFeedPatient
    {
        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> toils, Pawn ___pawn, Job ___job)
        {
            if (OdysseyPatchSettings.ShuttleFood)
            {
                Toil goToShuttle = Toils_Shuttle.GotoShuttle(TargetIndex.C, ___job, ___pawn);

                List<Toil> toilsList = toils.ToList();
                for (int i = 0; i < toilsList.Count; i++)
                {
                    Toil toil = toilsList[i];
                    if (i == 2)
                    {
                        yield return Toils_Shuttle.CheckItemInShuttle(___job.targetA.Thing, TargetIndex.C, goToShuttle);
                    }
                    else if (i == 6)
                    {
                        yield return goToShuttle;
                        yield return Toils_General.Wait(25).WithProgressBarToilDelay(TargetIndex.C);
                        yield return Toils_Haul.TakeFromOtherInventory(___job.targetA.Thing, ___pawn.inventory.innerContainer, (___job.targetA.Thing?.ParentHolder as CompTransporter)?.GetDirectlyHeldThings(), ___job.count, TargetIndex.A);
                        yield return Toils_Jump.Jump(toilsList[9]);
                    }
                    yield return toil;
                }
            }
        }
    }
}
