using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class aaoContext : DbContext
    {
        public aaoContext()
        {
        }

        public aaoContext(DbContextOptions<aaoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DriverInformation> DriverInformations { get; set; }
        public virtual DbSet<Licence> Licences { get; set; }
        public virtual DbSet<LicenceType> LicenceTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<RouteStatus> RouteStatuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=HOUGERZR\\SQLEXPRESS;Database=aao;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Cities__CountryI__3E52440B");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryName)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DriverInformation>(entity =>
            {
                entity.ToTable("DriverInformation");

                entity.HasIndex(e => e.UserId, "UQ__DriverIn__1788CC4DE8C8E873")
                    .IsUnique();

                entity.HasIndex(e => e.LorryLicenceId, "UQ__DriverIn__1B915FF2FAE83AB6")
                    .IsUnique();

                entity.HasIndex(e => e.Eucertificate, "UQ__DriverIn__48E89476C93A12B6")
                    .IsUnique();

                entity.HasIndex(e => e.DriverLicenceId, "UQ__DriverIn__9A7F9B9422C58C7B")
                    .IsUnique();

                entity.Property(e => e.Eucertificate).HasColumnName("EUCertificate");

                entity.HasOne(d => d.DriverLicence)
                    .WithOne(p => p.DriverInformationDriverLicence)
                    .HasForeignKey<DriverInformation>(d => d.DriverLicenceId)
                    .HasConstraintName("FK__DriverInf__Drive__4E88ABD4");

                entity.HasOne(d => d.EucertificateNavigation)
                    .WithOne(p => p.DriverInformationEucertificateNavigation)
                    .HasForeignKey<DriverInformation>(d => d.Eucertificate)
                    .HasConstraintName("FK__DriverInf__EUCer__5070F446");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.DriverInformations)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__DriverInf__Locat__4D94879B");

                entity.HasOne(d => d.LorryLicence)
                    .WithOne(p => p.DriverInformationLorryLicence)
                    .HasForeignKey<DriverInformation>(d => d.LorryLicenceId)
                    .HasConstraintName("FK__DriverInf__Lorry__4F7CD00D");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.DriverInformation)
                    .HasForeignKey<DriverInformation>(d => d.UserId)
                    .HasConstraintName("FK__DriverInf__UserI__4CA06362");
            });

            modelBuilder.Entity<Licence>(entity =>
            {
                entity.Property(e => e.LicenceExpirationDate).HasColumnType("date");

                entity.Property(e => e.LicenceImage)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.LicenceType)
                    .WithMany(p => p.Licences)
                    .HasForeignKey(d => d.LicenceTypeId)
                    .HasConstraintName("FK__Licences__Licenc__45F365D3");
            });

            modelBuilder.Entity<LicenceType>(entity =>
            {
                entity.Property(e => e.LicenceTypeName)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LocationPostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__Locations__CityI__412EB0B6");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.RouteDescription).HasColumnType("text");

                entity.Property(e => e.RouteEndDate).HasColumnType("datetime");

                entity.Property(e => e.RouteId).ValueGeneratedOnAdd();

                entity.Property(e => e.RouteStartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RouteStatus>(entity =>
            {
                entity.ToTable("RouteStatus");

                entity.Property(e => e.RouteStatusName)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserEmail, "UQ__Users__08638DF861F71845")
                    .IsUnique();

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserFullName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK__Users__UserTypeI__398D8EEE");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.UserTypeName)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
