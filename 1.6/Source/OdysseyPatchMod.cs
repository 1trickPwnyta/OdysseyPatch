using HarmonyLib;
using UnityEngine;
using Verse;

namespace OdysseyPatch
{
    public class OdysseyPatchMod : Mod
    {
        public const string PACKAGE_ID = "odysseypatch.1trickPwnyta";
        public const string PACKAGE_NAME = "1trickPwnyta's Odyssey Patch";

        public static bool specialSauce = AccessTools.TypeByName("SpecialSauce.SpecialSauceMod") != null;

        public static ModSettings_DLCPatch_Odyssey settings;

        public OdysseyPatchMod(ModContentPack content) : base(content)
        {
            if (specialSauce)
            {
                settings = GetSettings<ModSettings_DLCPatch_Odyssey>();
            }
            else
            {
                GetSettings<OdysseyPatchSettings>();
            }
            
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAllUncategorized();
            
            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }

        public override string SettingsCategory() => PACKAGE_NAME;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            if (specialSauce)
            {
                settings.DrawModSettings(inRect);
            }
            else
            {
                OdysseyPatchSettings.DoSettingsWindowContents(inRect);
            }
        }
    }
}
