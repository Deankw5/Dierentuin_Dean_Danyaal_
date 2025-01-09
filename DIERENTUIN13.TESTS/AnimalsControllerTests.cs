using DIERENTUIN13.Controllers;
using DIERENTUIN13.Data;
using DIERENTUIN13.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DIERENTUIN13.Tests
{
    public class AnimalsControllerTests
    {
        private readonly DIERENTUIN13Context _context;
        private readonly AnimalsController _controller;

        public AnimalsControllerTests()
        {
            var options = new DbContextOptionsBuilder<DIERENTUIN13Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DIERENTUIN13Context(options);
            _controller = new AnimalsController(_context);

            // Seed the database
            _context.Animal.AddRange(
                new Animal { Id = 1, Name = "Lion", Species = "Panthera leo", Category = new Category { Name = "Mammal" } },
                new Animal { Id = 2, Name = "Elephant", Species = "Loxodonta", Category = new Category { Name = "Mammal" } }
            );
            _context.SaveChanges();
        }

       
    }
}

