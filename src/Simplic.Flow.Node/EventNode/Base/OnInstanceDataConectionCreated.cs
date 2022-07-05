using System;

namespace Simplic.Flow.Node
{
    /// <summary>
    /// Fires whenever a instancedataconnection is created. Inpins can define which instancedata should be observed.
    /// </summary>
    [EventNodeDefinition(DisplayName = "On Instance data connection created", Name = "OnInstanceDataConectionCreated", EventName = "OnInstanceDataConectionCreated", Category = "Common")]
    public class OnInstanceDataConectionCreated : EventNode
    {
        /// <summary>
        /// Returns the related guids of the new connection.
        /// </summary>
        /// <param name="runtime"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var args = runtime.FlowEventArgs as OnInstanceDataChangedEventArgs;
            if (args != null)
            {
                Console.WriteLine($"Arguments not found in {nameof(OnInstanceDataChangedEventArgs)}");
                return false;
            }
            scope.SetValue(OutPinSourceStackGuid, args.SourceStackGuid);
            scope.SetValue(OutPinSourceGuid, args.SourceGuid);
            scope.SetValue(OutPinDestinationGuid, args.DestinationGuid);
            scope.SetValue(OutPinDestinationStackGuid, args.DestinationStackGuid);

            if (OutNode != null)
                runtime.EnqueueNode(OutNode, scope);

            return true;
        }

        /// <summary>
        /// Determines if the node should be triggered. 
        /// </summary>
        /// <param name="runtime"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public override bool ShouldExecute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var args = runtime.FlowEventArgs as OnInstanceDataChangedEventArgs;

            var sourceStackGuid = scope.GetValue<Guid>(InPinSourceStackGuid);
            var sourceGuid = scope.GetValue<Guid>(InPinSourceGuid);
            var destinationStackGuid = scope.GetValue<Guid>(InPinDestinationStackGuid);
            var destinationGuid = scope.GetValue<Guid>(InPinDestinationGuid);

            if (sourceStackGuid != null && !sourceStackGuid.Equals(args.SourceStackGuid))
                return false;

            if (sourceGuid != null && !sourceGuid.Equals(args.SourceGuid))
                return false;

            if (destinationStackGuid != null && !destinationStackGuid.Equals(args.DestinationStackGuid))
                return false;

            if (destinationGuid != null && !destinationGuid.Equals(args.DestinationGuid))
                return false;

            return true;
        }

        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "1BC98254-9C61-495E-ADAD-9A087831319C",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(Guid),
            Direction = PinDirection.Out,
            Name = nameof(OutPinSourceStackGuid),
            DisplayName = "Source Stack Guid")]
        public DataPin OutPinSourceStackGuid { get; set; }
        
        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "F994F9A5-9F18-455A-BB8D-51FD577538B4",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(Guid),
            Direction = PinDirection.Out,
            Name = nameof(OutPinSourceGuid),
            DisplayName = "Source Guid")]
        public DataPin OutPinSourceGuid { get; set; }
        
        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "044B67DE-85B5-4EF3-AD83-CAC019EEF58D",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(Guid),
            Direction = PinDirection.Out,
            Name = nameof(OutPinDestinationStackGuid),
            DisplayName = "Destination Stack Guid")]
        public DataPin OutPinDestinationStackGuid { get; set; }
        
        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "F12920E9-12EE-4852-9671-0E7831DF7F3A",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(Guid),
            Direction = PinDirection.Out,
            Name = nameof(OutPinDestinationGuid),
            DisplayName = "Destination Guid")]
        public DataPin OutPinDestinationGuid { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "7D4E3101-7DD9-4513-B131-8BE1C7F13E22",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(Guid),
            Direction = PinDirection.In,
            Name = nameof(InPinSourceStackGuid),
            DisplayName = "Source´Stack Guid")]
        public DataPin InPinSourceStackGuid { get; set; } 
        
        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "7AEA3A8F-3E95-488C-B273-3ADD95B9E8B0",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(Guid),
            Direction = PinDirection.In,
            Name = nameof(InPinSourceGuid),
            DisplayName = "Source Guid")]
        public DataPin InPinSourceGuid { get; set; } 
        
        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "896B38B6-574B-4BDD-AC1E-EDB39E3E9759",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(Guid),
            Direction = PinDirection.In,
            Name = nameof(InPinDestinationStackGuid),
            DisplayName = "Destination Source Guid")]
        public DataPin InPinDestinationStackGuid { get; set; } 
        
        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "E2BC6056-6F34-4CDF-AFFB-0A63E1D575AA",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(Guid),
            Direction = PinDirection.In,
            Name = nameof(InPinDestinationGuid),
            DisplayName = "Destination Guid")]
        public DataPin InPinDestinationGuid { get; set; }

        public override string EventName => nameof(OnInstanceDataConectionCreated);

        public override string Name => nameof(OnInstanceDataConectionCreated);

        public override string FriendlyName => nameof(OnInstanceDataConectionCreated);
    }
}
