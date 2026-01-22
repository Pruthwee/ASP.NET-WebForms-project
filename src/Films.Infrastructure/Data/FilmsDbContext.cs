using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Films.Domain.Entities;

namespace Films.Infrastructure.Data;

public class FilmsDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public FilmsDbContext(DbContextOptions<FilmsDbContext> options) : base(options)
    {
    }

    public DbSet<Actor> Actors => Set<Actor>();
    public DbSet<Film> Films => Set<Film>();
    public DbSet<DirectedBy> Directors => Set<DirectedBy>();
    public DbSet<Sex> Sexes => Set<Sex>();
    public DbSet<TypeUser> TypeUsers => Set<TypeUser>();
    public DbSet<Right> Rights => Set<Right>();
    public DbSet<UserRight> UserRights => Set<UserRight>();
    public DbSet<RefAF> RefAFs => Set<RefAF>();
    public DbSet<RefDAF> RefDAFs => Set<RefDAF>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilmsDbContext).Assembly);

        // Rename Identity tables to match existing schema
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
    }
}
