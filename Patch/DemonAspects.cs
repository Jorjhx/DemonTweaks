using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities;
using BlueprintCore.Utils;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Enums;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.ElementsSystem;
using System;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Mechanics.Properties;
using DemonFix.Utils;
//спизженно у PATH_OF_THE_RAGE
namespace DemonFix.Patch
{
    class DemonAspects
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix.AspectsFix");

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchBrimorakAspect();
                PatchKalavakusAspect();
                PatchShadowDemonAspect();
                PatchSuccubusDemonAspect();
                PatchNocticulaDemonAspect();
                PatchBalorDemonAspect();
            }

            static void PatchBrimorakAspect()
            {
                var brimorakAspectEffectBuff = BlueprintTool.Get<BlueprintBuff>("f154542e0b97908479a578dd7bf6d3f7");
                var brimorakAspectEffectProperty = BlueprintTool.Get<BlueprintUnitProperty>("d6a524d190f04a7ca3f920d2f96fa21b").ToReference<BlueprintUnitPropertyReference>();
                brimorakAspectEffectBuff.RemoveComponents<DraconicBloodlineArcana>();
                brimorakAspectEffectBuff.AddComponent<BrimorakAspectDamage>(c =>
                {
                    c.UseContextBonus = true; c.Value = new ContextValue()
                    {
                        ValueType = ContextValueType.CasterCustomProperty,
                        Value = 10,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                        m_CustomProperty = brimorakAspectEffectProperty
                    };
                });
                Logger.Info("Пропатчен бриморак");
            }

            static void PatchKalavakusAspect()
            {
                var kalavakusAspectEffectBuff = BlueprintTool.Get<BlueprintBuff>("c9cdd3af3c5f93c4fb8e9119adaa582e");
                kalavakusAspectEffectBuff.EditComponent<AddInitiatorAttackWithWeaponTrigger>(c =>
                c.RangeType = WeaponRangeType.Melee);
                Logger.Info("Пропатчен калавакус");
            }
            static void PatchShadowDemonAspect()
            {
                var shadowDemonAspectSwitchBuff = BlueprintTool.Get<BlueprintBuff>("d5336d599d004e74d9af6b8967c3f217");
                shadowDemonAspectSwitchBuff.RemoveComponents<AddContextStatBonus>();
                Logger.Info("Пропатчен теневой демон");
            }
            static void PatchSuccubusDemonAspect()
            {
                var succubusAspectEnemyBuff = BlueprintTool.Get<BlueprintBuff>("5a350c892f24f4f4880b93805be6c89b");
                var succubusAspectContextStatBonus = (AddContextStatBonus)succubusAspectEnemyBuff.Components[1];
                succubusAspectContextStatBonus.Stat = Kingmaker.EntitySystem.Stats.StatType.AdditionalAttackBonus;
                Logger.Info("Пропатчена суккуба");
            }

            static void PatchNocticulaDemonAspect()
            {

                var goldDragonCooldownBuff = BlueprintTool.Get<BlueprintBuff>("4e9ddf0456c4d65498ad90fe6e621c3b");

                var nocticulaCooldownBuffGuid = new BlueprintGuid(new Guid("08f90ecc-feed-403a-81a0-f9e44a50870a"));

                var nocticulaCooldownBuff = Helpers.CreateCopy(goldDragonCooldownBuff, bp =>
                {
                    bp.AssetGuid = nocticulaCooldownBuffGuid;
                    bp.name = "Nocticula Aspect Cooldown" + bp.AssetGuid;
                    bp.m_DisplayName = Helpers.CreateString(bp + ".Name", "Nocticula Aspect Already Used");
                    bp.m_Description = Helpers.CreateString(bp + ".Description", "This is the cooldown debuff for Nocticula's Aspect");
                    bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                });

                nocticulaCooldownBuff.AddComponent<CombatStateTrigger>(c =>
                {
                    c.CombatStartActions = new ActionList();
                    c.CombatEndActions = new ActionList() { Actions = new GameAction[] { Helpers.Create<ContextActionRemoveSelf>() } };
                });

                Helpers.AddBlueprint(nocticulaCooldownBuff, nocticulaCooldownBuffGuid);


                var nocticulaAspectAbility = BlueprintTool.Get<BlueprintAbility>("b968988d6c0e830458fd49efbfb86202");
                if (Main.Settings.Icons)
                {
                    nocticulaAspectAbility.m_Icon = AssetLoader.LoadInternal("Abilities", "Nocticula.png");
                }
                //nocticulaAspectAbility.m_Description = Helpers.CreateString(nocticulaAspectAbility + ".Description", "Once per combat, when you activate Demon Rage you can use this ability to grant one ally all the " +
                // "benefits of the demonic rage, including effects of your active aspects and major " +
                // "aspects as a {g|Encyclopedia:Free_Action}free action{/g} until the end of combat.");

                var applyCooldownBuff = Helpers.Create<ContextActionApplyBuff>(e =>
                {
                    e.m_Buff = nocticulaCooldownBuff.ToReference<BlueprintBuffReference>();
                    e.UseDurationSeconds = true;
                    e.DurationValue = new ContextDurationValue();
                    e.Permanent = true;
                    e.ToCaster = true;
                    e.IsFromSpell = true;
                });

                nocticulaAspectAbility.EditComponent<AbilityEffectRunAction>(c =>
                {
                    c.Actions.Actions = nocticulaAspectAbility.GetComponent<AbilityEffectRunAction>().Actions.Actions.AppendToArray(applyCooldownBuff);
                });

                nocticulaAspectAbility.AddComponent<AbilityCasterHasNoFacts>(c => { c.m_Facts = new BlueprintUnitFactReference[] { nocticulaCooldownBuff.ToReference<BlueprintUnitFactReference>() }; });

                var nocticulaAspectBuff = BlueprintTool.Get<BlueprintBuff>("ef035e3fee135504ebfe9d0d052762f8");
                if (Main.Settings.Icons)
                {
                    nocticulaAspectBuff.m_Icon = AssetLoader.LoadInternal("Abilities", "Nocticula.png");
                }

                var demonRageBuff = BlueprintTool.Get<BlueprintBuff>("36ca5ecd8e755a34f8da6b42ad4c965f");

                var bloodragerStandartRageBuff = BlueprintTool.Get<BlueprintBuff>("5eac31e457999334b98f98b60fc73b2f");
                var standartRageBuff = BlueprintTool.Get<BlueprintBuff>("da8ce41ac3cd74742b80984ccc3c9613");
                var elementalRampagerRampageBuff = BlueprintTool.Get<BlueprintBuff>("98798aa2d21a4c20a31d31527642b5f5");

                nocticulaAspectBuff.GetComponent<AddFactsFromCaster>().m_Facts = nocticulaAspectBuff.GetComponent<AddFactsFromCaster>().m_Facts.AppendToArray(bloodragerStandartRageBuff.ToReference<BlueprintUnitFactReference>());
                nocticulaAspectBuff.GetComponent<AddFactsFromCaster>().m_Facts = nocticulaAspectBuff.GetComponent<AddFactsFromCaster>().m_Facts.AppendToArray(standartRageBuff.ToReference<BlueprintUnitFactReference>());
                nocticulaAspectBuff.GetComponent<AddFactsFromCaster>().m_Facts = nocticulaAspectBuff.GetComponent<AddFactsFromCaster>().m_Facts.AppendToArray(elementalRampagerRampageBuff.ToReference<BlueprintUnitFactReference>());


            }
            static void PatchBalorDemonAspect()
            {
                var balorAspectEffectBuff = BlueprintTool.Get<BlueprintBuff>("516462cc6f2e4774292fc7922393e297");
                balorAspectEffectBuff.RemoveComponents<AddContextStatBonus>();
                Logger.Info("Пропатчен балор");

            }
        }
    }
}
