using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace OdysseyPatch.FishingInterruptions
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Odyssey.PACKAGE_ID, Settings.FishingInterruptions)]
    [HarmonyPatch(typeof(JobDriver_Fish))]
    [HarmonyPatch("MakeNewToils")]
    public static class Patch_JobDriver_Fish
    {
        public static void Postfix(ref IEnumerable<Toil> __result)
        {
            if (Settings.FishingInterruptions.Enabled())
            {
                List<Toil> list = __result.ToList();
                Toil toil = list[1];
                toil.tickAction = (Action)Delegate.Combine(toil.tickAction, (Action)(() =>
                {
                    Pawn pawn = toil.GetActor();
                    if (pawn.needs.food?.CurLevelPercentage <= 0.02f || pawn.needs.rest?.CurLevelPercentage <= 0.02f)
                    {
                        pawn.jobs.CheckForJobOverride(8f);
                    }
                }));
                __result = list;
            }
        }
    }
}
