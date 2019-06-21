using FilterNbaSuperstar.Arguments.Interfaces;
using System;
using System.Collections.Generic;

namespace FilterNbaSuperstar
{
    public static class Validator
    {
        public static void ValidateArgumentType(IArgument argument, Type expectedType)
        {
            if (argument.GetType() != expectedType)
            {
                throw new ArgumentException(ErrorMessages.IncorrectArgumentTypeError);
            }
        }

        public static void ValidatePositiveNumber(int number, string paramName)
        {
            if (number < 0)
            {
                throw new ArgumentException(ErrorMessages.NegativeNumberError, paramName);
            }
        }

        public static void ValidateArgumentAndSchemaMatching(string[] arguments, string[] schemaParts)
        {
            if (schemaParts.Length != arguments.Length)
            {
                throw new ArgumentException(ErrorMessages.SchemaAndArgumentsDontMatchError);
            }
        }

        public static void ValidateSchemaPart(string schemaPart, Dictionary<char, Type> schemaTypesArgumentsTypes)
        {
            if (schemaPart.Length < 2)
            {
                throw new ArgumentException(ErrorMessages.IncorrectSchemaError);
            }
            var schemaType = schemaPart[schemaPart.Length - 1];
            if (schemaTypesArgumentsTypes.ContainsKey(schemaType) == false)
            {
                throw new ArgumentException(ErrorMessages.IncorrectSchemaTypeError);
            }
        }

        public static void ValidateArgumentName(string argumentName, Dictionary<string, IArgument> namesArguments)
        {
            if (namesArguments.ContainsKey(argumentName) == false)
            {
                throw new ArgumentException(ErrorMessages.IncorrectArgumentNameError);
            }
        }

        public static void ValidateNotNull(params object[] objs)
        {
            foreach (var obj in objs)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(objs), ErrorMessages.NullArgumentError);
                }
            }
        }
    }
}
