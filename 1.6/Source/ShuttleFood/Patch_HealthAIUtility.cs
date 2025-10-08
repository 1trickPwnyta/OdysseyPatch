using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace OdysseyPatch.ShuttleFood
{
    [HarmonyPatch(typeof(HealthAIUtility))]
    [HarmonyPatch(nameof(HealthAIUtility.FindBestMedicine))]
    public static class Patch_HealthAIUtility
    {
        private static MethodBase healthAIUtility_FindBestMedicine_GetBestMedInInventory = typeof(HealthAIUtility).GetNestedType("<>c__DisplayClass9_0", BindingFlags.NonPublic).Method("<FindBestMedicine>g__GetBestMedInInventory|2");
        private static MethodBase healthAIUtility_FindBestMedicine_PriorityOf = typeof(HealthAIUtility).Method("<FindBestMedicine>g__PriorityOf|9_0");

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.opcode == OpCodes.Endfinally);
            List<Label> labels = instructionsList[index + 1].labels.ListFullCopy();
            instructionsList[index + 1].labels.Clear();
            instructionsList.InsertRange(index + 1, new[]
            {
                new CodeInstruction(OpCodes.Ldloca_S, 2) { labels = labels },
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Call, typeof(Patch_HealthAIUtility).Method(nameof(TryFindShuttleMedicine)))
            });
            return instructionsList;
        }

        private static void TryFindShuttleMedicine(ref Thing medicine, Pawn healer, object obj)
        {
            if (OdysseyPatchSettings.ShuttleFood && medicine == null)
            {
                foreach (Building_PassengerShuttle shuttle in healer.Map.GetShuttlesWithGrabbingEnabled())
                {
                    Thing thing = BestMedInInventory(obj, shuttle.TransporterComp.innerContainer);
                    if (thing != null && (medicine == null || PriorityOf(medicine) < PriorityOf(thing)) && !shuttle.IsForbidden(healer) && healer.CanReach(shuttle, PathEndMode.InteractionCell, Danger.Some))
                    {
                        medicine = thing;
                    }
                }
            }
        }

        private static Thing BestMedInInventory(object obj, ThingOwner inventory) => healthAIUtility_FindBestMedicine_GetBestMedInInventory.Invoke(obj, new object[] { inventory }) as Thing;

        private static float PriorityOf(Thing medicine) => (float)healthAIUtility_FindBestMedicine_PriorityOf.Invoke(null, new object[] { medicine });
    }
}
