using FilterNbaSuperstar.Arguments;
using System;
using System.Reflection;
using Xunit;

namespace FilterNbaSuperstar.Tests.ArgumentsTests.IntegerArgumentsTests
{
    public class SetValueTests
    {
        [Fact]
        public void WithNotANumberValue_ShouldThrowException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new IntegerArgument("a"));
            Assert.Equal(ErrorMessages.IncorrectArgumentError, exception.Message);
        }

        [Fact]
        public void WithNullValue_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new IntegerArgument(null));
        }

        [Fact]
        public void WithCorrectNumberValue_ShouldSetValueField()
        {
            var value = "5";
            var integerArgument = new IntegerArgument(value);

            var field = integerArgument.GetType().GetField("value", BindingFlags.NonPublic | BindingFlags.Instance);

            var expectedValue = int.Parse(value);
            var actualValue = (int)field.GetValue(integerArgument);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
