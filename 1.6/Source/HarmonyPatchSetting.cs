using HarmonyLib;
using System;

namespace OdysseyPatch
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true)]
    public class HarmonyPatchSetting : HarmonyAttribute
    {
        public HarmonyPatchSetting(string settingName)
        {
            if (!(bool)typeof(OdysseyPatchSettings).Field(settingName).GetValue(null))
            {
                info.category = "DisabledByHarmonyPatchMod";
            }
        }
    }
}
