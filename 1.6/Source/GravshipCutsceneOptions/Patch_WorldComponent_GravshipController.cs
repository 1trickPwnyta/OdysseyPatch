using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace OdysseyPatch.GravshipCutsceneOptions
{
    [HarmonyPatch(typeof(WorldComponent_GravshipController))]
    [HarmonyPatch(nameof(WorldComponent_GravshipController.WorldComponentUpdate))]
    public static class Patch_WorldComponent_GravshipController
    {
        public static void Postfix(WorldComponent_GravshipController __instance, bool ___isTakeoff, GravshipCapturer ___gravshipCapturer, bool ___cutsceneInProgress, Gravship ___gravship, GravshipAudio ___gravshipAudio)
        {
            if (OdysseyPatchSettings.GravshipCutsceneOptions && KeyBindingDefOf.Accept.IsDown && ___gravshipCapturer.IsCaptureComplete && ___cutsceneInProgress && ___gravship != null)
            {
                __instance.GetType().Method(___isTakeoff ? "TakeoffEnded" : "LandingEnded").Invoke(__instance, new object[] { });
                ___gravshipAudio.EndTakeoff();
            }
        }
    }
}
