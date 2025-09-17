using HarmonyLib;
using UnityEngine;
using Verse;

namespace OdysseyPatch
{
    public class OdysseyPatchMod : Mod
    {
        public const string PACKAGE_ID = "odysseypatch.1trickPwnyta";
        public const string PACKAGE_NAME = "1trickPwnyta's Odyssey Patch";

        public static OdysseyPatchSettings Settings;

        public OdysseyPatchMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<OdysseyPatchSettings>();
            
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();
            
            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }

        public override string SettingsCategory() => PACKAGE_NAME;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            OdysseyPatchSettings.DoSettingsWindowContents(inRect);
        }
    }
}
