using FilterNbaSuperstar.Arguments;
using FilterNbaSuperstar.Arguments.Interfaces;
using System;
using System.Collections.Generic;

namespace FilterNbaSuperstar
{
    public class Args
    {
        private readonly Dictionary<char, Type> schemaTypesArgumentsTypes = new Dictionary<char, Type>();
        private readonly Dictionary<string, IArgument> namesArguments = new Dictionary<string, IArgument>();

        public Args(string schema, string[] arguments)
        {
            Validator.ValidateNotNull(schema, arguments);
            this.InitializeSupportedArgumentsTypes();
            this.ParseArgumentsBySchema(arguments, schema);
        }

        private void InitializeSupportedArgumentsTypes()
        {
            schemaTypesArgumentsTypes.Add('*', typeof(StringArgument));
            schemaTypesArgumentsTypes.Add('#', typeof(IntegerArgument));
        }

        private void ParseArgumentsBySchema(string[] arguments, string schema)
        {
            var schemaParts = schema.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Validator.ValidateArgumentAndSchemaMatching(arguments, schemaParts);

            for (var i = 0; i < arguments.Length; i++)
            {
                var schemaPart = schemaParts[i];
                var argument = arguments[i];

                Validator.ValidateSchemaPart(schemaPart, this.schemaTypesArgumentsTypes);

                var schemaType = schemaPart[schemaPart.Length - 1];
                var argumentType = this.CreateArgumentType(schemaType, argument);

                var schemaName = schemaPart.Substring(0, schemaPart.Length - 1);
                this.namesArguments.Add(schemaName, argumentType);
            }
        }

        private IArgument CreateArgumentType(char schemaType, string value)
        {
            var argumentType = schemaTypesArgumentsTypes[schemaType];
            var argumentTypeInstance = (IArgument)Activator.CreateInstance(argumentType, new object[] { value });
            return argumentTypeInstance;
        }

        public string GetStringArgument(string argumentName)
        {
            Validator.ValidateNotNull(argumentName);
            Validator.ValidateArgumentName(argumentName, this.namesArguments);
            return StringArgument.GetValue(namesArguments[argumentName]);
        }

        public int GetIntArgument(string argumentName)
        {
            Validator.ValidateNotNull(argumentName);
            Validator.ValidateArgumentName(argumentName, this.namesArguments);
            return IntegerArgument.GetValue(namesArguments[argumentName]);
        }
    }
}
