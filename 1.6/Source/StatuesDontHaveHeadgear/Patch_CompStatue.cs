using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace OdysseyPatch.StatuesDontHaveHeadgear
{
    [HarmonyPatch(typeof(CompStatue))]
    [HarmonyPatch("CreateSnapshotOfPawn")]
    public static class Patch_CompStatue_CreateSnapshotOfPawn
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            CodeInstruction instruction = instructionsList.Find(i => i.Calls(typeof(Rand).PropertyGetter(nameof(Rand.Bool))));
            instruction.operand = typeof(Patch_CompStatue_CreateSnapshotOfPawn).Method(nameof(ShouldIncludeHeadgear));
            return instructionsList;
        }

        private static bool ShouldIncludeHeadgear() => OdysseyPatchSettings.StatuesDontHaveHeadgear || Rand.Bool;
    }

    [HarmonyPatch(typeof(CompStatue))]
    [HarmonyPatch("InitFakePawn_HookForMods")]
    public static class Patch_CompStatue_InitFakePawn_HookForMods
    {
        public static void Postfix(CompStatue __instance, Pawn fakePawn)
        {
            if (OdysseyPatchSettings.StatuesDontHaveHeadgear)
            {
                Comp_StatueHeadgear comp = __instance.parent.GetComp<Comp_StatueHeadgear>();
                if (comp != null && !comp.showHeadgear)
                {
                    fakePawn.apparel.WornApparel.RemoveWhere(a => PawnApparelGenerator.IsHeadgear(a.def));
                }
            }
        }
    }
}
