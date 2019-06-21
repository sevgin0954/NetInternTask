using FilterNbaSuperstar.Arguments;
using System;
using System.Reflection;
using Xunit;

namespace FilterNbaSuperstar.Tests.ArgumentsTests.StringArgumentTests
{
    public class SetValueTests
    {
        [Fact]
        public void WithNullArgument_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new StringArgument(null));
        }

        [Fact]
        public void WithCorrectArgument_ShouldSetValueField()
        {
            var value = "name";
            var integerArgument = new StringArgument(value);

            var field = integerArgument.GetType().GetField("value", BindingFlags.NonPublic | BindingFlags.Instance);

            var expectedValue = value;
            var actualValue = (string)field.GetValue(integerArgument);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
