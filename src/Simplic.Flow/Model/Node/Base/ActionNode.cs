using Simplic.Flow.Event;
using System;

namespace Simplic.Flow
{
    public abstract class ActionNode : BaseNode
    {
        public abstract bool Execute(IFlowRuntimeService runtime, DataPinScope scope);

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
