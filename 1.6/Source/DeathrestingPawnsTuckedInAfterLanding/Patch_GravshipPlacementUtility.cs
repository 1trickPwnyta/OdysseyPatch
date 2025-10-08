using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.DeathrestingPawnsTuckedInAfterLanding
{
    [HarmonyPatch(typeof(GravshipPlacementUtility))]
    [HarmonyPatch(nameof(GravshipPlacementUtility.PlaceGravshipInMap))]
    public static class Patch_GravshipPlacementUtility_PlaceGravshipInMap
    {
        public static void Postfix(Gravship gravship, Map map)
        {
            if (OdysseyPatchSettings.DeathrestingPawnsTuckedInAfterLanding)
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
            if (OdysseyPatchSettings.DeathrestingPawnsTuckedInAfterLanding)
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
