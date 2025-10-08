using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace OdysseyPatch.ShuttleFood
{
    [HarmonyPatch(typeof(Building_PassengerShuttle))]
    [HarmonyPatch(nameof(Building_PassengerShuttle.GetGizmos))]
    public static class Patch_Building_PassengerShuttle
    {
        public static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> gizmos, Building_PassengerShuttle __instance)
        {
            foreach (Gizmo gizmo in gizmos)
            {
                yield return gizmo;
            }
            if (OdysseyPatchSettings.ShuttleFood)
            {
                Comp_GrabbableSupplies comp = __instance.GetGrabbableSupplies();
                if (comp != null)
                {
                    yield return new Command_Toggle()
                    {
                        defaultLabel = "OdysseyPatch_AllowGrabbingSupplies".Translate(),
                        defaultDesc = "OdysseyPatch_AllowGrabbingSuppliesDesc".Translate(),
                        icon = TexCommand.ForbidOff,
                        isActive = () => comp.enabled,
                        toggleAction = () =>
                        {
                            comp.enabled = !comp.enabled;
                        }
                    };
                }
            }
        }
    }
}
