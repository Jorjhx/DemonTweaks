using BlueprintCore.Utils;
using DemonFix.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;

namespace DemonFix.Patch
{
    internal class BreakControl
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix.AbyssalStorm");
        [HarmonyPriority(Priority.Last)]
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchBreakControl();
            }
            public static void PatchBreakControl()
            {
                if (!Main.Settings.ForcedRage)
                {
                    return;
                }
                var demonRage = BlueprintTool.Get<BlueprintFeature>("6a8af3f208a0fa747a465b70b7043019");
                var breakControlFeature = BlueprintTool.Get<BlueprintFeature>("99631ee151e86684db2b5f33dddfb7a2");
                breakControlFeature.AddComponent<PrerequisiteNoFeature>(c =>
                {
                    c.m_Feature = demonRage.ToReference<BlueprintFeatureReference>();
                });
                Logger.Info("Пропатчено");
            }

        }
    }
}
