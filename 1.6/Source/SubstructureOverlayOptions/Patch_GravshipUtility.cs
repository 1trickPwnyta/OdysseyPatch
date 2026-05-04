using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;

namespace OdysseyPatch.SubstructureOverlayOptions
{
    [HarmonyPatch(typeof(GravshipUtility))]
    [HarmonyPatch(nameof(GravshipUtility.ShowConnectedSubstructure))]
    [HarmonyPatch(MethodType.Getter)]
    public static class Patch_GravshipUtility
    {
        public static void Postfix(ref bool __result)
        {
            if (Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.SUBSTRUCTURE_OVERLAY_OPTIONS) && Patch_CompSubstructureFootprint_CompGetGizmosExtra.alwaysEnabled.Any(c => c.parent.Map == Find.CurrentMap))
            {
                __result = true;
            }
        }
    }
}
