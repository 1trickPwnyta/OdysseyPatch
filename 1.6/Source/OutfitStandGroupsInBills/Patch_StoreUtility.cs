using HarmonyLib;
using RimWorld;
using Verse;

namespace OdysseyPatch.OutfitStandGroupsInBills
{
    [HarmonyPatch(typeof(StoreUtility))]
    [HarmonyPatch("TryFindBestBetterStoreCellForWorker")]
    public static class Patch_StoreUtility
    {
        public static bool Prefix(Thing t, ISlotGroup slotGroup)
        {
            if (OdysseyPatchSettings.OutfitStandGroupsInBills)
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
