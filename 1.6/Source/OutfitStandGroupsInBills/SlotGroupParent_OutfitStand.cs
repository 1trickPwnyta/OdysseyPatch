using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace OdysseyPatch.OutfitStandGroupsInBills
{
    public class SlotGroupParent_OutfitStand : Building_OutfitStand, ISlotGroupParent
    {
        private SlotGroup slotGroup;
        private List<IntVec3> cachedOccupiedCells;


        public SlotGroupParent_OutfitStand()
        {
            slotGroup = new SlotGroup(this);
        }

        public bool IgnoreStoredThingsBeauty => def.building.ignoreStoredThingsBeauty;

        public string GroupingLabel => def.building.groupingLabel;

        public int GroupingOrder => def.building.groupingOrder;

        public virtual IEnumerable<IntVec3> AllSlotCells()
        {
            if (!Spawned)
            {
                yield break;
            }

            foreach (IntVec3 item in GenAdj.CellsOccupiedBy(this))
            {
                yield return item;
            }
        }

        public List<IntVec3> AllSlotCellsList()
        {
            if (cachedOccupiedCells == null)
            {
                cachedOccupiedCells = AllSlotCells().ToList();
            }
            return cachedOccupiedCells;
        }

        public SlotGroup GetSlotGroup() => slotGroup;
        
        public void Notify_LostThing(Thing newItem)
        {
            
        }

        public void Notify_ReceivedThing(Thing newItem)
        {
            newItem.DeSpawn();
            if (newItem is Apparel apparel)
            {
                AddApparel(apparel);
            }
            else
            {
                TryAddHeldWeapon(newItem);
            }
        }

        public string SlotYielderLabel() => LabelCap;

        public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
        {
            base.DeSpawn(mode);
            cachedOccupiedCells = null;
        }
    }
}
