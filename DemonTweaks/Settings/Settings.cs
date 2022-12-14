using BlueprintCore.Utils;
using DemonTweaks.Utils;
using Kingmaker.Localization;
using ModMenu.Settings;

namespace DemonTweaks
{
    internal class SettingsModMenu
    {
        private static readonly string RootKey = "DemonTweaks.Settings".ToLower();
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonTweaks.Settings");

        #region Strings
        private static readonly string title = "Settings.Title";
        private static readonly string visual = "Settings.Visual";
        private static readonly string mechanics = "Settings.Mechanics";
        private static readonly string fun = "Settings.Fun";
        private static readonly string reload = "Settings.NeedReload";

        private static readonly string demonskin = "Settings.DemonSkinOff";
        private static readonly string demonskinlong = "Settings.DemonSkinOffLong";
        private static readonly string demonwings = "Settings.DemonWings";
        private static readonly string demonwingslong = "Settings.DemonWingsLong";
        private static readonly string demonskin2 = "Settings.DemonSkinOff2";
        private static readonly string demonskinlong2 = "Settings.DemonSkinOffLong2";
        private static readonly string demontail = "Settings.DemonTail";
        private static readonly string demontaillong = "Settings.DemonTailLong";
        private static readonly string demonhorns = "Settings.DemonHorns";
        private static readonly string demonhornslong = "Settings.DemonHornsLong";
        private static readonly string icons = "Settings.Icons";
        private static readonly string iconslong = "Settings.IconsDesc";

        private static readonly string abbysalstorm = "Settings.AbbysalStorm";
        private static readonly string demonrage = "Settings.DemonRage";
        private static readonly string demonragelimitless = "Settings.DemonRageLimit";
        private static readonly string teleport = "Settings.Teleport";
        private static readonly string addminor = "Settings.AddMinor";
        private static readonly string addmajor = "Settings.AddMajor";
        private static readonly string addlord = "Settings.AddLord";
        private static readonly string galluaspect = "Settings.GalluAspect";
        private static readonly string lilithuaspect = "Settings.LilithuAspect";
        private static readonly string forcedrage = "Settings.ForcedRage";
        private static readonly string deskari = "Settings.Deskari";

        private static readonly string abbysallong = "Settings.AbbysalStormDesc";
        private static readonly string demonragelong = "Settings.DemonRageDesc";
        private static readonly string demonragelimitlesslong = "Settings.DemonRageLimitDesc";
        private static readonly string teleportlong = "Settings.TeleportDesc";
        private static readonly string addminorlong = "Settings.AddMinorLong";
        private static readonly string addmajorlong = "Settings.AddMajorLong";
        private static readonly string addlordlong = "Settings.AddLordLong";
        private static readonly string galluaspectlong = "AspectOfGallu.Description";
        private static readonly string forcedragelong = "Settings.ForcedRageDesc";
        private static readonly string deskarilong = "Settings.DeskariLong";
        private static readonly string lilithuaspectlong = "AspectOfLilithu.Description";//Settings.LilithuAspectLong";
        
        private static readonly string succuba = "Settings.Succuba";
        private static readonly string succubalong = "Settings.SuccubaLong";
        private static readonly string balor = "Settings.Balor";
        private static readonly string balorlong = "Settings.BalorLong";
        private static readonly string kalavakus = "Settings.Kalavakus";
        private static readonly string kalavakuslong = "Settings.KalavakusLong";
        private static readonly string shadowdemon = "Settings.ShadowDemon";
        private static readonly string shadowdemonlong = "Settings.ShadowDemonLong";
        private static readonly string brimorak = "Settings.Brimorak";
        private static readonly string brimoraklong = "Settings.BrimorakLong";




        private static readonly string tailattack = "Settings.TailAttack";
        private static readonly string tailattacklong = "Settings.TailAttacklong";



        #endregion

