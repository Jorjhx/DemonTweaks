using BlueprintCore.Utils;
using DemonFix.Utils;
using Kingmaker.AreaLogic.Etudes;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.DialogSystem.Blueprints;
using Kingmaker.ElementsSystem;

namespace DemonFix.Other
{
    class NocticulaMusicFight
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("Warlock.NocticulaMus");
        public static void Configure()
        {
            var playerWithShamira = BlueprintTool.Get<BlueprintEtude>("e159b9b5022b01d41ae86b21ce2df701");
            var parrent = BlueprintTool.Get<BlueprintEtude>("0f0375b148485a342a4498f82a5a8c6e");
            var dialog = BlueprintTool.Get<BlueprintDialog>("5f51a7e0b854a31459928279812ec9ed");
            //var nocti = BlueprintTool.Get<BlueprintUnit>("0cca8c841d634d84fbec2609c8db3465");
            dialog.FinishActions = new ActionList()
            {
                Actions = new GameAction[]
                {
                       Helpers.Create<StopCustomMusic>(),
                       Helpers.Create<PlayCustomMusic>( c =>
                       {
                            c.MusicEventStart = ("MUS_CapitalDisaster_withBoss_Play");
                            c.MusicEventStop = ("MUS_CapitalDisaster_withBoss_Stop");
                       })
                }
            };
            Logger.Info("Добавлен в диалог");
        }
    }
}

