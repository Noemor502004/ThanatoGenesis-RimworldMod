using Verse;

namespace ThanatoGenesis
{
    public class ThanatoGenesisModSettings : ModSettings
    {
        public bool enableLuciferiumCrafting = false;
        public bool enableResurrectorMechSerumCrafting = true;
        public bool enableHealerMechSerumCrafting = true;
        public bool enableThanatoGenesisSerumCrafting = true;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref enableLuciferiumCrafting, "enableLuciferiumCrafting", false);
            Scribe_Values.Look(ref enableResurrectorMechSerumCrafting, "enableResurrectorMechSerumCrafting", true);
            Scribe_Values.Look(ref enableHealerMechSerumCrafting, "enableHealerMechSerumCrafting", true);
            Scribe_Values.Look(ref enableThanatoGenesisSerumCrafting, "enableThanatoGenesisSerumCrafting", true);
        }
    }
}