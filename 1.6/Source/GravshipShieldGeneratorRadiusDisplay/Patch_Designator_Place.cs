using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.GravshipShieldGeneratorRadiusDisplay
{
    [HarmonyPatch_Compatibility(SpecialMod_OdysseyPatch.PACKAGE_ID, Settings.GravshipShieldGeneratorRadiusDisplay)]
    [HarmonyPatch(typeof(Designator_Place))]
    [HarmonyPatch(nameof(Designator_Place.SelectedUpdate))]
    public static class Patch_Designator_Place
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.LoadsField(typeof(BuildableDef).Field(nameof(BuildableDef.specialDisplayRadius))));
            instructionsList.InsertRange(index - 3, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Callvirt, typeof(Designator_Place).PropertyGetter(nameof(Designator_Place.PlacingDef))),
                new CodeInstruction(OpCodes.Call, typeof(Patch_Designator_Place).Method(nameof(IsTrueOrGravshipShieldGenerator)))
            });
            return instructionsList;
        }

        private static bool IsTrueOrGravshipShieldGenerator(bool accepted, BuildableDef placingDef)
        {
            if (Settings.GravshipShieldGeneratorRadiusDisplay.Enabled())
            {
                return accepted || placingDef == PatchHelper_Designator_Place.gravshipShieldGeneratorDef;
            }
            else
            {
                return accepted;
            }
        }
    }

    public static class PatchHelper_Designator_Place
    {
        public static ThingDef gravshipShieldGeneratorDef = ThingDef.Named("GravshipShieldGenerator");
    }
}
