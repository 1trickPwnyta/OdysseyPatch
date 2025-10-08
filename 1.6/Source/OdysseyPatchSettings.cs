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
        public static bool SubstructureOverlayOptions = true;
        public static bool FloorsBlockedByHulls = true;
        public static bool FishingInterruptions = true;
        public static bool SilhouettesHiddenByGravshipLanding = true;
        public static bool AllowRemovingItemsFromOutfitStandAfterEquipping = true;
        public static bool ShuttleFood = true;
        public static bool GravshipCutsceneOptions = true;

        private static Vector2 scrollPosition;
        private static float y;
        
        public static void DoSettingsWindowContents(Rect inRect)
        {
            Rect viewRect = new Rect(0f, 0f, inRect.width - 20f, y);
            Widgets.BeginScrollView(inRect, ref scrollPosition, viewRect);

            Listing_Standard listing = new Listing_Standard() { maxOneColumn = true };
            listing.Begin(viewRect);

            DoHeader(listing, "OdysseyPatch_SpaceTravel");
            DoSetting(listing, "OdysseyPatch_GravshipCutsceneOptions", ref GravshipCutsceneOptions);
            DoSetting(listing, "OdysseyPatch_ShuttleFood", ref ShuttleFood);
            DoSetting(listing, "OdysseyPatch_SubstructureOverlayOptions", ref SubstructureOverlayOptions);
            DoSetting(listing, "OdysseyPatch_FloorsBlockedByHulls", ref FloorsBlockedByHulls, bugFix: true);
            DoSetting(listing, "OdysseyPatch_SilhouettesHiddenByGravshipLanding", ref SilhouettesHiddenByGravshipLanding, bugFix: true);

            listing.Gap();

            DoHeader(listing, "OdysseyPatch_Fishing");
            DoSetting(listing, "OdysseyPatch_FishingZoneCopy", ref FishingZoneCopy);
            DoSetting(listing, "OdysseyPatch_FishingInterruptions", ref FishingInterruptions);

            listing.Gap();

            DoHeader(listing, "OdysseyPatch_OutfitStands");
            DoSetting(listing, "OdysseyPatch_OutfitStandBodyType", ref OutfitStandBodyType);
            DoSetting(listing, "OdysseyPatch_OutfitStandGroupsInBills", ref OutfitStandGroupsInBills, restartRequired: true);
            DoSetting(listing, "OdysseyPatch_AllowRemovingItemsFromOutfitStand", ref AllowRemovingItemsFromOutfitStand);
            DoSetting(listing, "OdysseyPatch_AllowRemovingItemsFromOutfitStandAfterEquipping", ref AllowRemovingItemsFromOutfitStandAfterEquipping);

            listing.Gap();

            DoHeader(listing, "OdysseyPatch_Misc");
            DoSetting(listing, "OdysseyPatch_WorldSearchEmptyTiles", ref WorldSearchEmptyTiles);

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

        private static void DoSetting(Listing_Standard listing, string key, ref bool setting, bool restartRequired = false, bool bugFix = false)
        {
            listing.CheckboxLabeled((bugFix ? "OdysseyPatch_BugFix".Translate() + ": " : TaggedString.Empty) + key.Translate() + (restartRequired ? " " + "OdysseyPatch_RestartRequired".Translate() : TaggedString.Empty), ref setting);
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref OutfitStandGroupsInBills, "OutfitStandGroupsInBills", true);
            Scribe_Values.Look(ref OutfitStandBodyType, "OutfitStandBodyType", true);
            Scribe_Values.Look(ref FishingZoneCopy, "FishingZoneCopy", true);
            Scribe_Values.Look(ref AllowRemovingItemsFromOutfitStand, "AllowRemovingItemsFromOutfitStand", true);
            Scribe_Values.Look(ref WorldSearchEmptyTiles, "WorldSearchEmptyTiles", true);
            Scribe_Values.Look(ref SubstructureOverlayOptions, "SubstructureOverlayOptions", true);
            Scribe_Values.Look(ref FloorsBlockedByHulls, "FloorsBlockedByHulls", true);
            Scribe_Values.Look(ref FishingInterruptions, "FishingInterruptions", true);
            Scribe_Values.Look(ref SilhouettesHiddenByGravshipLanding, "SilhouettesHiddenByGravshipLanding", true);
            Scribe_Values.Look(ref AllowRemovingItemsFromOutfitStandAfterEquipping, "AllowRemovingItemsFromOutfitStandAfterEquipping", true);
            Scribe_Values.Look(ref ShuttleFood, "ShuttleFood", true);
            Scribe_Values.Look(ref GravshipCutsceneOptions, "GravshipCutsceneOptions", true);
        }
    }
}
