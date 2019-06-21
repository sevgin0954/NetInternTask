using System;
using Xunit;

namespace FilterNbaSuperstar.Tests.ArgsTests
{
    public class GetStringArgumentTests
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
            var args = this.InitializeArgs();

            var exception = Assert.Throws<ArgumentException>(() => args.GetStringArgument("abc"));
            Assert.Equal(ErrorMessages.IncorrectArgumentNameError, exception.Message);
        }

        [Fact]
        public void WithCorrectArgumentName_ShouldReturnArgumentValue()
        {
            var schemaName = "name";
            var schema = schemaName + "*";
            var arguments = new string[] { "sevgin" };
            var args = new Args(schema, arguments);

            var result = args.GetStringArgument(schemaName);

            Assert.Equal(arguments[0], result);
        }

        private Args InitializeArgs()
        {
            var args = new Args("name*", new string[] { "sevgin" });
            return args;
        }
    }
}
