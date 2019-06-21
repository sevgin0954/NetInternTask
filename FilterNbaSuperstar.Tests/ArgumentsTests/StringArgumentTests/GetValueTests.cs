using FilterNbaSuperstar.Arguments;
using System;
using Xunit;

namespace FilterNbaSuperstar.Tests.ArgumentsTests.StringArgumentTests
{
    public class GetValueTests
    {
        [Fact]
        public void WithNullArgument_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => StringArgument.GetValue(null));
        }

        [Fact]
        public void WithNotStringArgument_ShouldThrowException()
        {
            var notStringArgument = new IntegerArgument("4");

            var exception = Assert.Throws<ArgumentException>(() => StringArgument.GetValue(notStringArgument));

            Assert.Equal(ErrorMessages.IncorrectArgumentTypeError, exception.Message);
        }

        [Fact]
        public void WithCorrectArgument_ShouldReturnValue()
        {
            var value = "name";
            var stringArgument = new StringArgument(value);

            var result = StringArgument.GetValue(stringArgument);

            Assert.Equal(value, result);
        }
    }
}
