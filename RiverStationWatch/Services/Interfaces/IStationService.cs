using RiverStationWatch.Data.Model;

namespace RiverStationWatch.Services.Interfaces
{
    public interface IStationService
    {
        //public void AddStation(Station station);
        public void CheckStationForTimeout();
        public void CheckFloodLevelExceedance();
    }
}
