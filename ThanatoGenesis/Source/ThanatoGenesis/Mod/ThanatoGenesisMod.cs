using RimWorld;
using UnityEngine;
using Verse;

namespace ThanatoGenesis
{
    public class ThanatoGenesisMod : Mod
    {
        public static ThanatoGenesisModSettings settings;

        public ThanatoGenesisMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ThanatoGenesisModSettings>();
            LongEventHandler.ExecuteWhenFinished(() =>
            {
                ApplySettings();
            });
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard list = new Listing_Standard();
            list.Begin(inRect);

            list.CheckboxLabeled(
                "Enable Luciferium Crafting",
                ref settings.enableLuciferiumCrafting
            );
            list.CheckboxLabeled(
                "Enable Healer Mech Serum Crafting",
                ref settings.enableHealerMechSerumCrafting
            );
            list.CheckboxLabeled(
                "Enable Resurrector Mech Serum Crafting",
                ref settings.enableResurrectorMechSerumCrafting
            );
            list.CheckboxLabeled(
                "Enable ThanatoGenesis Serum Crafting",
                ref settings.enableThanatoGenesisSerumCrafting
            );
            list.Label("Note: Changes require a restart to apply.");
            list.End();
        }

        public override void WriteSettings()
        {
            base.WriteSettings();

            Messages.Message(
                "Recipe changes require restarting the game.",
                MessageTypeDefOf.NeutralEvent
            );
        }

        private static void ApplySettings()
        {
            DisableRecipe(
                "ThanatoGenesis_MakeThanatoGenesisSerum",
                settings.enableThanatoGenesisSerumCrafting);

            DisableRecipe(
                "ThanatoGenesis_MakeHealerMechSerum",
                settings.enableHealerMechSerumCrafting);

            DisableRecipe(
                "ThanatoGenesis_MakeResurrectorMechSerum",
                settings.enableResurrectorMechSerumCrafting);

            DisableRecipe(
                "ThanatoGenesis_MakeLuciferium",
                settings.enableLuciferiumCrafting);
        }

        private static void DisableRecipe(string defName, bool enabled)
        {
            Log.Message($"ThanatoGenesis: {defName} enabled={enabled}");
            if (enabled)
                return;

            RecipeDef recipe = DefDatabase<RecipeDef>.GetNamedSilentFail(defName);

            if (recipe == null)
            {
                Log.Error($"ThanatoGenesis: Recipe {defName} NOT FOUND");
                return;
            }
            Log.Message($"ThanatoGenesis: Removing recipe users from {defName}");
            recipe.recipeUsers.Clear();
        }

        public override string SettingsCategory()
        {
            return "Thanatogenesis";
        }
    }
}