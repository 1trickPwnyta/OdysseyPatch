using HarmonyLib;
using Verse;

namespace OdysseyPatch.LavaPathAvoidance
{
    [HarmonyPatch(typeof(DangerUtility))]
    [HarmonyPatch(nameof(DangerUtility.GetDangerFor))]
    public static class Patch_DangerUtility
    {
        public static Danger Postfix(Danger danger, IntVec3 c, Map map, Pawn p)
        {
            if (LavaPathAvoidanceUtility.IsDangerousCell(c, map))
            {
                return Danger.Deadly;
            }
            else
            {
                return danger;
            }
        }
    }
}
