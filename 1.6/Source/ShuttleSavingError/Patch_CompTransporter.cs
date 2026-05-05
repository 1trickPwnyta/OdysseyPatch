using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using System.Collections.Generic;

namespace OdysseyPatch.ShuttleSavingError
{
    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.SHUTTLE_SAVING_ERROR)]
    [HarmonyPatch(typeof(CompTransporter))]
    [HarmonyPatch(nameof(CompTransporter.PostExposeData))]
    public static class Patch_CompTransporter
    {
        public static void Prefix(ref List<TransferableOneWay> ___leftToLoad)
        {
            if (Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.SHUTTLE_SAVING_ERROR) && ___leftToLoad == null)
            {
                ___leftToLoad = new List<TransferableOneWay>();
            }
        }
    }
}
