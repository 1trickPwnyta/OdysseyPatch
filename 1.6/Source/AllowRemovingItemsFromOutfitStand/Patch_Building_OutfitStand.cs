using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;

namespace OdysseyPatch.AllowRemovingItemsFromOutfitStand
{
    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.ALLOW_REMOVING_ITEMS_FROM_OUTFIT_STAND)]
    [HarmonyPatch(typeof(Building_OutfitStand))]
    [HarmonyPatch(MethodType.Constructor)]
    public static class Patch_Building_OutfitStand
    {
        public static void Postfix(ref bool ___allowRemovingItems)
        {
            if (Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.ALLOW_REMOVING_ITEMS_FROM_OUTFIT_STAND))
            {
                ___allowRemovingItems = true;
            }
        }
    }
}