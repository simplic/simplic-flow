using System;
using System.Linq;

namespace Simplic.Flow
{
    public abstract class BaseNode
    {
        public Guid Id { get; set; }
        public abstract string Name { get; }
        public abstract string FriendlyName { get; }

        public virtual void CreateDataPins()
        {
            foreach (var property in GetType().GetProperties().Where(x => x.PropertyType == typeof(DataPin)))
            {
                // Find attribute
                var attribute = property.GetCustomAttributes(true).FirstOrDefault(x => x is DataPinDefinitionAttribute) as DataPinDefinitionAttribute;

                if (attribute != null)
                {
                    var pin = new DataPin
                    {
                        Id = Guid.Parse(attribute.Id),
                        ContainerType = attribute.ContainerType,
                        DataType = attribute.DataType,
                        TemporaryNodeId = this.Id
                    };

                    // Create instance
                    property.SetValue(this, pin);
                }
            }
        }

        public virtual DataPin CreateOutNode<T>(string name, Guid id, string friendlyName = null, string description = "", DataPinContainerType containerType = DataPinContainerType.Single, bool isNullable = false, object defaultValue = null)
        {
            return new DataPin
            {
                Id = id,
                Name = name,
                FriendlyName = friendlyName ?? name,
                DataType = typeof(T),
                Owner = this,
                Direction = PinDirection.Out,
                IsNullable = isNullable,
                DefaultValue = defaultValue,
                ContainerType = containerType,
                Description = description
            };
        }

        public virtual DataPin CreateInNode<T>(string name, Guid id, string friendlyName = null, string description = "", DataPinContainerType containerType = DataPinContainerType.Single, bool isNullable = false, object defaultValue = null)
        {
            return new DataPin
            {
                Id = id,
                Name = name,
                FriendlyName = friendlyName ?? name,
                DataType = typeof(T),
                Owner = this,
                Direction = PinDirection.In,
                IsNullable = isNullable,
                DefaultValue = defaultValue,
                ContainerType = containerType,
                Description = description
            };
        }
    }
}
