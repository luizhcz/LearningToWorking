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
    }
}
