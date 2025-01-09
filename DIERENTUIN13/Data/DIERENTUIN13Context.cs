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

        public DbSet<Animal> Animal { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Enclosure> Enclosure { get; set; }
        public DbSet<Zoo> Zoo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
    }

}