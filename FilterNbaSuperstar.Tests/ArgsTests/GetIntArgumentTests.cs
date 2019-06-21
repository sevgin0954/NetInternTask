using System;
using Xunit;

namespace FilterNbaSuperstar.Tests.ArgsTests
{
    public class GetIntArgumentTests
    {
        [Fact]
        public void WithNullArgument_ShouldThrowException()
        {
            var args = this.InitializeArgs();

            Assert.Throws<ArgumentNullException>(() => args.GetStringArgument(null));
        }

        [Fact]
        public void WithNotExistingArgumentName_ShouldThrowException()
        {
            var args = new Args("number#", new string[] { "1" });

            var exception = Assert.Throws<ArgumentException>(() => args.GetStringArgument("2"));
            Assert.Equal(ErrorMessages.IncorrectArgumentNameError, exception.Message);
        }

        [Fact]
        public void WithCorrectArgumentName_ShouldReturnArgumentValue()
        {
            var schemaName = "number";
            var schema = schemaName + "#";
            var arguments = new string[] { "1" };
            var args = new Args(schema, arguments);

            var result = args.GetIntArgument(schemaName);

            var expected = int.Parse(arguments[0]);
            Assert.Equal(expected, result);
        }

        private Args InitializeArgs()
        {
            var args = new Args("number#", new string[] { "1" });
            return args;
        }
    }
}
