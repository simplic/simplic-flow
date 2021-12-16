// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayCopy_Array_Int32_Array_Int32_Int32), DisplayName = "Copy(Array,Int32,Array,Int32,Int32)", Category = "System/Array")]
    public class SystemArrayCopy_Array_Int32_Array_Int32_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Array.Copy(
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayCopy_Array_Int32_Array_Int32_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayCopy_Array_Int32_Array_Int32_Int32); 
        public override string FriendlyName => nameof(SystemArrayCopy_Array_Int32_Array_Int32_Int32); 

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
        Id = "d9ae8d96-1acf-41e7-83e4-3572556f19de",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceArray),
        DisplayName = "SourceArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceArray { get; set; } 

        [DataPinDefinition(
        Id = "0b16abcb-1142-40f1-9af7-610209120386",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceIndex),
        DisplayName = "SourceIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceIndex { get; set; } 

        [DataPinDefinition(
        Id = "7b5f4609-3151-47fa-a544-c8c4386e62e5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationArray),
        DisplayName = "DestinationArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationArray { get; set; } 

        [DataPinDefinition(
        Id = "8ee136d4-309a-4cbb-82e3-857800f57266",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationIndex),
        DisplayName = "DestinationIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationIndex { get; set; } 

        [DataPinDefinition(
        Id = "3c98e479-8686-4491-8afa-bf8e34f6e65c",
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