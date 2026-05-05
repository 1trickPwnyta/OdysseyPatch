using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using System.Linq;
using Verse;

namespace OdysseyPatch.SubstructureOverlayOptions
{
    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.SUBSTRUCTURE_OVERLAY_OPTIONS)]
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
