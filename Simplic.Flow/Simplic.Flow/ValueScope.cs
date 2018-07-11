using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow
{
    public class ValueScope
    {
        public ValueScope CreateChild()
        {
            var scope = new ValueScope();
            scope.Parent = this;

            return scope;
        }

        public IDictionary<Guid, object> PinValues = new Dictionary<Guid, object>();
        public ValueScope Parent { get; set; }
        public T GetValue<T>(DataPin inPin)
        {
            var value = (T)PinValues.FirstOrDefault(x => x.Key == inPin.Id).Value;

            // Check

            return value;
        }

        public IList<T> GetListValue<T>(DataPin inPin)
        {
            var value = (IList<T>)PinValues.FirstOrDefault(x => x.Key == inPin.Id).Value;

            // Check

            return value;
        }

        public void SetValue(DataPin outPin, object value)
        {
            PinValues[outPin.Id] = value;
        }
    }
}
