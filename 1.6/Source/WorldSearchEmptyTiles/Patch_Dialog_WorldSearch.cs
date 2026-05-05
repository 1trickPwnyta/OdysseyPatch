using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using SpecialSauce.ModSettings;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace OdysseyPatch.WorldSearchEmptyTiles
{
    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.WORLD_SEARCH_EMPTY_TILES)]
    [HarmonyPatch(typeof(Dialog_WorldSearch))]
    [HarmonyPatch(MethodType.Constructor)]
    public static class Patch_Dialog_WorldSearch_ctor
    {
        public static void Prefix(List<WorldSearchElement> ___searchSet)
        {
            Patch_Dialog_Search.fullSearch = false;
        }
    }

    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.WORLD_SEARCH_EMPTY_TILES)]
    [HarmonyPatch(typeof(Dialog_WorldSearch))]
    [HarmonyPatch("InitializeSearchSet")]
    public static class Patch_Dialog_WorldSearch_InitializeSearchSet
    {
        public static void Postfix(Dialog_WorldSearch __instance, List<WorldSearchElement> ___searchSet)
        {
            if (Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.WORLD_SEARCH_EMPTY_TILES))
            {
                if (Patch_Dialog_Search.fullSearch)
                {
                    LongEventHandler.QueueLongEvent(() =>
                    {
                        foreach (int i in Find.World.tilesInRandomOrder.Tiles)
                        {
                            SurfaceTile tile = Find.World.grid[i];
                            bool hasWorldObject = Find.WorldObjects.AnyWorldObjectAt(i);
                            bool hasLandmark = Find.World.landmarks.landmarks.TryGetValue(tile.tile) != null;
                            if (!hasWorldObject && !hasLandmark && tile.Mutators.Any())
                            {
                                ___searchSet.Add(new WorldSearchElement
                                {
                                    tile = tile.tile,
                                    mutators = tile.mutatorsNullable
                                });
                            }
                        }
                    }, "OdysseyPatch_InitializingSearchSet", false, e => throw e, callback: __instance.Notify_CommonSearchChanged);
                }
                else
                {
                    __instance.Notify_CommonSearchChanged();
                }
            }
        }
    }
}
