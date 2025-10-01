using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace OdysseyPatch.ShuttleFood
{
    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("<TryFindBestFoodSourceFor>g__FirstFoodInClosestPackAnimalInventory|11_0")]
    public static class Patch_FoodUtility
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            CodeInstruction instruction = instructionsList[instructionsList.Count - 2];
            List<Label> labels = instruction.labels.ListFullCopy();
            instruction.labels.Clear();
            instructionsList.InsertRange(instructionsList.Count - 2, new[]
            {
                new CodeInstruction(OpCodes.Ldloca_S, 0) { labels = labels },
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Ldfld, typeof(FoodUtility).GetNestedType("<>c__DisplayClass11_0", System.Reflection.BindingFlags.NonPublic).Field("eater")),
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Ldfld, typeof(FoodUtility).GetNestedType("<>c__DisplayClass11_0", System.Reflection.BindingFlags.NonPublic).Field("getter")),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Ldfld, typeof(FoodUtility).GetNestedType("<>c__DisplayClass11_0", System.Reflection.BindingFlags.NonPublic).Field("allowVenerated")),
                new CodeInstruction(OpCodes.Call, typeof(Patch_FoodUtility).Method(nameof(TryFindShuttleFood)))
            });
            return instructionsList;
        }

        private static void TryFindShuttleFood(ref Thing food, Pawn eater, Pawn getter, FoodPreferability foodPref, bool allowVenerated)
        {
            if (OdysseyPatchSettings.ShuttleFood && food == null)
            {
                Building_PassengerShuttle bestShuttle = null;
                foreach (Building_PassengerShuttle shuttle in getter.Map.listerThings.GetThingsOfType<Building_PassengerShuttle>().Where(s => s.Faction.IsPlayer && s.GetGrabbableSupplies().enabled))
                {
                    Thing thing = BestFoodInContainer(shuttle.TransporterComp.innerContainer, eater, getter, foodPref, allowVenerated);
                    if (thing != null && (bestShuttle == null || (getter.Position - bestShuttle.Position).LengthManhattan > (getter.Position - shuttle.Position).LengthManhattan) && !shuttle.IsForbidden(getter) && getter.CanReach(shuttle, PathEndMode.InteractionCell, Danger.Some))
                    {
                        bestShuttle = shuttle;
                        food = thing;
                    }
                }
            }
        }

        private static Thing BestFoodInContainer(ThingOwner holder, Pawn eater, Pawn getter, FoodPreferability minFoodPref, bool allowVenerated)
        {
            for (int i = 0; i < holder.Count; i++)
            {
                Thing thing = holder[i];
                if (thing.def.IsNutritionGivingIngestible && thing.IngestibleNow && eater.WillEat(thing, getter, allowVenerated: allowVenerated) && thing.def.ingestible.preferability >= minFoodPref && !thing.def.IsDrug)
                {
                    return thing;
                }
            }

            return null;
        }
    }
}
