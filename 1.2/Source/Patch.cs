using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using Verse;
using RimWorld;


namespace RimRancher
{
    /*[HarmonyPatch(typeof(RaceProperties), "ResolvedDietCategory")]
    class ResolvedDietCategory_Patch
    {
        static DietCategory Prefix(ref DietCategory __result)    
    }

    [HarmonyPatch(typeof(RaceProperties), "CanEverEat")]
    class CanEverEat_Patch
    {
        static bool Prefix(ref bool __result, ThingDef t)
        {
            
            if (t.defName.Contains("DOOR") && (t.slimeFood))
            {
                this.EatsFood && t.ingestible != null && t.ingestible.preferability != FoodPreferability.Undefined && (this.willNeverEat == null || !this.willNeverEat.Contains(t)) &&
                __result = true;
                return false;
            }

            return true;
        }
    }*/
}
