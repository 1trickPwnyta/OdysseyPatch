using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using SpecialSauce.ModSettings;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.DeathrestingPawnsTuckedInAfterLanding
{
    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.DEATHRESTING_PAWNS_TUCKED_IN_AFTER_LANDING)]
    [HarmonyPatch(typeof(GravshipPlacementUtility))]
    [HarmonyPatch(nameof(GravshipPlacementUtility.PlaceGravshipInMap))]
    public static class Patch_GravshipPlacementUtility_PlaceGravshipInMap
    {
        public static void Postfix(Gravship gravship, Map map)
        {
            if (Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.DEATHRESTING_PAWNS_TUCKED_IN_AFTER_LANDING))
            {
                foreach (Pawn pawn in gravship.Pawns.Where(p => p.Deathresting))
                {
                    Building_Bed bed = map.thingGrid.ThingAt<Building_Bed>(pawn.Position);
                    if (bed != null)
                    {
                        RestUtility.TuckIntoBed(bed, pawn, pawn, false);
                    }
                }
            }
        }
    }

    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.DEATHRESTING_PAWNS_TUCKED_IN_AFTER_LANDING)]
    [HarmonyPatch(typeof(GravshipPlacementUtility))]
    [HarmonyPatch("SpawnPawns")]
    public static class Patch_GravshipPlacementUtility_SpawnPawns
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.opcode == OpCodes.Callvirt && i.operand is MethodInfo m && m == typeof(Pawn).PropertyGetter(nameof(Pawn.Downed)));
            instructionsList.RemoveRange(index, 4);
            instructionsList.Insert(index, new CodeInstruction(OpCodes.Call, typeof(Patch_GravshipPlacementUtility_SpawnPawns).Method(nameof(ShouldTuckIn))));
            return instructionsList;
        }

        private static bool ShouldTuckIn(Pawn pawn)
        {
            if (Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.DEATHRESTING_PAWNS_TUCKED_IN_AFTER_LANDING))
            {
                return pawn.Downed && !pawn.Deathresting;
            }
            else
            {
                return pawn.Downed || pawn.Deathresting;
            }
        }
    }
}
