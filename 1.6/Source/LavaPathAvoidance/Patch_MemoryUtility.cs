using HarmonyLib;
using Verse.Profile;

namespace OdysseyPatch.LavaPathAvoidance
{
    [HarmonyPatch(typeof(MemoryUtility))]
    [HarmonyPatch(nameof(MemoryUtility.ClearAllMapsAndWorld))]
    public static class Patch_MemoryUtility
    {
        public static void Postfix()
        {
            LavaPathAvoidanceUtility.ClearCache();
        }
    }
}
