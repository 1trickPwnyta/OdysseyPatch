using SpecialSauce.ModSettings;

namespace OdysseyPatch
{
    public static class Utility
    {
        public static bool CheckSetting(string key) => Setting.Get<bool, ModSettings_DLCPatch_Odyssey>(key);
    }
}
