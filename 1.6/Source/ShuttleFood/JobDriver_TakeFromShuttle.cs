using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace OdysseyPatch.ShuttleFood
{
    public class JobDriver_TakeFromShuttle : JobDriver
    {
        private Building_PassengerShuttle ItemHoldingShuttle => job.GetTarget(TargetIndex.B).Thing as Building_PassengerShuttle;

        private ThingOwner ItemHoldingContainer => (TargetThingA.ParentHolder as CompTransporter).GetDirectlyHeldThings();

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.InteractionCell).FailOn(() => ItemHoldingShuttle.IsForbidden(pawn));
            yield return Toils_Haul.TakeFromOtherInventory(TargetThingA, pawn.inventory.innerContainer, ItemHoldingContainer);
        }
    }
}
