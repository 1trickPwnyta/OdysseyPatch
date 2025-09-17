using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace OdysseyPatch.FishingZoneCopy
{
    [HarmonyPatch(typeof(Zone_Fishing))]
    [HarmonyPatch(nameof(Zone_Fishing.GetGizmos))]
    public static class Patch_Zone_Fishing
    {
        private static Zone_Fishing clipboard;

        public static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> gizmos, Zone_Fishing __instance)
        {
            foreach (Gizmo gizmo in gizmos)
            {
                yield return gizmo;
            }

            if (OdysseyPatchSettings.FishingZoneCopy)
            {
                Command_Action copyAction = new Command_Action();
                copyAction.icon = ContentFinder<Texture2D>.Get("UI/Commands/CopySettings");
                copyAction.defaultLabel = "CommandCopyZoneSettingsLabel".Translate();
                copyAction.defaultDesc = "OdysseyPatch_FishingZoneCopyDesc".Translate();
                copyAction.action = () =>
                {
                    SoundDefOf.Tick_High.PlayOneShotOnCamera();
                    clipboard = __instance;
                };
                copyAction.hotKey = KeyBindingDefOf.Misc4;
                yield return copyAction;

                Command_Action pasteAction = new Command_Action();
                pasteAction.icon = ContentFinder<Texture2D>.Get("UI/Commands/PasteSettings");
                pasteAction.defaultLabel = "CommandPasteZoneSettingsLabel".Translate();
                pasteAction.defaultDesc = "OdysseyPatch_FishingZonePasteDesc".Translate();
                pasteAction.action = () =>
                {
                    SoundDefOf.Tick_Low.PlayOneShotOnCamera();
                    __instance.repeatCount = clipboard.repeatCount;
                    __instance.targetCount = clipboard.targetCount;
                    if (clipboard.Map != null && Find.Maps.Contains(clipboard.Map) && clipboard.Cells?.Count > 0 && clipboard.Cells[0].GetWaterBody(clipboard.Map) != null)
                    {
                        float maxPop = __instance.Cells[0].GetWaterBody(__instance.Map).MaxPopulation;
                        __instance.targetPopulationPct = Mathf.Clamp(clipboard.targetPopulationPct * clipboard.Cells[0].GetWaterBody(clipboard.Map).MaxPopulation / maxPop, 10f / maxPop, 1f);
                    }
                    __instance.repeatMode = clipboard.repeatMode;
                    __instance.pauseWhenSatisfied = clipboard.pauseWhenSatisfied;
                    __instance.unpauseAtCount = clipboard.unpauseAtCount;

                };
                pasteAction.hotKey = KeyBindingDefOf.Misc5;
                if (clipboard == null)
                {
                    pasteAction.Disable();
                }
                yield return pasteAction;
            }
        }
    }
}
