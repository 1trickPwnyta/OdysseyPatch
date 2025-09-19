using HarmonyLib;
using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace OdysseyPatch.WorldSearchEmptyTiles
{
    [HarmonyPatch(typeof(Dialog_Search<WorldSearchElement>))]
    [HarmonyPatch(nameof(Dialog_Search<WorldSearchElement>.DoWindowContents))]
    public static class Patch_Dialog_Search
    {
        public static bool fullSearch = false;

        public static void Postfix(Dialog_Search<WorldSearchElement> __instance, Rect inRect)
        {
            if (OdysseyPatchSettings.WorldSearchEmptyTiles && __instance is Dialog_WorldSearch)
            {
                bool previousFullSearch = fullSearch;
                float height = Text.CalcHeight("OdysseyPatch_FullSearch".Translate(), inRect.width);
                using (new TextBlock(TextAnchor.UpperRight)) Widgets.CheckboxLabeled(new Rect(inRect.width - 100f, inRect.yMax - 24f - height, 100f, height), "OdysseyPatch_FullSearch".Translate(), ref fullSearch);
                if (fullSearch != previousFullSearch)
                {
                    typeof(Dialog_WorldSearch).Method("InitializeSearchSet").Invoke(__instance, new object[] { });
                }
            }
        }
    }
}
