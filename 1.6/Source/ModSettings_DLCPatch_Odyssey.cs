using SpecialSauce.ModSettings;
using System.Collections.Generic;
using Verse;

namespace OdysseyPatch
{
    public class ModSettings_DLCPatch_Odyssey : ModSettings_DLCPatch
    {
        SettingsCategory[] categories = new[]
        {
            new SettingsCategory()
            {
                labelKey = "OdysseyPatch_SpaceTravel",
                settings = new[]
                {
                    new DLCSetting("OdysseyPatch_GravshipCutsceneOptions"),
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
                }
            }
        };

        protected override IEnumerable<SettingsCategory> Categories => categories;
    }
}
