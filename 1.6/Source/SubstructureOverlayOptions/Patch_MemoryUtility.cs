using HarmonyLib;
using SpecialSauce.ModSettings;
using Verse.Profile;

namespace OdysseyPatch.SubstructureOverlayOptions
{
    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.SUBSTRUCTURE_OVERLAY_OPTIONS)]
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
