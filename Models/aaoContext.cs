using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class AAOContext : DbContext
    {
        public AAOContext()
        {
        }

        public AAOContext(DbContextOptions<AAOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DriverInformation> DriverInformations { get; set; }
        public virtual DbSet<DriversAvailable> DriversAvailables { get; set; }
        public virtual DbSet<Licence> Licences { get; set; }
        public virtual DbSet<LicenceType> LicenceTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<RouteStatus> RouteStatuses { get; set; }
        public virtual DbSet<SignUpDriver> SignUpDrivers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=sql.uclweb.dk;database=uclweb_gr1;user id=uclweb_gr1;password=Odense2021!;");
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
                    .HasConstraintName("FK__Cities__CountryI__489AC854");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(e => e.CountryName, "UQ__Countrie__E056F2010D45F3C4")
                    .IsUnique();

                entity.Property(e => e.CountryName)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.DepartmentName, "UQ__Departme__D949CC3425C6F0AC")
                    .IsUnique();

                entity.Property(e => e.DepartmentContactNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DriverInformation>(entity =>
            {
                entity.ToTable("DriverInformation");

                entity.HasIndex(e => e.UserId, "UQ__DriverIn__1788CC4D48BAE0E0")
                    .IsUnique();

                entity.HasIndex(e => e.LorryLicenceId, "UQ__DriverIn__1B915FF277B578EB")
                    .IsUnique();

                entity.HasIndex(e => e.Eucertificate, "UQ__DriverIn__48E8947694EFBFB6")
                    .IsUnique();

                entity.HasIndex(e => e.DriverLicenceId, "UQ__DriverIn__9A7F9B94CA92C78B")
                    .IsUnique();

                entity.Property(e => e.Eucertificate).HasColumnName("EUCertificate");

                entity.HasOne(d => d.DriverLicence)
                    .WithOne(p => p.DriverInformationDriverLicence)
                    .HasForeignKey<DriverInformation>(d => d.DriverLicenceId)
                    .HasConstraintName("FK__DriverInf__Drive__59C55456");

                entity.HasOne(d => d.EucertificateNavigation)
                    .WithOne(p => p.DriverInformationEucertificateNavigation)
                    .HasForeignKey<DriverInformation>(d => d.Eucertificate)
                    .HasConstraintName("FK__DriverInf__EUCer__5BAD9CC8");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.DriverInformations)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__DriverInf__Locat__58D1301D");

                entity.HasOne(d => d.LorryLicence)
                    .WithOne(p => p.DriverInformationLorryLicence)
                    .HasForeignKey<DriverInformation>(d => d.LorryLicenceId)
                    .HasConstraintName("FK__DriverInf__Lorry__5AB9788F");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.DriverInformation)
                    .HasForeignKey<DriverInformation>(d => d.UserId)
                    .HasConstraintName("FK__DriverInf__UserI__57DD0BE4");
            });

            modelBuilder.Entity<DriversAvailable>(entity =>
            {
                entity.ToTable("DriversAvailable");

                entity.Property(e => e.DriversAvailableDate).HasColumnType("date");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DriversAvailables)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DriversAv__UserI__6AEFE058");
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
                    .HasConstraintName("FK__Licences__Licenc__51300E55");
            });

            modelBuilder.Entity<LicenceType>(entity =>
            {
                entity.HasIndex(e => e.LicenceTypeName, "UQ__LicenceT__CFCA78ACBF0F17A7")
                    .IsUnique();

                entity.Property(e => e.LicenceTypeName)
                    .HasMaxLength(20)
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
                    .HasConstraintName("FK__Locations__CityI__4B7734FF");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.Property(e => e.RouteDescription).HasColumnType("text");

                entity.Property(e => e.RouteEndDate).HasColumnType("datetime");

                entity.Property(e => e.RouteStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__Routes__Departme__681373AD");

                entity.HasOne(d => d.RouteEndLocation)
                    .WithMany(p => p.RouteRouteEndLocations)
                    .HasForeignKey(d => d.RouteEndLocationId)
                    .HasConstraintName("FK__Routes__RouteEnd__671F4F74");

                entity.HasOne(d => d.RouteStartLocation)
                    .WithMany(p => p.RouteRouteStartLocations)
                    .HasForeignKey(d => d.RouteStartLocationId)
                    .HasConstraintName("FK__Routes__RouteSta__662B2B3B");

                entity.HasOne(d => d.RouteStatus)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.RouteStatusId)
                    .HasConstraintName("FK__Routes__RouteSta__65370702");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Routes__UserId__6442E2C9");
            });

            modelBuilder.Entity<RouteStatus>(entity =>
            {
                entity.ToTable("RouteStatus");

                entity.HasIndex(e => e.RouteStatusName, "UQ__RouteSta__CC835486299A50CA")
                    .IsUnique();

                entity.Property(e => e.RouteStatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignUpDriver>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RouteId })
                    .HasName("PK__SignUpDr__DF81B5F8FB09D55A");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.SignUpDrivers)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SignUpDri__Route__6EC0713C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SignUpDrivers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SignUpDri__UserI__6DCC4D03");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserEmail, "UQ__Users__08638DF849840F63")
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
                    .HasConstraintName("FK__Users__UserTypeI__42E1EEFE");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasIndex(e => e.UserTypeName, "UQ__UserType__9262CB71A514CD61")
                    .IsUnique();

                entity.Property(e => e.UserTypeName)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
