using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using Verse;

namespace OdysseyPatch.OutfitStandGroupsInBills
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Odyssey.PACKAGE_ID, Settings.OutfitStandGroupsInBills)]
    [HarmonyPatch(typeof(StoreUtility))]
    [HarmonyPatch("TryFindBestBetterStoreCellForWorker")]
    public static class Patch_StoreUtility
    {
        public static bool Prefix(Thing t, ISlotGroup slotGroup)
        {
            if (Settings.OutfitStandGroupsInBills.Enabled())
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
