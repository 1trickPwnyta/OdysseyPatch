using Verse;

namespace OdysseyPatch.VacuumIntensityRoomStat
{
    public class RoomStatWorker_VacuumIntensity : RoomStatWorker
    {
        public bool IsHidden(Room room) => !Utility.CheckSetting(SpecialModSettings_Multipatch_Odyssey.VACUUM_INTENSITY_ROOM_STAT) || !room.Map.Biome.inVacuum;

        public override float GetScore(Room room) => room.Vacuum;
    }
}
