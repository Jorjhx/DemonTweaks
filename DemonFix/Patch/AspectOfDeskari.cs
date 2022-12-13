using BlueprintCore.Utils;
using DemonFix.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DemonFix.Patch
{
    internal class AspectOfDeskari
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix.AspectOfDeskari");
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                Configure();
            }
            public static void Configure()
            {
                if (!Main.Settings.Deskari)
                {
                    Logger.Info("Нефикс");
                    return;
                }
                var deskariAspectFeature = BlueprintTool.Get<BlueprintFeature>("60f57fe80fa1986478f474c0fb5e90ac");
                var deskariAspectBuff = BlueprintTool.Get<BlueprintBuff>("1c8b0722a3694854db5b2fa8800575c4");
                deskariAspectBuff.RemoveComponents<DeskariAspect>();
                deskariAspectBuff.AddComponent<DeskariAspectFix>();
                Logger.Info("Фикс");
            }
        }
    }
}
