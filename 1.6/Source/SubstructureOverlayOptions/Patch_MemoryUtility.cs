using HarmonyLib;
using Verse.Profile;

namespace OdysseyPatch.SubstructureOverlayOptions
{
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
