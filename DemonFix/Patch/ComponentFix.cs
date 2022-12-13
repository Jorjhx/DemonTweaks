using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;

namespace DemonFix.Patch
{

    [TypeId("ee02b85ccda74844d9b393b1cd3bef9c")]
    public class DeskariAspectFix : UnitFactComponentDelegate, IInitiatorRulebookHandler<RulePrepareDamage>, IRulebookHandler<RulePrepareDamage>, ISubscriber, IInitiatorRulebookSubscriber
    {
        //private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix.DeskariAspect");

        public void OnEventAboutToTrigger(RulePrepareDamage evt)
        {
            foreach (BaseDamage item in evt.DamageBundle)
            {
                EnergyDamage energyDamage = item as EnergyDamage;
                if (energyDamage != null)
                {
                    energyDamage.ReplaceEnergy(DamageEnergyType.Unholy);
                    //Logger.Info("Нечестивый");
                    continue;
                }

                PhysicalDamage physicalDamage = item as PhysicalDamage;
                if (physicalDamage != null)
                {
                    physicalDamage.IgnoreImmunities = true;
                    physicalDamage.IgnoreReduction = true;
                    //Logger.Info("Физический");
                }
            }
        }

        public void OnEventDidTrigger(RulePrepareDamage evt)
        {
        }
    }
}

