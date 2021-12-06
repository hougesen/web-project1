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
                  .HasConstraintName("FK__Cities__CountryI__2E1BDC42");
      });

      modelBuilder.Entity<Country>(entity =>
      {
        entity.HasIndex(e => e.CountryName, "UQ__Countrie__E056F201D85A6EE6")
                  .IsUnique();

        entity.Property(e => e.CountryName)
                  .HasMaxLength(3)
                  .IsUnicode(false);
      });

      modelBuilder.Entity<Department>(entity =>
      {
        entity.HasIndex(e => e.DepartmentName, "UQ__Departme__D949CC34B56F119D")
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

        entity.HasIndex(e => e.UserId, "UQ__DriverIn__1788CC4D24004C50")
                  .IsUnique();

        entity.HasIndex(e => e.LorryLicenceId, "UQ__DriverIn__1B915FF255047D4A")
                  .IsUnique();

        entity.HasIndex(e => e.Eucertificate, "UQ__DriverIn__48E894762F02CE10")
                  .IsUnique();

        entity.HasIndex(e => e.DriverLicenceId, "UQ__DriverIn__9A7F9B942059EEF0")
                  .IsUnique();

        entity.Property(e => e.Eucertificate).HasColumnName("EUCertificate");

        entity.HasOne(d => d.DriverLicence)
                  .WithOne(p => p.DriverInformationDriverLicence)
                  .HasForeignKey<DriverInformation>(d => d.DriverLicenceId)
                  .HasConstraintName("FK__DriverInf__Drive__3F466844");

        entity.HasOne(d => d.EucertificateNavigation)
                  .WithOne(p => p.DriverInformationEucertificateNavigation)
                  .HasForeignKey<DriverInformation>(d => d.Eucertificate)
                  .HasConstraintName("FK__DriverInf__EUCer__412EB0B6");

        entity.HasOne(d => d.Location)
                  .WithMany(p => p.DriverInformations)
                  .HasForeignKey(d => d.LocationId)
                  .HasConstraintName("FK__DriverInf__Locat__3E52440B");

        entity.HasOne(d => d.LorryLicence)
                  .WithOne(p => p.DriverInformationLorryLicence)
                  .HasForeignKey<DriverInformation>(d => d.LorryLicenceId)
                  .HasConstraintName("FK__DriverInf__Lorry__403A8C7D");

        entity.HasOne(d => d.User)
                  .WithOne(p => p.DriverInformation)
                  .HasForeignKey<DriverInformation>(d => d.UserId)
                  .HasConstraintName("FK__DriverInf__UserI__3D5E1FD2");
      });

      modelBuilder.Entity<DriversAvailable>(entity =>
      {
        entity.ToTable("DriversAvailable");

        entity.Property(e => e.DriversAvailableDate).HasColumnType("date");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.DriversAvailables)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__DriversAv__UserI__5CD6CB2B");
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
                  .HasConstraintName("FK__Licences__Licenc__36B12243");
      });

      modelBuilder.Entity<LicenceType>(entity =>
      {
        entity.HasIndex(e => e.LicenceTypeName, "UQ__LicenceT__CFCA78ACB7D080E6")
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
                  .HasConstraintName("FK__Locations__CityI__30F848ED");
      });

      modelBuilder.Entity<Route>(entity =>
      {
        entity.Property(e => e.RouteDescription).HasColumnType("text");

        entity.Property(e => e.RouteEndDate).HasColumnType("datetime");

        entity.Property(e => e.RouteStartDate).HasColumnType("datetime");

        entity.HasOne(d => d.Department)
                  .WithMany(p => p.Routes)
                  .HasForeignKey(d => d.DepartmentId)
                  .HasConstraintName("FK__Routes__Departme__4D94879B");

        entity.HasOne(d => d.Driver)
                  .WithMany(p => p.Routes)
                  .HasForeignKey(d => d.DriverId)
                  .HasConstraintName("FK__Routes__DriverId__49C3F6B7");

        entity.HasOne(d => d.RouteEndLocation)
                  .WithMany(p => p.RouteRouteEndLocations)
                  .HasForeignKey(d => d.RouteEndLocationId)
                  .HasConstraintName("FK__Routes__RouteEnd__4CA06362");

        entity.HasOne(d => d.RouteStartLocation)
                  .WithMany(p => p.RouteRouteStartLocations)
                  .HasForeignKey(d => d.RouteStartLocationId)
                  .HasConstraintName("FK__Routes__RouteSta__4BAC3F29");

        entity.HasOne(d => d.RouteStatus)
                  .WithMany(p => p.Routes)
                  .HasForeignKey(d => d.RouteStatusId)
                  .HasConstraintName("FK__Routes__RouteSta__4AB81AF0");
      });

      modelBuilder.Entity<RouteStatus>(entity =>
      {
        entity.ToTable("RouteStatus");

        entity.HasIndex(e => e.RouteStatusName, "UQ__RouteSta__CC835486B2825A9D")
                  .IsUnique();

        entity.Property(e => e.RouteStatusName)
                  .HasMaxLength(50)
                  .IsUnicode(false);
      });

      modelBuilder.Entity<SignUpDriver>(entity =>
      {
        entity.HasKey(e => new { e.UserId, e.RouteId })
                  .HasName("PK__SignUpDr__DF81B5F88B0CFF07");

        entity.HasOne(d => d.Route)
                  .WithMany(p => p.SignUpDrivers)
                  .HasForeignKey(d => d.RouteId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__SignUpDri__Route__60A75C0F");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.SignUpDrivers)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__SignUpDri__UserI__5FB337D6");
      });

      modelBuilder.Entity<User>(entity =>
      {
        entity.HasIndex(e => e.UserEmail, "UQ__Users__08638DF8BC7C1F3D")
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
                  .HasConstraintName("FK__Users__UserTypeI__286302EC");
      });

      modelBuilder.Entity<UserType>(entity =>
      {
        entity.HasIndex(e => e.UserTypeName, "UQ__UserType__9262CB7107BEDF83")
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
