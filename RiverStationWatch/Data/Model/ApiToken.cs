using System.ComponentModel.DataAnnotations;

namespace RiverStationWatch.Data.Model
{
    public class ApiToken
    {
        [Key]
        public Guid Id { get; set; }
        public string Token { get; set; }
        //[Required]
        public DateTime Validity { get; set; }
    }
}
