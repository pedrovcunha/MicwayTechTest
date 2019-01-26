using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MicwayTechTest.Models
{
    public partial class DriversRDSStorageDbContext : DbContext
    {
        public DriversRDSStorageDbContext()
        {
        }

        public DriversRDSStorageDbContext(DbContextOptions<DriversRDSStorageDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Driver> Driver { get; set; }

        
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=micwaytestdbinstance.caqbw1dpdyst.ap-southeast-2.rds.amazonaws.com;Initial Catalog=DriversRDSStorageDb;User ID = MicWayTest; Password = MicWayTest; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
