using RimWorld;
using Verse;
using Verse.AI;

namespace RimRancher
{
	public class JobGiver_LayPlort : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
			CompPlortLayer compPlortLayer = pawn.TryGetComp<CompPlortLayer>();
			if (compPlortLayer == null || !compPlortLayer.CanLayNow)
			{
				return null;
			}
			IntVec3 c = RCellFinder.RandomWanderDestFor(pawn, pawn.Position, 5f, null, Danger.Some);
			return JobMaker.MakeJob(LayPlort_JobDefOf.LayPlort, c);
		}
		private const float LayRadius = 5f;
	}

	[DefOf]
	class LayPlort_JobDefOf
	{
		public static JobDef LayPlort;
	}
}