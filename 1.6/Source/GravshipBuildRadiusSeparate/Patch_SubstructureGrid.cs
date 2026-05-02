using HarmonyLib;
using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace OdysseyPatch.GravshipBuildRadiusSeparate
{
    [HarmonyPatch(typeof(SubstructureGrid))]
    [HarmonyPatch(nameof(SubstructureGrid.DrawSubstructureFootprint))]
    public static class Patch_SubstructureGrid
    {
        private static int lastUpdateFrame = -1;

        public static void Postfix()
        {
            if (OdysseyPatchSettings.GravshipBuildRadiusSeparate)
            {
                if (Time.frameCount == lastUpdateFrame)
                {
                    return;
                }
                lastUpdateFrame = Time.frameCount;

                if (Find.DesignatorManager.SelectedDesignator is Designator_Place designator)
                {
                    CompProperties_SubstructureFootprint props = (designator.PlacingDef as ThingDef)?.comps?.OfType<CompProperties_SubstructureFootprint>().FirstOrDefault();
                    if (props != null)
                    {
                        GenDraw.DrawFieldEdges(GenRadial.RadialCellsAround(UI.MouseCell(), props.radius, true).ToList(), Color.white);
                    }
                }
                foreach (Building building in Find.Selector.SelectedObjects.OfType<Building>())
                {
                    CompSubstructureFootprint comp = building.TryGetComp<CompSubstructureFootprint>();
                    if (comp != null)
                    {
                        GenDraw.DrawFieldEdges(GenRadial.RadialCellsAround(building.Position, building.GetComp<CompSubstructureFootprint>().Props.radius, true).ToList(), Color.white);
                    }
                }
            }
        }
    }
}
