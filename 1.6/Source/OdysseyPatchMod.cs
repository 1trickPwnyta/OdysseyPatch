using HarmonyLib;
using Verse;

namespace OdysseyPatch
{
    public class OdysseyPatchMod : Mod
    {
        public const string PACKAGE_ID = "odysseypatch.1trickPwnyta";
        public const string PACKAGE_NAME = "Odyssey Patch";

        public OdysseyPatchMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
