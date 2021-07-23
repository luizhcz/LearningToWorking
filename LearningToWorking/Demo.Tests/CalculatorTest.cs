using System;
using Xunit;

namespace Demo.Tests
{
    public class CalculatorTest
    {
        [Fact]
         public void Calculator_Sum_ReturnValue()
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            var result = calculator.Sum(2, 2);

            //Assert
            Assert.Equal(4, result);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2 ,2, 4)]
        [InlineData(3, 3, 6)]
        [InlineData(4, 4, 8)]
        [InlineData(5, 5, 10)]
        public void Calculator_Sum_ReturnValuesSumCorrect(double v1, double v2, double total) {
            //Arrange
            var calculator = new Calculator();

            //Act
            var result = calculator.Sum(v1, v2);

            //Assert
            Assert.Equal(total, result);
        }




    }
}
