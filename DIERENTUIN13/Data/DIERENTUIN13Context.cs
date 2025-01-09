using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DIERENTUIN13.Models;

namespace DIERENTUIN13.Data
{
    public class DIERENTUIN13Context : DbContext
    {
        public DIERENTUIN13Context(DbContextOptions<DIERENTUIN13Context> options)
            : base(options)
        {
        }

        // DbSet properties for each entity type
        public DbSet<Animal> Animal { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Enclosure> Enclosure { get; set; }
        public DbSet<Zoo> Zoo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and cascading deletes
            ConfigureRelationships(modelBuilder);

            // Add unique constraints to Name properties
            AddUniqueConstraints(modelBuilder);
        }

        // Helper method to configure relationships and cascading deletes
        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enclosure>()
                .HasMany(e => e.Animals)
                .WithOne(a => a.Enclosure)
                .HasForeignKey(a => a.EnclosureId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Zoo>()
                .HasMany(z => z.Enclosures)
                .WithOne(e => e.Zoo)
                .HasForeignKey(e => e.ZooId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Animals)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        // Helper method to add unique constraints to Name properties
        private void AddUniqueConstraints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Enclosure>()
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder.Entity<Zoo>()
                .HasIndex(z => z.Name)
                .IsUnique();
        }
    }
}
