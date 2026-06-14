using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace ThanatoGenesis
{
    public class CompTargetEffect_ThanatoGenesisResurrect : CompTargetEffect_Resurrect
    {
        public override void DoEffectOn(Pawn user, Thing target)
        {
            Log.Message("ThanatoGenesis activated");

            Corpse corpse = target as Corpse;
            if (corpse == null)
                return;
            if (corpse.InnerPawn == null)
                return;
            JobDef def = DefDatabase<JobDef>.GetNamedSilentFail("ThanatoGenesis_Resurrect");
            if (def == null)
            {
                Log.Error("ThanatoGenesis: JobDef missing!");
                return;
            }
            if (user.IsColonistPlayerControlled)
            {
                Job job = JobMaker.MakeJob(def, corpse, this.parent);
                job.count = 1;
                job.playerForced = true;
                user.jobs.TryTakeOrderedJob(job, JobTag.Misc);
            }
        }
    }
}