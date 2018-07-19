using System;
using System.Collections;
using System.Collections.Generic;
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
            var value = (T)PinValues.FirstOrDefault(x => x.Key == inPin.Id).Value;

            if (value == null)
                value = (T)Convert.ChangeType(inPin.DefaultValue, typeof(T));

            // Check

            return value;
        }

        public IList<T> GetListValue<T>(DataPin inPin)
        {
            var value = (PinValues.FirstOrDefault(x => x.Key == inPin.Id).Value as IList);
            var list = value.Cast<T>().ToList();

            // Check

            return list;
        }

        public void SetValue(DataPin outPin, object value)
        {
            PinValues[outPin.Id] = value;
        }
    }
}
