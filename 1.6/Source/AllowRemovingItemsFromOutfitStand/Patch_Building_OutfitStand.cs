﻿using HarmonyLib;
using RimWorld;

namespace OdysseyPatch.AllowRemovingItemsFromOutfitStand
{
    [HarmonyPatch(typeof(Building_OutfitStand))]
    [HarmonyPatch(MethodType.Constructor)]
    public static class Patch_Building_OutfitStand
    {
        public static void Postfix(ref bool ___allowRemovingItems)
        {
            if (OdysseyPatchSettings.AllowRemovingItemsFromOutfitStand)
            {
                ___allowRemovingItems = true;
            }
        }
    }
}