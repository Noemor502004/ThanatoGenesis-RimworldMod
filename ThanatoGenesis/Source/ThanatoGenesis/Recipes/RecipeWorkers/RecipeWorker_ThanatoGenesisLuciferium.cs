using Verse;
using RimWorld;

namespace ThanatoGenesis
{
    public class RecipeWorker_ThanatoGenesisLuciferium : RecipeWorker
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            return base.AvailableOnNow(thing, part)
                && ThanatoGenesisMod.settings.enableLuciferiumCrafting;
        }
    }
}
