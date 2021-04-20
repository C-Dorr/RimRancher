using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace RimRancher
{
    class CompFrugivore : ThingComp
    {
        public CompProperties_Frugivore Props
        {
            get
            {
                return (CompProperties_Frugivore)this.props;
            }
        }
        
        public bool CanEatThing(ThingDef t)
        {
            return t.ingestible != null && t.ingestible.preferability != FoodPreferability.Undefined && this.parent.def.defName.Contains("DOOR") && this.Props.fruits.Contains(t.defName);
        }

        public override void CompTick()
        {
            base.CompTick();
        }

        public override string CompInspectStringExtra()
        {
            return "Frugivore comp inspection.\n";
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
        }
    }
}
