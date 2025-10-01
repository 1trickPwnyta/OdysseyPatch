using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace OdysseyPatch.AllowRemovingItemsFromOutfitStandAfterEquipping
{
    public static class Patch_Building_OutfitStand
    {
        [HarmonyPatch(typeof(Building_OutfitStand))]
        [HarmonyPatch("<GetGizmos>b__97_1")]
        public static class Patch_Building_OutfitStand_GetGizmos
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) => CommonTranspiler(instructions);
        }

        [HarmonyPatch]
        public static class Patch_Building_OutfitStand_GetFloatMenuOptions
        {
            public static IEnumerable<MethodBase> TargetMethods()
            {
                yield return typeof(Building_OutfitStand).GetNestedType("<>c__DisplayClass90_0", BindingFlags.NonPublic).Method("<GetFloatMenuOptions>b__0");
            }

            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) => CommonTranspiler(instructions);
        }

        private static IEnumerable<CodeInstruction> CommonTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            CodeInstruction instruction = instructionsList.Find(i => i.opcode == OpCodes.Call && i.operand is MethodInfo m && m == typeof(Building_OutfitStand).Method("SetAllowHauling"));
            instruction.operand = typeof(Patch_Building_OutfitStand).Method(nameof(SetAllowHauling));
            return instructionsList;
        }

        private static void SetAllowHauling(Building_OutfitStand stand, bool allow)
        {
            if (!OdysseyPatchSettings.AllowRemovingItemsFromOutfitStandAfterEquipping)
            {
                typeof(Building_OutfitStand).Method("SetAllowHauling").Invoke(stand, new object[] { allow });
            }
        }
    }
}
