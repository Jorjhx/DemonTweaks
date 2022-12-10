using BlueprintCore.Blueprints.Configurators.Root;
using BlueprintCore.Utils;
using DemonFix.Feats;
using DemonFix.Other;
using DemonFix.Items;
using DemonFix.Patch;
using DemonFix.Spells;
using DemonFix.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;
using System.Reflection;
using UnityModManagerNet;

namespace DemonFix
{
    static class Main
    {
        public static UnityModManager.ModEntry modInfo = null;
        public static bool Enabled;
        public static SettingsModMenu Settings;

    private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix");

    public static bool Load(UnityModManager.ModEntry modEntry)
    {
      try
      {
                Settings = new SettingsModMenu();
                var harmony = new Harmony(modEntry.Info.Id);
        AssetLoader.ModEntry = modEntry;
        modInfo = modEntry;
        modEntry.OnToggle = OnToggle;
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        Logger.Info("Finished patching.");
      }
      catch (Exception e)
      {
        Logger.Error("Failed to patch", e);
      }
      return true;
    }


    public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
    {
      Enabled = value;
      return true;
    }

    [HarmonyPatch(typeof(BlueprintsCache))]
    static class BlueprintsCaches_Patch
    {
      private static bool Initialized = false;

      [HarmonyPriority(Priority.First)]
      [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
      static void Init()
      {
        try
        {
          if (Initialized)
          {
            Logger.Info("Already configured blueprints.");
            return;
          }
          Initialized = true;

          Logger.Info("Configuring blueprints."); 
                    MyFeat.Configure();
                    ExtraMajorAspect.AddExtraMajorAspect();
                    DemonForcedRage.DemonForcedRageAbility();
                    DemonForcedRage.DemonForcedRageFeature();
                    ExtraArcanePoolFix.Fix();
                    DemonPolymorph.PatchGalluPolymorph();
                    DeskariAspectPatch.Configure();
                    NocticulaMusicFight.Configure();
                    NoCampingInDungeon.NoRest();
                    CorruptionPoison.ClearCorruptionAbility();
                    CorruptionPoison.Poison();
                }
        catch (Exception e)
        {
          Logger.Error("Failed to configure blueprints.", e);
        }
      }
    }

    [HarmonyPatch(typeof(StartGameLoader))]
    static class StartGameLoader_Patch
    {
      private static bool Initialized = false;

      [HarmonyPatch(nameof(StartGameLoader.LoadPackTOC)), HarmonyPostfix]
      static void LoadPackTOC()
      {
        try
        {
          if (Initialized)
          {
            Logger.Info("Already configured delayed blueprints.");
            return;
          }
          Initialized = true;

          RootConfigurator.ConfigureDelayedBlueprints();
        }
        catch (Exception e)
        {
          Logger.Error("Failed to configure delayed blueprints.", e);
        }
      }
    }
    internal class SettingsStarter
        {
            [HarmonyPatch(typeof(BlueprintsCache), nameof(BlueprintsCache.Init))]
            internal static class BlueprintsCache_Init_Patch
            {
                private static bool _initialized;

                [HarmonyPostfix]
                static void Postfix()
                {
                    if (_initialized) return;
                    _initialized = true;
                    Main.Settings.Initialize();
                }
            }
        }
    }
}

