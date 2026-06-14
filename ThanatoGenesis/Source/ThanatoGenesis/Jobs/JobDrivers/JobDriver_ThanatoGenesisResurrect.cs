using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace ThanatoGenesis
{
    internal class JobDriver_ThanatoGenesisResurrect : JobDriver_Resurrect
    {
        private Corpse Corpse => (Corpse)job.GetTarget(TargetIndex.A).Thing;
        private Thing Item => job.GetTarget(TargetIndex.B).Thing;
        private Mote warmupMote;
        private static readonly List<HediffDef> PossibleEffects = new List<HediffDef>
        {
            HediffDef.Named("Dementia"),
            HediffDef.Named("Alzheimers"),
            HediffDef.Named("Carcinoma"),
            HediffDef.Named("HearingLoss"),
            HediffDef.Named("Blindness"),
            HediffDef.Named("Cataract"),
            HediffDef.Named("Frail"),
            HediffDef.Named("BadBack"),
        };

        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.B).FailOnDespawnedOrNull(TargetIndex.A);
            yield return Toils_Haul.StartCarryThing(TargetIndex.B);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.A);
            Toil toil = Toils_General.Wait(600);
            toil.WithProgressBarToilDelay(TargetIndex.A);
            toil.FailOnDespawnedOrNull(TargetIndex.A);
            toil.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
            toil.tickAction = delegate
            {
                CompUsable compUsable = Item.TryGetComp<CompUsable>();
                if (compUsable != null && warmupMote == null && compUsable.Props.warmupMote != null)
                {
                    warmupMote = MoteMaker.MakeAttachedOverlay(Corpse, compUsable.Props.warmupMote, Vector3.zero);
                }

                warmupMote?.Maintain();
            };
            yield return toil;
            yield return Toils_General.Do(Resurrect);
        }

        private void Resurrect()
        {
            Pawn innerPawn = Corpse.InnerPawn;
            CompTargetEffect_ThanatoGenesisResurrect comp = Item.TryGetComp<CompTargetEffect_ThanatoGenesisResurrect>();
            bool flag = true;
            if (!comp.Props.withSideEffects)
            {
                if (!ResurrectionUtility.TryResurrect(innerPawn))
                {
                    flag = false;
                }
            }
            else if (!ResurrectionUtility.TryResurrectWithSideEffects(innerPawn))
            {
                flag = false;
            }

            if (flag)
            {
                SoundDefOf.MechSerumUsed.PlayOneShot(SoundInfo.InMap(innerPawn));
                Messages.Message("MessagePawnResurrected".Translate(innerPawn), innerPawn, MessageTypeDefOf.PositiveEvent);
                if (comp.Props.moteDef != null)
                {
                    MoteMaker.MakeAttachedOverlay(innerPawn, comp.Props.moteDef, Vector3.zero);
                }

                if (comp.Props.addsHediff != null)
                {
                    innerPawn.health.AddHediff(comp.Props.addsHediff);
                }
                ApplyThanatogenesisEffects(innerPawn);
            }
            Item.SplitOff(1).Destroy();
        }

        private void ApplyThanatogenesisEffects(Pawn innerPawn)
        {
            int value = Rand.Range(0, 4);
            for (int i = 0; i < value; i++)
            {
                HediffDef def = PossibleEffects.RandomElement();

                if (!innerPawn.health.hediffSet.HasHediff(def))
                {
                    innerPawn.health.AddHediff(def);
                }
            }
        }
    }
}
