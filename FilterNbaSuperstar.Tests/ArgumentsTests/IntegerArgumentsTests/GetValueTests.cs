using FilterNbaSuperstar.Arguments;
using System;
using Xunit;

namespace FilterNbaSuperstar.Tests.ArgumentsTests.IntegerArgumentsTests
{
    public class GetValueTests
    {
        [Fact]
        public void WithNullArgument_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => IntegerArgument.GetValue(null));
        }

        [Fact]
        public void WithNotIntegerArgument_ShouldThrowException()
        {
            var notIntegerArgument = new StringArgument("4");

            var exception = Assert.Throws<ArgumentException>(() => IntegerArgument.GetValue(notIntegerArgument));

            Assert.Equal(ErrorMessages.IncorrectArgumentTypeError, exception.Message);
        }

        [Fact]
        public void WithCorrectArgument_ShouldReturnValue()
        {
            var value = 5;
            var integerArgument = new IntegerArgument(value.ToString());

            var result = IntegerArgument.GetValue(integerArgument);

            Assert.Equal(value, result);
        }
    }
}
