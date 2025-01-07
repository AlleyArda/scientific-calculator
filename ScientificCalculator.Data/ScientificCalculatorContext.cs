using Microsoft.EntityFrameworkCore;
using ScientificCalculator.Model;

namespace ScientificCalculator.Data;

public class ScientificCalculatorContext : DbContext
{
    public ScientificCalculatorContext(DbContextOptions<ScientificCalculatorContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Calculation> Calculations { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .Property(u => u.IsAdmin)
            .HasDefaultValue(false);
        
        
        base.OnModelCreating(modelBuilder);
    }
    
    
}