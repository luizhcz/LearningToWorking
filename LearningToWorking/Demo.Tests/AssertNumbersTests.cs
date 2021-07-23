using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demo.Tests
{
    public class AssertNumbersTests
    {
        [Fact]
        public void Calculator_Sum_Equals() {
            //Arrange
            var calculator = new Calculator();

            //Act
            var result = calculator.Sum(1, 2);

            //Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Calculator_Sum_NotEquals()
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            var result = calculator.Sum(1.1231314, 2.23204931);

            //Assert
            Assert.NotEqual(3.3, result, 1);
        }
    }
}