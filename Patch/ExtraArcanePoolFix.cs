using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;


namespace DemonFix.Patch
{
    class ExtraArcanePoolFix
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("Warlock.ExtraArcanePoolFix");
        public static void Fix()
        {
            var extraArcanePoolFeature = BlueprintTool.Get<BlueprintFeature>("42f96fc8d6c80784194262e51b0a1d25");
            var abundantArcanePoolFeature = BlueprintTool.Get<BlueprintFeature>("8acebba92ada26043873cae5b92cef7b");
            var spellDanceFeature = BlueprintTool.Get<BlueprintFeature>("330d055badafd5e45a5f5624dd005756");
            FeatureConfigurator.For(extraArcanePoolFeature)
                .AddPrerequisiteFeature(spellDanceFeature, group: Prerequisite.GroupType.Any)
                .Configure();
            FeatureConfigurator.For(abundantArcanePoolFeature)
                .AddPrerequisiteFeature(spellDanceFeature, group: Prerequisite.GroupType.Any)
                .Configure();
            Logger.Info("Пропатчен");
        }
    }
}
