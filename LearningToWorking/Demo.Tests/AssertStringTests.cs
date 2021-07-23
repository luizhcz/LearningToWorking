using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demo.Tests
{
    public class AssertStringTests
    {
        [Fact]
        public void StringTools_UnionNames_ReturnCompleteName() {
            //Arrange
            var sut = new StringTools();

            //Act
            var nomeCompleto = sut.Union("Luiz", "Cerqueira");

            //Assert
            Assert.Equal("Cerqueira Luiz", nomeCompleto);
        }

        [Fact]
        public void StringTools_UnionNames_IgnoreCase()
        {
            //Arrange
            var sut = new StringTools();

            //Act
            var nomeCompleto = sut.Union("Luiz", "Cerqueira");

            //Assert
            Assert.Equal("CERQUEIRA Luiz", nomeCompleto, true);
        }

        [Fact]
        public void StringTools_UnionNames_Contains()
        {
            //Arrange
            var sut = new StringTools();

            //Act
            var nomeCompleto = sut.Union("Luiz", "Cerqueira");

            //Assert
            Assert.Contains("que", nomeCompleto);
        }

        [Fact]
        public void StringTools_UnionNames_StartWith()
        {
            //Arrange
            var sut = new StringTools();

            //Act
            var nomeCompleto = sut.Union("Luiz", "Cerqueira");

            //Assert
            Assert.StartsWith("Cerqueira", nomeCompleto);
        }

        [Fact]
        public void StringTools_UnionNames_EndWith()
        {
            //Arrange
            var sut = new StringTools();

            //Act
            var nomeCompleto = sut.Union("Luiz", "Cerqueira");

            //Assert
            Assert.EndsWith("Luiz", nomeCompleto);
        }
    }
}
