using Verse;

namespace RimRancher
{
	public class CompProperties_PlortLayer : CompProperties
	{
		public CompProperties_PlortLayer()
		{
			this.compClass = typeof(CompPlortLayer);
		}

		public IntRange plortCountRange = IntRange.one;
		
		public float plortLayIntervalDays = 0.25f;
		
		public ThingDef plortDef;

		public float plortProgress = 0.0f;
	}
}