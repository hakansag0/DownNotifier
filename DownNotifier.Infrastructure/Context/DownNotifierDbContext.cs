using DownNotifier.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Infrastructure.Context
{
    public class EfConnectionString
    {
        public static string ConnectionString { get; set; }
    }
    public class DownNotifierDbContext : DbContext
    {
        private string _connectionString => EfConnectionString.ConnectionString;
        public DbSet<User> Users { get; set; }
        public DbSet<TargetApp> TargetApps { get; set; }
        public DownNotifierDbContext(DbContextOptions<DownNotifierDbContext> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(_connectionString);
            }
#endif
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyUsersTableModel(modelBuilder);
            ApplyTargetAppsTableModel(modelBuilder);
        }

        private static void ApplyUsersTableModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(t =>
            {
                t.ToTable("users").HasKey(s => s.Id);
                t.HasIndex(s => s.Id).IsUnique();
                t.HasIndex(s => s.Email).IsUnique();


                t.Property(p => p.Id).HasColumnName("Id").HasColumnOrder(1);
                t.Property(p => p.Email).HasColumnName("Email").IsRequired().HasColumnOrder(2);
                t.Property(p => p.Name).HasColumnName("Name").IsRequired().HasColumnOrder(3);
                t.Property(p => p.PasswordHash).HasColumnName("PasswordHash").IsRequired().HasColumnOrder(4);
                t.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt").IsRequired().HasColumnOrder(5);
                t.Property(p => p.CreatedDate).HasColumnName("CreatedDate").HasColumnOrder(6);
            });
        }

        private static void ApplyTargetAppsTableModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TargetApp>(t =>
            {
                t.ToTable("targetApps").HasKey(s => s.Id);
                t.HasIndex(s => s.Id).IsUnique();

                t.Property(p => p.Id).HasColumnName("Id").HasColumnOrder(1);
                t.Property(p => p.UserId).HasColumnName("UserID").IsRequired().HasColumnOrder(2);
                t.Property(p => p.Name).HasColumnName("Name").IsRequired().HasColumnOrder(3);
                t.Property(p => p.URL).HasColumnName("URL").IsRequired().HasColumnOrder(4);
                t.Property(p => p.MonitoringIntervalInSeconds).HasColumnName("MonitoringIntervalInSeconds").IsRequired().HasColumnOrder(5);
                t.Property(p => p.LastCheckDate).HasColumnName("LastCheckDate").IsRequired().HasColumnOrder(6);
                t.Property(p => p.CreatedDate).HasColumnName("CreatedDate").HasColumnOrder(7);
            });
        }
    }
}
