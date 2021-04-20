using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace RimRancher
{
    class StockGenerator_Slimes : StockGenerator
	{
		// Token: 0x0600594F RID: 22863 RVA: 0x001DC58E File Offset: 0x001DA78E
		public override IEnumerable<Thing> GenerateThings(int forTile, Faction faction = null)
		{
			int randomInRange = this.kindCountRange.RandomInRange;
			int count = this.countRange.RandomInRange;
			List<PawnKindDef> kinds = new List<PawnKindDef>();

			for (int j = 0; j < randomInRange; j++)
			{
				IEnumerable<PawnKindDef> allDefs = DefDatabase<PawnKindDef>.AllDefs;	
				Func<PawnKindDef, bool> predicate;
				predicate = (((PawnKindDef k) => !slimeKindDefs.Contains(k) && this.PawnKindAllowed(k, forTile)));

				IEnumerable<PawnKindDef> source = allDefs.Where(predicate);
				Func<PawnKindDef, float> weightSelector;
				weightSelector = (((PawnKindDef k) => this.SelectionChance(k)));
				
				PawnKindDef item;
				if (!source.TryRandomElementByWeight(weightSelector, out item))
				{
					break;
				}
				kinds.Add(item);
			}
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				PawnKindDef kind;
				if (!kinds.TryRandomElement(out kind))
				{
					yield break;
				}
				PawnGenerationRequest request = new PawnGenerationRequest(kind, null, PawnGenerationContext.NonPlayer, forTile, false, false, false, false, true, false, 1f, false, true, true, true, false, false, false, false, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null);
				yield return PawnGenerator.GeneratePawn(request);
				num = i;
			}
			yield break;
		}
		private float SelectionChance(PawnKindDef k)
		{
			return StockGenerator_Slimes.SelectionChanceFromWildnessCurve.Evaluate(k.RaceProps.wildness);
		}

		public override bool HandlesThingDef(ThingDef thingDef)
		{
			return thingDef.category == ThingCategory.Pawn && thingDef.tradeability != Tradeability.None && (this.tradeTagsSell.Any((string tag) => thingDef.tradeTags != null && thingDef.tradeTags.Contains(tag)) || this.tradeTagsBuy.Any((string tag) => thingDef.tradeTags != null && thingDef.tradeTags.Contains(tag)));
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x001DC638 File Offset: 0x001DA838
		//private bool PawnKindAllowed(PawnKindDef kind, int forTile);
		private bool PawnKindAllowed(PawnKindDef kind, int forTile)
		{
			if (kind.RaceProps.wildness < this.minWildness || kind.RaceProps.wildness > this.maxWildness || kind.RaceProps.wildness > 1f)
			{
				return false;
			}
			if (this.checkTemperature)
			{
				int num = forTile;
				if (num == -1 && Find.AnyPlayerHomeMap != null)
				{
					num = Find.AnyPlayerHomeMap.Tile;
				}
				if (num != -1 && !Find.World.tileTemperatures.SeasonAndOutdoorTemperatureAcceptableFor(num, kind.race))
				{
					return false;
				}
			}
			return kind.race.tradeTags != null && this.tradeTagsSell.Any((string x) => kind.race.tradeTags.Contains(x)) && kind.race.tradeability.TraderCanSell();
		}

		public void LogSlimeChances()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PawnKindDef pawnKindDef in DefDatabase<PawnKindDef>.AllDefs)
			{
				stringBuilder.AppendLine(pawnKindDef.defName + ": " + this.SelectionChance(pawnKindDef).ToString("F2"));
			}
			Log.Message(stringBuilder.ToString(), false);
		}

		[DebugOutput]
		private static void StockGenerationSlimes()
		{
			new StockGenerator_Slimes
			{
				tradeTagsSell =
				{
					"DOOR_Slime_Tag"
				}
			}.LogSlimeChances();
		}

		private List<PawnKindDef> slimeKindDefs = new List<PawnKindDef>();

     	[NoTranslate]
		private List<string> tradeTagsSell = new List<string>();

		[NoTranslate]
		private List<string> tradeTagsBuy = new List<string>();

		private IntRange kindCountRange = new IntRange(1, 1);

		private float minWildness;
	
		private float maxWildness = 1f;

		private bool checkTemperature = true;

		private static readonly SimpleCurve SelectionChanceFromWildnessCurve = new SimpleCurve
		{
			{
				new CurvePoint(0f, 100f),
				true
			},
			{
				new CurvePoint(0.25f, 60f),
				true
			},
			{
				new CurvePoint(0.5f, 30f),
				true
			},
			{
				new CurvePoint(0.75f, 12f),
				true
			},
			{
				new CurvePoint(1f, 2f),
				true
			}
		};
	}
}
