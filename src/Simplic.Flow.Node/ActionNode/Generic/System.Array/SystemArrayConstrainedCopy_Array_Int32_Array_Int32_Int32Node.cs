// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayConstrainedCopy_Array_Int32_Array_Int32_Int32), DisplayName = "ConstrainedCopy(Array,Int32,Array,Int32,Int32)", Category = "System/Array")]
    public class SystemArrayConstrainedCopy_Array_Int32_Array_Int32_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Array.ConstrainedCopy(
                scope.GetValue<System.Array>(InPinSourceArray),
                scope.GetValue<System.Int32>(InPinSourceIndex),
                scope.GetValue<System.Array>(InPinDestinationArray),
                scope.GetValue<System.Int32>(InPinDestinationIndex),
                scope.GetValue<System.Int32>(InPinLength));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayConstrainedCopy_Array_Int32_Array_Int32_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayConstrainedCopy_Array_Int32_Array_Int32_Int32); 
        public override string FriendlyName => nameof(SystemArrayConstrainedCopy_Array_Int32_Array_Int32_Int32); 

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "Success", 
        Name = nameof(OutNodeSuccess), 
        AllowMultiple = false)] 
        public ActionNode OutNodeSuccess { get; set; } 

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "Failed", 
        Name = nameof(OutNodeFailed), 
        AllowMultiple = false)] 
        public ActionNode OutNodeFailed { get; set; } 

        [DataPinDefinition(
        Id = "3ea1e31e-6119-48cf-ac22-ed99d9437c20",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceArray),
        DisplayName = "SourceArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceArray { get; set; } 

        [DataPinDefinition(
        Id = "d6a91bf5-fdba-4109-a492-14f370d8d09d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceIndex),
        DisplayName = "SourceIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceIndex { get; set; } 

        [DataPinDefinition(
        Id = "ffe9ce69-bd8c-45dc-94d6-49d3bd2249bb",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationArray),
        DisplayName = "DestinationArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationArray { get; set; } 

        [DataPinDefinition(
        Id = "7084c921-7339-4a3e-ad00-b2d9fbe292dd",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationIndex),
        DisplayName = "DestinationIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationIndex { get; set; } 

        [DataPinDefinition(
        Id = "73af301b-26d1-434f-ae5c-a9e7d88e95f8",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

    }
}