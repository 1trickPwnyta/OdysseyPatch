using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Linq;
using Verse;

namespace OdysseyPatch.SubstructureOverlayOptions
{
    [HarmonyPatch_Compatibility(SpecialMod_OdysseyPatch.PACKAGE_ID, SpecialModSettings_Multipatch_Odyssey.SUBSTRUCTURE_OVERLAY_OPTIONS)]
    [HarmonyPatch(typeof(GravshipUtility))]
    [HarmonyPatch(nameof(GravshipUtility.ShowConnectedSubstructure))]
    [HarmonyPatch(MethodType.Getter)]
    public static class Patch_GravshipUtility
    {
        public static void Postfix(ref bool __result)
        {
            if (Utility.CheckSetting(SpecialModSettings_Multipatch_Odyssey.SUBSTRUCTURE_OVERLAY_OPTIONS) && Patch_CompSubstructureFootprint_CompGetGizmosExtra.alwaysEnabled.Any(c => c.parent.Map == Find.CurrentMap))
            {
                __result = true;
            }
        }
    }
}
