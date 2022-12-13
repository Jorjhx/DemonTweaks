using BlueprintCore.Utils;
using DemonFix.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
//From PATH_OF_THE_RAGE
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
                if (!Main.Settings.Brimorak)
                {
                    return;
                }
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
                if (!Main.Settings.Kalavacus)
                {
                    return;
                }
                var kalavakusAspectEffectBuff = BlueprintTool.Get<BlueprintBuff>("c9cdd3af3c5f93c4fb8e9119adaa582e");
                kalavakusAspectEffectBuff.EditComponent<AddInitiatorAttackWithWeaponTrigger>(c =>
                c.RangeType = WeaponRangeType.Melee);
                Logger.Info("Пропатчен калавакус");
            }
            static void PatchShadowDemonAspect()
            {
                if (!Main.Settings.ShadowDemon)
                {
                    return;
                }
                var shadowDemonAspectSwitchBuff = BlueprintTool.Get<BlueprintBuff>("d5336d599d004e74d9af6b8967c3f217");
                shadowDemonAspectSwitchBuff.RemoveComponents<AddContextStatBonus>();
                Logger.Info("Пропатчен теневой демон");
            }
            static void PatchSuccubusDemonAspect()
            {
                if (!Main.Settings.Succuba)
                {
                    return;
                }
                var succubusAspectEnemyBuff = BlueprintTool.Get<BlueprintBuff>("5a350c892f24f4f4880b93805be6c89b");
                var succubusAspectContextStatBonus = (AddContextStatBonus)succubusAspectEnemyBuff.Components[1];
                succubusAspectContextStatBonus.Stat = Kingmaker.EntitySystem.Stats.StatType.AdditionalAttackBonus;
                Logger.Info("Пропатчена суккуба");
            }

            static void PatchNocticulaDemonAspect()
            {
                var nocticulaAspectBuff = BlueprintTool.Get<BlueprintBuff>("ef035e3fee135504ebfe9d0d052762f8");
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
                if (!Main.Settings.Balor)
                {
                    return;
                }
                var balorAspectEffectBuff = BlueprintTool.Get<BlueprintBuff>("516462cc6f2e4774292fc7922393e297");
                balorAspectEffectBuff.RemoveComponents<AddContextStatBonus>();
                Logger.Info("Пропатчен балор");
            }
        }
    }
}
