using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demo.Tests
{
    public class AssertingExcepetionsTests
    {
        [Fact]
        public void Calculator_Divide_ReturnZeroDivideError() {
            //Arrange
            var calculator = new Calculator();

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => calculator.Divd(10, 0));
        }

        [Fact]
        public void Employee_Salary_ReturnLowSalaryAllowedError() {
            //Arrange & Act & Assert
            var exception = Assert.Throws<Exception>(() => EmployeeFactory.Create("Luiz", 250));

            Assert.Equal("Salary less than allowed", exception.Message);
        }
    }
}
