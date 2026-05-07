using SpecialSauce.ModSettings;

namespace OdysseyPatch
{
    public static class Utility
    {
        public static bool Enabled(this Settings key) => Setting<Settings>.Get<bool>(key);
    }
}
