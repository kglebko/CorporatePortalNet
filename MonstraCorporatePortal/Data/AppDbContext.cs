using Microsoft.EntityFrameworkCore;
using MonstraCorporatePortal.Models;

namespace MonstraCorporatePortal.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Collaborator> Collaborators => Set<Collaborator>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<WorkFormat> WorkFormats => Set<WorkFormat>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Collaborator>().ToTable("collaborators");
        modelBuilder.Entity<Position>().ToTable("positions");
        modelBuilder.Entity<Department>().ToTable("departments");
        modelBuilder.Entity<WorkFormat>().ToTable("workformats");
        modelBuilder.Entity<Organization>().ToTable("organizations");
        modelBuilder.Entity<Role>().ToTable("roles");
        
        modelBuilder.Entity<Collaborator>().Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
        modelBuilder.Entity<Position>().Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
        modelBuilder.Entity<Department>().Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
        modelBuilder.Entity<WorkFormat>().Property(w => w.Id).HasColumnName("id").ValueGeneratedOnAdd();
        modelBuilder.Entity<Organization>().Property(o => o.Id).HasColumnName("id").ValueGeneratedOnAdd();
        modelBuilder.Entity<Role>().Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();
        
        
        modelBuilder.Entity<Collaborator>()
            .HasOne(c => c.Position)
            .WithMany(p => p.Collaborators)
            .HasForeignKey(c => c.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Collaborator>()
            .HasOne(c => c.Department)
            .WithMany(d => d.Collaborators)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Collaborator>()
            .HasOne(c => c.WorkFormat)
            .WithMany(w => w.Collaborators)
            .HasForeignKey(c => c.WorkFormatId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        modelBuilder.Entity<Collaborator>()
            .HasOne(c => c.Organization)
            .WithMany(o => o.Collaborators)
            .HasForeignKey(c => c.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Collaborator>()
            .HasOne(c => c.Role)
            .WithMany(r => r.Collaborators)
            .HasForeignKey(c => c.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Collaborator>().Property(c => c.FullName).HasColumnName("full_name").IsRequired().HasMaxLength(150);
        modelBuilder.Entity<Collaborator>().Property(c => c.BirthDate).HasColumnName("birth_date").HasColumnType("date").IsRequired();
        modelBuilder.Entity<Collaborator>().Property(c => c.PositionId).HasColumnName("position_id").IsRequired();
        modelBuilder.Entity<Collaborator>().Property(c => c.DepartmentId).HasColumnName("department_id").IsRequired();
        modelBuilder.Entity<Collaborator>().Property(c => c.WorkFormatId).HasColumnName("work_format_id").IsRequired(false);
        modelBuilder.Entity<Collaborator>().Property(c => c.OrganizationId).HasColumnName("organization_id").IsRequired();
        modelBuilder.Entity<Collaborator>().Property(c => c.RoleId).HasColumnName("role_id").IsRequired();
        modelBuilder.Entity<Collaborator>().Property(c => c.Login).HasColumnName("login").IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Collaborator>().Property(c => c.LoginLowercase).HasColumnName("login_lowercase").IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Collaborator>().Property(c => c.Email).HasColumnName("email").IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Collaborator>().Property(c => c.MobilePhone).HasColumnName("mobile_phone").HasMaxLength(20).IsRequired(false);;
        modelBuilder.Entity<Collaborator>().Property(c => c.InternalPhone).HasColumnName("internal_phone").HasMaxLength(20).IsRequired(false);;
        modelBuilder.Entity<Collaborator>().Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();


        modelBuilder.Entity<Position>().Property(p => p.Name).HasColumnName("name").IsRequired();
        modelBuilder.Entity<Department>().Property(d => d.Name).HasColumnName("name").IsRequired();
        modelBuilder.Entity<WorkFormat>().Property(w => w.Name).HasColumnName("name").IsRequired();
        modelBuilder.Entity<Organization>().Property(o => o.Name).HasColumnName("name").IsRequired();
        modelBuilder.Entity<Role>().Property(r => r.Name).HasColumnName("name").IsRequired();
    }
}
