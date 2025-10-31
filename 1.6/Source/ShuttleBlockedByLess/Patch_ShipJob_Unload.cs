using HarmonyLib;
using RimWorld;
using Verse;

namespace OdysseyPatch.ShuttleBlockedByLess
{
    [HarmonyPatch(typeof(ShipJob_Unload))]
    [HarmonyPatch(nameof(ShipJob_Unload.UnloadThingFromShuttle))]
    public static class Patch_ShipJob_Unload
    {
        public static void Postfix(Thing thingToDrop)
        {
            if (OdysseyPatchSettings.ShuttleBlockedByLess && thingToDrop.Spawned && thingToDrop is Pawn pawn)
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
