using System.Collections.Immutable;
using CalculatorLib;
using Xunit;

namespace CalculatorLibUnitTests
{
    public class CalculatorUnitTest
    {
        [Fact]
        public void TestAdding2And2()
        {
            var builder = ImmutableDictionary.CreateBuilder<string, int>();
            builder.Add("a", 1);
            builder.Add("b", 2);   
            var result = builder.ToImmutable();
            Assert.Equal(4, new Calculator().Add(2, 2));
        }

        [Fact]
        public void TestAdding2And3()
        {
            Assert.Equal(5, new Calculator().Add(2, 3));
        }
    }
}