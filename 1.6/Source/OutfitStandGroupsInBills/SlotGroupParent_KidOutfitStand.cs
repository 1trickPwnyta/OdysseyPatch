using RimWorld;

namespace OdysseyPatch.OutfitStandGroupsInBills
{
    public class SlotGroupParent_KidOutfitStand : SlotGroupParent_OutfitStand
    {
        protected override BodyTypeDef BodyTypeDefForRendering => BodyTypeDefOf.Child;

        protected override float WeaponDrawDistanceFactor => 0.55f;
    }
}
