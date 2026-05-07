using HarmonyLib;
using SpecialSauce.Multipatch;
using Verse;

namespace OdysseyPatch.SilhouettesHiddenByGravshipLanding
{
    [HarmonyPatch_Compatibility(SpecialMod_OdysseyPatch.PACKAGE_ID, SpecialModSettings_Multipatch_Odyssey.SILHOUETTES_HIDDEN_BY_GRAVSHIP_LANDING)]
    [HarmonyPatch(typeof(SilhouetteUtility))]
    [HarmonyPatch("ShouldDrawPawnDotSilhouette")]
    public static class Patch_SilhouetteUtility
    {
        public static void Postfix(Thing thing, ref bool __result)
        {
            if (Utility.CheckSetting(SpecialModSettings_Multipatch_Odyssey.SILHOUETTES_HIDDEN_BY_GRAVSHIP_LANDING))
            {
                if (WorldComponent_GravshipController.GravshipRenderInProgess || Find.ScreenshotModeHandler.Active)
                {
                    __result = false;
                }
            }
        }
    }
}
