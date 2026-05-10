using HarmonyLib;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using Verse;

namespace OdysseyPatch.SilhouettesHiddenByGravshipLanding
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Odyssey.PACKAGE_ID, Settings.SilhouettesHiddenByGravshipLanding)]
    [HarmonyPatch(typeof(SilhouetteUtility))]
    [HarmonyPatch("ShouldDrawPawnDotSilhouette")]
    public static class Patch_SilhouetteUtility
    {
        public static void Postfix(Thing thing, ref bool __result)
        {
            if (Settings.SilhouettesHiddenByGravshipLanding.Enabled())
            {
                if (WorldComponent_GravshipController.GravshipRenderInProgess || Find.ScreenshotModeHandler.Active)
                {
                    __result = false;
                }
            }
        }
    }
}
