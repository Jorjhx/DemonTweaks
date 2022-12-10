using BlueprintCore.Blueprints.Configurators.Area;
using BlueprintCore.Utils;
using DemonFix.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Area;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DemonFix.Other
{
    internal class NoCampingInDungeon
    {
        public static void NoRest()
        {
            var dlc3_rewardStatueRest = BlueprintTool.Get<ActionsHolder>("ba6419bd0f844d3fa3911ac8ce1fd78f");

            var fakePartyRest = Helpers.Create<FakePartyRest>(e =>
            {
                e.m_Immediate = true;
                e.m_IgnoreCorruption = false;
                e.m_RestWithCraft = false;
            });
            //Из ДЛЦ1 крестоносец
            var izCorraptReducer2 = BlueprintTool.Get<ActionsHolder>("34d2128c45c2470ab81772f14038c6c4");
            izCorraptReducer2.Actions.Actions[1] = null;
            izCorraptReducer2.Actions.Actions = izCorraptReducer2.Actions.Actions.AppendToArray(fakePartyRest);

            var vaultofgraves = BlueprintTool.GetRef<BlueprintAreaReference>("646d29390deeba548b9605329897801f");
            var campingSettings = new CampingSettings()
            { CampingAllowed = true, CamouflageDC = 66};
            AreaConfigurator.For(vaultofgraves)
            .SetCR(66)
            .SetMusicTheme("MUS_Corrupted_02_withDemonLords_TBM_Play")
            .SetMusicThemeStop("MUS_Corrupted_02_withDemonLords_TBM_Stop")
            .SetCampingSettings(campingSettings)
            .SetWeatherInclemencyMin(Owlcat.Runtime.Visual.Effects.WeatherSystem.InclemencyType.Storm)
            .Configure();
            }
        }
    }
