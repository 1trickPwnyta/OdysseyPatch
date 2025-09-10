using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace OdysseyPatch.OutfitStandGroupsInBills
{
    [HarmonyPatch(typeof(Building_OutfitStand))]
    [HarmonyPatch(nameof(Building_OutfitStand.SpawnSetup))]
    public static class Patch_Building_OutfitStand
    {
        public static void Postfix(Building_OutfitStand __instance, Map map)
        {
            if (__instance.def.thingClass != __instance.GetType())
            {
                LongEventHandler.ExecuteWhenFinished(() =>
                {
                    Building_OutfitStand outfitStand = ThingMaker.MakeThing(__instance.def, __instance.Stuff) as Building_OutfitStand;

                    outfitStand.StoreSettings.CopyFrom(__instance.StoreSettings);
                    bool allowHauling = (__instance as IHaulSource).HaulSourceEnabled;
                    typeof(Building_OutfitStand).Method("SetAllowHauling").Invoke(outfitStand, new object[] { allowHauling });
                    outfitStand.HitPoints = __instance.HitPoints;
                    outfitStand.SetFaction(__instance.Faction);
                    outfitStand.SetStorageGroup((__instance as IStorageGroupMember).Group);
                    __instance.SetStorageGroup(null);

                    IntVec3 position = __instance.Position;
                    Rot4 rotation = __instance.Rotation;
                    
                    List<Thing> items = __instance.HeldItems.ToList();
                    foreach (Thing item in items)
                    {
                        if (item is Apparel apparel)
                        {
                            __instance.RemoveApparel(apparel);
                        }
                        else
                        {
                            __instance.RemoveHeldWeapon(item);
                        }
                    }

                    __instance.DeSpawn();
                    GenSpawn.Spawn(outfitStand, position, map);

                    outfitStand.Rotation = rotation;
                    
                    foreach (Thing item in items)
                    {
                        if (item is Apparel apparel)
                        {
                            outfitStand.AddApparel(apparel);
                        }
                        else
                        {
                            outfitStand.TryAddHeldWeapon(item);
                        }
                    }
                });
            }
        }
    }
}
