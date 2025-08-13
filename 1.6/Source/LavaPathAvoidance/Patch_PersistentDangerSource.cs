using HarmonyLib;
using System.Collections.Generic;
using Unity.Collections;
using Verse;

namespace OdysseyPatch.LavaPathAvoidance
{
    [HarmonyPatch(typeof(PersistentDangerSource))]
    [HarmonyPatch(nameof(PersistentDangerSource.ComputeAll))]
    public static class Patch_PersistentDangerSource_ComputeAll
    {
        public static void Postfix(Map ___map, int ___cellCount, ref NativeBitArray ___data)
        {
            if (OdysseyPatchSettings.LavaPathAvoidance)
            {
                for (int i = 0; i < ___cellCount; i++)
                {
                    IntVec3 cell = ___map.cellIndices.IndexToCell(i);
                    if (___map.terrainGrid.TempTerrainAt(i)?.dangerous == true)
                    {
                        ___data.Set(i, true);
                    }
                    if (___data.IsSet(i))
                    {
                        LavaPathAvoidanceUtility.SetCellBecameDangerous(___map, cell);
                    }
                    else
                    {
                        LavaPathAvoidanceUtility.UnsetCellBecameDangerous(___map, cell);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(PersistentDangerSource))]
    [HarmonyPatch(nameof(PersistentDangerSource.UpdateIncrementally))]
    public static class Patch_PersistentDangerSource_UpdateIncrementally
    {
        public static void Postfix(Map ___map, ref NativeBitArray ___data, List<IntVec3> cellDeltas)
        {
            if (OdysseyPatchSettings.LavaPathAvoidance)
            {
                foreach (IntVec3 cell in cellDeltas)
                {
                    int index = ___map.cellIndices.CellToIndex(cell);
                    if (___map.terrainGrid.TempTerrainAt(cell)?.dangerous == true)
                    {
                        ___data.Set(index, true);
                    }
                    if (___data.IsSet(index))
                    {
                        LavaPathAvoidanceUtility.SetCellBecameDangerous(___map, cell);
                    }
                    else
                    {
                        LavaPathAvoidanceUtility.UnsetCellBecameDangerous(___map, cell);
                    }
                }
            }
        }
    }
}
