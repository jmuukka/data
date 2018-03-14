using System;
using System.Data;
using System.Reflection;

namespace Mutex.Data
{
#if DEBUG
    public
#endif
    static class ICommandExtensions
    {
        public static void AddParameters(this ICommand command, object parameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (parameters != null)
            {
                var properties = parameters.GetType().GetProperties();
                foreach (var property in properties)
                {
                    command.AddParameter(parameters, property);
                }
            }
        }

        static void AddParameter(this ICommand command, object parameters, PropertyInfo property)
        {
            var dbType = DbTypeResolvers.Instance.TryResolve(property.PropertyType);
            if (!dbType.HasValue)
            {
                // this will ignore all types we do not know :(
                // maybe it's better to let package user to define will it be ignored or not
                // TODO if configured to allow unknown types and treat them as Object
                return;
            }

            command.AddParameter(parameters, property, dbType);
        }

        static void AddParameter(this ICommand command, object parameters, PropertyInfo property, DbType? dbType)
        {
            var value = GetValue(property, parameters);
            command.Parameters.Add(property.Name, value, dbType ?? DbType.Object);
        }

        static object GetValue(PropertyInfo property, object obj)
        {
            // TODO why null parameter?
            //return property.GetValue(obj, null); // note that this will require a public property
            return property.GetValue(obj, BindingFlags.Public, null, null, null);
        }
    }
}
