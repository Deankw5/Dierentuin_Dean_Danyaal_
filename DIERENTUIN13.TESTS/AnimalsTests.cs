using DIERENTUIN13.Models;
using Xunit;

namespace DIERENTUIN13.TESTS
{
    public class AnimalsTests
    {
        [Fact]
        public void ActieSunrise_ShouldReturnCorrectAction()
        {
            // Arrange
            var animal = new Animal { ActivityPattern = ActivityPatternEnum.Diurnal };

            // Act
            var result = animal.ActieSunrise();

            // Assert
            Assert.Equal("Wakes up", result);
        }

        [Fact]
        public void ActieSunset_ShouldReturnCorrectAction()
        {
            // Arrange
            var animal = new Animal { ActivityPattern = ActivityPatternEnum.Nocturnal };

            // Act
            var result = animal.ActieSunset();

            // Assert
            Assert.Equal("Wakes up", result);
        }

        [Fact]
        public void ActieFeedingTime_ShouldReturnCorrectPrey()
        {
            // Arrange
            var animal = new Animal { Prey = "Zebra, Buffalo" };

            // Act
            var result = animal.ActieFeedingTime();

            // Assert
            Assert.Equal("Eats: Zebra, Buffalo", result);
        }
    }
}

