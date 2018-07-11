using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IDictionary<Guid, object> PinValues = new Dictionary<Guid, object>();
        public DataPinScope Parent { get; set; }
        public T GetValue<T>(DataPin inPin)
        {
            var value = (T)PinValues.FirstOrDefault(x => x.Key == inPin.Id).Value;

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
