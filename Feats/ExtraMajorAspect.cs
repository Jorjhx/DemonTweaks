﻿using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using DemonFix.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.FactLogic;
using System;

namespace DemonFix.Feats
{
    class ExtraMajorAspect
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DeemonFix.ExtraMajorAspect");
        private static readonly string ExtraMajorAspectName = "ExtraMajorAspect.Name";
        private static readonly string ExtraMajorAspectDescription = "ExtraMajorAspect.Description";
        public static void AddExtraMajorAspect()
        {
            var extraMinorAspect = BlueprintTool.Get<BlueprintFeature>("a35cf0b2ceaa3524db9fcb7847ffef08");

            var extraMajorAspectGuid = new BlueprintGuid(new Guid("d93b74fc-08aa-42c5-be88-e21e0abcc379"));


            var extraMajorAspect = Helpers.CreateCopy(extraMinorAspect, bp =>
            {
                bp.AssetGuid = extraMajorAspectGuid;
                bp.name = ExtraMajorAspectName + bp.AssetGuid;
            });
            extraMajorAspect.m_DisplayName = Helpers.CreateString(extraMajorAspect + ".Name", ExtraMajorAspectName);
            var extraMajorAspectDescription = ExtraMajorAspectDescription;

            extraMajorAspect.m_Description = Helpers.CreateString(extraMajorAspect + ".Description", ExtraMajorAspectDescription);

            extraMajorAspect.RemoveComponents<IncreaseActivatableAbilityGroupSize>();
            extraMajorAspect.AddComponent<IncreaseActivatableAbilityGroupSize>(c =>
            {
                c.Group = ActivatableAbilityGroup.DemonMajorAspect;
            });

            Helpers.AddBlueprint(extraMajorAspect, extraMajorAspectGuid);

            FeatureConfigurator.For(extraMajorAspect)
            .SetDisplayName(ExtraMajorAspectName)
            .SetDescription(ExtraMajorAspectDescription)
            .Configure(delayed: true);
            Logger.Info("Добавлен фит: " + extraMajorAspectGuid);
        }
    }
}