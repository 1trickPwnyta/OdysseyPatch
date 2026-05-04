using HarmonyLib;
using SpecialSauce.Mod;
using Verse;

namespace OdysseyPatch
{
    public class Mod_OdysseyPatch : SpecialMod<ModSettings_DLCPatch_Odyssey>
    {
        public const string PACKAGE_NAME = "1trickPwnyta's Odyssey Patch";
        public const string PACKAGE_ID = "1trickPwnyta.odysseypatch";

        public Mod_OdysseyPatch(ModContentPack content) : base(content)
        {
        }

        protected override string PackageName => PACKAGE_NAME;

        protected override string PackageId => PACKAGE_ID;

        protected override bool LoadSettingsEarly => true;

        protected override void OnInitialized()
        {
            var harmony = new Harmony(PackageId);
            harmony.PatchAllUncategorized();
            Log.Info("Ready.");
        }
    }
}