        #region Bools
        public bool Test => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("test"));
        public bool DemonSkin => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demonskin"));
        public bool DemonWings => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demonwings"));
        public bool DemonSkin2 => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demonskin2"));
        public bool DemonTail => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demontail"));
        public bool DemonHorns => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demonhorns"));
        public bool Icons => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("icons"));

        public bool AbbysalStorm => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("abbysalstorm"));
        public bool DemonRage => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demonrage"));
        public bool DemonRageLimitless => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("demonragelimitless"));
        public bool Teleport => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("teleport"));
        public bool AddMinor => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("addminor"));
        public bool AddMajor => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("addmajor"));
        public bool AddLord => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("addlord"));

        public bool GalluAspect => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("galluaspect"));
        public bool LilithuAspect => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("lilithuaspect"));
        public bool ForcedRage => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("forcedrage"));
        public bool Deskari => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("deskari"));
        public bool ShadowDemon => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("shadowdemon"));
        public bool Balor => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("balor"));
        public bool Succuba => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("succuba"));
        public bool Kalavacus => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("kalavakus"));
        public bool Brimorak => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("brimorak"));


        #endregion


        public bool TailAttack => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tailattack"));

        internal void Initialize()
        {
            ModMenu.ModMenu.AddSettings(
              SettingsBuilder
                    .New(GetKey("title"), LocalizationTool.GetString(title))
                    .AddImage(AssetLoader.LoadInternal("Settings", "demonsettings.png", 1200, 215), 215)
                    //Внешний вид
                    .AddSubHeader(LocalizationTool.GetString(visual), startExpanded: false)
                    .AddToggle(Toggle.New(GetKey("icons"), defaultValue: true, LocalizationTool.GetString(icons))
                    .WithLongDescription(LocalizationTool.GetString(iconslong)))
                    .AddToggle(Toggle.New(GetKey("demonwings"), defaultValue: true, LocalizationTool.GetString(demonwings))
                    .WithLongDescription(LocalizationTool.GetString(demonwingslong)))
                    .AddToggle(Toggle.New(GetKey("demonskin"), defaultValue: false, LocalizationTool.GetString(demonskin))
                    .WithLongDescription(LocalizationTool.GetString(demonskinlong)))
                    .AddToggle(Toggle.New(GetKey("demonskin2"), defaultValue: false, LocalizationTool.GetString(demonskin2))
                    .WithLongDescription(LocalizationTool.GetString(demonskinlong2)))
                    .AddToggle(Toggle.New(GetKey("demontail"), defaultValue: false, LocalizationTool.GetString(demontail))
                    .WithLongDescription(LocalizationTool.GetString(demontaillong)))
                    .AddToggle(Toggle.New(GetKey("demonhorns"), defaultValue: false, LocalizationTool.GetString(demonhorns))
                    .WithLongDescription(LocalizationTool.GetString(demonhornslong)))

                    //Патчи
                    .AddSubHeader(LocalizationTool.GetString(mechanics), startExpanded: false)
                    .AddToggle(Toggle.New(GetKey("demonrage"), defaultValue: true, LocalizationTool.GetString(demonrage))
                    .WithLongDescription(LocalizationTool.GetString(demonragelong)))
                    .AddToggle(Toggle.New(GetKey("abbysalstorm"), defaultValue: true, LocalizationTool.GetString(abbysalstorm))
                    .WithLongDescription(LocalizationTool.GetString(abbysallong)))
                    .AddToggle(Toggle.New(GetKey("forcedrage"), defaultValue: true, LocalizationTool.GetString(forcedrage))
                    .WithLongDescription(LocalizationTool.GetString(forcedragelong)))
                    .AddToggle(Toggle.New(GetKey("teleport"), defaultValue: true, LocalizationTool.GetString(teleport))
                    .WithLongDescription(LocalizationTool.GetString(teleportlong)))
                    .AddToggle(Toggle.New(GetKey("deskari"), defaultValue: true, LocalizationTool.GetString(deskari))
                    .WithLongDescription(LocalizationTool.GetString(deskarilong)))
                    .AddToggle(Toggle.New(GetKey("shadowdemon"), defaultValue: true, LocalizationTool.GetString(shadowdemon))
                    .WithLongDescription(LocalizationTool.GetString(shadowdemonlong)))
                    .AddToggle(Toggle.New(GetKey("balor"), defaultValue: true, LocalizationTool.GetString(balor))
                    .WithLongDescription(LocalizationTool.GetString(balorlong)))
                    .AddToggle(Toggle.New(GetKey("succuba"), defaultValue: true, LocalizationTool.GetString(succuba))
                    .WithLongDescription(LocalizationTool.GetString(succubalong)))
                    .AddToggle(Toggle.New(GetKey("brimorak"), defaultValue: true, LocalizationTool.GetString(brimorak))
                    .WithLongDescription(LocalizationTool.GetString(brimoraklong)))
                    .AddToggle(Toggle.New(GetKey("kalavakus"), defaultValue: true, LocalizationTool.GetString(kalavakus))
                    .WithLongDescription(LocalizationTool.GetString(kalavakuslong)))

                    //Хомбрю
                    .AddSubHeader(LocalizationTool.GetString(fun), startExpanded: false)
                    //.AddToggle(Toggle.New(GetKey("galluaspect"), defaultValue: false, LocalizationTool.GetString(galluaspect))
                    //.WithLongDescription(LocalizationTool.GetString(galluaspectlong)))
                    .AddToggle(Toggle.New(GetKey("lilithuaspect"), defaultValue: false, LocalizationTool.GetString(lilithuaspect))
                    .WithLongDescription(LocalizationTool.GetString(lilithuaspectlong)))
                    .AddToggle(Toggle.New(GetKey("demonragelimitless"), defaultValue: false, LocalizationTool.GetString(demonragelimitless))
                    .WithLongDescription(LocalizationTool.GetString(demonragelimitlesslong)))
                    .AddToggle(Toggle.New(GetKey("addminor"), defaultValue: false, LocalizationTool.GetString(addminor))
                    .WithLongDescription(LocalizationTool.GetString(addminorlong)))
                    .AddToggle(Toggle.New(GetKey("addmajor"), defaultValue: false, LocalizationTool.GetString(addmajor))
                    .WithLongDescription(LocalizationTool.GetString(addmajorlong)))
                    .AddToggle(Toggle.New(GetKey("addlord"), defaultValue: false, LocalizationTool.GetString(addlord))
                    .WithLongDescription(LocalizationTool.GetString(addlordlong)))
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
