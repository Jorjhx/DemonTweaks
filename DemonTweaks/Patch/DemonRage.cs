using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.ActivatableAbilities;
using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace DemonTweaks.Patch
{
    class DemonRagePatch
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonTweaks.DemonRage");
        private static readonly string demonRageDescription = "DemonRage.Description";
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;
                DemonRageResourcePatch();
                PatchLimitlessDemonRage();
            }
            public static void DemonRageResourcePatch()
            {
                if (!Main.Settings.DemonRage)
                {
                    return;
                }
                var demonRageFeature = BlueprintTool.Get<BlueprintFeature>("6a8af3f208a0fa747a465b70b7043019");
                var demonRageBuff = BlueprintTool.Get<BlueprintBuff>("36ca5ecd8e755a34f8da6b42ad4c965f");
                var demonRageAbility = BlueprintTool.Get<BlueprintAbility>("260daa5144194a8ab5117ff568b680f5");
                demonRageFeature.m_Description = LocalizationTool.GetString(demonRageDescription); 
                demonRageBuff.m_Description = LocalizationTool.GetString(demonRageDescription);
                demonRageAbility.m_Description = LocalizationTool.GetString(demonRageDescription);
                var demonRageResource = BlueprintTool.Get<BlueprintAbilityResource>("f3bf174f0f86b4f45a823e9ed6ccc7a5");
                demonRageResource.m_MaxAmount.LevelStep = 1;
                demonRageResource.m_MaxAmount.PerStepIncrease = 1;
                Logger.Info("Пропатчено");
            }
            //form DarkCodex
            public static void PatchLimitlessDemonRage()
            {
                if (!Main.Settings.DemonRageLimitless)
                {
                    return;
                }
                var demonRageFeature = BlueprintTool.Get<BlueprintFeature>("6a8af3f208a0fa747a465b70b7043019");
                var limitlessRageFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("5cb58e6e406525342842a073fb70d068");
                limitlessRageFeature.ComponentsArray = limitlessRageFeature.ComponentsArray
                    .Where(c => !(c is PrerequisiteFeaturesFromList))
                    .ToArray();
                var rageFeature = BlueprintTool.Get<BlueprintFeature>("2479395977cfeeb46b482bc3385f4647");
                var focusedRageFeature = BlueprintTool.Get<BlueprintFeature>("17b5ab9075c34e24a46ca655406041ea");
                var bloodRageFeature = BlueprintTool.Get<BlueprintFeature>("6991ee8175d87c04790067515f6fb322");
                FeatureConfigurator.For(limitlessRageFeature)
                    .AddPrerequisiteFeature(demonRageFeature, group: Prerequisite.GroupType.Any)
                    .AddPrerequisiteFeature(rageFeature, group: Prerequisite.GroupType.Any)
                    .AddPrerequisiteFeature(focusedRageFeature, group: Prerequisite.GroupType.Any)
                    .AddPrerequisiteFeature(bloodRageFeature, group: Prerequisite.GroupType.Any)
                    .Configure();
                var rage = BlueprintTool.Get<BlueprintActivatableAbility>("0999f99d6157e5c4888f4cfe2d1ce9d6"); //DemonRageAbility
                var rage2 = BlueprintTool.Get<BlueprintAbility>("260daa5144194a8ab5117ff568b680f5"); //DemonRageActivateAbility
                rage.GetComponent<ActivatableAbilityResourceLogic>().m_FreeBlueprint = limitlessRageFeature.ToReference<BlueprintUnitFactReference>();
                rage2.GetComponent<AbilityResourceLogic>().ResourceCostDecreasingFacts.Add(limitlessRageFeature.ToReference<BlueprintUnitFactReference>());
                Logger.Info("Добавлен лимитлесс рэйдж");
            }
        }
    }
}
