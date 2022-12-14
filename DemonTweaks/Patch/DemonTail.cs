using BlueprintCore.Utils;
using DemonTweaks.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.FactLogic;

namespace DemonTweaks.Patch
{
    internal class DemonTail
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonTweaks.Tail");
        [HarmonyPriority(Priority.Last)]
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                DemonTail();
            }
            static void DemonTail()
            {
                if (Main.Settings.DemonTail)
                {
                    return;
                }
                var hvost = BlueprintTool.Get<BlueprintFeature>("037f8f3d95b6d1d4d96bcb7927f2e489");
                hvost.RemoveComponents<AddAdditionalLimb>();
                hvost.HideInUI = true;
                hvost.HideInCharacterSheetAndLevelUp = true;
                hvost.HideNotAvailibleInUI = true;
                Logger.Info("Пропатчен");
            }
        }
    }
}
