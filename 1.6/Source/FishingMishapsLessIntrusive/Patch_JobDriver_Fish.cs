using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.FishingMishapsLessIntrusive
{
    [HarmonyPatch(typeof(JobDriver_Fish))]
    [HarmonyPatch("<CompleteFishingToil>b__4_0")]
    public static class Patch_JobDriver_Fish
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.Calls(typeof(LetterStack).Method(nameof(LetterStack.ReceiveLetter), new[] { typeof(TaggedString), typeof(TaggedString), typeof(LetterDef), typeof(LookTargets), typeof(Faction), typeof(Quest), typeof(List<ThingDef>), typeof(string), typeof(int), typeof(bool) })));
            instructionsList[index].opcode = OpCodes.Call;
            instructionsList[index].operand = typeof(Patch_JobDriver_Fish).Method(nameof(DoNotification));
            instructionsList.Insert(index, new CodeInstruction(OpCodes.Ldloc_S, 5));
            instructionsList.InsertRange(index, Enumerable.Repeat(new CodeInstruction(OpCodes.Pop), 6));
            instructionsList.InsertRange(index - 9, Enumerable.Repeat(new CodeInstruction(OpCodes.Pop), 3));
            return instructionsList;
        }

        private static void DoNotification(LetterStack letterStack, LookTargets lookTargets, NegativeFishingOutcomeDef outcome)
        {
            Pawn pawn = lookTargets.PrimaryTarget.Pawn;
            if (OdysseyPatchSettings.FishingMishapsLessIntrusive)
            {
                if (outcome.damageDef != null || outcome.addsHediff != null)
                {
                    Messages.Message(outcome.letterText.Formatted(pawn.Named("PAWN")), pawn, MessageTypeDefOf.NegativeEvent);
                }
            }
            else
            {
                letterStack.ReceiveLetter(outcome.letterLabel, outcome.letterText.Formatted(pawn.Named("PAWN")), outcome.letterDef, pawn);
            }
        }
    }
}
