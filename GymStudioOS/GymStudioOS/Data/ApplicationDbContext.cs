using GymStudioOS.Models.Gym.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymStudioOS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<GymBranch> GymBranches { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<GymUserRole> GymUserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GymUserRole>()
                .HasOne<Gym>(gur => gur.Gym)
                .WithMany()
                .HasForeignKey(gur => gur.GymId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GymUserRole>()
                .HasOne<ApplicationUser>(gur => gur.User)
                .WithMany()
                .HasForeignKey(gur => gur.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Class>()
                .HasOne<Gym>(c => c.Gym)
                .WithMany()
                .HasForeignKey(c => c.GymId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProfile>()
                .HasOne<ApplicationUser>(up => up.User)
                .WithMany()
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
