using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace OdysseyPatch
{
    public class OdysseyPatchSettings : ModSettings
    {
        public const string OUTFIT_STAND_GROUPS_IN_BILLS = "OdysseyPatch_OutfitStandGroupsInBills";
        public const string OUTFIT_STAND_BODY_TYPE = "OdysseyPatch_OutfitStandBodyType";
        public const string FISHING_ZONE_COPY = "OdysseyPatch_FishingZoneCopy";
        public const string ALLOW_REMOVING_ITEMS_FROM_OUTFIT_STAND = "OdysseyPatch_AllowRemovingItemsFromOutfitStand";
        public const string WORLD_SEARCH_EMPTY_TILES = "OdysseyPatch_WorldSearchEmptyTiles";
        public const string SUBSTRUCTURE_OVERLAY_OPTIONS = "OdysseyPatch_SubstructureOverlayOptions";
        public const string FLOORS_BLOCKED_BY_HULLS = "OdysseyPatch_FloorsBlockedByHulls";
        public const string FISHING_INTERRUPTIONS = "OdysseyPatch_FishingInterruptions";
        public const string SILHOUETTES_HIDDEN_BY_GRAVSHIP_LANDING = "OdysseyPatch_SilhouettesHiddenByGravshipLanding";
        public const string ALLOW_REMOVING_ITEMS_FROM_OUTFIT_STAND_AFTER_EQUIPPING = "OdysseyPatch_AllowRemovingItemsFromOutfitStandAfterEquipping";
        public const string SHUTTLE_FOOD = "OdysseyPatch_ShuttleFood";
        public const string GRAVSHIP_CUTSCENE_OPTIONS = "OdysseyPatch_GravshipCutsceneOptions";
        public const string FLICK_SWITCHES_AFTER_LANDING = "OdysseyPatch_FlickSwitchesAfterLanding";
        public const string DEATHRESTING_PAWNS_TUCKED_IN_AFTER_LANDING = "OdysseyPatch_DeathrestingPawnsTuckedInAfterLanding";
        public const string BIOME_DANGER_WARNING_SUPPRESSED = "OdysseyPatch_BiomeDangerWarningSuppressed";
        public const string OUTFIT_STANDS_IGNORE_STORED_THINGS_BEAUTY = "OdysseyPatch_OutfitStandsIgnoreStoredThingsBeauty";
        public const string FISHING_MISHAPS_LESS_INTRUSIVE = "OdysseyPatch_FishingMishapsLessIntrusive";
        public const string SHUTTLE_SAVING_ERROR = "OdysseyPatch_ShuttleSavingError";
        public const string SHUTTLE_BLOCKED_BY_LESS = "OdysseyPatch_ShuttleBlockedByLess";
        public const string STATUES_DONT_HAVE_HEADGEAR = "OdysseyPatch_StatuesDontHaveHeadgear";
        public const string STATUE_CONSISTENCY = "OdysseyPatch_StatueConsistency";
        public const string FILTH_MULTIPLIER_FIX_FOR_SUBSTRUCTURE = "OdysseyPatch_FilthMultiplierFixForSubstructure";
        public const string SHOW_REMAINING_SUBSTRUCTURE_CAPACITY = "OdysseyPatch_ShowRemainingSubstructureCapacity";
        public const string GRAVSHIP_SHIELD_GENERATOR_RADIUS_DISPLAY = "OdysseyPatch_GravshipShieldGeneratorRadiusDisplay";
        public const string VACUUM_INTENSITY_ROOM_STAT = "OdysseyPatch_VacuumIntensityRoomStat";
        public const string GRAVSHIP_BUILD_RADIUS_SEPARATE = "OdysseyPatch_GravshipBuildRadiusSeparate";

        public static bool Check(string key)
        {
            if (OdysseyPatchMod.specialSauce)
            {
                return OdysseyPatchMod.settings.Get<bool>(key);
            }
            else
            {
                switch (key)
                {
                    case OUTFIT_STAND_GROUPS_IN_BILLS: return OutfitStandGroupsInBills;
                    case OUTFIT_STAND_BODY_TYPE: return OutfitStandBodyType;
                    case FISHING_ZONE_COPY: return FishingZoneCopy;
                    case ALLOW_REMOVING_ITEMS_FROM_OUTFIT_STAND: return AllowRemovingItemsFromOutfitStand;
                    case WORLD_SEARCH_EMPTY_TILES: return WorldSearchEmptyTiles;
                    case SUBSTRUCTURE_OVERLAY_OPTIONS: return SubstructureOverlayOptions;
                    case FLOORS_BLOCKED_BY_HULLS: return FloorsBlockedByHulls;
                    case FISHING_INTERRUPTIONS: return FishingInterruptions;
                    case SILHOUETTES_HIDDEN_BY_GRAVSHIP_LANDING: return SilhouettesHiddenByGravshipLanding;
                    case ALLOW_REMOVING_ITEMS_FROM_OUTFIT_STAND_AFTER_EQUIPPING: return AllowRemovingItemsFromOutfitStandAfterEquipping;
                    case SHUTTLE_FOOD: return ShuttleFood;
                    case GRAVSHIP_CUTSCENE_OPTIONS: return GravshipCutsceneOptions;
                    case FLICK_SWITCHES_AFTER_LANDING: return FlickSwitchesAfterLanding;
                    case DEATHRESTING_PAWNS_TUCKED_IN_AFTER_LANDING: return DeathrestingPawnsTuckedInAfterLanding;
                    case BIOME_DANGER_WARNING_SUPPRESSED: return BiomeDangerWarningSuppressed;
                    case OUTFIT_STANDS_IGNORE_STORED_THINGS_BEAUTY: return OutfitStandsIgnoreStoredThingsBeauty;
                    case FISHING_MISHAPS_LESS_INTRUSIVE: return FishingMishapsLessIntrusive;
                    case SHUTTLE_SAVING_ERROR: return ShuttleSavingError;
                    case SHUTTLE_BLOCKED_BY_LESS: return ShuttleBlockedByLess;
                    case STATUES_DONT_HAVE_HEADGEAR: return StatuesDontHaveHeadgear;
                    case STATUE_CONSISTENCY: return StatueConsistency;
                    case FILTH_MULTIPLIER_FIX_FOR_SUBSTRUCTURE: return FilthMultiplierFixForSubstructure;
                    case SHOW_REMAINING_SUBSTRUCTURE_CAPACITY: return ShowRemainingSubstructureCapacity;
                    case GRAVSHIP_SHIELD_GENERATOR_RADIUS_DISPLAY: return GravshipShieldGeneratorRadiusDisplay;
                    case VACUUM_INTENSITY_ROOM_STAT: return VacuumIntensityRoomStat;
                    case GRAVSHIP_BUILD_RADIUS_SEPARATE: return GravshipBuildRadiusSeparate;
                }
                throw new Exception("Invalid key for settings: " + key);
            }
        }

        private static bool OutfitStandGroupsInBills = true;
        private static bool OutfitStandBodyType = true;
        private static bool FishingZoneCopy = true;
        private static bool AllowRemovingItemsFromOutfitStand = true;
        private static bool WorldSearchEmptyTiles = true;
        private static bool SubstructureOverlayOptions = true;
        private static bool FloorsBlockedByHulls = true;
        private static bool FishingInterruptions = true;
        private static bool SilhouettesHiddenByGravshipLanding = true;
        private static bool AllowRemovingItemsFromOutfitStandAfterEquipping = true;
        private static bool ShuttleFood = true;
        private static bool GravshipCutsceneOptions = true;
        private static bool FlickSwitchesAfterLanding = true;
        private static bool DeathrestingPawnsTuckedInAfterLanding = true;
        private static bool BiomeDangerWarningSuppressed = true;
        private static bool OutfitStandsIgnoreStoredThingsBeauty = true;
        private static bool FishingMishapsLessIntrusive = true;
        private static bool ShuttleSavingError = true;
        private static bool ShuttleBlockedByLess = true;
        private static bool StatuesDontHaveHeadgear = true;
        private static bool StatueConsistency = true;
        private static bool FilthMultiplierFixForSubstructure = true;
        private static bool ShowRemainingSubstructureCapacity = true;
        private static bool GravshipShieldGeneratorRadiusDisplay = true;
        private static bool VacuumIntensityRoomStat = true;
        private static bool GravshipBuildRadiusSeparate = true;
        
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
            DoSetting(listing, "OdysseyPatch_SubstructureOverlayOptions", ref SubstructureOverlayOptions);
            DoSetting(listing, "OdysseyPatch_GravshipBuildRadiusSeparate", ref GravshipBuildRadiusSeparate);
            DoSetting(listing, "OdysseyPatch_ShowRemainingSubstructureCapacity", ref ShowRemainingSubstructureCapacity);
            DoSetting(listing, "OdysseyPatch_GravshipShieldGeneratorRadiusDisplay", ref GravshipShieldGeneratorRadiusDisplay);
            DoSetting(listing, "OdysseyPatch_ShuttleFood", ref ShuttleFood);
            DoSetting(listing, "OdysseyPatch_ShuttleBlockedByLess", ref ShuttleBlockedByLess);
            DoSetting(listing, "OdysseyPatch_VacuumIntensityRoomStat", ref VacuumIntensityRoomStat);
            DoSetting(listing, "OdysseyPatch_FloorsBlockedByHulls", ref FloorsBlockedByHulls, bugFix: true);
            DoSetting(listing, "OdysseyPatch_SilhouettesHiddenByGravshipLanding", ref SilhouettesHiddenByGravshipLanding, bugFix: true);
            DoSetting(listing, "OdysseyPatch_DeathrestingPawnsTuckedInAfterLanding", ref DeathrestingPawnsTuckedInAfterLanding, bugFix: true, dependsOn: ModsConfig.BiotechActive);
            DoSetting(listing, "OdysseyPatch_FlickSwitchesAfterLanding", ref FlickSwitchesAfterLanding, bugFix: true);
            DoSetting(listing, "OdysseyPatch_ShuttleSavingError", ref ShuttleSavingError, bugFix: true);
            DoSetting(listing, "OdysseyPatch_FilthMultiplierFixForSubstructure", ref FilthMultiplierFixForSubstructure, bugFix: true);
            
            listing.Gap();

            DoHeader(listing, "OdysseyPatch_Fishing");
            DoSetting(listing, "OdysseyPatch_FishingZoneCopy", ref FishingZoneCopy);
            DoSetting(listing, "OdysseyPatch_FishingInterruptions", ref FishingInterruptions);
            DoSetting(listing, "OdysseyPatch_FishingMishapsLessIntrusive", ref FishingMishapsLessIntrusive);

            listing.Gap();

            DoHeader(listing, "OdysseyPatch_OutfitStands");
            DoSetting(listing, "OdysseyPatch_OutfitStandBodyType", ref OutfitStandBodyType);
            DoSetting(listing, "OdysseyPatch_OutfitStandGroupsInBills", ref OutfitStandGroupsInBills, restartRequired: true);
            DoSetting(listing, "OdysseyPatch_AllowRemovingItemsFromOutfitStand", ref AllowRemovingItemsFromOutfitStand);
            DoSetting(listing, "OdysseyPatch_AllowRemovingItemsFromOutfitStandAfterEquipping", ref AllowRemovingItemsFromOutfitStandAfterEquipping);
            DoSetting(listing, "OdysseyPatch_OutfitStandsIgnoreStoredThingsBeauty", ref OutfitStandsIgnoreStoredThingsBeauty);
            DoSetting(listing, "OdysseyPatch_StatuesDontHaveHeadgear", ref StatuesDontHaveHeadgear);
            DoSetting(listing, "OdysseyPatch_StatueConsistency", ref StatueConsistency, bugFix: true, dependsOn: ModsConfig.BiotechActive);

            listing.Gap();

            DoHeader(listing, "OdysseyPatch_Misc");
            DoSetting(listing, "OdysseyPatch_WorldSearchEmptyTiles", ref WorldSearchEmptyTiles, restartRequired: true);
            DoSetting(listing, "OdysseyPatch_BiomeDangerWarningSuppressed", ref BiomeDangerWarningSuppressed);

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

        private static void DoSetting(Listing_Standard listing, string key, ref bool setting, bool restartRequired = false, bool bugFix = false, bool dependsOn = true, int indentLevel = 0)
        {
            if (dependsOn)
            {
                string indent = new string(' ', indentLevel * 2);
                listing.CheckboxLabeled(indent + (bugFix ? "OdysseyPatch_BugFix".Translate() + ": " : TaggedString.Empty) + key.Translate() + (restartRequired ? " " + "OdysseyPatch_RestartRequired".Translate() : TaggedString.Empty), ref setting);
            }
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
            Scribe_Values.Look(ref FlickSwitchesAfterLanding, "FlickSwitchesAfterLanding", true);
            Scribe_Values.Look(ref DeathrestingPawnsTuckedInAfterLanding, "DeathrestingPawnsTuckedInAfterLanding", true);
            Scribe_Values.Look(ref OutfitStandsIgnoreStoredThingsBeauty, "OutfitStandsIgnoreStoredThingsBeauty", true);
            Scribe_Values.Look(ref FishingMishapsLessIntrusive, "FishingMishapsLessIntrusive", true);
            Scribe_Values.Look(ref ShuttleSavingError, "ShuttleSavingError", true);
            Scribe_Values.Look(ref ShuttleBlockedByLess, "ShuttleBlockedByLess", true);
            Scribe_Values.Look(ref StatuesDontHaveHeadgear, "StatuesDontHaveHeadgear", true);
            Scribe_Values.Look(ref StatueConsistency, "StatueConsistency", true);
            Scribe_Values.Look(ref FilthMultiplierFixForSubstructure, "FilthMultiplierFixForSubstructure", true);
            Scribe_Values.Look(ref ShowRemainingSubstructureCapacity, "ShowRemainingSubstructureCapacity", true);
            Scribe_Values.Look(ref GravshipShieldGeneratorRadiusDisplay, "GravshipShieldGeneratorRadiusDisplay", true);
            Scribe_Values.Look(ref VacuumIntensityRoomStat, "VacuumIntensityRoomStat", true);
            Scribe_Values.Look(ref GravshipBuildRadiusSeparate, "GravshipBuildRadiusSeparate", true);
        }
    }
}
