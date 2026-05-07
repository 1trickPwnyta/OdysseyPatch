using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using Verse;

namespace OdysseyPatch.ShuttleBlockedByLess
{
    [HarmonyPatch_Compatibility(SpecialMod_OdysseyPatch.PACKAGE_ID, SpecialModSettings_Multipatch_Odyssey.SHUTTLE_BLOCKED_BY_LESS)]
    [HarmonyPatch(typeof(ShipJob_Unload))]
    [HarmonyPatch(nameof(ShipJob_Unload.UnloadThingFromShuttle))]
    public static class Patch_ShipJob_Unload
    {
        public static void Postfix(Thing thingToDrop)
        {
            if (Utility.CheckSetting(SpecialModSettings_Multipatch_Odyssey.SHUTTLE_BLOCKED_BY_LESS) && thingToDrop.Spawned && thingToDrop is Pawn pawn)
            {
                Building_Door door = pawn.Position.GetDoor(pawn.Map);
                if (door != null)
                {
                    door.StartManualOpenBy(pawn);
                }
            }
        }
    }
}
