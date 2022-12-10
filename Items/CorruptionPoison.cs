using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using DemonFix.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Corruption;
using Kingmaker.ElementsSystem;
using Kingmaker.Kingdom.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic;
using System;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;

namespace DemonFix.Items
{
    internal class CorruptionPoison
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("Warlock.Poison");
        private static readonly string poisonName = "PoisonOfCorrupt.Name";
        private static readonly string poisonDescription = "PoisonOfCorrupt.Description";
        public static void ClearCorruptionAbility()
        {
            var breakControlAbility = BlueprintTool.Get<BlueprintAbility>("a3fd9fb8383f4f349a37faf4e7644bc3");
            var clearCorruptionAbilityGuid = new BlueprintGuid(new Guid("4f3ee057-0569-4e77-8be0-82770a99108d"));
            var clearCorruptionAbility = Helpers.CreateCopy(breakControlAbility, bp =>
            {
                bp.AssetGuid = clearCorruptionAbilityGuid;
                bp.name = "ClearCorruptionAbility" + bp.AssetGuid;
                bp.m_DisplayName = LocalizationTool.GetString(poisonName);
                bp.m_Description = LocalizationTool.GetString(poisonDescription);
            });
            clearCorruptionAbility.RemoveComponents<AbilityEffectRunAction>();
            clearCorruptionAbility.AddComponent<AbilityEffectRunAction>(c =>
            {
                c.Actions = new ActionList()
                {
                    Actions = new GameAction[]
                    {
                       new ClearCorruptionLevelAction()
                    }
                };
            });
            //var demonForcedRageAbilityIcon = AssetLoader.LoadInternal("Abilities", "DemonFocredRage.png");
            //demonForcedRageAbility.m_Icon = demonForcedRageAbilityIcon;
            Helpers.AddBlueprint(clearCorruptionAbility, clearCorruptionAbilityGuid);
            Logger.Info("Создана абилка ClearCorruptionAbility: " + clearCorruptionAbilityGuid);
        }
        public static void Poison()
        {
            var clearCorruptionAbility = BlueprintTool.Get<BlueprintAbility>("4f3ee05705694e778be082770a99108d");
            var poisonOfHeal = BlueprintTool.Get<BlueprintItemEquipmentUsable>("f3132b08c5942ba4db998b8dcc794409");
            var poisonOfPurityGuid = new BlueprintGuid(new Guid("cccd15b7-514b-4a1e-9121-5db99c5c5afc"));
            var poisonOfPurity = Helpers.CreateCopy(poisonOfHeal, bp =>
            {
               bp.AssetGuid = poisonOfPurityGuid;
               bp.name = "DemonForcedRageAbility" + bp.AssetGuid;
               bp.m_DisplayNameText = LocalizationTool.GetString(poisonName);
               bp.m_DescriptionText = LocalizationTool.GetString(poisonDescription);
            });
            poisonOfPurity.m_Ability = null;
            poisonOfPurity.m_Ability = clearCorruptionAbility.ToReference<BlueprintAbilityReference>();
            Helpers.AddBlueprint(poisonOfPurity, poisonOfPurityGuid);
            Logger.Info("Создано: " + poisonOfPurityGuid);
        }
    }
}
