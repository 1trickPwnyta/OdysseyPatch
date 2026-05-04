using HarmonyLib;
using RimWorld;

namespace OdysseyPatch.FlickSwitchesAfterLanding
{
    [HarmonyPatch(typeof(CompFlickable))]
    [HarmonyPatch(nameof(CompFlickable.CompGetGizmosExtra))]
    public static class Patch_CompFlickable
    {
        public static void Prefix(CompFlickable __instance, bool ___switchOnInt, ref bool ___wantSwitchOn)
        {
            if (Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.FLICK_SWITCHES_AFTER_LANDING) && __instance.parent.Faction == Faction.OfPlayer)
            {
                if (__instance.parent.Map.designationManager.DesignationOn(__instance.parent, DesignationDefOf.Flick) == null)
                {
                    ___wantSwitchOn = ___switchOnInt;
                }
            }
        }
    }
}
