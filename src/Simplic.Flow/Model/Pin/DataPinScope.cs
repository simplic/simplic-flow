﻿using System;
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

        public IDictionary<Guid, object> PinValues { get; set; } = new Dictionary<Guid, object>();
        public DataPinScope Parent { get; set; }
        public T GetValue<T>(DataPin inPin)
        {
            if (inPin == null)
                return default(T);

            var value = default(T);

            if (PinValues.Any(x => x.Key == inPin.Id))
            {
                var rawValue = PinValues.FirstOrDefault(x => x.Key == inPin.Id);
                value = (T)rawValue.Value;
            }

            // Log if inPin is null
            if (value?.Equals(default(T)) == true && inPin.DefaultValue != null)
            {
                if (typeof(T) == inPin.DefaultValue?.GetType())
                {
                    value = (T)inPin.DefaultValue;
                }
                else if (inPin?.DefaultValue?.ToString() != null)
                {
                    value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(inPin?.DefaultValue?.ToString());
                }
            }

            return value;
        }

        public IList<T> GetListValue<T>(DataPin inPin)
        {
            if (PinValues.Any(x => x.Key == inPin.Id))
            {
                var value = (PinValues.FirstOrDefault(x => x.Key == inPin.Id).Value as IList);
                var list = value.Cast<T>().ToList();

                // Check

                return list;
            }
            else
                return new List<T>();
        }

        public void SetValue(DataPin outPin, object value)
        {
            PinValues[outPin.Id] = value;
        }
    }
}