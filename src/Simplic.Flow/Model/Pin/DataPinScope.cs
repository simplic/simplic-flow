using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Simplic.Flow
{
    /// <summary>
    /// Flow scope instance
    /// </summary>
    public class DataPinScope
    {
        /// <summary>
        /// Create new child scope and set parent
        /// </summary>
        /// <returns>Scope instance witht he current scope as parent</returns>
        public DataPinScope CreateChild()
        {
            var scope = new DataPinScope();
            scope.Parent = this;

            return scope;
        }

        /// <summary>
        /// Build unique value key
        /// </summary>
        /// <param name="nodeId">Unique node id</param>
        /// <param name="pinId">Unique pin id</param>
        /// <returns>Value key as string</returns>
        private string BuildPinHash(Guid nodeId, Guid pinId) => $"{nodeId}_{pinId}";

        /// <summary>
        /// Get value from pin instance
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="inPin">Pin instance</param>
        /// <returns>Value of type <see cref="T"/></returns>
        public T GetValue<T>(DataPin inPin)
        {
            if (inPin == null)
                return default(T);

            var pinKey = BuildPinHash(inPin.TemporaryNodeId, inPin.Id);

            var value = default(T);

            if (!PinValues.Any(x => x.Key == pinKey) && Parent != null)
                return Parent.GetValue<T>(inPin);

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

        /// <summary>
        /// Get pin value as list
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="inPin">Pin instance</param>
        /// <returns>Result as list</returns>
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

        /// <summary>
        /// Set pin value
        /// </summary>
        /// <param name="outPin">Pin instance</param>
        /// <param name="value">Pin value</param>
        public void SetValue(DataPin outPin, object value)
        {
            var pinKey = BuildPinHash(outPin.TemporaryNodeId, outPin.Id);

            PinValues[pinKey] = value;
        }

        /// <summary>
        /// Gets or sets all pin values
        /// </summary>
        public IDictionary<string, object> PinValues { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets the parent scope
        /// </summary>
        public DataPinScope Parent { get; set; }
    }
}
