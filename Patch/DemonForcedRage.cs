using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using DemonFix.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using System;
namespace DemonFix.Patch
{
    internal class DemonForcedRage
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("Warlock.FocredRage");
        //private static readonly string demonForcedRageAbilityName = "DemonForcedRageAbility.Name";
        private static readonly string demonForcedRageFeatureName = "DemonForcedRageFeature.Name";
        private static readonly string demonForcedRageFeatureDescription = "DemonForcedRageFeature.Description";
        public static void DemonForcedRageAbility()
        {

            var breakControlAbility = BlueprintTool.Get<BlueprintAbility>("a3fd9fb8383f4f349a37faf4e7644bc3"); 
            var demonForcedRageAbilityGuid = new BlueprintGuid(new Guid("f47c5c07-d050-4ccd-929d-b0a746043aca"));
            var demonForcedRageAbility = Helpers.CreateCopy(breakControlAbility, bp =>
            {
                bp.AssetGuid = demonForcedRageAbilityGuid;
                bp.name = "DemonForcedRageAbility" + bp.AssetGuid;
                bp.m_DisplayName = LocalizationTool.GetString(demonForcedRageFeatureName);
                bp.m_Description = LocalizationTool.GetString(demonForcedRageFeatureDescription);
            });
            var demonForcedRageAbilityIcon = AssetLoader.LoadInternal("Abilities", "DemonFocredRage.png");
            demonForcedRageAbility.m_Icon = demonForcedRageAbilityIcon;
            Helpers.AddBlueprint(demonForcedRageAbility, demonForcedRageAbilityGuid);
            Logger.Info("Создана абилка DemonForcedRageAbility: " + demonForcedRageAbilityGuid);
        }

        public static void DemonForcedRageFeature()
        {
            var demonForcedRageAbility = BlueprintTool.Get<BlueprintAbility>("f47c5c07d0504ccd929db0a746043aca");

            var demonForcedRageBuff = BlueprintTool.Get<BlueprintBuff>("325e00281f7e4a54cbc60627f2f66cec");
            demonForcedRageBuff.m_DisplayName = LocalizationTool.GetString(demonForcedRageFeatureName);
            demonForcedRageBuff.m_Description = LocalizationTool.GetString(demonForcedRageFeatureDescription);
            demonForcedRageBuff.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            demonForcedRageBuff.Stacking = StackingType.Prolong;
            var demonForcedRageBuffIcon = AssetLoader.LoadInternal("Abilities", "DemonFocredRage.png");
            demonForcedRageBuff.m_Icon = demonForcedRageBuffIcon;
            demonForcedRageBuff.RemoveComponents<RaiseStatToMinimum>();
            demonForcedRageBuff.RemoveComponents<CombatStateTrigger>();
            demonForcedRageBuff.RemoveComponents<AddStatBonus>();
            demonForcedRageBuff.AddComponent<AddStatBonus>(c =>
            {
                c.Value = 8;
                c.Stat = Kingmaker.EntitySystem.Stats.StatType.Strength;
                c.Descriptor = ModifierDescriptor.Morale;
            });
            demonForcedRageBuff.AddComponent<AddStatBonus>(c =>
            {
                c.Value = 8;
                c.Stat = Kingmaker.EntitySystem.Stats.StatType.Dexterity;
                c.Descriptor = ModifierDescriptor.Morale;
            });
            demonForcedRageBuff.AddComponent<AddStatBonus>(c =>
            {
                c.Value = 8;
                c.Stat = Kingmaker.EntitySystem.Stats.StatType.Constitution;
                c.Descriptor = ModifierDescriptor.Morale;
            });
            demonForcedRageBuff.AddComponent<AddStatBonus>(c =>
            {
                c.Value = 4;
                c.Stat = Kingmaker.EntitySystem.Stats.StatType.AC;
                c.Descriptor = ModifierDescriptor.NaturalArmor;
            });
            demonForcedRageBuff.AddComponent<AddStatBonus>(c =>
            {
                c.Value = 30;
                c.Stat = Kingmaker.EntitySystem.Stats.StatType.Speed;
                c.Descriptor = ModifierDescriptor.Enhancement;
            });
            demonForcedRageBuff.AddComponent<BuffDescriptorImmunity>(c =>
            {
                c.Descriptor.m_IntValue = 4456480;
            });
            demonForcedRageBuff.AddComponent<BuffDescriptorImmunity>(c =>
            {
                c.Descriptor.m_IntValue = 562949986975744;
            });
            demonForcedRageBuff.AddComponent<BuffDescriptorImmunity>(c =>
            {
                c.Descriptor.m_IntValue = 96;
            });
            var demonRageBuff = BlueprintTool.Get<BlueprintBuff>("36ca5ecd8e755a34f8da6b42ad4c965f");
            var contextDuration = ContextDuration.Fixed(2);
            var demonForcedRageFeature = BlueprintTool.Get<BlueprintFeature>("2a5d1de842d4c514495a195a808b14c9");
            demonForcedRageFeature.m_Icon = demonForcedRageBuffIcon;
            demonForcedRageFeature.HideInUI = false;
            demonForcedRageFeature.HideInCharacterSheetAndLevelUp = false;
            demonForcedRageFeature.HideNotAvailibleInUI = false;
            demonForcedRageFeature.RemoveComponents<BuffSubstitutionOnApply>();
            demonForcedRageFeature.AddComponent<AddConditionTrigger>(c =>
            {
                c.m_TriggerType = AddConditionTrigger.TriggerType.OnConditionAdded;
                c.Conditions = new UnitCondition[] { UnitCondition.Dazed, UnitCondition.Confusion, UnitCondition.Frightened, UnitCondition.Paralyzed, UnitCondition.Sickened, UnitCondition.Shaken, UnitCondition.Staggered, UnitCondition.Nauseated, UnitCondition.Stunned };
                c.Action = new ActionList()
                {
                    Actions = new GameAction[]
                    {
                       Helpers.Create<ContextActionApplyBuff>(e =>
                       {
                       e.m_Buff = demonForcedRageBuff.ToReference<BlueprintBuffReference>();
                       e.UseDurationSeconds = false;
                       e.DurationValue = contextDuration;
                       e.Permanent = false;
                       e.ToCaster = true;
                       e.IsFromSpell = true;
                       }),
                       Helpers.Create<ContextActionCastSpell>(e =>
                       {
                       e.m_Spell = demonForcedRageAbility.ToReference<BlueprintAbilityReference>();
                       }),Helpers.Create<ContextActionApplyBuff>(e =>
                       {
                       e.m_Buff = demonRageBuff.ToReference<BlueprintBuffReference>();
                       e.UseDurationSeconds = false;
                       e.DurationValue = contextDuration;
                       e.Permanent = false;
                       e.ToCaster = true;
                       e.IsFromSpell = true;
                       }),
                    }
                };
            });
            FeatureConfigurator.For(demonForcedRageFeature)
                .SetDisplayName(demonForcedRageFeatureName)
                .SetDescription(demonForcedRageFeatureDescription)
                .Configure();
            Logger.Info("Пропатчен");
        }
    }
}
