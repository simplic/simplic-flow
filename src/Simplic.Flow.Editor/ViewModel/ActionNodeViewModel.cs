using Simplic.Flow.Configuration;
using Simplic.Flow.Editor.Definition;
using System;
using System.Linq;

namespace Simplic.Flow.Editor
{
    public class ActionNodeViewModel : NodeViewModel
    {
        public ActionNodeViewModel(NodeDefinition nodeDefinition, NodeConfiguration nodeConfiguration) 
            : base(nodeDefinition, nodeConfiguration)
        {
            // add flow in pin manually if it is not an event            
            if (!nodeDefinition.InFlowPins.Any())
            {
                var pin = new FlowPinDefinition
                {
                    AllowMultiple = false,
                    DisplayName = "In",
                    Id = Guid.NewGuid(),
                    Name = "FlowIn",
                    PinDirection = PinDirectionDefinition.In
                };
                nodeDefinition.InFlowPins.Add(pin);

                FlowPins.Add(new FlowConnectorViewModel(pin)
                {
                    Parent = this,
                    PinDirection = PinDirectionDefinition.In
                });
            }
        }
    }
}
