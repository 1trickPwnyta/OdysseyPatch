using HarmonyLib;
using System.Linq;
using Verse;

namespace OdysseyPatch.LavaPathAvoidance
{
    [HarmonyPatch(typeof(TerrainGrid))]
    [HarmonyPatch(nameof(TerrainGrid.SetTempTerrain))]
    public static class Patch_TerrainGrid
    {
        public static void Postfix(Map ___map, TerrainDef[] ___tempGrid, IntVec3 c)
        {
            if (OdysseyPatchSettings.LavaPathAvoidance)
            {
                int index = ___map.cellIndices.CellToIndex(c);
                if (___tempGrid[index]?.dangerous == true)
                {
                    foreach (Pawn pawn in ___map.mapPawns.AllPawns.Where(p => p.pather?.MovingNow == true).ToList())
                    {
                        if (pawn.Position.IsValid && pawn.Position.InHorDistOf(c, 20f))
                        {
                            LavaPathAvoidanceUtility.RepathIfNecessary(pawn);
                        }
                        else
                        {
                            LavaPathAvoidanceUtility.SetPathDirty(pawn, true);
                        }
                    }
                }
            }
        }
    }
}
