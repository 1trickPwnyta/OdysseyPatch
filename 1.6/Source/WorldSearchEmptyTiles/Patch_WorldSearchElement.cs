using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.WorldSearchEmptyTiles
{
    [HarmonyPatch(typeof(WorldSearchElement))]
    [HarmonyPatch(nameof(WorldSearchElement.DisplayLabel))]
    [HarmonyPatch(MethodType.Getter)]
    public static class Patch_WorldSearchElement
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();

            int zeroIndex = instructionsList.FindIndex(i => i.opcode == OpCodes.Ldc_I4_0);
            instructionsList.RemoveAt(zeroIndex);
            instructionsList.InsertRange(zeroIndex, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, typeof(WorldSearchElement).Field(nameof(WorldSearchElement.tile))),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, typeof(WorldSearchElement).Field(nameof(WorldSearchElement.mutators))),
                new CodeInstruction(OpCodes.Call, typeof(Patch_WorldSearchElement).Method(nameof(GetMatchingFeatureIndex)))
            });

            int labelIndex = instructionsList.FindIndex(i => i.opcode == OpCodes.Ldfld && i.operand is FieldInfo f && f == typeof(Def).Field(nameof(Def.label)));
            instructionsList.RemoveAt(labelIndex);
            instructionsList.InsertRange(labelIndex, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, typeof(WorldSearchElement).Field(nameof(WorldSearchElement.tile))),
                new CodeInstruction(OpCodes.Call, typeof(TileMutatorDef).Method(nameof(TileMutatorDef.Label)))
            });

            return instructionsList;
        }

        private static int GetMatchingFeatureIndex(PlanetTile tile, List<TileMutatorDef> mutators)
        {
            if (OdysseyPatchSettings.WorldSearchEmptyTiles && Find.WindowStack.TryGetWindow(out Dialog_WorldSearch dialog))
            {
                for (int i = 0; i < mutators.Count; i++)
                {
                    TileMutatorDef mutator = mutators[i];
                    if ((bool)typeof(Dialog_Search<WorldSearchElement>).Method("TextMatch").Invoke(dialog, new[] { mutator.label }) || (bool)typeof(Dialog_Search<WorldSearchElement>).Method("TextMatch").Invoke(dialog, new[] { mutator.Label(tile) }))
                    {
                        return i;
                    }
                }
            }
            return 0;
        }
    }
}
