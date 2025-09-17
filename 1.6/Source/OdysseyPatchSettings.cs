using RimWorld;
using UnityEngine;
using Verse;

namespace OdysseyPatch
{
    public class OdysseyPatchSettings : ModSettings
    {
        public static bool OutfitStandGroupsInBills = true;
        public static bool OutfitStandBodyType = true;
        public static bool FishingZoneCopy = true;
        public static bool AllowRemovingItemsFromOutfitStand = true;
        public static bool WorldSearchEmptyTiles = true;

        private static Vector2 scrollPosition;
        private static float y;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Rect viewRect = new Rect(0f, 0f, inRect.width - 20f, y);
            Widgets.BeginScrollView(inRect, ref scrollPosition, viewRect);

            Listing_Standard listing = new Listing_Standard() { maxOneColumn = true };
            listing.Begin(viewRect);

            DoHeader(listing, "OdysseyPatch_Misc");
            DoSetting(listing, "OdysseyPatch_OutfitStandGroupsInBills", ref OutfitStandGroupsInBills, true);
            DoSetting(listing, "OdysseyPatch_OutfitStandBodyType", ref OutfitStandBodyType);
            DoSetting(listing, "OdysseyPatch_FishingZoneCopy", ref FishingZoneCopy);
            DoSetting(listing, "OdysseyPatch_AllowRemovingItemsFromOutfitStand", ref AllowRemovingItemsFromOutfitStand);
            DoSetting(listing, "OdysseyPatch_WorldSearchEmptyTiles", ref WorldSearchEmptyTiles);

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

        private static void DoSetting(Listing_Standard listing, string key, ref bool setting, bool restartRequired = false)
        {
            listing.CheckboxLabeled(key.Translate() + (restartRequired ? " " + "OdysseyPatch_RestartRequired".Translate() : TaggedString.Empty), ref setting);
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref OutfitStandGroupsInBills, "OutfitStandGroupsInBills", true);
            Scribe_Values.Look(ref OutfitStandBodyType, "OutfitStandBodyType", true);
            Scribe_Values.Look(ref FishingZoneCopy, "FishingZoneCopy", true);
            Scribe_Values.Look(ref AllowRemovingItemsFromOutfitStand, "AllowRemovingItemsFromOutfitStand", true);
            Scribe_Values.Look(ref WorldSearchEmptyTiles, "WorldSearchEmptyTiles", true);
        }
    }
}
