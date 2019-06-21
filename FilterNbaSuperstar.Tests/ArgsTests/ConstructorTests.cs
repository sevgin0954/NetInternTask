using System;
using Xunit;

namespace FilterNbaSuperstar.Tests.ArgsTests
{
    public class ConstructorTests
    {
        [Fact]
        public void WithNullSchema_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Args(null, new string[0]));
        }

        [Fact]
        public void WithNullArguments_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Args("", null));
        }

        [Fact]
        public void WithNotEqualArgumentsAndSchemasLengths_ShouldThrowException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Args("", new string[1]));
            Assert.Equal(ErrorMessages.SchemaAndArgumentsDontMatchError, exception.Message);
        }

        [Fact]
        public void WithIncorrectSchemaType_ShouldThrowException()
        {
            var schema = "a$";
            var arguments = new string[] { "arg" };

            var exception = Assert.Throws<ArgumentException>(() => new Args(schema, arguments));
            Assert.Equal(ErrorMessages.IncorrectSchemaTypeError, exception.Message);
        }
    }
}
