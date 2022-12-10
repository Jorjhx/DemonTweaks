using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//From DarkCodex
namespace DemonFix.Utils
{
    internal class AbilityAtWillPatch
    {
        [AllowedOn(typeof(BlueprintAbility))]
        public class AbilityAtWill : BlueprintComponent
        {

        }
    }
}
