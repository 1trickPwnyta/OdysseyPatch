using SpecialSauce.ModSettings;
using System.Collections.Generic;
using Verse;

namespace OdysseyPatch
{
    public class ModSettings_DLCPatch_Odyssey : ModSettings_DLCPatch
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

        SettingsCategory[] categories = new[]
        {
            new SettingsCategory()
            {
                labelKey = "OdysseyPatch_SpaceTravel",
                settings = new[]
                {
                    new DLCSetting(GRAVSHIP_CUTSCENE_OPTIONS),
                    new DLCSetting(SUBSTRUCTURE_OVERLAY_OPTIONS),
                    new DLCSetting(GRAVSHIP_BUILD_RADIUS_SEPARATE),
                    new DLCSetting(SHOW_REMAINING_SUBSTRUCTURE_CAPACITY),
                    new DLCSetting(GRAVSHIP_SHIELD_GENERATOR_RADIUS_DISPLAY),
                    new DLCSetting(SHUTTLE_FOOD),
                    new DLCSetting(SHUTTLE_BLOCKED_BY_LESS),
                    new DLCSetting(VACUUM_INTENSITY_ROOM_STAT),
                    new DLCSetting(FLOORS_BLOCKED_BY_HULLS) { bugFix = true },
                    new DLCSetting(SILHOUETTES_HIDDEN_BY_GRAVSHIP_LANDING) { bugFix = true },
                    new DLCSetting(DEATHRESTING_PAWNS_TUCKED_IN_AFTER_LANDING) { visibilityGetter = () => ModsConfig.BiotechActive, bugFix = true },
                    new DLCSetting(FLICK_SWITCHES_AFTER_LANDING) { bugFix = true },
                    new DLCSetting(SHUTTLE_SAVING_ERROR) { bugFix = true },
                    new DLCSetting(FILTH_MULTIPLIER_FIX_FOR_SUBSTRUCTURE) { bugFix = true }
                }
            },
            new SettingsCategory()
            {
                labelKey = "OdysseyPatch_Fishing",
                settings = new[]
                {
                    new DLCSetting(FISHING_ZONE_COPY),
                    new DLCSetting(FISHING_INTERRUPTIONS),
                    new DLCSetting(FISHING_MISHAPS_LESS_INTRUSIVE)
                }
            },
            new SettingsCategory()
            {
                labelKey = "OdysseyPatch_OutfitStands",
                settings = new[]
                {
                    new DLCSetting(OUTFIT_STAND_BODY_TYPE),
                    new DLCSetting(OUTFIT_STAND_GROUPS_IN_BILLS) { restartRequired = true },
                    new DLCSetting(ALLOW_REMOVING_ITEMS_FROM_OUTFIT_STAND),
                    new DLCSetting(ALLOW_REMOVING_ITEMS_FROM_OUTFIT_STAND_AFTER_EQUIPPING),
                    new DLCSetting(OUTFIT_STANDS_IGNORE_STORED_THINGS_BEAUTY),
                    new DLCSetting(STATUES_DONT_HAVE_HEADGEAR),
                    new DLCSetting(STATUE_CONSISTENCY) { visibilityGetter = () => ModsConfig.BiotechActive, bugFix = true }
                }
            },
            new SettingsCategory()
            {
                labelKey = "OdysseyPatch_Misc",
                settings = new[]
                {
                    new DLCSetting(WORLD_SEARCH_EMPTY_TILES) { restartRequired = true },
                    new DLCSetting(BIOME_DANGER_WARNING_SUPPRESSED)
                }
            }
        };

        protected override IEnumerable<SettingsCategory> Categories => categories;
    }
}
