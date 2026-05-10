using Verse;

namespace OdysseyPatch.VacuumIntensityRoomStat
{
    public class RoomStatWorker_VacuumIntensity : RoomStatWorker
    {
        public bool IsHidden(Room room) => !Settings.VacuumIntensityRoomStat.Enabled() || !room.Map.Biome.inVacuum;

        public override float GetScore(Room room) => room.Vacuum;
    }
}
