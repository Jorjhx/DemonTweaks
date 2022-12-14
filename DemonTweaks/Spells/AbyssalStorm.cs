using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using BlueprintCore.Utils;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using DemonTweaks.Utils;
//From PATH_OF_THE_RAGE
namespace DemonTweaks.Spells
{
    class AbyssalStorm
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonTweaks.AbyssalStorm");
        [HarmonyPriority(Priority.Last)]
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchAbyssalStorm();
            }
            static void PatchAbyssalStorm()
            {
                if (!Main.Settings.AbbysalStorm)
                {
                    return;
                }
                var abyssalStorm = BlueprintTool.Get<BlueprintAbility>("58e9e2883bca1574e9c932e72fd361f9");

                abyssalStorm.EditComponent<AbilityTargetsAround>(c =>
                {
                    c.m_Condition = new ConditionsChecker()
                    {
                        Conditions = new Condition[] {
                        new ContextConditionIsCaster() {
                            Not = true
                        }
                        }
                    };
                });
                Logger.Info("Пропатчен");
            }
        }
    }
}
