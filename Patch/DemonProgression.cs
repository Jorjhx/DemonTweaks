using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;

namespace DemonFix.DemonProgression
{
    class DemonProgression
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix.DemonProgression");
        [HarmonyPriority(Priority.Last)]
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;
            static BlueprintProgression demonProgression;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;
                demonProgression = BlueprintTool.Get<BlueprintProgression>("285fe49f7df8587468f676aa49362213");
                Logger.Info("Патчи прогрессии");
                PatchDemonAspects();
                AddForcedRage();
                AddTeleport();
                AddMinor();
                AddMajor();
                AddLord();
                AddHvost();
            }

            public static void PatchDemonAspects()
            {
                var demonForcedRageFeature = BlueprintTool.Get<BlueprintFeature>("2a5d1de842d4c514495a195a808b14c9").ToReference<BlueprintFeatureBaseReference>();
                demonProgression.LevelEntries[5].m_Features.Remove(demonForcedRageFeature);
                Logger.Info("Удалён DemonForcedRageFeature");
            }
            public static void AddForcedRage()
            {
                if (!Main.Settings.ForcedRage)
                {
                    return;
                }
                var demonForcedRageFeature = BlueprintTool.Get<BlueprintFeature>("2a5d1de842d4c514495a195a808b14c9").ToReference<BlueprintFeatureBaseReference>();
                demonProgression.LevelEntries[4].m_Features.Add(demonForcedRageFeature);
                Logger.Info("Пропатчена неудержимая ярость");
            }
            public static void AddMinor()
            {
                if (!Main.Settings.AddMinor)
                {
                    return;
                }
                var demonAspectFeature = BlueprintTool.Get<BlueprintFeatureSelection>("bbfc0d06955db514ba23337c7bf2cca6").ToReference<BlueprintFeatureBaseReference>();
                demonProgression.LevelEntries[1].m_Features.Add(demonAspectFeature);
                Logger.Info("Добавлены аспекты");
            }
            public static void AddMajor()
            {
                if (!Main.Settings.AddMajor)
                {
                    return;
                }
                var extraMajorAspectFeature = BlueprintTool.Get<BlueprintFeature>("d93b74fc08aa42c5be88e21e0abcc379").ToReference<BlueprintFeatureBaseReference>();
                var demonMajorAspectFeature = BlueprintTool.Get<BlueprintFeatureSelection>("5eba1d83a078bdd49a0adc79279e1ffe").ToReference<BlueprintFeatureBaseReference>();
                demonProgression.LevelEntries[4].m_Features.Add(extraMajorAspectFeature);
                demonProgression.LevelEntries[6].m_Features.Add(demonMajorAspectFeature);
                Logger.Info("Добавлены аспекты");
            }
            public static void AddLord()
            {
                if (!Main.Settings.AddLord)
                {
                    return;
                }
                var demonLordAspectFeature = BlueprintTool.Get<BlueprintFeatureSelection>("fc93daa527ec58c40afbe874c157bc91").ToReference<BlueprintFeatureBaseReference>();
                demonProgression.LevelEntries[6].m_Features.Add(demonLordAspectFeature);
                Logger.Info("Добавлены аспекты");
            }
            public static void AddTeleport()
            {
                if (!Main.Settings.Teleport)
                {
                    return;
                }
                var teleport = BlueprintTool.Get<BlueprintFeature>("b96fd434d5fb4d09aa76cb602000972d").ToReference<BlueprintFeatureBaseReference>();
                demonProgression.LevelEntries[3].m_Features.Add(teleport);
                Logger.Info("Пропатчен телепорт");
            }
            public static void AddHvost()
            {
                if (!Main.Settings.TailAttack)
                {
                    return;
                }
                var hvost = BlueprintTool.Get<BlueprintFeature>("037f8f3d95b6d1d4d96bcb7927f2e489").ToReference<BlueprintFeatureBaseReference>();
                demonProgression.LevelEntries[0].m_Features.Add(hvost);
                Logger.Info("Рофлы с хвостом");
            }
        }
    }
}