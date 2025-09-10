using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.OutfitStandGroupsInBills
{
    [HarmonyPatch(typeof(Dialog_BillConfig))]
    [HarmonyPatch("FillOutputDropdownOptions")]
    public static class Patch_Dialog_BillConfig
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FirstIndexOf(i => i.opcode == OpCodes.Isinst && i.operand is Type t && t == typeof(IRenameable));
            Label endLabel = (Label)instructionsList[index + 1].operand;
            Label startLabel = instructionsList[index + 2].labels[0];
            instructionsList[index + 2].labels.Clear();
            instructionsList.InsertRange(index + 2, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_3) { labels = new List<Label>() { startLabel } },
                new CodeInstruction(OpCodes.Ldfld, typeof(SlotGroup).Field(nameof(SlotGroup.parent))),
                new CodeInstruction(OpCodes.Isinst, typeof(SlotGroupParent_OutfitStand)),
                new CodeInstruction(OpCodes.Brtrue, endLabel)
            });
            return instructionsList;
        }
    }
}
