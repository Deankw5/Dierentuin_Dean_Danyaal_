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
        public DIERENTUIN13Context (DbContextOptions<DIERENTUIN13Context> options)
            : base(options)
        {
        }

        public DbSet<DIERENTUIN13.Models.Animal> Animal { get; set; } = default!;
        public DbSet<DIERENTUIN13.Models.Category> Category { get; set; } = default!;
        public DbSet<DIERENTUIN13.Models.Enclosure> Enclosure { get; set; } = default!;
        public DbSet<DIERENTUIN13.Models.Zoo> Zoo { get; set; } = default!;
    }
}
