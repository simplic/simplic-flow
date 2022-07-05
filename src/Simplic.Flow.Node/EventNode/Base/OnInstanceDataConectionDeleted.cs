using System;

namespace Simplic.Flow.Node
{
    /// <summary>
    /// Fires whenever a instancedataconnection is deleted. Inpins can define which instancedata should be observed.
    /// </summary>
    [EventNodeDefinition(DisplayName = "On Instance data connection deleted", Name = "OnInstanceDataConectionDeleted", EventName = "OnInstanceDataConectionDeleted", Category = "Common")]
    public class OnInstanceDataConectionDeleted : EventNode
    {
        /// <summary>
        /// Returns the related guids of the deleted connection.
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
            Id = "55597BC2-DFC3-4531-8638-74FE05736652",
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
            Id = "73E300DE-3502-445B-A7A1-023D77E1178F",
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
            Id = "F6E65F87-37C6-4287-A2F3-E63E238B4DF5",
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
            Id = "64F460B0-AF23-432D-BC42-9B4AE8C5C4FE",
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
            Id = "F9CED866-178D-4DE5-9178-173DCBA14416",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(Guid),
            Direction = PinDirection.In,
            Name = nameof(InPinSourceStackGuid),
            DisplayName = "Stack Source Guid")]
        public DataPin InPinSourceStackGuid { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "CE9417D5-F74C-4AAA-8DB9-611726C83EB5",
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
            Id = "0BDD055E-69BF-4893-8592-011C5DF3AA57",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(Guid),
            Direction = PinDirection.In,
            Name = nameof(InPinDestinationStackGuid),
            DisplayName = "Destination Stack Guid")]
        public DataPin InPinDestinationStackGuid { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
           Id = "D6C83468-7090-4938-A471-06650F2CCDB9",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(Guid),
            Direction = PinDirection.In,
            Name = nameof(InPinDestinationGuid),
            DisplayName = "Destination Guid")]

        public DataPin InPinDestinationGuid { get; set; }

        public override string EventName => nameof(OnInstanceDataConectionDeleted);

        public override string Name => nameof(OnInstanceDataConectionDeleted);

        public override string FriendlyName => nameof(OnInstanceDataConectionDeleted);
    }
}
