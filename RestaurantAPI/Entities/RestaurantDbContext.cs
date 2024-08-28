using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities;
public class RestaurantDbContext : DbContext
    {
    private string _connectionString = "Server=DESKTOP-PGNQOR2\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True; Encrypt=True;TrustServerCertificate=True";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .Property(u => u.Email)
        .IsRequired();

        modelBuilder.Entity<Role>()
            .Property(r => r.Name)
            .IsRequired();

        modelBuilder.Entity<Restaurant>()
        .Property(r => r.Name)
        .IsRequired()
        .HasMaxLength(25);
 

        modelBuilder.Entity<Dish>()
        .Property(d => d.Name)
        .IsRequired();

        modelBuilder.Entity<Dish>()
            .Property(d => d.Price)
            .HasColumnType("decimal(18,2)");


        modelBuilder.Entity<Address>()
        .Property(c => c.City)
        .IsRequired()
        .HasMaxLength (50);

        modelBuilder.Entity<Address>()
        .Property(s => s.Street)
        .IsRequired()
        .HasMaxLength(50);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

}
