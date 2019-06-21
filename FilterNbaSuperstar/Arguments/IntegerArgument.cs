using FilterNbaSuperstar.Arguments.Interfaces;
using System;

namespace FilterNbaSuperstar.Arguments
{
    public class IntegerArgument : IArgument
    {
        private int value;

        public IntegerArgument(string value)
        {
            this.SetValue(value);
        }

        public void SetValue(string argument)
        {
            Validator.ValidateNotNull(argument);

            var parsedArgument = 0;
            var isParsedSuccessfully = int.TryParse(argument, out parsedArgument);
            if (isParsedSuccessfully)
            {
                this.value = parsedArgument;
            }
            else
            {
                throw new ArgumentException(ErrorMessages.IncorrectArgumentError);
            }
        }

        public static int GetValue(IArgument argument)
        {
            Validator.ValidateNotNull(argument);
            Validator.ValidateArgumentType(argument, typeof(IntegerArgument));

            return (argument as IntegerArgument).value;
        }
    }
}
