using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Collections.Generic;

namespace OdysseyPatch.ShuttleSavingError
{
    [HarmonyPatch_Compatibility(SpecialMod_OdysseyPatch.PACKAGE_ID, Settings.ShuttleSavingError)]
    [HarmonyPatch(typeof(CompTransporter))]
    [HarmonyPatch(nameof(CompTransporter.PostExposeData))]
    public static class Patch_CompTransporter
    {
        public static void Prefix(ref List<TransferableOneWay> ___leftToLoad)
        {
            if (Settings.ShuttleSavingError.Enabled() && ___leftToLoad == null)
            {
                ___leftToLoad = new List<TransferableOneWay>();
            }
        }
    }
}
