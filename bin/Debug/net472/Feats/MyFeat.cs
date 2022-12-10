using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;

namespace DemonFix.Feats
{
  /// <summary>
  /// Creates a feat that does nothing but show up.
  /// </summary>
        class MyFeat
        {
                private static readonly string FeatName = "MyFeat";
                private static readonly string FeatGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e20bb";

                private static readonly string DisplayName = "MyFeat.Name";
                private static readonly string Description = "MyFeat.Description";
                private static readonly string Icon = "assets/icons/quillen.jpg";

            public static void Configure()
            {
                    var spellDanceFeature1 = BlueprintTool.Get<BlueprintFeature>("330d055badafd5e45a5f5624dd005756");
                    var arcanePoolFeature = BlueprintTool.Get<BlueprintFeature>("3ce9bb90749c21249adc639031d5eed1");
                    var eldritchPoolFeature = BlueprintTool.Get<BlueprintFeature>("95e04a9e86aa9e64dad7122625b79c62");
                    var armoredBattlemageArcanePollFeature = BlueprintTool.Get<BlueprintFeature>("466c40aba50096341bf6532b1e53e8bd");
        
                    FeatureConfigurator.New(FeatName, FeatGuid, FeatureGroup.Feat)

                        .SetDisplayName(DisplayName)
                        .SetDescription(Description)
                        .SetIcon(Icon)
                        .AddPrerequisiteFeature(spellDanceFeature1)
                        .AddPrerequisiteFeature(arcanePoolFeature)
                        .AddPrerequisiteFeature(eldritchPoolFeature)
                        .AddPrerequisiteFeature(armoredBattlemageArcanePollFeature)
                        .Configure(delayed: true);
            }
        }
}

