using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace RimRancher
{
    public class JobDriver_LayPlort : JobDriver
    {
		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return true;
		}

		protected override IEnumerable<Toil> MakeNewToils()
		{
			yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
			yield return Toils_General.Wait(500, TargetIndex.None);
			yield return Toils_General.Do(delegate
			{
				GenSpawn.Spawn(this.pawn.GetComp<CompPlortLayer>().ProducePlort(), this.pawn.Position, base.Map, WipeMode.Vanish).SetForbiddenIfOutsideHomeArea();
			});
			yield break;
		}
	}
}
