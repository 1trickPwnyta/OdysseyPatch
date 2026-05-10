using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.DeathrestingPawnsTuckedInAfterLanding
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Odyssey.PACKAGE_ID, Settings.DeathrestingPawnsTuckedInAfterLanding)]
    [HarmonyPatch(typeof(GravshipPlacementUtility))]
    [HarmonyPatch(nameof(GravshipPlacementUtility.PlaceGravshipInMap))]
    public static class Patch_GravshipPlacementUtility_PlaceGravshipInMap
    {
        public static void Postfix(Gravship gravship, Map map)
        {
            if (Settings.DeathrestingPawnsTuckedInAfterLanding.Enabled())
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

    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Odyssey.PACKAGE_ID, Settings.DeathrestingPawnsTuckedInAfterLanding)]
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
            if (Settings.DeathrestingPawnsTuckedInAfterLanding.Enabled())
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
