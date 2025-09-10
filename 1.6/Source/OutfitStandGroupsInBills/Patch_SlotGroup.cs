using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace OdysseyPatch.OutfitStandGroupsInBills
{
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
