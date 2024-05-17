using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RiverStationWatch.Data.Model;

namespace RiverStationWatch.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Station>()
                .HasIndex(u => u.StationName)
                .IsUnique();


        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<ApiToken> StationTokens { get; set; }
        public DbSet<SmtpConfig> SmtpConfigs { get; set; }
    }
}
