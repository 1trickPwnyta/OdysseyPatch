using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace OdysseyPatch.StatuesDontHaveHeadgear
{
    public class Comp_StatueHeadgear : ThingComp
    {
        private ThingDef headgear;
        public bool showHeadgear = !Prefs.HatsOnlyOnMap;

        public CompStatue Statue => parent.GetComp<CompStatue>();

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (OdysseyPatchSettings.StatuesDontHaveHeadgear)
            {
                if (headgear == null)
                {
                    headgear = (typeof(CompStatue).Field("apparel").GetValue(Statue) as List<ThingDef>).FirstOrDefault(t => PawnApparelGenerator.IsHeadgear(t));
                }

                if (headgear != null)
                {
                    yield return new Command_Toggle()
                    {
                        defaultLabel = "OdysseyPatch_ShowHeadgear".Translate(),
                        icon = headgear.graphic.MatSouth.mainTexture,
                        defaultIconColor = parent.def.GetColorForStuff(parent.Stuff),
                        isActive = () => showHeadgear,
                        toggleAction = () =>
                        {
                            showHeadgear = !showHeadgear;
                            typeof(CompStatue).Method("InitFakePawn").Invoke(Statue, new object[] { });
                        }
                    };
                }
            }
        }

        public override void PostExposeData()
        {
            Scribe_Values.Look(ref showHeadgear, "showHeadgear", !Prefs.HatsOnlyOnMap);
        }
    }
}
