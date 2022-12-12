using BlueprintCore.Utils;
using DemonFix.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DemonFix.Patch
{
    internal class DeskariAspectPatch
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix.DeskariConfigure");
        public static void Configure()
        {
            var deskariAspectFeature = BlueprintTool.Get<BlueprintFeature>("60f57fe80fa1986478f474c0fb5e90ac");
            var deskariAspectBuff = BlueprintTool.Get<BlueprintBuff>("1c8b0722a3694854db5b2fa8800575c4");
            deskariAspectBuff.RemoveComponents<DeskariAspect>();
            deskariAspectBuff.AddComponent<DeskariAspectFix>();
            Logger.Info("Пропатчено");
        }
    }

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

