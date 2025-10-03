using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace OdysseyPatch.OutfitStandBodyType
{
    [StaticConstructorOnStartup]
    public class Comp_OutfitStandBodyType : ThingComp
    {
        public static readonly float hulkScale = 0.75f;
        private static readonly Dictionary<BodyTypeDef, Graphic_Multi> graphicCache = new Dictionary<BodyTypeDef, Graphic_Multi>();
        private static readonly Dictionary<BodyTypeDef, Texture2D> textureCache = new Dictionary<BodyTypeDef, Texture2D>();
        private static readonly Texture2D baseTexture = ContentFinder<Texture2D>.Get("Things/Building/OutfitStand/OutfitStand_Base_south");
        private static readonly Texture2D headTexture = ContentFinder<Texture2D>.Get("Things/Building/OutfitStand/OutfitStand_Head_south");
        private static readonly Texture2D maleIcon = ContentFinder<Texture2D>.Get("Things/Building/OutfitStand/OutfitStand_MenuIcon_south");
        private static readonly List<BodyTypeDef> bodyTypes = new List<BodyTypeDef>()
        {
            BodyTypeDefOf.Female,
            BodyTypeDefOf.Male,
            BodyTypeDefOf.Fat,
            BodyTypeDefOf.Thin,
            BodyTypeDefOf.Hulk
        };

        static Comp_OutfitStandBodyType()
        {
            foreach (BodyTypeDef def in bodyTypes)
            {
                GetGraphicForBodyType(def);
            }
        }

        public static Graphic_Multi GetGraphicForBodyType(BodyTypeDef def)
        {
            if (def != BodyTypeDefOf.Male)
            {
                if (!graphicCache.ContainsKey(def))
                {
                    graphicCache[def] = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>("OdysseyPatch/OutfitStands/OutfitStand_" + def.defName, ShaderDatabase.Cutout, new Vector2(1.2f, 1.2f) * (def == BodyTypeDefOf.Hulk ? hulkScale : 1f), Color.white);
                    textureCache[def] = TextureUtility.Combine(
                        new TextureUtility.TextureOptions() { tex = baseTexture },
                        new TextureUtility.TextureOptions() { tex = graphicCache[def].MatSouth.mainTexture as Texture2D },
                        new TextureUtility.TextureOptions() { tex = headTexture, offset = new Vector2(0f, def.headOffset.y - 0.05f) });
                }
                return graphicCache[def];
            }
            else
            {
                textureCache[def] = maleIcon;
                return typeof(Building_OutfitStand).Field("bodyGraphic").GetValue(null) as Graphic_Multi;
            }
        }

        public BodyTypeDef bodyType = BodyTypeDefOf.Male;

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (!OdysseyPatchSettings.OutfitStandBodyType)
            {
                yield break;
            }

            yield return new Command_Action()
            {
                defaultLabel = "OdysseyPatch_BodyType".Translate(bodyType.defName),
                defaultDesc = "OdysseyPatch_BodyTypeDesc".Translate(),
                icon = textureCache[bodyType],
                defaultIconColor = parent.def.GetColorForStuff(parent.Stuff),
                action = () =>
                {
                    Find.WindowStack.Add(new FloatMenu(bodyTypes.Select(b => new FloatMenuOption(b.defName, () =>
                    {
                        foreach (Building_OutfitStand stand in Find.Selector.SelectedObjects.OfType<Building_OutfitStand>())
                        {
                            Comp_OutfitStandBodyType comp = stand.GetComp<Comp_OutfitStandBodyType>();
                            if (comp != null)
                            {
                                comp.bodyType = b;
                                typeof(Building_OutfitStand).Method("RecacheGraphics").Invoke(stand, new object[] { });
                            }
                        }
                    }, textureCache[b], parent.def.GetColorForStuff(parent.Stuff))).ToList()));
                },
                groupKeyIgnoreContent = "OdysseyPatch_OutfitStandBodyType".GetHashCode()
            };
        }

        public override void PostExposeData()
        {
            if (OdysseyPatchSettings.OutfitStandBodyType)
            {
                Scribe_Defs.Look(ref bodyType, "bodyType");
                if (Scribe.mode == LoadSaveMode.PostLoadInit && bodyType == null)
                {
                    bodyType = BodyTypeDefOf.Male;
                }
            }
        }
    }
}
