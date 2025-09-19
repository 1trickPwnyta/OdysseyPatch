using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace OdysseyPatch.SubstructureOverlayOptions
{
    [HarmonyPatch(typeof(CompSubstructureFootprint))]
    [HarmonyPatch(nameof(CompSubstructureFootprint.CompGetGizmosExtra))]
    public static class Patch_CompSubstructureFootprint_CompGetGizmosExtra
    {
        public static HashSet<CompSubstructureFootprint> alwaysEnabled = new HashSet<CompSubstructureFootprint>();

        public static void Postfix(CompSubstructureFootprint __instance, ref IEnumerable<Gizmo> __result)
        {
            if (OdysseyPatchSettings.SubstructureOverlayOptions)
            {
                List<Gizmo> list = __result.ToList();
                Gizmo gizmo = list.FirstOrDefault(g => g is Command_Toggle c && c.defaultLabel.Equals("CommandShowSubstructureOverlay".Translate()));
                if (gizmo != null)
                {
                    list.Remove(gizmo);
                    Command_Toggle oldGizmo = gizmo as Command_Toggle;
                    list.Add(new Command_Dropdown(() => alwaysEnabled.Contains(__instance) ? Widgets.CheckboxOnTex : (bool)typeof(CompSubstructureFootprint).Field("displaySubstructureOverlay").GetValue(__instance) ? Widgets.CheckboxPartialTex : Widgets.CheckboxOffTex)
                    {
                        defaultLabel = oldGizmo.defaultLabel,
                        defaultDesc = oldGizmo.defaultDesc,
                        icon = oldGizmo.icon,
                        action = () =>
                        {
                            Find.WindowStack.Add(new FloatMenu(new List<FloatMenuOption>()
                            {
                                new FloatMenuOption("OdysseyPatch_Always".Translate(), () => alwaysEnabled.Add(__instance), Widgets.CheckboxOnTex, Color.white),
                                new FloatMenuOption("OdysseyPatch_WhenSelected".Translate(), () =>
                                {
                                    alwaysEnabled.Remove(__instance);
                                    typeof(CompSubstructureFootprint).Field("displaySubstructureOverlay").SetValue(__instance, true);
                                }, Widgets.CheckboxPartialTex, Color.white),
                                new FloatMenuOption("OdysseyPatch_Never".Translate(), () =>
                                {
                                    alwaysEnabled.Remove(__instance);
                                    typeof(CompSubstructureFootprint).Field("displaySubstructureOverlay").SetValue(__instance, false);
                                }, Widgets.CheckboxOffTex, Color.white)
                            }));
                        }
                    });
                    __result = list;
                }
            }
        }
    }

    [HarmonyPatch(typeof(CompSubstructureFootprint))]
    [HarmonyPatch(nameof(CompSubstructureFootprint.PostExposeData))]
    public static class Patch_CompSubstructureFootprint_PostExposeData
    {
        public static void Postfix(CompSubstructureFootprint __instance)
        {
            if (OdysseyPatchSettings.SubstructureOverlayOptions)
            {
                bool alwaysEnabled = Patch_CompSubstructureFootprint_CompGetGizmosExtra.alwaysEnabled.Contains(__instance);
                Scribe_Values.Look(ref alwaysEnabled, "alwaysEnabled", false);
                if (Scribe.mode == LoadSaveMode.LoadingVars)
                {
                    if (alwaysEnabled) Patch_CompSubstructureFootprint_CompGetGizmosExtra.alwaysEnabled.Add(__instance);
                    else Patch_CompSubstructureFootprint_CompGetGizmosExtra.alwaysEnabled.Remove(__instance);
                }
            }
        }
    }
}
