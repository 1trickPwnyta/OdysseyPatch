using RimWorld;
using Verse;

namespace OdysseyPatch.ShuttleFood
{
    public class Comp_GrabbableSupplies : ThingComp
    {
        public bool enabled = true;

        public override void PostExposeData()
        {
            Scribe_Values.Look(ref enabled, "enabled", true);
        }
    }

    public static class GrabbableSuppliesUtility
    {
        public static Comp_GrabbableSupplies GetGrabbableSupplies(this Building_PassengerShuttle shuttle) => shuttle.GetComp<Comp_GrabbableSupplies>();
    }
}
