using HarmonyLib;
using System.Xml;
using Verse;

namespace OdysseyPatch
{
    public class PatchOperationReplaceIf : PatchOperationReplace
    {
        private XmlContainer setting;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            string settingText = setting.node.InnerText;
            return (bool)typeof(OdysseyPatchSettings).Field(settingText).GetValue(null) ? base.ApplyWorker(xml) : true;
        }
    }
}
