using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace OdysseyPatch.WorldSearchEmptyTiles
{
    [HarmonyPatch(typeof(ExpandableLandmarksUtility))]
    [HarmonyPatch("get_LandmarksToShow")]
    public static class Patch_ExpandableLandmarksUtility_LandmarksToShow
    {
        public static List<PlanetTile> Postfix(List<PlanetTile> tiles)
        {
            if (OdysseyPatchSettings.WorldSearchEmptyTiles && Patch_Dialog_Search.fullSearch && Find.WindowStack.TryGetWindow(out Dialog_WorldSearch dialog))
            {
                tiles = tiles.ListFullCopy();
                HashSet<PlanetTile> listedTiles = typeof(Dialog_WorldSearch).Field("listedTiles").GetValue(dialog) as HashSet<PlanetTile>;
                foreach (PlanetTile tile in listedTiles)
                {
                    if (!tiles.Contains(tile) && !Find.WorldObjects.AnyWorldObjectAt(tile))
                    {
                        tiles.Add(tile);
                    }
                }
            }
            return tiles;
        }
    }

    [HarmonyPatch(typeof(ExpandableLandmarksUtility))]
    [HarmonyPatch(nameof(ExpandableLandmarksUtility.ExpandableLandmarksOnGUI))]
    public static class Patch_ExpandableLandmarksUtility_ExpandableLandmarksOnGUI
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.opcode == OpCodes.Call && i.operand is MethodInfo m && m == typeof(Color).PropertyGetter(nameof(Color.white)));
            Label label = instructionsList[index].labels[0];
            instructionsList.RemoveAt(index);
            instructionsList.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_1) { labels = new List<Label>() { label } },
                new CodeInstruction(OpCodes.Call, typeof(Patch_ExpandableLandmarksUtility_ExpandableLandmarksOnGUI).Method(nameof(GetLandmarkColor)))
            });
            return instructionsList;
        }

        private static Color GetLandmarkColor(PlanetTile tile)
        {
            if (OdysseyPatchSettings.WorldSearchEmptyTiles && Patch_Dialog_Search.fullSearch)
            {
                List<PlanetTile> cachedLandmarks = typeof(ExpandableLandmarksUtility).Field("cachedLandmarksToShow").GetValue(null) as List<PlanetTile>;
                if (!cachedLandmarks.Contains(tile))
                {
                    return new Color(0f, 1f, 0.3f);
                }
            }
            return Color.white;
        }
    }
}
