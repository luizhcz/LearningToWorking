using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Features.Tests
{
    public class OrderTests
    {
        public static bool Test1Call;
        public static bool Test2Call;
        public static bool Test3Call;
        public static bool Test4Call;

        [Fact(DisplayName = "Test 04")]
        [Trait("Category", "Tests Order")]
        public void Test04() {
            Test4Call = true;

            Assert.True(Test3Call);
            Assert.True(Test1Call);
            Assert.False(Test2Call);
        }

        [Fact(DisplayName = "Test 01")]
        [Trait("Category", "Tests Order")]
        public void Test01()
        {
            Test1Call = true;

            Assert.True(Test3Call);
            Assert.False(Test4Call);
            Assert.False(Test2Call);
        }

        [Fact(DisplayName = "Test 03")]
        [Trait("Category", "Tests Order")]
        public void Test03()
        {
            Test3Call = true;

            Assert.False(Test1Call);
            Assert.False(Test2Call);
            Assert.False(Test4Call);
        }

        [Fact(DisplayName = "Test 02")]
        [Trait("Category", "Tests Order")]
        public void Test02()
        {
            Test2Call = true;

            Assert.True(Test1Call);
            Assert.True(Test2Call);
            Assert.True(Test4Call);
        }


    }
}
