using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ThanatoGenesis.RecipeWorkers
{
    public class RecipeWorker_ThanatoGenesisSerum : RecipeWorker
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            return base.AvailableOnNow(thing, part)
                && ThanatoGenesisMod.settings.enableThanatoGenesisSerumCrafting;
        }
    }
}
