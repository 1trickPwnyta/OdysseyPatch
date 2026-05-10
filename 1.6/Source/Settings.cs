using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using Verse;

namespace OdysseyPatch
{
    public class SpecialModSettings_Multipatch_Odyssey : SpecialModSettings_Multipatch<Settings>
    {
        protected override string SettingKeyPrefix => "OdysseyPatch";
    }

    public static class Category
    {
        public const string SpaceTravel = "OdysseyPatch_SpaceTravel";
        public const string Fishing = "OdysseyPatch_Fishing";
        public const string OutfitStands = "OdysseyPatch_OutfitStands";
        public const string Misc = "OdysseyPatch_Misc";
    }

    public enum Settings
    {
        [MultipatchSetting(Category.SpaceTravel)] GravshipCutsceneOptions,
        [MultipatchSetting(Category.SpaceTravel)] SubstructureOverlayOptions,
        [MultipatchSetting(Category.SpaceTravel)] GravshipBuildRadiusSeparate,
        [MultipatchSetting(Category.SpaceTravel)] ShowRemainingSubstructureCapacity,
        [MultipatchSetting(Category.SpaceTravel)] GravshipShieldGeneratorRadiusDisplay,
        [MultipatchSetting(Category.SpaceTravel)] ShuttleFood,
        [MultipatchSetting(Category.SpaceTravel)] ShuttleBlockedByLess,
        [MultipatchSetting(Category.SpaceTravel)] VacuumIntensityRoomStat,
        [MultipatchSetting(Category.SpaceTravel, bugFix: true)] FloorsBlockedByHulls,
        [MultipatchSetting(Category.SpaceTravel, bugFix: true)] SilhouettesHiddenByGravshipLanding,
        [MultipatchSetting(Category.SpaceTravel, enablerType: typeof(Enabler_Biotech), bugFix: true)] DeathrestingPawnsTuckedInAfterLanding,
        [MultipatchSetting(Category.SpaceTravel, bugFix: true)] FlickSwitchesAfterLanding,
        [MultipatchSetting(Category.SpaceTravel, bugFix: true)] ShuttleSavingError,
        [MultipatchSetting(Category.SpaceTravel, bugFix: true)] FilthMultiplierFixForSubstructure,

        [MultipatchSetting(Category.Fishing)] FishingZoneCopy,
        [MultipatchSetting(Category.Fishing)] FishingInterruptions,
        [MultipatchSetting(Category.Fishing)] FishingMishapsLessIntrusive,

        [MultipatchSetting(Category.OutfitStands)] OutfitStandBodyType,
        [MultipatchSetting(Category.OutfitStands, restartRequired: true)] OutfitStandGroupsInBills,
        [MultipatchSetting(Category.OutfitStands)] AllowRemovingItemsFromOutfitStand,
        [MultipatchSetting(Category.OutfitStands)] AllowRemovingItemsFromOutfitStandAfterEquipping,
        [MultipatchSetting(Category.OutfitStands)] OutfitStandsIgnoreStoredThingsBeauty,
        [MultipatchSetting(Category.OutfitStands)] StatuesDontHaveHeadgear,
        [MultipatchSetting(Category.OutfitStands, enablerType: typeof(Enabler_Biotech), bugFix: true)] StatueConsistency,

        [MultipatchSetting(Category.Misc, restartRequired: true)] WorldSearchEmptyTiles,
        [MultipatchSetting(Category.Misc)] BiomeDangerWarningSuppressed,
    }

    public class Enabler_Biotech : SettingAttribute.IEnabler
    {
        public bool Enabled() => ModsConfig.BiotechActive;
    }
}
