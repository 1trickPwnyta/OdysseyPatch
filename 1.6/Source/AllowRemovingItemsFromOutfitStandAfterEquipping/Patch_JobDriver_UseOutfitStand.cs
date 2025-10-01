using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace OdysseyPatch.AllowRemovingItemsFromOutfitStandAfterEquipping
{
    [HarmonyPatch(typeof(JobDriver_UseOutfitStand))]
    [HarmonyPatch("DoTransfer")]
    public static class Patch_JobDriver_UseOutfitStand
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.opcode == OpCodes.Callvirt && i.operand is MethodInfo m && m == typeof(Building_OutfitStand).Method(nameof(Building_OutfitStand.RemoveApparel)));
            instructionsList.RemoveAt(index + 1);
            instructionsList.InsertRange(index + 1, new[]
            {
                new CodeInstruction(OpCodes.Call, typeof(Patch_JobDriver_UseOutfitStand).Method(nameof(ShouldWearApparel))),
                new CodeInstruction(OpCodes.Brfalse_S, instructionsList[index - 7].operand)
            });
            return instructionsList;
        }

        private static bool ShouldWearApparel(bool wasRemoved) => wasRemoved || !OdysseyPatchSettings.AllowRemovingItemsFromOutfitStandAfterEquipping;
    }
}
