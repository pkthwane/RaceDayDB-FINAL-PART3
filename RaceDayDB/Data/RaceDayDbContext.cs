using Microsoft.EntityFrameworkCore;
using RaceDayDB.Models;

namespace RaceDayDB.Data
{
    public class RaceDayDbContext : DbContext
    {
        public RaceDayDbContext(DbContextOptions<RaceDayDbContext> options)
            : base(options)
        {
        }

        public DbSet<Organizer> Organizers { get; set; }

        public DbSet<Runner> Runners { get; set; }

        public DbSet<Race> Races { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<Sponsor> Sponsors { get; set; }

        public DbSet<RaceSponsor> RaceSponsors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organizer>().ToTable("Organizer");
            modelBuilder.Entity<Runner>().ToTable("Runner");
            modelBuilder.Entity<Race>().ToTable("Race");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Registration>().ToTable("Registration");
            modelBuilder.Entity<Result>().ToTable("Result");
            modelBuilder.Entity<Sponsor>().ToTable("Sponsor");
            modelBuilder.Entity<RaceSponsor>().ToTable("RaceSponsor");

            base.OnModelCreating(modelBuilder);
        }
    }
}
