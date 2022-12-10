using BlueprintCore.Utils;
using DemonFix.Spells;
using DemonFix.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Localization;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UI.MVVM._PCView.Settings.Entities.Difficulty;
using Kingmaker.UI.MVVM._VM.Teleport;
using Kingmaker.UI.SettingsUI;
using ModMenu.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DemonFix
{
    internal class SettingsModMenu
    {
        private static readonly string RootKey = "DemonFix.Settings".ToLower();
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix.Settings");

        #region Strings
        private static readonly string title = "Settings.Title";
        private static readonly string visual = "Settings.Visual";
        private static readonly string mechanics = "Settings.Mechanics";
        private static readonly string fun = "Settings.Fun";

        private static readonly string demonskin = "Settings.DemonSkinOff";
        private static readonly string demonskinlong = "Settings.DemonSkinOffLong";
        private static readonly string demonskin2 = "Settings.DemonSkinOff2";
        private static readonly string demonskinlong2 = "Settings.DemonSkinOffLong2";
        private static readonly string demontail = "Settings.DemonTail";
        private static readonly string demontaillong = "Settings.DemonTailLong";
        private static readonly string icons = "Settings.Icons";
        private static readonly string iconslong = "Settings.IconsDesc";

        private static readonly string abbysalstorm = "Settings.AbbysalStorm";
        private static readonly string demonrage = "Settings.DemonRage";
        private static readonly string demonragelimitless = "Settings.DemonRageLimit";
        private static readonly string teleport = "Settings.Teleport";
        private static readonly string moreaspects = "Settings.MoreAspects";
        private static readonly string galluaspect = "Settings.GalluAspect";
        private static readonly string forcedrage = "Settings.ForcedRage";


        private static readonly string abbysallong = "Settings.AbbysalStormDesc";
        private static readonly string demonragelong = "Settings.DemonRageDesc";
        private static readonly string demonragelimitlesslong = "Settings.DemonRageLimitDesc";
        private static readonly string teleportlong = "Settings.TeleportDesc";
        private static readonly string moreaspectslong = "Settings.MoreAspectsDesc";
        private static readonly string galluaspectlong = "AspectOfGallu.Description";
        private static readonly string forcedragelong = "Settings.ForcedRageDesc";


        private static readonly string tailattack = "Settings.TailAttack";
        private static readonly string tailattacklong = "Settings.TailAttacklong";
        #endregion

        #region Bools
        public bool Test => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("test"));
        public bool DemonSkin => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demonskin"));
        public bool DemonSkin2 => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demonskin2"));
        public bool DemonTail => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demontail"));
        public bool Icons => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("icons"));

        public bool AbbysalStorm => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("abbysalstorm"));
        public bool DemonRage => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demonrage"));
        public bool DemonRageLimitless => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demonragelimitless"));
        public bool Teleport => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("teleport"));
        public bool MoreAspects => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("moreaspects"));

        public bool GalluAspect => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("galluaspect"));
        public bool ForcedRage => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("forcedrage"));
        #endregion


        public bool TailAttack => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tailattack"));

        internal void Initialize()
        {
            ModMenu.ModMenu.AddSettings(
              SettingsBuilder
                    .New(GetKey("title"), LocalizationTool.GetString(title))
                    .AddImage(AssetLoader.LoadInternal("Settings", "demonsettings.png", 1200, 215), 215)
                    //ВИЗУАЛ
                    .AddSubHeader(LocalizationTool.GetString(visual), startExpanded: false)
                    .AddToggle(Toggle.New(GetKey("demonskin"), defaultValue: false, LocalizationTool.GetString(demonskin))
                    .WithLongDescription(LocalizationTool.GetString(demonskinlong)))
                    .AddToggle(Toggle.New(GetKey("demonskin2"), defaultValue: false, LocalizationTool.GetString(demonskin2))
                    .WithLongDescription(LocalizationTool.GetString(demonskinlong2)))
                    .AddToggle(Toggle.New(GetKey("demontail"), defaultValue: false, LocalizationTool.GetString(demontail))
                    .WithLongDescription(LocalizationTool.GetString(demontaillong)))
                    .AddToggle(Toggle.New(GetKey("icons"), defaultValue: true, LocalizationTool.GetString(icons))
                    .WithLongDescription(LocalizationTool.GetString(iconslong)))
                    //МЕХАН
                    .AddSubHeader(LocalizationTool.GetString(mechanics), startExpanded: false)
                    .AddToggle(Toggle.New(GetKey("demonrage"), defaultValue: false, LocalizationTool.GetString(demonrage))
                    .WithLongDescription(LocalizationTool.GetString(demonragelong)))
                    .AddToggle(Toggle.New(GetKey("demonragelimitless"), defaultValue: false, LocalizationTool.GetString(demonragelimitless))
                    .WithLongDescription(LocalizationTool.GetString(demonragelimitlesslong)))
                    .AddToggle(Toggle.New(GetKey("abbysalstorm"), defaultValue: false, LocalizationTool.GetString(abbysalstorm))
                    .WithLongDescription(LocalizationTool.GetString(abbysallong)))
                    .AddToggle(Toggle.New(GetKey("forcedrage"), defaultValue: false, LocalizationTool.GetString(forcedrage))
                    .WithLongDescription(LocalizationTool.GetString(forcedragelong)))
                    .AddToggle(Toggle.New(GetKey("teleport"), defaultValue: false, LocalizationTool.GetString(teleport))
                    .WithLongDescription(LocalizationTool.GetString(teleportlong)))
                    .AddToggle(Toggle.New(GetKey("moreaspects"), defaultValue: false, LocalizationTool.GetString(moreaspects))
                    .WithLongDescription(LocalizationTool.GetString(moreaspectslong)))
                    .AddToggle(Toggle.New(GetKey("galluaspect"), defaultValue: false, LocalizationTool.GetString(galluaspect))
                    .WithLongDescription(LocalizationTool.GetString(galluaspectlong)))
                    //ФАН
                    .AddSubHeader(LocalizationTool.GetString(fun), startExpanded: false)
                    .AddToggle(Toggle.New(GetKey("tailattack"), defaultValue: false, LocalizationTool.GetString(tailattack))
                    .WithLongDescription(LocalizationTool.GetString(tailattacklong))));
            Logger.Info("Работай сука");
        }

        private static LocalizedString CreateString(string partialKey, string text)
        {
            return CreateStringInner(GetKey(partialKey, "--"), text);
        }

        private static string GetKey(string partialKey, string separator = ".")
        {
            return $"{RootKey}{separator}{partialKey}";
        }

        private static LocalizedString GetString(string key, bool usePrefix = true)
        {
            var fullKey = usePrefix ? $"{RootKey}.{key}" : key;
            return LocalizationTool.GetString(fullKey);
        }

        private static LocalizedString CreateStringInner(string key, string value)
        {
            LocalizedString result = new()
            {
                m_Key = key
            };
            LocalizationManager.CurrentPack.PutString(key, value);
            return result;
        }
    }
}
