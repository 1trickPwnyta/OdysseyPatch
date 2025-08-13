using HarmonyLib;
using Verse;

namespace OdysseyPatch.LavaPathAvoidance
{
    [HarmonyPatch(typeof(PathFinderCostTuning))]
    [HarmonyPatch(nameof(PathFinderCostTuning.For))]
    public static class Patch_PathFinderCostTuning
    {
        public static PathFinderCostTuning Postfix(PathFinderCostTuning tuning)
        {
            if (OdysseyPatchSettings.LavaPathAvoidance)
            {
                tuning.costDanger = 3000;
            }
            return tuning;
        }
    }
}
