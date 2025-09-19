using HarmonyLib;
using Verse;

namespace OdysseyPatch.SilhouettesHiddenByGravshipLanding
{
    [HarmonyPatch(typeof(SilhouetteUtility))]
    [HarmonyPatch("ShouldDrawPawnDotSilhouette")]
    public static class Patch_SilhouetteUtility
    {
        public static void Postfix(Thing thing, ref bool __result)
        {
            if (OdysseyPatchSettings.SilhouettesHiddenByGravshipLanding)
            {
                if (WorldComponent_GravshipController.GravshipRenderInProgess || Find.ScreenshotModeHandler.Active)
                {
                    __result = false;
                }
            }
        }
    }
}
