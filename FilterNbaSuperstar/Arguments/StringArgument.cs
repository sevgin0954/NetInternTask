using FilterNbaSuperstar.Arguments.Interfaces;

namespace FilterNbaSuperstar.Arguments
{
    public class StringArgument : IArgument
    {
        private string value;

        public StringArgument(string value)
        {
            this.SetValue(value);
        }

        public void SetValue(string argument)
        {
            Validator.ValidateNotNull(argument);
            this.value = argument;
        }

        public static string GetValue(IArgument argument)
        {
            Validator.ValidateNotNull(argument);
            Validator.ValidateArgumentType(argument, typeof(StringArgument));
            return (argument as StringArgument).value;
        }
    }
}
