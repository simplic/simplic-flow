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
        Id = "78b98482-2f5e-425f-b824-f2f61f53b1b9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceArray),
        DisplayName = "SourceArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceArray { get; set; } 

        [DataPinDefinition(
        Id = "c880f417-28c3-4448-8068-d35a3f11a8cb",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceIndex),
        DisplayName = "SourceIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceIndex { get; set; } 

        [DataPinDefinition(
        Id = "f5e9129b-1e73-44b5-b895-0655a5b78a06",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationArray),
        DisplayName = "DestinationArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationArray { get; set; } 

        [DataPinDefinition(
        Id = "e2ef0d69-1619-4619-9cea-673ef1b43921",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationIndex),
        DisplayName = "DestinationIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationIndex { get; set; } 

        [DataPinDefinition(
        Id = "7c60a7ba-9234-46e8-9483-78371aad99b6",
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