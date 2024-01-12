using Microsoft.EntityFrameworkCore;

namespace WebTutorial.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Storage> Storages { get; set; }

        public StoreContext() { }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.; Database=Store;Integrated Security=False;TrustServerCertificate=True; Trusted_Connection=True;")
                .UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("products");

                e.HasKey(x => x.Id).HasName("productId");
                e.HasIndex(x => x.Name).IsUnique();

                e.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

                e.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(255);

                e.Property(e => e.Price)
                .HasColumnName("price");

                e.HasOne(x => x.Category)
                .WithMany(c => c.Products)
                .HasConstraintName("GroupToProduct");
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("categories");
                e.HasKey(x => x.Id).HasName("categoryId");

                e.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

                e.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Storage>(e => 
            {
                e.ToTable("Storage");
                e.HasKey(x => x.Id).HasName("storageId");

                e.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

                e.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(255);

                e.HasMany(s => s.Products)
                .WithMany(p => p.Storages)
                .UsingEntity(t => t.ToTable("StorageProduct"));
            });
        }
    }
}
