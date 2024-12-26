using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDbContext : DbContext
{
    public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
    { }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }
    public DbSet<ProjectComment> ProjectComments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Skill>(s =>
        {
            s.HasKey(item => item.Id);
        });
        
        builder.Entity<UserSkill>(s =>
        {
            s.HasKey(item => item.Id);
            
            s.HasOne(item => item.Skill)
                .WithMany(item => item.UserSkills)
                .HasForeignKey(item => item.IdSkill)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ProjectComment>(e =>
        {
            e.HasKey(item => item.Id);
            
            e.HasOne(item => item.Project)
                .WithMany(item => item.Comments)
                .HasForeignKey(item => item.IdProject)
                .OnDelete(DeleteBehavior.Restrict);
            
            e.HasOne(item => item.User)
                .WithMany(item => item.Comments)
                .HasForeignKey(item => item.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<User>(e =>
        {
            e.HasKey(item => item.Id);
            
            e.HasMany(item => item.Skills)
                .WithOne(item => item.User)
                .HasForeignKey(item => item.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<Project>(e =>
        {
            e.HasKey(item => item.Id);
            
            e.HasOne(item => item.Freelancer)
                .WithMany(item => item.FreelanceProjects)
                .HasForeignKey(item => item.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict);
            
            e.HasOne(item => item.Client)
                .WithMany(item => item.OwnedProjects)
                .HasForeignKey(item => item.IdClient)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        base.OnModelCreating(builder);
    }
}