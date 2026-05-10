using HarmonyLib;
using SpecialSauce.Multipatch;
using Verse;

namespace OdysseyPatch
{
    public class SpecialMod_Multipatch_Odyssey : SpecialMod_Multipatch<SpecialModSettings_Multipatch_Odyssey, Settings>
    {
        public const string PACKAGE_NAME = "1trickPwnyta's Odyssey Patch";
        public const string PACKAGE_ID = "1trickpwnyta.odysseypatch";

        public SpecialMod_Multipatch_Odyssey(ModContentPack content) : base(content)
        {
        }

        protected override string PackageName => PACKAGE_NAME;

        protected override string PackageId => PACKAGE_ID;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            var harmony = new Harmony(PackageId);
            harmony.PatchCategory(HarmonyPatch_Compatibility.EnabledCategory);
            Log.Info("Ready.");
        }
    }
}
