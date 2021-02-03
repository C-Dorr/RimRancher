using System;
using Verse;

namespace RimWorld
{
	// Token: 0x02000893 RID: 2195
	public class CompProperties_PlortLayer : CompProperties
	{
		// Token: 0x06003674 RID: 13940 RVA: 0x001298FC File Offset: 0x00127AFC
		public CompProperties_PlortLayer()
		{
			this.compClass = typeof(CompPlortLayer);
		}

		public IntRange plortCountRange = IntRange.one;

		public ThingDef plortDef;
	}
}