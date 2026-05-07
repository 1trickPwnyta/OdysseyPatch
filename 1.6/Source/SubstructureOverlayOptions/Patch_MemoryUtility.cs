using HarmonyLib;
using SpecialSauce.Multipatch;
using Verse.Profile;

namespace OdysseyPatch.SubstructureOverlayOptions
{
    [HarmonyPatch_Compatibility(SpecialMod_OdysseyPatch.PACKAGE_ID, SpecialModSettings_Multipatch_Odyssey.SUBSTRUCTURE_OVERLAY_OPTIONS)]
    [HarmonyPatch(typeof(MemoryUtility))]
    [HarmonyPatch(nameof(MemoryUtility.ClearAllMapsAndWorld))]
    public static class Patch_MemoryUtility
    {
        public static void Postfix()
        {
            Patch_CompSubstructureFootprint_CompGetGizmosExtra.alwaysEnabled.Clear();
        }
    }
}
