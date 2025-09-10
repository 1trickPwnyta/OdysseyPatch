using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace OdysseyPatch.OutfitStandBodyType
{
    [HarmonyPatch(typeof(Building_OutfitStand))]
    [HarmonyPatch("BodyTypeDefForRendering")]
    [HarmonyPatch(MethodType.Getter)]
    public static class Patch_Building_OutfitStand_BodyTypeDefForRendering
    {
        public static void Postfix(Building_OutfitStand __instance, ref BodyTypeDef __result)
        {
            if (OdysseyPatchSettings.OutfitStandBodyType)
            {
                Comp_OutfitStandBodyType comp = __instance.TryGetComp<Comp_OutfitStandBodyType>();
                if (comp != null)
                {
                    __result = comp.bodyType;
                }
            }
        }
    }

    [HarmonyPatch(typeof(Building_OutfitStand))]
    [HarmonyPatch("DrawAt")]
    public static class Patch_Building_OutfitStand_DrawAt
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();

            int bodyGraphicIndex = instructionsList.FindIndex(i => i.opcode == OpCodes.Ldsfld && i.operand is FieldInfo f && f == typeof(Building_OutfitStand).Field("bodyGraphic"));
            instructionsList.RemoveAt(bodyGraphicIndex);
            instructionsList.InsertRange(bodyGraphicIndex, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, typeof(Patch_Building_OutfitStand_DrawAt).Method(nameof(GetOutfitStandGraphic)))
            });

            int itemGraphicIndex = instructionsList.FindIndex(i => i.opcode == OpCodes.Call && i.operand is MethodInfo m && m == typeof(Matrix4x4).Method(nameof(Matrix4x4.TRS)));
            instructionsList.InsertRange(itemGraphicIndex, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, typeof(Patch_Building_OutfitStand_DrawAt).Method(nameof(GetItemScale)))
            });
            
            return instructionsList;
        }

        private static Graphic_Multi GetOutfitStandGraphic(Building_OutfitStand stand)
        {
            if (OdysseyPatchSettings.OutfitStandBodyType)
            {
                Comp_OutfitStandBodyType comp = stand.GetComp<Comp_OutfitStandBodyType>();
                if (comp.bodyType != BodyTypeDefOf.Male)
                {
                    return Comp_OutfitStandBodyType.GetGraphicForBodyType(comp.bodyType);
                }
            }
            return typeof(Building_OutfitStand).Field("bodyGraphic").GetValue(null) as Graphic_Multi;
        }

        private static Vector3 GetItemScale(Vector3 scale, Building_OutfitStand stand)
        {
            if (OdysseyPatchSettings.OutfitStandBodyType)
            {
                scale /= 1.2f;
                Comp_OutfitStandBodyType comp = stand.GetComp<Comp_OutfitStandBodyType>();
                if (comp.bodyType == BodyTypeDefOf.Hulk)
                {
                    return scale * Comp_OutfitStandBodyType.hulkScale;
                }
            }
            return scale;
        }
    }
}
