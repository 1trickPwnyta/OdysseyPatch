using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace OdysseyPatch.LavaPathAvoidance
{
    public class PotentialDangerSource : SimpleBoolPathFinderDataSource
    {
        public PotentialDangerSource(Map map) : base(map)
        {
        }

        public override void ComputeAll(IEnumerable<PathRequest> requests)
        {
            data.Clear();
            for (int i = 0; i < cellCount; i++)
            {
                IntVec3 cell = map.cellIndices.IndexToCell(i);
                data.Set(i, IsPotentiallyDangerous(cell));
            }
        }

        public override bool UpdateIncrementally(IEnumerable<PathRequest> requests, List<IntVec3> cellDeltas)
        {
            foreach (IntVec3 cell in cellDeltas)
            {
                foreach (IntVec3 nearbyCell in GenRadial.RadialCellsAround(cell, 3f, false).Where(c => c.InBounds(map)))
                {
                    int i = map.cellIndices.CellToIndex(nearbyCell);
                    data.Set(i, IsPotentiallyDangerous(nearbyCell));
                }
            }

            return false;
        }

        private bool IsPotentiallyDangerous(IntVec3 cell)
        {
            return GenRadial.RadialCellsAround(cell, 3f, false).Any(c => c.InBounds(map) && map.terrainGrid.TempTerrainAt(c) == TerrainDefOf.LavaShallow);
        }
    }
}
