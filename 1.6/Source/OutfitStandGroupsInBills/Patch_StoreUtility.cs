using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using Verse;

namespace OdysseyPatch.OutfitStandGroupsInBills
{
    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.OUTFIT_STAND_GROUPS_IN_BILLS)]
    [HarmonyPatch(typeof(StoreUtility))]
    [HarmonyPatch("TryFindBestBetterStoreCellForWorker")]
    public static class Patch_StoreUtility
    {
        public static bool Prefix(Thing t, ISlotGroup slotGroup)
        {
            if (Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.OUTFIT_STAND_GROUPS_IN_BILLS))
            {
                if (slotGroup is SlotGroup realSlotGroup && realSlotGroup.parent is SlotGroupParent_OutfitStand outfitStand)
                {
                    if (!(outfitStand as IHaulDestination).Accepts(t))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
