using RimWorld;
using Verse;
using Verse.AI;

namespace OdysseyPatch.ShuttleFood
{
    public static class Toils_Shuttle
    {
        public static Toil GotoShuttle(TargetIndex ind, Job job, Pawn pawn) => Toils_Goto.GotoThing(ind, PathEndMode.InteractionCell).FailOn(() => job.GetTarget(ind).Thing.IsForbidden(pawn));

        public static Toil CheckItemInShuttle(Thing item, TargetIndex targetShuttleIfIn, Toil jumpIfIn)
        {
            Toil toil = ToilMaker.MakeToil("CheckItemInShuttle");
            toil.initAction = () =>
            {
                Building_PassengerShuttle shuttle = (item?.ParentHolder as CompTransporter)?.parent as Building_PassengerShuttle;
                if (shuttle != null)
                {
                    if (targetShuttleIfIn != 0)
                    {
                        toil.actor.jobs.curJob.SetTarget(targetShuttleIfIn, shuttle);
                    }

                    if (jumpIfIn != null)
                    {
                        toil.actor.jobs.curDriver.JumpToToil(jumpIfIn);
                    }
                }
            };
            toil.defaultCompleteMode = ToilCompleteMode.Instant;
            toil.atomicWithPrevious = true;
            return toil;
        }
    }
}
