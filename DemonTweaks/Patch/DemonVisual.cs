using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Visual.CharacterSystem;
using System;
using static Kingmaker.Modding.OwlcatModificationsWindow;

namespace DemonTweaks.Patch
{

    [HarmonyPriority(Priority.Last)]
    [HarmonyPatch(typeof(BlueprintsCache), "Init")]

    static class BlueprintsCache_Init_Patch
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonTweaks.Visual");
        static bool Initialized;

        static void Postfix()
        {
            if (Initialized) return;
            Initialized = true;
            DisableSkin();  //порядок важен
            DisableSkin2();
            DisableTail();
            DisableHorns();
        }
        public static void DisableHorns()
        {
            if (!Main.Settings.DemonHorns)
            {
                Logger.Info("Норм");
                return;
            }
            var demonVis2 = BlueprintTool.Get<BlueprintClassAdditionalVisualSettings>("200266dd7e7341968944e03c8ec9e8db");
            Array.Resize(ref demonVis2.CommonSettings.m_EquipmentEntities, demonVis2.CommonSettings.m_EquipmentEntities.Length - 1);
            Logger.Info("Линька");
        }
        public static void DisableSkin2()
        {
            if (!Main.Settings.DemonSkin2)
            {
                Logger.Info("Норм");
                return;
            }
            var ee_skin = BlueprintTool.Get<KingmakerEquipmentEntity>("c4f9908c5af344ea83641167d81cc029");
            var ee_skin2 = BlueprintTool.Get<KingmakerEquipmentEntity>("8898c707da5343aa9ee546d7fdac640b");
            KingmakerEquipmentEntity.TwoLists[] twoLists = ee_skin.m_RaceDependentArrays;
            KingmakerEquipmentEntity.TwoLists[] twoLists2 = ee_skin2.m_RaceDependentArrays;
            var lenght = ee_skin.m_RaceDependentArrays.Length;
            for (int i = 0; i < lenght; i++)
            {
                if (i == 7)
                {
                    i++;
                }
                ee_skin.m_RaceDependentArrays[i].MaleArray[0] = twoLists[i].MaleArray[1];
                ee_skin.m_RaceDependentArrays[i].FemaleArray[0] = twoLists[i].FemaleArray[1];
                ee_skin2.m_RaceDependentArrays[i].MaleArray[0] = twoLists2[i].MaleArray[1];
                ee_skin2.m_RaceDependentArrays[i].FemaleArray[0] = twoLists2[i].FemaleArray[1];
                Array.Resize(ref ee_skin2.m_RaceDependentArrays[i].MaleArray, ee_skin2.m_RaceDependentArrays[i].MaleArray.Length - 1);
                Array.Resize(ref ee_skin2.m_RaceDependentArrays[i].FemaleArray, ee_skin2.m_RaceDependentArrays[i].FemaleArray.Length - 1);
                Array.Resize(ref ee_skin.m_RaceDependentArrays[i].MaleArray, ee_skin.m_RaceDependentArrays[i].MaleArray.Length - 1);
                Array.Resize(ref ee_skin.m_RaceDependentArrays[i].FemaleArray, ee_skin.m_RaceDependentArrays[i].FemaleArray.Length - 1);
                Logger.Info("Линька");
            }
        }
        public static void DisableTail()
        {
            if (!Main.Settings.DemonTail)
            {
                Logger.Info("Охвостился");
                return;
            }
            var ee_skin = BlueprintTool.Get<KingmakerEquipmentEntity>("c4f9908c5af344ea83641167d81cc029");
            var ee_skin2 = BlueprintTool.Get<KingmakerEquipmentEntity>("8898c707da5343aa9ee546d7fdac640b");
            var lenght = ee_skin.m_RaceDependentArrays.Length;
            for (int i = 0; i < lenght; i++)
            {
                if (i == 7)
                {
                    i++;
                }
                Array.Resize(ref ee_skin2.m_RaceDependentArrays[i].MaleArray, ee_skin2.m_RaceDependentArrays[i].MaleArray.Length - 1);
                Array.Resize(ref ee_skin2.m_RaceDependentArrays[i].FemaleArray, ee_skin2.m_RaceDependentArrays[i].FemaleArray.Length - 1);
                Array.Resize(ref ee_skin.m_RaceDependentArrays[i].MaleArray, ee_skin.m_RaceDependentArrays[i].MaleArray.Length - 1);
                Array.Resize(ref ee_skin.m_RaceDependentArrays[i].FemaleArray, ee_skin.m_RaceDependentArrays[i].FemaleArray.Length - 1);
                Logger.Info("Обезхвостился");
            }
        }

