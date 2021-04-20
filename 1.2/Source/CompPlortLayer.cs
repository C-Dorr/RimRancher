using RimWorld;
using Verse;

namespace RimRancher
{
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
				return (pawn == null || (pawn.needs.food.CurCategory == HungerCategory.Fed));
			}
		}

		public bool CanLayNow
        {
			get
            {
				return this.Active && Props.plortProgress >= 1f;
            }
        }

		public override void CompTick()
		{
			if (this.Active)
			{	
				float num = 1f / (this.Props.plortLayIntervalDays * 60000f);
				Pawn pawn = this.parent as Pawn;
				if (pawn != null)
				{
					num *= PawnUtility.BodyResourceGrowthSpeed(pawn);
				}
				Props.plortProgress += num;
				if (Props.plortProgress > 1f)
				{
					Props.plortProgress = 1f;
				}
			}
		}

		public virtual Thing ProducePlort()
		{
			Log.Warning("Attached PlortDef " + this.Props.plortDef, false);
			if (!this.Active)
			{
				Log.Error("ProducePlort while not Active: " + this.parent, false);
			}

			Props.plortProgress = 0f;
			int randomInRange = this.Props.plortCountRange.RandomInRange;
			if (randomInRange == 0)
			{
				Log.Warning("random in range is 0");
				return null;
			}

			Thing thing;
			thing = ThingMaker.MakeThing(this.Props.plortDef, null);
			Log.Warning("Making one " + thing.DescriptionFlavor, false);
			thing.stackCount = randomInRange;	

			return thing;
		}

		public override string CompInspectStringExtra()
		{
			if (!this.Active)
			{
				return null;
			}
			if(Props.plortProgress != null)
            {
				return "Plort Progress".Translate() + ": " + Props.plortProgress.ToStringPercent();
			}
			else
            {
				return "Plort Progress: 0.0%".Translate();
            }

		}
		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look<float>(ref Props.plortProgress, "plortProgress", 0f, false);
		}

		private float plortProgress;
	}

}