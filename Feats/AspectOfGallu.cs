using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Utils;
using DemonFix.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;

namespace DemonFix.Feats
{
    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    static class AspectOfGallu
    {
        static bool Initialized;
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix.AspectOfGallu");
        private static readonly string GalluAspectName = "AspectOfGallu";
        private static readonly string GalluAspectDescription = "AspectOfGallu";
        private static readonly string GalluAspectNameDisplay = "AspectOfGallu.Name";
        private static readonly string GalluAspectDescriptionDisplay = "AspectOfGallu.Description";
        private static readonly string GalluAuraNameDisplay = "GalluAura.Name";
        private static readonly string GalluAuraDescriptionDisplay = "GalluAura.Description";
        static void Postfix()
        {
            if (Initialized) return;
            Initialized = false;

            AddGalluAspect();
        }

        public static void AddGalluAspect()
        {

            var coloxusAspectActivatableAbility = BlueprintTool.Get<BlueprintActivatableAbility>("49e1df551bc9cdc499930be39a3fc8cf");
            var coloxusAspectSwitchBuff = BlueprintTool.Get<BlueprintBuff>("0e735301761c86d4184a92f18f42a1aa");
            var coloxusAspectBuff = BlueprintTool.Get<BlueprintBuff>("303e34666de545d4d8b604d720da41b4");
            var coloxusAspectFeature = BlueprintTool.Get<BlueprintFeature>("04f5985258e1d594280b5e02916a6326");
            var galluBuffGuid = new BlueprintGuid(new Guid("0fd57020-7673-483c-8583-c9db32488e34"));

            var galluBuff = Helpers.CreateCopy(coloxusAspectBuff, bp =>
            {
                bp.AssetGuid = galluBuffGuid;
                bp.name = GalluAspectName + bp.AssetGuid;
            });

            galluBuff.m_DisplayName = LocalizationTool.GetString(GalluAspectNameDisplay);
            var galluBuffDescription = GalluAspectDescription;
            
            galluBuff.m_Description = LocalizationTool.GetString(GalluAspectDescriptionDisplay);
            galluBuff.RemoveComponents<AddMechanicsFeature>();
            galluBuff.AddComponent<AddFacts>(c =>
            {
                c.m_Facts = new BlueprintUnitFactReference[]{
                        BlueprintTool.Get<BlueprintFeature>("c0622e01eba849b4ea30a9703b3d3db9").ToReference<BlueprintUnitFactReference>()
                };
            });
            if (Main.Settings.Icons)
            {
                galluBuff.m_Icon = AssetLoader.LoadInternal("Abilities", "Gallu.png");
            }
            Helpers.AddBlueprint(galluBuff, galluBuffGuid);
            Logger.Info("Создан баф. Guid: " + galluBuffGuid);
            var galluAuraOfHavocAbility = BlueprintTool.Get<BlueprintActivatableAbility>("f7e2e72de33228f428441e23b6b8b3d5");

            ActivatableAbilityConfigurator.For(galluAuraOfHavocAbility)
               .SetDisplayName(GalluAuraNameDisplay)
               .SetDescription(GalluAuraDescriptionDisplay)
               .Configure();

            var galluSwitchBuffGuid = new BlueprintGuid(new Guid("229ecb3f-1acb-4033-9a0b-3bb569fb94dd"));

            var galluSwitchBuff = Helpers.CreateCopy(coloxusAspectSwitchBuff, bp =>
            {
                bp.AssetGuid = galluSwitchBuffGuid;
                bp.m_Icon = galluBuff.m_Icon;
                bp.name = GalluAspectName + bp.AssetGuid;
            });

            galluSwitchBuff.m_DisplayName = galluBuff.m_DisplayName;
            var galluSwitchBuffDescription = galluBuff.m_Description;
            galluSwitchBuff.m_Description = LocalizationTool.GetString(GalluAspectDescriptionDisplay);

            var bee = (BuffExtraEffects)galluSwitchBuff.Components[0];
            bee.m_ExtraEffectBuff = galluBuff.ToReference<BlueprintBuffReference>();

            Helpers.AddBlueprint(galluSwitchBuff, galluSwitchBuffGuid);

            ///

            var galluActivatableAspectAbilityGuid = new BlueprintGuid(new Guid("ffa686ce-6804-45e0-adb3-61424442be75"));

            var galluActivatableAspectAbility = Helpers.CreateCopy(coloxusAspectActivatableAbility, bp =>
            {
                bp.AssetGuid = galluActivatableAspectAbilityGuid;
                bp.m_Icon = galluBuff.m_Icon;
                bp.name = GalluAspectName + bp.AssetGuid;
                bp.m_Buff = galluSwitchBuff.ToReference<BlueprintBuffReference>();
            });

            galluActivatableAspectAbility.m_DisplayName = galluBuff.m_DisplayName;
            var galluActivatableAspectAbilityDescription = galluBuff.m_Description;
            galluActivatableAspectAbility.m_Description = LocalizationTool.GetString(GalluAspectDescriptionDisplay);

            Helpers.AddBlueprint(galluActivatableAspectAbility, galluActivatableAspectAbilityGuid);
            ActivatableAbilityConfigurator.For(galluActivatableAspectAbility)
                .SetDisplayName(GalluAspectNameDisplay)
                .SetDescription(GalluAspectDescriptionDisplay)
                .Configure();
            Logger.Info("Создана абилка. Guid: " + galluActivatableAspectAbilityGuid);

            var galluAspectFeatureGuid = new BlueprintGuid(new Guid("93f229a2-bb1d-4ac0-9372-a4c1e90e47f1"));

            var galluAspectFeature = Helpers.CreateCopy(coloxusAspectFeature, bp =>
            {
                bp.AssetGuid = galluAspectFeatureGuid;
                bp.m_Icon = galluBuff.m_Icon;
                bp.name = GalluAspectName + bp.AssetGuid;
                bp.m_DisplayName = galluActivatableAspectAbility.m_DisplayName;
                bp.m_Description = galluActivatableAspectAbility.m_Description;
            });
            //var acsb = (AddContextStatBonus)galluAspectFeature.Components[1];
            //acsb.Stat = Kingmaker.EntitySystem.Stats.StatType.Charisma;
            //var falchion6 = BlueprintTool.Get<BlueprintItemWeapon>("61bc14eca5f8c1040900215000cfc218");

            galluAspectFeature.RemoveComponents<AddFacts>();
            galluAspectFeature.AddComponent<AddSecondaryAttacks>(c =>
            {
                c.m_Weapon = new BlueprintItemWeaponReference[] {
                        BlueprintTool.Get<BlueprintItemWeapon>("61bc14eca5f8c1040900215000cfc218").ToReference<BlueprintItemWeaponReference>()
            }; ;
            });
            galluAspectFeature.AddComponent<AddFacts>(c =>
            {
                c.m_Facts = new BlueprintUnitFactReference[]{
                        galluActivatableAspectAbility.ToReference<BlueprintUnitFactReference>()
                    };
            });

            Helpers.AddBlueprint(galluAspectFeature, galluAspectFeatureGuid);

            Logger.Info("Создан фит. Guid: " + galluAspectFeatureGuid);

            if (!Main.Settings.GalluAspect)
            {
                return;
            }
            var demonMajorAspectSelection = BlueprintTool.Get<BlueprintFeatureSelection>("5eba1d83a078bdd49a0adc79279e1ffe");

            demonMajorAspectSelection.AddFeatures(galluAspectFeature);

            Logger.Info("Добавлен для аспекта Ноктикулы.");

            var nocticulaAspectBuff = BlueprintTool.Get<BlueprintBuff>("ef035e3fee135504ebfe9d0d052762f8");
            nocticulaAspectBuff.GetComponent<AddFactsFromCaster>().m_Facts = nocticulaAspectBuff.GetComponent<AddFactsFromCaster>().m_Facts.AppendToArray(galluSwitchBuff.ToReference<BlueprintUnitFactReference>());

        }

    }
}