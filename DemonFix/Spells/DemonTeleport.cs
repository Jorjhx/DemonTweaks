using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using DemonFix.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using System.Collections.Generic;

namespace DemonFix.Spells
{
    [HarmonyPriority(Priority.Last)]
    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    static class BlueprintsCache_Init_Patch
    {
        static bool Initialized;

        static void Postfix()
        {
            if (Initialized) return;
            Initialized = true;
            DemonTeleportFeature();
            PatchDemonTeleport();
        }
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix.Teleport");
        private static readonly string FeatName = "DemonTeleportFeature";
        private static readonly string FeatGuid = "b96fd434-d5fb-4d09-aa76-cb602000972d";
        private static readonly string FeatDescr = "DemonTeleportFeature.Description";

        public static void DemonTeleportFeature()
        {
            var demonDemonTeleportTrue = BlueprintTool.Get<BlueprintAbility>("32be7f3cce724f57a0f91aa21512b9ae");
            var teleport = BlueprintTool.Get<BlueprintAbility>("b3e8e307811b2a24387c2c9226fb4c10");
            teleport.m_Description = LocalizationTool.GetString(FeatDescr);
            var facts = new List<Blueprint<BlueprintUnitFactReference>>() { teleport };
            FeatureConfigurator.New(FeatName, FeatGuid)
            .SetDisplayName(demonDemonTeleportTrue.m_DisplayName)
            .SetDescription(FeatDescr)
            .SetIcon(demonDemonTeleportTrue.m_Icon)
            .AddFacts(facts)
            .Configure();
        }

        public static void PatchDemonTeleport()
        {
            if (!Main.Settings.Teleport)
            {
                return;
            }
            var demonSpellList = BlueprintTool.Get<BlueprintSpellList>("abb1991bf6e996348bb743471ee7e1c1");
            demonSpellList.SpellsByLevel[4].m_Spells[4] = null;         
            var teleport = BlueprintTool.Get<BlueprintAbility>("b3e8e307811b2a24387c2c9226fb4c10");
            teleport.m_Description = LocalizationTool.GetString(FeatDescr);
            teleport.RemoveComponents<SpellListComponent>();
            teleport.ActionType = UnitCommand.CommandType.Swift;
            teleport.Range = AbilityRange.DoubleMove;
        }
    }
}
