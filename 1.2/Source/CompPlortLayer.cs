using System;
using UnityEngine;
using Verse;

namespace RimWorld
{
    // Token: 0x02000D83 RID: 3459
    public class CompPlortLayer : ThingComp
    {
		public CompProperties_PlortLayer Props
        {
			get
            {
				return (CompProperties_PlortLayer)this.props;
            }
        }

		private bool Active
		{
			get
			{
				Pawn pawn = this.parent as Pawn;
				return ( && (pawn == null || pawn.ageTracker.CurLifeStage.milkable);
			}
		}

		private bool CanLayNow
        {
			get
            {
				Pawn pawn = this.parent as pawn;
				return (pawn == null && (pawn.needs.food.CurCategory == HungerCategory.Fed) && pawn);
            }
        }

		public override void CompTick()
		{
			if (this.Active)
			{
				float num = 1f / (this.Props.eggLayIntervalDays * 60000f);
				Pawn pawn = this.parent as Pawn;
				if (pawn != null)
				{
					num *= PawnUtility.BodyResourceGrowthSpeed(pawn);
				}
				this.plortProgress += num;
				if (this.plortProgress > 1f)
				{
					this.plortProgress = 1f;
				}
			}
		}
		public virtual Thing ProducePlort()
		{
			if (!this.Active)
			{
				Log.Error("ProducePlort while not Active: " + this.parent, false);
			}

			this.plortProgress = 0f;
			int randomInRange = this.Props.plortCountRange.RandomInRange;
			if (randomInRange == 0)
			{
				return null;
			}

			Thing thing;
			thing = ThingMaker.MakeThing(this.Props.plortDef, null);

			thing.stackCount = randomInRange;	

			return thing;
		}

		public override string CompInspectStringExtra()
		{
			if (!this.Active)
			{
				return null;
			}
			return "PlortProgress".Translate() + ": " + this.PlortProgress.ToStringPercent();
		}
	}

	private float plortProgress;
}