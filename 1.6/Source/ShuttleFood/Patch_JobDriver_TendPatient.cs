using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace OdysseyPatch.ShuttleFood
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Odyssey.PACKAGE_ID, Settings.ShuttleFood)]
    [HarmonyPatch(typeof(JobDriver_TendPatient))]
    [HarmonyPatch(nameof(JobDriver_TendPatient.CollectMedicineToils))]
    public static class Patch_JobDriver_TendPatient
    {
        public static void Postfix(Pawn doctor, Job job, Pawn patient, Toil gotoToil, List<Toil> __result)
        {
            if (Settings.ShuttleFood.Enabled())
            {
                Toil goToShuttle = Toils_Shuttle.GotoShuttle(TargetIndex.C, job, doctor);
                __result.InsertRange(7, new[]
                {
                    goToShuttle,
                    Toils_General.Wait(25).WithProgressBarToilDelay(TargetIndex.C),
                    Toils_Haul.TakeFromOtherInventory(job.targetB.Thing, doctor.inventory.innerContainer, (job.targetB.Thing?.ParentHolder as CompTransporter)?.GetDirectlyHeldThings(), Medicine.GetMedicineCountToFullyHeal(patient), TargetIndex.B),
                    Toils_Jump.Jump(gotoToil)
                });
                __result.Insert(2, Toils_Shuttle.CheckItemInShuttle(job.targetB.Thing, TargetIndex.C, goToShuttle));
            }
        }
    }
}
