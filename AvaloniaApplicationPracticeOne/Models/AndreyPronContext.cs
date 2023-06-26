using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplicationPracticeOne.Models;

public partial class AndreyPronContext : DbContext
{
    public AndreyPronContext()
    {
    }

    public AndreyPronContext(DbContextOptions<AndreyPronContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User?> Users { get; set; }

    public virtual DbSet<TypeCar> TypeCar { get; set; }
    
    public virtual DbSet<Car> Car { get; set; }
    
    public virtual DbSet<Driver> Driver { get; set; }
    
    public virtual DbSet<RightCategory> RightsCategory { get; set; }
    
    public virtual DbSet<DriverRightsCategory> DriverRightsCategories { get; set; }
    
    public virtual DbSet<Itinerary> Itineraries { get; set; }
    
    public virtual DbSet<Route> Routes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=QWEasd12");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasMaxLength(150)
                .HasColumnName("surname");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("users_id_role_fkey");
        });
        
        modelBuilder.Entity<TypeCar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_car_pkey");

            entity.ToTable("type_car");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });
        
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("car_pkey");

            entity.ToTable("car");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdTypeCar).HasColumnName("id_type_car");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.StateNumber)
                .HasColumnName("state_number");
            entity.Property(e => e.CountPassengers)
                .HasColumnName("number_passengers");
            entity.HasOne(d => d.IdTypeCarNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.IdTypeCar)
                .HasConstraintName("car_id_role_fkey");
            entity.HasIndex(e => e.StateNumber).IsUnique();
        });
        
        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("driver_pkey");

            entity.ToTable("driver");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
             entity.Property(e => e.Birthdate).HasColumnName("birthdate");
        });
        
        modelBuilder.Entity<RightCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rights_category_pkey");

            entity.ToTable("rights_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });
        
        modelBuilder.Entity<DriverRightsCategory>(entity =>
        {
            entity.HasKey(e => new {e.IdDriver, e.IdRightsCategory}).HasName("driver_rights_category_pkey");
            entity.ToTable("driver_rights_category");

            entity.Property(e => e.IdDriver).HasColumnName("id_driver");
            entity.Property(e => e.IdRightsCategory).HasColumnName("id_rights_category");
            entity.HasOne(d => d.IdDriverNavigation).WithMany(p => p.DriverRightsCategories)
                .HasForeignKey(d => d.IdDriver)
                .HasConstraintName("driver_rights_category_id_driver_fkey");
            entity.HasOne(d => d.IdRightsCategoryNavigation).WithMany(p => p.DriverRightsCategories)
                .HasForeignKey(d => d.IdRightsCategory)
                .HasConstraintName("driver_rights_category_id_rights_category_fkey");
        });
        
        modelBuilder.Entity<Itinerary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("itinerary_pkey");

            entity.ToTable("itinerary");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });
        
        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("route_pkey");

            entity.ToTable("route");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdDriver).HasColumnName("id_driver");
            entity.Property(e => e.IdCar).HasColumnName("id_car");
            entity.Property(e => e.IdItinerary).HasColumnName("id_itinerary");
            entity.Property(e => e.NumberPassengers).HasColumnName("number_passengers");
            entity.HasOne(d => d.IdDriverNavigation).WithMany(p => p.Routes)
                .HasForeignKey(d => d.IdDriver)
                .HasConstraintName("route_id_driver_fkey");
            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.Routes)
                .HasForeignKey(d => d.IdCar)
                .HasConstraintName("route_id_car_fkey");
            entity.HasOne(d => d.IdItineraryNavigation).WithMany(p => p.Routes)
                .HasForeignKey(d => d.IdItinerary)
                .HasConstraintName("route_id_itinerary_fkey");
        });
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
