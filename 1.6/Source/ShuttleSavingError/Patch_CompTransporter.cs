using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Collections.Generic;

namespace OdysseyPatch.ShuttleSavingError
{
    [HarmonyPatch_Compatibility(SpecialMod_OdysseyPatch.PACKAGE_ID, SpecialModSettings_Multipatch_Odyssey.SHUTTLE_SAVING_ERROR)]
    [HarmonyPatch(typeof(CompTransporter))]
    [HarmonyPatch(nameof(CompTransporter.PostExposeData))]
    public static class Patch_CompTransporter
    {
        public static void Prefix(ref List<TransferableOneWay> ___leftToLoad)
        {
            if (Utility.CheckSetting(SpecialModSettings_Multipatch_Odyssey.SHUTTLE_SAVING_ERROR) && ___leftToLoad == null)
            {
                ___leftToLoad = new List<TransferableOneWay>();
            }
        }
    }
}
