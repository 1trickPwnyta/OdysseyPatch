using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using System.Collections.Generic;
using Verse;

namespace OdysseyPatch.OutfitStandGroupsInBills
{
    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.OUTFIT_STAND_GROUPS_IN_BILLS)]
    [HarmonyPatch(typeof(SlotGroup))]
    [HarmonyPatch(MethodType.Getter)]
    [HarmonyPatch(nameof(SlotGroup.HeldThings))]
    public static class Patch_SlotGroup_HeldThings
    {
        public static bool Prefix(ISlotGroupParent ___parent, ref IEnumerable<Thing> __result)
        {
            if (___parent is SlotGroupParent_OutfitStand outfitStand)
            {
                __result = outfitStand.HeldItems;
                return false;
            }
            return true;
        }
    }

    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.OUTFIT_STAND_GROUPS_IN_BILLS)]
    [HarmonyPatch(typeof(SlotGroup))]
    [HarmonyPatch(MethodType.Getter)]
    [HarmonyPatch(nameof(SlotGroup.HeldThingsCount))]
    public static class Patch_SlotGroup_HeldThingsCount
    {
        public static bool Prefix(ISlotGroupParent ___parent, ref int __result)
        {
            if (___parent is SlotGroupParent_OutfitStand outfitStand)
            {
                __result = outfitStand.HeldItems.Count;
                return false;
            }
            return true;
        }
    }
}
