using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Linq;
using Verse;

namespace OdysseyPatch.SubstructureOverlayOptions
{
    [HarmonyPatch_Compatibility(SpecialMod_OdysseyPatch.PACKAGE_ID, Settings.SubstructureOverlayOptions)]
    [HarmonyPatch(typeof(GravshipUtility))]
    [HarmonyPatch(nameof(GravshipUtility.ShowConnectedSubstructure))]
    [HarmonyPatch(MethodType.Getter)]
    public static class Patch_GravshipUtility
    {
        public static void Postfix(ref bool __result)
        {
            if (Settings.SubstructureOverlayOptions.Enabled() && Patch_CompSubstructureFootprint_CompGetGizmosExtra.alwaysEnabled.Any(c => c.parent.Map == Find.CurrentMap))
            {
                __result = true;
            }
        }
    }
}
