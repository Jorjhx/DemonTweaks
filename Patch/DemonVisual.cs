﻿using BlueprintCore.Utils;
using DemonFix.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.ResourceLinks;
using Kingmaker.Utility;
using Kingmaker.Visual.CharacterSystem;
using System;
using System.Linq;

namespace DemonFix.Patch
{

    [HarmonyPriority(Priority.Last)]
    [HarmonyPatch(typeof(BlueprintsCache), "Init")]

    static class BlueprintsCache_Init_Patch
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix.Visual");
        static bool Initialized;

        static void Postfix()
        {
            if (Initialized) return;
            Initialized = true;
            DisableSkin();  //порядок важен
            DisableSkin2();
            DisableTail();
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
}
