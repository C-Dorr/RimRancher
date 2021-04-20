using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimRancher
{
    class CompProperties_Frugivore : CompProperties
    {

        public CompProperties_Frugivore()
        {
            this.compClass = typeof(CompFrugivore);
        }

        public List<String> fruits;
    }
}
