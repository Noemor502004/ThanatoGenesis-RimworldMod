using RimWorld;
using Verse;

namespace ThanatoGenesis.RecipeWorkers
{
    public class RecipeWorker_ThanatoGenesisResurrectorMechSerum : RecipeWorker
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            return base.AvailableOnNow(thing, part)
                && ThanatoGenesisMod.settings.enableResurrectorMechSerumCrafting;
        }
    }
}
