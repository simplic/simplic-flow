// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayCopy_Array_Int64_Array_Int64_Int64), DisplayName = "Copy(Array,Int64,Array,Int64,Int64)", Category = "System/Array")]
    public class SystemArrayCopy_Array_Int64_Array_Int64_Int64 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Array.Copy(
                scope.GetValue<System.Array>(InPinSourceArray),
                scope.GetValue<System.Int64>(InPinSourceIndex),
                scope.GetValue<System.Array>(InPinDestinationArray),
                scope.GetValue<System.Int64>(InPinDestinationIndex),
                scope.GetValue<System.Int64>(InPinLength));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayCopy_Array_Int64_Array_Int64_Int64: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayCopy_Array_Int64_Array_Int64_Int64); 
        public override string FriendlyName => nameof(SystemArrayCopy_Array_Int64_Array_Int64_Int64); 

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
        Id = "c5b408ef-354d-403e-b54f-a92519dcc328",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceArray),
        DisplayName = "SourceArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceArray { get; set; } 

        [DataPinDefinition(
        Id = "5325ecd7-b9f4-4509-a3e8-8a1ed134a8a0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceIndex),
        DisplayName = "SourceIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceIndex { get; set; } 

        [DataPinDefinition(
        Id = "d6881c47-f109-4d7c-be24-a1fe11e56827",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationArray),
        DisplayName = "DestinationArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationArray { get; set; } 

        [DataPinDefinition(
        Id = "784b676f-cd0b-404f-b8a6-c46284fa7730",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationIndex),
        DisplayName = "DestinationIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationIndex { get; set; } 

        [DataPinDefinition(
        Id = "0c67484c-63d0-46e6-9d27-dfee5c64437b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

    }
}