using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace OdysseyPatch.FilthMultiplierFixForSubstructure
{
    [HarmonyPatch(typeof(FilthMaker))]
    [HarmonyPatch(nameof(FilthMaker.CanMakeFilth))]
    public static class Patch_FilthMaker
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            CodeInstruction instruction = instructionsList.Find(i => i.Calls(typeof(TerrainGrid).Method(nameof(TerrainGrid.FoundationAt), new[] { typeof(IntVec3) })));
            instruction.operand = typeof(Patch_FilthMaker).Method(nameof(TerrainAt));
            return instructionsList;
        }

        private static TerrainDef TerrainAt(TerrainGrid grid, IntVec3 c) => OdysseyPatchSettings.FilthMultiplierFixForSubstructure ? grid.TerrainAt(c) : grid.FoundationAt(c);
    }
}
