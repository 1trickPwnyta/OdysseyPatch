using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;

namespace OdysseyPatch.AllowRemovingItemsFromOutfitStand
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Odyssey.PACKAGE_ID, Settings.AllowRemovingItemsFromOutfitStand)]
    [HarmonyPatch(typeof(Building_OutfitStand))]
    [HarmonyPatch(MethodType.Constructor)]
    public static class Patch_Building_OutfitStand
    {
        public static void Postfix(ref bool ___allowRemovingItems)
        {
            if (Settings.AllowRemovingItemsFromOutfitStand.Enabled())
            {
                ___allowRemovingItems = true;
            }
        }
    }
}