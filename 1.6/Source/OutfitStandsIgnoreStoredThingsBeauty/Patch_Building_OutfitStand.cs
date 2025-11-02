using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace OdysseyPatch.OutfitStandsIgnoreStoredThingsBeauty
{
    [HarmonyPatch]
    public static class Patch_Building_OutfitStand
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return typeof(Building_OutfitStand).GetNestedType("<>c__DisplayClass57_0", BindingFlags.NonPublic).Method("<get_BeautyOffset>b__0");
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            CodeInstruction instruction = instructionsList.Find(i => i.Calls(typeof(Thing).Method(nameof(Thing.GetBeauty))));
            instruction.opcode = OpCodes.Call;
            instruction.operand = typeof(Patch_Building_OutfitStand).Method(nameof(GetBeauty));
            return instructionsList;
        }

        private static float GetBeauty(Thing thing, bool outside)
        {
            float beauty = thing.GetBeauty(outside);
            return OdysseyPatchSettings.OutfitStandsIgnoreStoredThingsBeauty ? Mathf.Max(0f, beauty) : beauty;
        }
    }
}