        public static void DisableSkin()
        {
            if (!Main.Settings.DemonSkin)
            {
                Logger.Info("Покраснел");
                return;
            }
            var vis1 = BlueprintTool.Get<BlueprintClassAdditionalVisualSettings>("a445d385eac846c5af9a7960e659fce9");
            var vis2 = BlueprintTool.Get<BlueprintClassAdditionalVisualSettings>("200266dd7e7341968944e03c8ec9e8db");
            var visAngel = BlueprintTool.Get<BlueprintClassAdditionalVisualSettings>("93869a61deba4fbfbe5b2584fac62650");
            BlueprintClassAdditionalVisualSettings.ColorRamp[] colorRamps = visAngel.ColorRamps;
            vis1.ColorRamps = colorRamps;
            vis2.ColorRamps = colorRamps;
            Logger.Info("Побледнел");
        }
    }

    [HarmonyPatch(typeof(UnitEntityData), nameof(UnitEntityData.OnViewDidAttach))]
    public static class UnitEntityData_CreateView_Patch
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonTweaks.Visual.Wings");
        public static void Prefix(UnitEntityData __instance)
        {
            try
            {
                if (!Main.Settings.DemonWings)
                {
                    Logger.Info("Нефикс");
                    return;
                }
                {
                    var wingsDemonBigM = ResourcesLibrary.TryGetResource<EquipmentEntity>("02e52c3d561c3a943a62acf88a0e1086").OutfitParts[1];
                    var wingsDemonBigLM = ResourcesLibrary.TryGetResource<EquipmentEntity>("c4e728537d9ed6a44973cd354bc972db").OutfitParts[1];
                    var wingsDemonBigF = ResourcesLibrary.TryGetResource<EquipmentEntity>("a665e2687fbcc5246b85ef7ace24107b").OutfitParts[0];
                    var wingsDemonBigFL = ResourcesLibrary.TryGetResource<EquipmentEntity>("86e182b836710fe438b0094237533015").OutfitParts[0];
                    var wingsDemonicStandart = ResourcesLibrary.TryGetResource<EquipmentEntity>("23b3eed8f78e69c40a2c6d416cac2f9e");
                    //var newDemonWingsBig = new EquipmentEntity()
                    //{
                    //    OutfitParts = new List<OutfitPart>()
                    //    { ResourcesLibrary.TryGetResource<EquipmentEntity>("23b3eed8f78e69c40a2c6d416cac2f9e").OutfitParts[0]}
                    //};
                    var demonSkin2_M_Any = ResourcesLibrary.TryGetResource<EquipmentEntity>("02e52c3d561c3a943a62acf88a0e1086");
                    var demonSkin2_M_Lisa = ResourcesLibrary.TryGetResource<EquipmentEntity>("c4e728537d9ed6a44973cd354bc972db");
                    var demonSkin2_F_Any = ResourcesLibrary.TryGetResource<EquipmentEntity>("a665e2687fbcc5246b85ef7ace24107b");
                    var demonSkin2_F_Lisa = ResourcesLibrary.TryGetResource<EquipmentEntity>("86e182b836710fe438b0094237533015");
                    demonSkin2_M_Any.OutfitParts.Remove(wingsDemonBigM);
                    demonSkin2_M_Lisa.OutfitParts.Remove(wingsDemonBigLM);
                    demonSkin2_F_Any.OutfitParts.Remove(wingsDemonBigF);
                    demonSkin2_F_Lisa.OutfitParts.Remove(wingsDemonBigFL);
                    wingsDemonicStandart.OutfitParts[0] = wingsDemonBigF;
                    wingsDemonicStandart.OutfitParts[0].m_Scale.x = 1.3f;
                    wingsDemonicStandart.OutfitParts[0].m_Scale.y = 1.3f;
                    wingsDemonicStandart.OutfitParts[0].m_Scale.z = 1.3f;
                    Logger.Info("Фикс крыльев");
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        }
    }

}
