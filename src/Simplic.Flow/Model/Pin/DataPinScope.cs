using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Simplic.Flow
{
    public class DataPinScope
    {
        public DataPinScope CreateChild()
        {
            var scope = new DataPinScope();
            scope.Parent = this;

            return scope;
        }

        public IDictionary<string, object> PinValues { get; set; } = new Dictionary<string, object>();

        private string BuildPinHash(Guid nodeId, Guid pinId) => $"{nodeId}_{pinId}";

        public DataPinScope Parent { get; set; }
        public T GetValue<T>(DataPin inPin)
        {
            if (inPin == null)
                return default(T);

            var pinKey = BuildPinHash(inPin.TemporaryNodeId, inPin.Id);

            var value = default(T);

            if (PinValues.Any(x => x.Key == pinKey))
            {
                var rawValue = PinValues.FirstOrDefault(x => x.Key == pinKey);

                try
                {
                    value = (T)rawValue.Value;
                }
                catch (InvalidCastException)
                {
                    if (rawValue.Value != null)
                        value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(rawValue.Value?.ToString());
                }
            }

            // Log if inPin is null
            if ((value == null || value.Equals(default(T))) && inPin.DefaultValue != null)
            {
                var defaultValueType = inPin.DefaultValue?.GetType();
                var valueType = typeof(T);

                /* ugly fix: if the value type is object and default value type is string, 
                 * converter can not convert, so check if its object and string and just cast it.
                 */
                if (defaultValueType == valueType || (valueType == typeof(object) && defaultValueType == typeof(string)))
                {
                    value = (T)inPin.DefaultValue;
                }                
                else if (inPin?.DefaultValue?.ToString() != null)
                {
                    value = (T)TypeDescriptor.GetConverter(valueType).ConvertFromInvariantString(inPin?.DefaultValue?.ToString());
                }
            }

            return value;
        }

        public IList<T> GetListValue<T>(DataPin inPin)
        {
            var pinKey = BuildPinHash(inPin.TemporaryNodeId, inPin.Id);

            if (PinValues.Any(x => x.Key == pinKey))
            {
                var value = (PinValues.FirstOrDefault(x => x.Key == pinKey).Value as IList);
                var list = value.Cast<T>().ToList();

                // Check

                return list;
            }
            else
                return new List<T>();
        }        

        public void SetValue(DataPin outPin, object value)
        {
            var pinKey = BuildPinHash(outPin.TemporaryNodeId, outPin.Id);

            PinValues[pinKey] = value;
        }
    }
}
