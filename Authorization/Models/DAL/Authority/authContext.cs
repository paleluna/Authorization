using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Models.DAL.Authority;

public partial class authContext : DbContext
{
    public authContext(DbContextOptions<authContext> options)
        : base(options)
    {
    }

    public virtual DbSet<App> Apps { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesUsersApp> RolesUsersApps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<App>(entity =>
        {
            entity.Property(e => e.AppId).HasColumnName("appId");
            entity.Property(e => e.AppName)
                .HasMaxLength(100)
                .HasColumnName("appName");
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.UserLogin);

            entity.Property(e => e.UserLogin)
                .HasMaxLength(255)
                .HasColumnName("userLogin");
            entity.Property(e => e.EmpEmail)
                .HasMaxLength(255)
                .HasColumnName("empEmail");
            entity.Property(e => e.EmpIsBlocked).HasColumnName("empIsBlocked");
            entity.Property(e => e.EmpName)
                .HasMaxLength(255)
                .HasColumnName("empName");
            entity.Property(e => e.EmpPhone)
                .HasMaxLength(255)
                .HasColumnName("empPhone");
            entity.Property(e => e.EmpSurname)
                .HasMaxLength(255)
                .HasColumnName("empSurname");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.Property(e => e.DataCreated).HasColumnType("datetime");
            entity.Property(e => e.DataRevoked).HasColumnType("datetime");
            entity.Property(e => e.Expiration).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsExpired).HasColumnName("isExpired");
            entity.Property(e => e.Token).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshTokens_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.AppId).HasColumnName("appId");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(255)
                .HasColumnName("roleDescription");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("roleName");

            entity.HasOne(d => d.App).WithMany(p => p.Roles)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Apps");
        });

        modelBuilder.Entity<RolesUsersApp>(entity =>
        {
            entity.HasKey(e => new { e.ReleId, e.UserId, e.AppId });

            entity.Property(e => e.ReleId).HasColumnName("releId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.AppId).HasColumnName("appId");

            entity.HasOne(d => d.App).WithMany(p => p.RolesUsersApps)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolesUsersApps_Apps");

            entity.HasOne(d => d.Rele).WithMany(p => p.RolesUsersApps)
                .HasForeignKey(d => d.ReleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolesUsersApps_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.RolesUsersApps)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolesUsersApps_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserLogin)
                .HasMaxLength(255)
                .HasColumnName("userLogin");

            entity.HasOne(d => d.UserLoginNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Employes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
