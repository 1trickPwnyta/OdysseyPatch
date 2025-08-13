using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace OdysseyPatch.LavaPathAvoidance
{
    public static class LavaPathAvoidanceUtility
    {
        private static HashSet<Pawn> pawnsWithDirtyPathing = new HashSet<Pawn>();
        private static Dictionary<Map, Dictionary<IntVec3, int>> cellBecameDangerousAt = new Dictionary<Map, Dictionary<IntVec3, int>>();
        private static Dictionary<Pawn, int> pawnStartedMovingAt = new Dictionary<Pawn, int>();

        public static bool HasDirtyPath(Pawn pawn) => pawnsWithDirtyPathing.Contains(pawn);

        public static void SetPathDirty(Pawn pawn, bool dirty)
        {
            if (dirty)
            {
                pawnsWithDirtyPathing.Add(pawn);
            }
            else
            {
                pawnsWithDirtyPathing.Remove(pawn);
            }
        }

        public static void SetCellBecameDangerous(Map map, IntVec3 cell)
        {
            if (!cellBecameDangerousAt.ContainsKey(map))
            {
                cellBecameDangerousAt[map] = new Dictionary<IntVec3, int>();
            }
            if (!cellBecameDangerousAt[map].ContainsKey(cell))
            {
                cellBecameDangerousAt[map][cell] = GenTicks.TicksGame;
            }
        }

        public static void UnsetCellBecameDangerous(Map map, IntVec3 cell)
        {
            if (!cellBecameDangerousAt.ContainsKey(map))
            {
                cellBecameDangerousAt[map] = new Dictionary<IntVec3, int>();
            }
            cellBecameDangerousAt[map].Remove(cell);
        }

        public static void SetPawnStartedMoving(Pawn pawn)
        {
            pawnStartedMovingAt[pawn] = GenTicks.TicksGame;
        }

        public static void RepathIfNecessary(Pawn pawn)
        {
            if (pawn.Map != null && pawn.pather != null)
            {
                IntVec3 destination = pawn.pather.Destination.Cell;
                if (IsDangerousCell(destination, pawn.Map))
                {
                    if (pawnStartedMovingAt[pawn] < cellBecameDangerousAt[pawn.Map][destination])
                    {
                        pawn.mindState.priorityWork.ClearPrioritizedWorkAndJobQueue();
                        pawn.jobs.StopAll();
                    }
                }
                else if (pawn.pather.curPath?.PeekNextCells(20).Any(c => IsPotentiallyDangerousCell(c, pawn.Map)) == true)
                {
                    pawn.pather.DisposeAndClearCurPath();
                }
            }
            pawnsWithDirtyPathing.Remove(pawn);
        }

        public static bool IsDangerousCell(IntVec3 cell, Map map) => cellBecameDangerousAt.TryGetValue(map)?.ContainsKey(cell) ?? false;

        private static bool IsPotentiallyDangerousCell(IntVec3 cell, Map map)
        {
            if (IsDangerousCell(cell, map))
            {
                return true;
            }
            else
            {
                int i = map.cellIndices.CellToIndex(cell);
                List<IPathFinderDataSource> sources = typeof(PathFinderMapData).Field("sources").GetValue(map.pathFinder.MapData) as List<IPathFinderDataSource>;
                PotentialDangerSource source = sources.OfType<PotentialDangerSource>().First();
                return source.Data.IsSet(i);
            }
        }

        public static void ClearCache()
        {
            pawnsWithDirtyPathing.Clear();
            cellBecameDangerousAt.Clear();
            pawnStartedMovingAt.Clear();
        }
    }
}
