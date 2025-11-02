using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace OdysseyPatch.ShuttleFood
{
    public class Comp_GrabbableSupplies : ThingComp
    {
        public bool enabled = true;

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (OdysseyPatchSettings.ShuttleFood)
            {
                yield return new Command_Toggle()
                {
                    defaultLabel = "OdysseyPatch_AllowGrabbingSupplies".Translate(),
                    defaultDesc = "OdysseyPatch_AllowGrabbingSuppliesDesc".Translate(),
                    icon = TexCommand.ForbidOff,
                    isActive = () => enabled,
                    toggleAction = () =>
                    {
                        enabled = !enabled;
                    }
                };
            }
        }

        public override void PostExposeData()
        {
            Scribe_Values.Look(ref enabled, "enabled", true);
        }
    }

    public static class GrabbableSuppliesUtility
    {
        public static Comp_GrabbableSupplies GetGrabbableSupplies(this Building_PassengerShuttle shuttle) => shuttle.GetComp<Comp_GrabbableSupplies>();

        public static IEnumerable<Building_PassengerShuttle> GetShuttlesWithGrabbingEnabled(this Map map) => map.listerBuildings.AllBuildingsColonistOfClass<Building_PassengerShuttle>().Where(s => s.GetGrabbableSupplies()?.enabled == true);
    }
}
