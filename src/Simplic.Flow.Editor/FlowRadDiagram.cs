using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace Simplic.Flow.Editor
{
    public class FlowRadDiagram : RadDiagram
    {
        //protected override bool IsItemItsOwnShapeContainerOverride(object item)
        //{
        //    return item is ActionNodeViewModel || item is EventNodeViewModel;
        //}

        protected override Telerik.Windows.Diagrams.Core.IShape GetShapeContainerForItemOverride(object item)
        {
            if (item is ActionNodeViewModel)
            {
                var actionNodeViewModel = item as ActionNodeViewModel;

                var flowConnectors = new List<FlowConnector>();
                foreach (var flowPin in actionNodeViewModel.FlowPins)
                {
                    flowConnectors.Add(new FlowConnector(flowPin.Name, "", flowPin.PinDirection == 
                        PinDirectionDefinition.In ? ConnectorDirection.In : ConnectorDirection.Out));
                }

                var dataConnectors = new List<DataConnector>();
                foreach (var dataPin in actionNodeViewModel.DataPins)
                {
                    dataConnectors.Add(new DataConnector(dataPin.Name, dataPin.Name, dataPin.PinDirection ==
                        PinDirectionDefinition.In ? ConnectorDirection.In : ConnectorDirection.Out, 
                        dataPin.Type));
                }
                
                return new ActionNodeShape(actionNodeViewModel.DisplayName, flowConnectors, dataConnectors);
            }
            else if (item is EventNodeViewModel)
            {
                var eventNodeViewModel = item as EventNodeViewModel;

                var flowConnectors = new List<FlowConnector>();
                foreach (var flowPin in eventNodeViewModel.FlowPins)
                {
                    flowConnectors.Add(new FlowConnector(flowPin.Name, "", flowPin.PinDirection ==
                        PinDirectionDefinition.In ? ConnectorDirection.In : ConnectorDirection.Out));
                }

                var dataConnectors = new List<DataConnector>();
                foreach (var dataPin in eventNodeViewModel.DataPins)
                {
                    dataConnectors.Add(new DataConnector(dataPin.Name, dataPin.Name, dataPin.PinDirection ==
                        PinDirectionDefinition.In ? ConnectorDirection.In : ConnectorDirection.Out,
                        dataPin.Type));
                }

                //return new ActionNodeShape(actionNodeViewModel.DisplayName, flowConnectors, dataConnectors);
                
                return new EventNodeShape(eventNodeViewModel.DisplayName, flowConnectors, dataConnectors);
            }
            else
                return null;
        }
    }
}
