using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using Verse;

namespace OdysseyPatch.ShuttleBlockedByLess
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Odyssey.PACKAGE_ID, Settings.ShuttleBlockedByLess)]
    [HarmonyPatch(typeof(RoyalTitlePermitWorker_CallShuttle))]
    [HarmonyPatch(nameof(RoyalTitlePermitWorker_CallShuttle.ShuttleCanLandHere))]
    public static class Patch_RoyalTitlePermitWorker_CallShuttle
    {
        public static bool Prefix(LocalTargetInfo target, Map map, ThingDef shuttleDef, Rot4? rot, ref AcceptanceReport __result)
        {
            if (Settings.ShuttleBlockedByLess.Enabled() && shuttleDef == ThingDefOf.PassengerShuttle)
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
