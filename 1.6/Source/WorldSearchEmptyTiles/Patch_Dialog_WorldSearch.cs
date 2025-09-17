using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace OdysseyPatch.WorldSearchEmptyTiles
{
    [HarmonyPatch(typeof(Dialog_WorldSearch))]
    [HarmonyPatch("InitializeSearchSet")]
    public static class Patch_Dialog_WorldSearch
    {
        public static void Postfix(List<WorldSearchElement> ___searchSet)
        {
            if (OdysseyPatchSettings.WorldSearchEmptyTiles)
            {
                foreach (int i in Find.World.tilesInRandomOrder.Tiles)
                {
                    SurfaceTile tile = Find.World.grid[i];
                    if (!___searchSet.Any(s => s.tile == tile.tile) && tile.Mutators.Any())
                    {
                        ___searchSet.Add(new WorldSearchElement
                        {
                            tile = tile.tile,
                            mutators = tile.mutatorsNullable
                        });
                    }
                }
            }
        }
    }
}
