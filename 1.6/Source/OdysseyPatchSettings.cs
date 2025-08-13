using UnityEngine;
using Verse;

namespace OdysseyPatch
{
    public class OdysseyPatchSettings : ModSettings
    {
        public static bool LavaPathAvoidance = true;

        private static Vector2 scrollPosition;
        private static float y;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Rect viewRect = new Rect(0f, 0f, inRect.width - 20f, y);
            Widgets.BeginScrollView(inRect, ref scrollPosition, viewRect);

            Listing_Standard listing = new Listing_Standard() { maxOneColumn = true };
            listing.Begin(viewRect);

            DoHeader(listing, "OdysseyPatch_Misc");
            listing.CheckboxLabeled("OdysseyPatch_LavaPathAvoidance".Translate(), ref LavaPathAvoidance);

            listing.Gap();

            DoHeader(listing, "OdysseyPatch_Misc");
            

            y = listing.CurHeight;
            listing.End();

            Widgets.EndScrollView();
        }

        private static void DoHeader(Listing_Standard listing, string key)
        {
            using (new TextBlock(GameFont.Medium))
            {
                listing.Label(key.Translate());
            }
            listing.GapLine();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref LavaPathAvoidance, "LavaPathAvoidance", true);
        }
    }
}
