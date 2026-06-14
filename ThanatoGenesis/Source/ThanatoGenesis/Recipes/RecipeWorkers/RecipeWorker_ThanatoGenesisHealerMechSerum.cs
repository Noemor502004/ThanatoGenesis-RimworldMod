using RimWorld;
using Verse;

namespace ThanatoGenesis.RecipeWorkers
{
    internal class RecipeWorker_ThanatoGenesisHealerMechSerum : RecipeWorker
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            return base.AvailableOnNow(thing, part)
                && ThanatoGenesisMod.settings.enableHealerMechSerumCrafting;
        }
    }
}
