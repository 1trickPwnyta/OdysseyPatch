using HarmonyLib;
using RimWorld;
using Verse;

namespace OdysseyPatch.ShuttleBlockedByLess
{
    [HarmonyPatch(typeof(RoyalTitlePermitWorker_CallShuttle))]
    [HarmonyPatch(nameof(RoyalTitlePermitWorker_CallShuttle.ShuttleCanLandHere))]
    public static class Patch_RoyalTitlePermitWorker_CallShuttle
    {
        public static bool Prefix(LocalTargetInfo target, Map map, ThingDef shuttleDef, Rot4? rot, ref AcceptanceReport __result)
        {
            if (Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.SHUTTLE_BLOCKED_BY_LESS) && shuttleDef == ThingDefOf.PassengerShuttle)
            {
                if (GenConstruct.CanPlaceBlueprintAt(shuttleDef, target.Cell, rot ?? shuttleDef.defaultPlacingRot, map))
                {
                    __result = AcceptanceReport.WasAccepted;
                    return false;
                }
            }
            return true;
        }
    }
}
