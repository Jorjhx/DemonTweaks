using BlueprintCore.Utils;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.Blueprints;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes;
using DemonFix.Utils;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.ElementsSystem;
using System;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace DemonFix.Patch
{
    internal class DemonPolymorph
    {
        public static void PatchGalluPolymorph()
        {

            var falchion6 = BlueprintTool.GetRef<BlueprintItemWeaponReference>("19bf1da2b5192164c9921e4b6b7c2086");
            var demonicFormIVGalluBuff = BlueprintTool.Get<BlueprintBuff>("051c8dea7acf6aa41b8b1c1f65cda421");
            demonicFormIVGalluBuff.EditComponent<Polymorph>(c =>
            {
                c.m_MainHand = falchion6;
                c.m_Facts = new BlueprintUnitFactReference[]{
                        BlueprintTool.Get<BlueprintAbility>("bd09b025ee2a82f46afab922c4decca9").ToReference<BlueprintUnitFactReference>(),
                        BlueprintTool.Get<BlueprintFeature>("28549cff02334f2cbf6724080944ce42").ToReference<BlueprintUnitFactReference>(),
                        BlueprintTool.Get<BlueprintFeature>("c0622e01eba849b4ea30a9703b3d3db9").ToReference<BlueprintUnitFactReference>()
                };
                c.StrengthBonus = 8;
                c.ConstitutionBonus = 4;
                c.NaturalArmor = 6;
            });
        }
    }
}
