using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demo.Tests
{
    public class AssertingObjectTypesTests
    {
        [Fact]
        public void Employeefactory_create_ReturnTypeEmployee() {
            //Arrange & Act
            var employee = EmployeeFactory.Create("Luiz", 6000);

            //Assert
            Assert.IsType<Employee>(employee);
        }

        [Fact]
        public void EmployeeFactory_Create_ReturnTypeExtendPeople() {
            //Arrange & Act
            var employee = EmployeeFactory.Create("Luiz", 6000);

            //Assert
            Assert.IsAssignableFrom<People>(employee);
        }
    }
}
