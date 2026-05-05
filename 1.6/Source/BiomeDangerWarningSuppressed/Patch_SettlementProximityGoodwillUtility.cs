using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using SpecialSauce.ModSettings;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace OdysseyPatch.BiomeDangerWarningSuppressed
{
    [ModSettings_DLCPatch.HarmonyPatch_Compatibility(Mod_OdysseyPatch.PACKAGE_ID, ModSettings_DLCPatch_Odyssey.BIOME_DANGER_WARNING_SUPPRESSED)]
    public static class Patch_SettlementProximityGoodwillUtility
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return typeof(SettlementProximityGoodwillUtility).GetNestedType("<GetConfirmationDescriptions>d__8", BindingFlags.NonPublic).Method("MoveNext");
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.opcode == OpCodes.Ldfld && i.operand is FieldInfo f && f == typeof(BiomeDef).Field(nameof(BiomeDef.settleWarning)));
            instructionsList[index + 1].operand = typeof(Patch_SettlementProximityGoodwillUtility).Method(nameof(ShouldSkipBiomeWarning));
            CodeInstruction instruction = instructionsList.FindLast(i => i.opcode == OpCodes.Call && i.operand is MethodInfo m && m == typeof(ModsConfig).PropertyGetter(nameof(ModsConfig.OdysseyActive)));
            instruction.operand = typeof(Patch_SettlementProximityGoodwillUtility).Method(nameof(ShouldShowOrbitalWarning));
            return instructionsList;
        }

        private static bool ShouldSkipBiomeWarning(string settleWarning) => Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.BIOME_DANGER_WARNING_SUPPRESSED) || settleWarning.NullOrEmpty();

        private static bool ShouldShowOrbitalWarning() => !Utility.CheckSetting(ModSettings_DLCPatch_Odyssey.BIOME_DANGER_WARNING_SUPPRESSED);
    }
}
