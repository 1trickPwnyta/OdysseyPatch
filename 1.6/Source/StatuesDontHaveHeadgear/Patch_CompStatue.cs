using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace OdysseyPatch.StatuesDontHaveHeadgear
{
    [HarmonyPatch_Compatibility(SpecialMod_OdysseyPatch.PACKAGE_ID, SpecialModSettings_Multipatch_Odyssey.STATUES_DONT_HAVE_HEADGEAR)]
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

        private static bool ShouldIncludeHeadgear() => Utility.CheckSetting(SpecialModSettings_Multipatch_Odyssey.STATUES_DONT_HAVE_HEADGEAR) || Rand.Bool;
    }

    [HarmonyPatch_Compatibility(SpecialMod_OdysseyPatch.PACKAGE_ID, SpecialModSettings_Multipatch_Odyssey.STATUES_DONT_HAVE_HEADGEAR)]
    [HarmonyPatch(typeof(CompStatue))]
    [HarmonyPatch("InitFakePawn_HookForMods")]
    public static class Patch_CompStatue_InitFakePawn_HookForMods
    {
        public static void Postfix(CompStatue __instance, Pawn fakePawn)
        {
            if (Utility.CheckSetting(SpecialModSettings_Multipatch_Odyssey.STATUES_DONT_HAVE_HEADGEAR))
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
