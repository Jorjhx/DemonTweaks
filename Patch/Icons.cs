using BlueprintCore.Utils;
using DemonFix.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemonFix.Patch
{
    class PatchIcons
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("DemonFix.Icons");
        [HarmonyPriority(Priority.Last)]
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchDemonIcons();
            }
            static void PatchDemonIcons()
            {
                if (!Main.Settings.Icons)
                {
                    return;
                }
                var demonRageIcon = AssetLoader.LoadInternal("Abilities", "DemonRage.png");
                var babauAspectIcon = AssetLoader.LoadInternal("Abilities", "Baubau.png");
                var brimorakAspectIcon = AssetLoader.LoadInternal("Abilities", "Brimorak.png");
                var incubusAspectIcon = AssetLoader.LoadInternal("Abilities", "Incubus.png");
                var kalavakusAspectIcon = AssetLoader.LoadInternal("Abilities", "Kalavakus.png");
                var nabasuAspectIcon = AssetLoader.LoadInternal("Abilities", "Nabasu.png");
                var schirAspectIcon = AssetLoader.LoadInternal("Abilities", "Schir.png");
                var succubusAspectIcon = AssetLoader.LoadInternal("Abilities", "Succubus.png");
                var vrockAspectIcon = AssetLoader.LoadInternal("Abilities", "Vrock.png");
                var balorAspectIcon = AssetLoader.LoadInternal("Abilities", "Balor.png");
                var coloxusAspectIcon = AssetLoader.LoadInternal("Abilities", "Coloxus.png");
                var omoxAspectIcon = AssetLoader.LoadInternal("Abilities", "Omox.png");
                var shadowDemonAspectIcon = AssetLoader.LoadInternal("Abilities", "ShadowDemon.png");
                var vavakiaAspectIcon = AssetLoader.LoadInternal("Abilities", "Vavakia.png");
                var vrolikaiAspectIcon = AssetLoader.LoadInternal("Abilities", "Vrolikai.png");
                var areshkagalAspectIcon = AssetLoader.LoadInternal("Abilities", "Areshkagal.png");
                var deskariAspectIcon = AssetLoader.LoadInternal("Abilities", "Deskari.png");
                var kabririAspectIcon = AssetLoader.LoadInternal("Abilities", "Kabriri.png");
                var nocticulaAspectIcon = AssetLoader.LoadInternal("Abilities", "Nocticula.png");
                var pazuzuAspectIcon = AssetLoader.LoadInternal("Abilities", "Pazuzu.png");
                var socothbenothAspectIcon = AssetLoader.LoadInternal("Abilities", "Socothbenoth.png");
                ///
                var demonRageFeature = BlueprintTool.Get<BlueprintFeature>("6a8af3f208a0fa747a465b70b7043019");
                var babauAspectFeature = BlueprintTool.Get<BlueprintFeature>("99a34a0fa0c3a154fbc5b11fe2d18009");
                var brimorakAspectFeature = BlueprintTool.Get<BlueprintFeature>("28f08ce20ee81b24abfd49404e4b9577");
                var incubusAspectFeature = BlueprintTool.Get<BlueprintFeature>("64c6b4e233e718d429dab47eb3a25ac6");
                var kalavakusAspectFeature = BlueprintTool.Get<BlueprintFeature>("4f94ea9a5c2779a458263976dfa02340");
                var nabasuAspectFeature = BlueprintTool.Get<BlueprintFeature>("cadc545dd171fb54ab4e62f8f4e0b670");
                var schirAspectFeature = BlueprintTool.Get<BlueprintFeature>("90f1347b3f40dc94aa254235cb8bcda2");
                var succubusAspectFeature = BlueprintTool.Get<BlueprintFeature>("709ae9be4d75f094f84f26bccb07351d");
                var vrockAspectFeature = BlueprintTool.Get<BlueprintFeature>("38a54d808e7e1d8479d309a7e8b0ab3e");
                var balorAspectFeature = BlueprintTool.Get<BlueprintFeature>("45507637ea8b28948a21c9ecbfa664e4");
                var coloxusAspectFeature = BlueprintTool.Get<BlueprintFeature>("04f5985258e1d594280b5e02916a6326");
                var omoxAspectFeature = BlueprintTool.Get<BlueprintFeature>("daf030c7563b2664eb1031d91eaae7ab");
                var shadowDemonAspectFeature = BlueprintTool.Get<BlueprintFeature>("18d0dafd5a1f8c043b5f01539f7b096c");
                var vavakiaAspectFeature = BlueprintTool.Get<BlueprintFeature>("3df57316028799448aa1b18cad1ad939");
                var vrolikaiAspectFeature = BlueprintTool.Get<BlueprintFeature>("0ed608f1a0695cd4cb80bf6d415ab295");
                var areshkagalAspectFeature = BlueprintTool.Get<BlueprintFeature>("a4e7223c6d8a7b941b9464ae73b59bcc");
                var deskariAspectFeature = BlueprintTool.Get<BlueprintFeature>("60f57fe80fa1986478f474c0fb5e90ac");
                var kabririAspectFeature = BlueprintTool.Get<BlueprintFeature>("043932e79c20f1547b3f20499df7dd7a");
                var nocticulaAspectFeature = BlueprintTool.Get<BlueprintFeature>("d48e0039bcfde694d8691418d12ebea1");
                var pazuzuAspectFeature = BlueprintTool.Get<BlueprintFeature>("fd126181b5886e54e88d9f7ed09d077b");
                var socothbenothAspectFeature = BlueprintTool.Get<BlueprintFeature>("089746782a827754cbdaf18d5ea6a5a2");
                ///
                demonRageFeature.m_Icon = demonRageIcon;
                babauAspectFeature.m_Icon = babauAspectIcon;
                brimorakAspectFeature.m_Icon = brimorakAspectIcon;
                incubusAspectFeature.m_Icon = incubusAspectIcon;
                kalavakusAspectFeature.m_Icon = kalavakusAspectIcon;
                nabasuAspectFeature.m_Icon = nabasuAspectIcon;
                schirAspectFeature.m_Icon = schirAspectIcon;
                succubusAspectFeature.m_Icon = succubusAspectIcon;
                vrockAspectFeature.m_Icon = vrockAspectIcon;
                balorAspectFeature.m_Icon = balorAspectIcon;
                coloxusAspectFeature.m_Icon = coloxusAspectIcon;
                omoxAspectFeature.m_Icon = omoxAspectIcon;
                shadowDemonAspectFeature.m_Icon = shadowDemonAspectIcon;
                vavakiaAspectFeature.m_Icon = vavakiaAspectIcon;
                vrolikaiAspectFeature.m_Icon = vrolikaiAspectIcon;
                areshkagalAspectFeature.m_Icon = areshkagalAspectIcon;
                deskariAspectFeature.m_Icon = deskariAspectIcon;
                kabririAspectFeature.m_Icon = kabririAspectIcon;
                nocticulaAspectFeature.m_Icon = nocticulaAspectIcon;
                pazuzuAspectFeature.m_Icon = pazuzuAspectIcon;
                socothbenothAspectFeature.m_Icon = socothbenothAspectIcon;
                ///
                var demonRageAbility = BlueprintTool.Get<BlueprintActivatableAbility>("0999f99d6157e5c4888f4cfe2d1ce9d6");
                var demonRageAbility2 = BlueprintTool.Get<BlueprintAbility>("260daa5144194a8ab5117ff568b680f5");
                var babauAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("e24fbd97558f06b45a09c7fbe7170a55");
                var brimorakAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("e642444d21a4dab4ea07cd042e6f9dc0");
                var incubusAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("55c6e91192b92b8479fa66d6aee33074");
                var kalavakusAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("37bfe9e5535e54c49b248bd84305ebd5");
                var nabasuAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("868c4957c5671114eaaf8e0b6b55ad3f");
                var schirAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("fae00e8f4de9cd54da800d383ede7812");
                var succubusAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("8a474cae6e2788a498f616d36b56b5d2");
                var vrockAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("600cf1ff1d381d8488faed4e7fbda865");
                var balorAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("3070984d4c8bd4f48877337da6c7535d");
                var coloxusAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("49e1df551bc9cdc499930be39a3fc8cf");
                var omoxAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("e305991cb9461a04a97e4f5b02b8b767");
                var shadowDemonAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("0d817aa4f8bc00541a43ea2f822d124b");
                var vavakiaAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("b6dc815e86a12654eb7f78c5f14008df");
                var vrolikaiAspectAbility = BlueprintTool.Get<BlueprintActivatableAbility>("df9e7bbc606b0cd4087ee2d08cc2c09b");
                ///
                demonRageAbility.m_Icon = demonRageIcon;
                demonRageAbility2.m_Icon = demonRageIcon;
                babauAspectAbility.m_Icon = babauAspectIcon;
                brimorakAspectAbility.m_Icon = brimorakAspectIcon;
                incubusAspectAbility.m_Icon = incubusAspectIcon;
                kalavakusAspectAbility.m_Icon = kalavakusAspectIcon;
                nabasuAspectAbility.m_Icon = nabasuAspectIcon;
                schirAspectAbility.m_Icon = schirAspectIcon;
                succubusAspectAbility.m_Icon = succubusAspectIcon;
                vrockAspectAbility.m_Icon = vrockAspectIcon;
                balorAspectAbility.m_Icon = balorAspectIcon;
                coloxusAspectAbility.m_Icon = coloxusAspectIcon;
                omoxAspectAbility.m_Icon = omoxAspectIcon;
                shadowDemonAspectAbility.m_Icon = shadowDemonAspectIcon;
                vavakiaAspectAbility.m_Icon = vavakiaAspectIcon;
                vrolikaiAspectAbility.m_Icon = vrolikaiAspectIcon;
                ///
                var demonRageBuff = BlueprintTool.Get<BlueprintBuff>("36ca5ecd8e755a34f8da6b42ad4c965f");
                var babauAspectBuff = BlueprintTool.Get<BlueprintBuff>("756b77ed5c070b8489922ad66da84df4");
                var brimorakAspectBuff = BlueprintTool.Get<BlueprintBuff>("f154542e0b97908479a578dd7bf6d3f7");
                var incubusAspectBuff = BlueprintTool.Get<BlueprintBuff>("491e5c7e0acfa1149ad901aecf1fb657");
                var kalavakusAspectBuff = BlueprintTool.Get<BlueprintBuff>("c9cdd3af3c5f93c4fb8e9119adaa582e");
                var nabasuAspectBuff = BlueprintTool.Get<BlueprintBuff>("de74410d47d41de4cbae7ea8e341e9af");
                var schirAspectBuff = BlueprintTool.Get<BlueprintBuff>("b0b1dfa396e137749bd628b1f82d1160");
                var succubusAspectBuff = BlueprintTool.Get<BlueprintBuff>("51fa8cdc3754c0946b80e81f7952b77f");
                var vrockAspectBuff = BlueprintTool.Get<BlueprintBuff>("76eb2cd9b1eec0b4681c648d33c5ae3b");
                var balorAspectBuff = BlueprintTool.Get<BlueprintBuff>("516462cc6f2e4774292fc7922393e297");
                var coloxusAspectBuff = BlueprintTool.Get<BlueprintBuff>("303e34666de545d4d8b604d720da41b4");
                var omoxAspectBuff = BlueprintTool.Get<BlueprintBuff>("85b5c055c144ae34c98d3414d51e314e");
                var shadowDemonBuff = BlueprintTool.Get<BlueprintBuff>("8b1e3c9b630d61244a3da3fe5e15cd47");
                var vavakiaAspectBuff = BlueprintTool.Get<BlueprintBuff>("4a032a066bbb4414284a6b49a5653f9d");
                var vrolikaiAspectBuff = BlueprintTool.Get<BlueprintBuff>("b8e52fe4b85bcb540b273fa8aea26339");
                var kabririAspectBuff = BlueprintTool.Get<BlueprintBuff>("64408435cbfd1674b955ddc3b942e22b");
                var socothbenothAspectBuff = BlueprintTool.Get<BlueprintBuff>("006210142499ccb4ea837d4b61506055");
                var pazuzuAspectBuff = BlueprintTool.Get<BlueprintBuff>("46bfd4e6b19df60449cbb2a016b50c7f");
                var areshkagalAspectBuff = BlueprintTool.Get<BlueprintBuff>("663dd94af4271a6479d25d83b60dd067");
                var deskariAspectBuff = BlueprintTool.Get<BlueprintBuff>("1c8b0722a3694854db5b2fa8800575c4");
                ///
                demonRageBuff.m_Icon = demonRageIcon;
                babauAspectBuff.m_Icon = babauAspectIcon;
                brimorakAspectBuff.m_Icon = brimorakAspectIcon;
                incubusAspectBuff.m_Icon = incubusAspectIcon;
                kalavakusAspectBuff.m_Icon = kalavakusAspectIcon;
                nabasuAspectBuff.m_Icon = nabasuAspectIcon;
                schirAspectBuff.m_Icon = schirAspectIcon;
                succubusAspectBuff.m_Icon = succubusAspectIcon;
                vrockAspectBuff.m_Icon = vrockAspectIcon;
                balorAspectBuff.m_Icon = balorAspectIcon;
                coloxusAspectBuff.m_Icon = coloxusAspectIcon;
                omoxAspectBuff.m_Icon = omoxAspectIcon;
                shadowDemonBuff.m_Icon = shadowDemonAspectIcon;
                vavakiaAspectBuff.m_Icon = vavakiaAspectIcon;
                vrolikaiAspectBuff.m_Icon = vrolikaiAspectIcon;
                kabririAspectBuff.m_Icon = deskariAspectIcon;
                socothbenothAspectBuff.m_Icon = socothbenothAspectIcon;
                pazuzuAspectBuff.m_Icon = pazuzuAspectIcon;
                areshkagalAspectBuff.m_Icon = areshkagalAspectIcon;
                deskariAspectBuff.m_Icon = deskariAspectIcon;
                ///
                var demonWingsIcon = AssetLoader.LoadInternal("Abilities", "DemonWings.png");
                var demonWingsFeature = BlueprintTool.Get<BlueprintFeature>("36db25d9e0848f04da604ff9e3d931af");
                var demonWingsAbility = BlueprintTool.Get<BlueprintActivatableAbility>("3c5c902ec6397094184195419a231ee6");
                var demonWingsBuff = BlueprintTool.Get<BlueprintBuff>("3c958be25ab34dc448569331488bee27");
                demonWingsFeature.m_Icon = demonWingsIcon;
                demonWingsAbility.m_Icon = demonWingsIcon;
                demonWingsBuff.m_Icon = demonWingsIcon;
                Logger.Info("Пропатчены иконки");
            }
        }
    }
}
