using DogHouseService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogHouseService.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dog>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(d => d.Color)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(d => d.TailLength)
                    .IsRequired()
                    .HasDefaultValue(0);

                entity.Property(d => d.Weight)
                    .IsRequired();
            });
        }
    }
}
