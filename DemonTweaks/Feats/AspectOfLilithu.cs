using BlueprintCore.Utils;
using DemonTweaks.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;

namespace DemonTweaks.Feats
{
    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    static class AspectOfLilithu
    {
        static bool Initialized;
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonTweaks.AspectOfLilithu");
        private static readonly string LilithuAspectName = "AspectOfLilithu";
        private static readonly string LilithuAspectDescription = "AspectOfLilithu";
        private static readonly string LilithuAspectNameDisplay = "AspectOfLilithu.Name";
        private static readonly string LilithuAspectDescriptionDisplay = "AspectOfLilithu.Description";
        //private static readonly string LilithuAuraNameDisplay = "lilithuAura.Name";
        //private static readonly string LilithuAuraDescriptionDisplay = "lilithuAura.Description";
        static void Postfix()
        {
            if (Initialized) return;
            Initialized = true;

            AddLilithuAspect();
        }

        public static void AddLilithuAspect()
        {

            var coloxusAspectActivatableAbility = BlueprintTool.Get<BlueprintActivatableAbility>("49e1df551bc9cdc499930be39a3fc8cf");
            var coloxusAspectSwitchBuff = BlueprintTool.Get<BlueprintBuff>("0e735301761c86d4184a92f18f42a1aa");
            var coloxusAspectBuff = BlueprintTool.Get<BlueprintBuff>("303e34666de545d4d8b604d720da41b4");
            var coloxusAspectFeature = BlueprintTool.Get<BlueprintFeature>("04f5985258e1d594280b5e02916a6326");
            var lilithuBuffGuid = new BlueprintGuid(new Guid("985d6d7a-3309-4330-b35b-806811fe7f7f"));

            var lilithuBuff = Helpers.CreateCopy(coloxusAspectBuff, bp =>
            {
                bp.AssetGuid = lilithuBuffGuid;
                bp.name = LilithuAspectName + bp.AssetGuid;
            });

            lilithuBuff.m_DisplayName = LocalizationTool.GetString(LilithuAspectNameDisplay);
            var lilithuBuffDescription = LilithuAspectDescription;

            lilithuBuff.m_Description = LocalizationTool.GetString(LilithuAspectDescriptionDisplay);
            lilithuBuff.RemoveComponents<AddMechanicsFeature>();
            lilithuBuff.AddComponent<AddConditionImmunity>( c =>
            {
                c.Condition = Kingmaker.UnitLogic.UnitCondition.SpellcastingForbidden;
            });
            lilithuBuff.AddComponent<AddConditionImmunity>(c =>
            {
                c.Condition = Kingmaker.UnitLogic.UnitCondition.MagicItemsForbidden;
            });
            if (Main.Settings.Icons)
            {
                lilithuBuff.m_Icon = AssetLoader.LoadInternal("Abilities", "Lilithu.png");
            }
            Helpers.AddBlueprint(lilithuBuff, lilithuBuffGuid);
            Logger.Info("Создан баф. Guid: " + lilithuBuffGuid);
          
            var lilithuSwitchBuffGuid = new BlueprintGuid(new Guid("601ec894-fa7e-4177-9a82-4101a54d7c8a"));

            var lilithuSwitchBuff = Helpers.CreateCopy(coloxusAspectSwitchBuff, bp =>
            {
                bp.AssetGuid = lilithuSwitchBuffGuid;
                bp.m_Icon = lilithuBuff.m_Icon;
                bp.name = LilithuAspectName + bp.AssetGuid;
            });

            lilithuSwitchBuff.m_DisplayName = lilithuBuff.m_DisplayName;
            var lilithuSwitchBuffDescription = lilithuBuff.m_Description;
            lilithuSwitchBuff.m_Description = LocalizationTool.GetString(LilithuAspectDescriptionDisplay);

            var bee = (BuffExtraEffects)lilithuSwitchBuff.Components[0];
            bee.m_ExtraEffectBuff = lilithuBuff.ToReference<BlueprintBuffReference>();

            Helpers.AddBlueprint(lilithuSwitchBuff, lilithuSwitchBuffGuid);

            ///

            var lilithuActivatableAspectAbilityGuid = new BlueprintGuid(new Guid("baf64d34-6104-49b1-bced-b68dc34af316"));

            var lilithuActivatableAspectAbility = Helpers.CreateCopy(coloxusAspectActivatableAbility, bp =>
            {
                bp.AssetGuid = lilithuActivatableAspectAbilityGuid;
                bp.m_Icon = lilithuBuff.m_Icon;
                bp.name = LilithuAspectName + bp.AssetGuid;
                bp.m_Buff = lilithuSwitchBuff.ToReference<BlueprintBuffReference>();
            });

            lilithuActivatableAspectAbility.m_DisplayName = lilithuBuff.m_DisplayName;
            var lilithuActivatableAspectAbilityDescription = lilithuBuff.m_Description;
            lilithuActivatableAspectAbility.m_Description = LocalizationTool.GetString(LilithuAspectDescriptionDisplay);

            Helpers.AddBlueprint(lilithuActivatableAspectAbility, lilithuActivatableAspectAbilityGuid);

            Logger.Info("Создана абилка. Guid: " + lilithuActivatableAspectAbilityGuid);

            var lilithuAspectFeatureGuid = new BlueprintGuid(new Guid("45d64ba8-dca9-4540-b6c8-35bc196324d9"));

            var lilithuAspectFeature = Helpers.CreateCopy(coloxusAspectFeature, bp =>
            {
                bp.AssetGuid = lilithuAspectFeatureGuid;
                bp.m_Icon = lilithuBuff.m_Icon;
                bp.name = LilithuAspectName + bp.AssetGuid;
                bp.m_DisplayName = lilithuActivatableAspectAbility.m_DisplayName;
                bp.m_Description = lilithuActivatableAspectAbility.m_Description;
            });
            var acsb = (AddContextStatBonus)lilithuAspectFeature.Components[1];
            acsb.Stat = Kingmaker.EntitySystem.Stats.StatType.Charisma;

            lilithuAspectFeature.RemoveComponents<AddFacts>();
            lilithuAspectFeature.AddComponent<AddFacts>(c =>
            {
                c.m_Facts = new BlueprintUnitFactReference[]{
                        lilithuActivatableAspectAbility.ToReference<BlueprintUnitFactReference>()
                    };
            });

            Helpers.AddBlueprint(lilithuAspectFeature, lilithuAspectFeatureGuid);

            Logger.Info("Создан фит. Guid: " + lilithuAspectFeatureGuid);

            if (!Main.Settings.LilithuAspect)
            {
                return;
            }
            var demonMajorAspectSelection = BlueprintTool.Get<BlueprintFeatureSelection>("5eba1d83a078bdd49a0adc79279e1ffe");

            demonMajorAspectSelection.AddFeatures(lilithuAspectFeature);

            Logger.Info("Добавлен для аспекта Ноктикулы.");

            var nocticulaAspectBuff = BlueprintTool.Get<BlueprintBuff>("ef035e3fee135504ebfe9d0d052762f8");
            nocticulaAspectBuff.GetComponent<AddFactsFromCaster>().m_Facts = nocticulaAspectBuff.GetComponent<AddFactsFromCaster>().m_Facts.AppendToArray(lilithuSwitchBuff.ToReference<BlueprintUnitFactReference>());

        }

    }
}