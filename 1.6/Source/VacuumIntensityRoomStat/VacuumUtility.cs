using Verse;

namespace OdysseyPatch.VacuumIntensityRoomStat
{
    public static class VacuumUtility
    {
        public static Room statsRoom;

        public static bool HiddenOrHiddenVacuum(bool isHidden, RoomStatDef def, Room room) => isHidden || ((def.Worker is RoomStatWorker_VacuumIntensity r) && r.IsHidden(room));
    }
}
