using System.ComponentModel.DataAnnotations;

namespace RiverStationWatch.Data.Model
{
    public class SmtpConfig
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SmtpServer { get; set; }

        [Required]
        public int Port { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
