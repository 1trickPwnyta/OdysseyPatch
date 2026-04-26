using Verse;

namespace OdysseyPatch.VacuumIntensityRoomStat
{
    public class RoomStatWorker_VacuumIntensity : RoomStatWorker
    {
        public bool IsHidden(Room room) => !OdysseyPatchSettings.VacuumIntensityRoomStat || !room.Map.Biome.inVacuum;

        public override float GetScore(Room room) => room.Vacuum;
    }
}
