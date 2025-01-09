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

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfAnimals()
        {
            // Act
            var result = await _controller.Index(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Animal>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Index_WithSearchString_ReturnsFilteredAnimals()
        {
            // Act
            var result = await _controller.Index("Lion");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Animal>>(viewResult.ViewData.Model);
            var animal = Assert.Single(model);
            Assert.Equal("Lion", animal.Name);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenAnimalNotFound()
        {
            // Act
            var result = await _controller.Details(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithAnimal()
        {
            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Animal>(viewResult.ViewData.Model);
            Assert.Equal("Lion", model.Name);
        }
    }
}

