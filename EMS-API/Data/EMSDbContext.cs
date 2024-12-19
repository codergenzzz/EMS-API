using EMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS_API.Data
{
    public class EMSDbContext : DbContext
    {
        public EMSDbContext(DbContextOptions<EMSDbContext> options) : base(options)
        {
        }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ProfileDevice> ProfileDevices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Account - Profile (1-1)
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profile)
                .WithOne(p => p.Account)
                .HasForeignKey<Profile>(p => p.AccountId);

            // Account - Role (Many-to-1)
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Role)
                .WithMany(r => r.Accounts)
                .HasForeignKey(a => a.RoleId);

            // Profile - Device (Many-to-Many)
            modelBuilder.Entity<ProfileDevice>()
                .HasKey(pd => new { pd.ProfileID, pd.DeviceID });

            modelBuilder.Entity<ProfileDevice>()
                .HasOne(pd => pd.Profile)
                .WithMany(p => p.ProfileDevices)
                .HasForeignKey(pd => pd.ProfileID);

            modelBuilder.Entity<ProfileDevice>()
                .HasOne(pd => pd.Device)
                .WithMany(d => d.ProfileDevices)
                .HasForeignKey(pd => pd.DeviceID);

            // Device - Log (1-to-Many)
            modelBuilder.Entity<Log>()
                .HasOne(l => l.Device)
                .WithMany(d => d.Logs)
                .HasForeignKey(l => l.DeviceId);
        }
    }

}
