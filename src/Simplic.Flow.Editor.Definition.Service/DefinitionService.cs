﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Simplic.Flow.Editor.Definition.Service
{
    public class DefinitionService : IDefinitionService
    {
        public IList<NodeDefinition> Create(IList<Assembly> assemblies)
        {
            var nodes = new List<NodeDefinition>();

            var result = assemblies.Select(x => x.GetTypes().Where(y => typeof(BaseNode).IsAssignableFrom(y)));

            foreach (var typeList in result)
            {
                foreach (var nodeType in typeList)
                {
                    NodeDefinition nodeDefinition = null;

                    // Create node definition here...
                    var nodeAttribute = nodeType.GetCustomAttributes().OfType<NodeDefinitionAttribute>().FirstOrDefault();
                    if (nodeAttribute is ActionNodeDefinitionAttribute)
                    {
                        nodeDefinition = new ActionNodeDefinition {
                            DisplayName = nodeAttribute.DisplayName,
                            Name = nodeAttribute.Name                            
                        };                                                
                    }
                    else if (nodeAttribute is EventNodeDefinitionAttribute)
                    {
                        nodeDefinition = new EventNodeDefinition
                        {
                            DisplayName = nodeAttribute.DisplayName,
                            Name = nodeAttribute.Name
                        };
                    }

                    // if we cant find what type the node definition is, just return the empty list
                    if (nodeDefinition == null)
                        return nodes;

                    // create flow pins from attributes
                    var flowPins = nodeType.GetProperties().Where(x => x.PropertyType == typeof(ActionNode));
                    foreach (var property in flowPins)
                    {
                        // Find attribute
                        var attribute = property.GetCustomAttributes(true)
                            .FirstOrDefault(x => x is FlowPinDefinitionAttribute) as FlowPinDefinitionAttribute;

                        if (attribute != null)
                        {
                            var flowPinDefinition = new FlowPinDefinition
                            {
                                DisplayName = attribute.DisplayName,
                                Name = attribute.Name,                                
                                PinDirection = attribute.PinDirection == PinDirection.In ? PinDirectionDefinition.In : PinDirectionDefinition.Out                                
                            };

                            if (attribute.PinDirection == PinDirection.In)
                                nodeDefinition.InFlowPins.Add(flowPinDefinition);
                            else
                                nodeDefinition.OutFlowPins.Add(flowPinDefinition);
                        }
                    }

                    // create data pins from attributes
                    var dataPins = nodeType.GetProperties().Where(x => x.PropertyType == typeof(DataPin));
                    foreach (var property in dataPins)
                    {
                        // Find attribute
                        var attribute = property.GetCustomAttributes(true)
                            .FirstOrDefault(x => x is DataPinDefinitionAttribute) as DataPinDefinitionAttribute;

                        if (attribute != null)
                        {
                            var dataPinDefinition = new DataPinDefinition
                            {
                                DisplayName = attribute.DisplayName,
                                Name = attribute.Name,
                                Type = attribute.DataType,
                                PinDirection = attribute.Direction == PinDirection.In ? PinDirectionDefinition.In : PinDirectionDefinition.Out,
                                Id = Guid.Parse(attribute.Id)
                            };

                            if (attribute.Direction == PinDirection.In)
                                nodeDefinition.InDataPins.Add(dataPinDefinition);
                            else
                                nodeDefinition.OutDataPins.Add(dataPinDefinition);
                        }
                    }

                    nodes.Add(nodeDefinition);
                }
            }

            return nodes;
        }
    }
}
