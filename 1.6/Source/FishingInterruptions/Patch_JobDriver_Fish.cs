using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace OdysseyPatch.FishingInterruptions
{
    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.FISHING_INTERRUPTIONS)]
    [HarmonyPatch(typeof(JobDriver_Fish))]
    [HarmonyPatch("MakeNewToils")]
    public static class Patch_JobDriver_Fish
    {
        public static void Postfix(ref IEnumerable<Toil> __result)
        {
            if (Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.FISHING_INTERRUPTIONS))
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
