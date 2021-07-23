using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demo.Tests
{
    public class AssertingCollectionsTests
    {
        [Fact]
        public void Employee_Habilits_NoShouldHabilitsEmpty() {
            //Arrange & Act
            var employee = EmployeeFactory.Create("Luiz", 10000);

            //Assert
            Assert.All(employee.Habilits, habilit => Assert.False(String.IsNullOrEmpty(habilit)));
        }

        [Fact]
        public void Employee_Habilits_JuniorShouldHabilitsBasic() {
            //Arrange & Act
            var employee = EmployeeFactory.Create("Luiz", 1000);

            //Assert
            Assert.Contains("OOP", employee.Habilits);
        }

        [Fact]
        public void Employee_Habilits_JuniorNotShouldHabilitsBasic()
        {
            //Arrange & Act
            var employee = EmployeeFactory.Create("Luiz", 1000);

            //Assert
            Assert.DoesNotContain("Microservices", employee.Habilits);
        }

        [Fact]
        public void Employee_Habilits_SeniorShouldAllHabilits()
        {
            //Arrange & Act
            var employee = EmployeeFactory.Create("Luiz", 15000);

            var HabilitsSenior = new[] 
            {
                "Logic of Program",
                "OOP",
                "Tests",
                "Microservices"
            };

            //Assert
            Assert.Equal(employee.Habilits, HabilitsSenior);
        }
    }
}
