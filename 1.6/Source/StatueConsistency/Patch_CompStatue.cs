using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace OdysseyPatch.StatueConsistency
{
    public class OverriddenGenes : IExposable
    {
        public List<GeneDef> list = new List<GeneDef>();

        public void ExposeData()
        {
            Scribe_Collections.Look(ref list, "list", LookMode.Def);
        }
    }

    [HarmonyPatch(typeof(CompStatue))]
    [HarmonyPatch("CreateSnapshotOfPawn")]
    public static class Patch_CompStatue_CreateSnapshotOfPawn
    {
        public static void Postfix(Pawn p, Dictionary<string, object> ___additionalSavedPawnDataForMods)
        {
            if (ModsConfig.BiotechActive && p.genes != null)
            {
                List<GeneDef> overridenGenes = p.genes.Endogenes.Concat(p.genes.Xenogenes).Where(g => g.overriddenByGene != null).Select(g => g.def).ToList();
                ___additionalSavedPawnDataForMods["OdysseyPatch.StatueConsistency.overridenGenes"] = new OverriddenGenes() { list = overridenGenes };
            }
        }
    }

    [HarmonyPatch(typeof(CompStatue))]
    [HarmonyPatch("InitFakePawn_HookForMods")]
    public static class Patch_CompStatue_InitFakePawn_HookForMods
    {
        public static void Postfix(Pawn fakePawn, Dictionary<string, object> additionalSavedPawnDataForMods)
        {
            if (ModsConfig.BiotechActive && fakePawn.genes != null && OdysseyPatchSettings.StatueConsistency)
            {
                List<GeneDef> overridenGenes = (additionalSavedPawnDataForMods.TryGetValue("OdysseyPatch.StatueConsistency.overridenGenes") as OverriddenGenes)?.list;
                if (overridenGenes != null)
                {
                    List<Gene> genesToRemove = new List<Gene>();
                    foreach (Gene gene in fakePawn.genes.Endogenes.Concat(fakePawn.genes.Xenogenes))
                    {
                        if (overridenGenes.Contains(gene.def))
                        {
                            genesToRemove.Add(gene);
                        }
                    }
                    foreach (Gene gene in genesToRemove)
                    {
                        fakePawn.genes.RemoveGene(gene);
                    }
                }
            }
        }
    }
}
