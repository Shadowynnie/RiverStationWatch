using System.ComponentModel.DataAnnotations;

namespace RiverStationWatch.Data.Model
{
    public class Record
    {
        [Key]
        public Guid Id { get; set; }
        public virtual int StationId { get; set; }
        public virtual Station? Station { get; set; } // Nutne na napojeni ID ze Stations na Station ID
        public DateTime TimeStamp { get; set; }
        public int Value { get; set; }
    }
}
