// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayCopy_Array_Array_Int64), DisplayName = "Copy(Array,Array,Int64)", Category = "System/Array")]
    public class SystemArrayCopy_Array_Array_Int64 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Array.Copy(
                scope.GetValue<System.Array>(InPinSourceArray),
                scope.GetValue<System.Array>(InPinDestinationArray),
                scope.GetValue<System.Int64>(InPinLength));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayCopy_Array_Array_Int64: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayCopy_Array_Array_Int64); 
        public override string FriendlyName => nameof(SystemArrayCopy_Array_Array_Int64); 

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
        Id = "0ffa1441-7646-40a8-86ea-01e2411b52be",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceArray),
        DisplayName = "SourceArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceArray { get; set; } 

        [DataPinDefinition(
        Id = "c8742b88-81d4-4613-960e-455c3ac5402f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationArray),
        DisplayName = "DestinationArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationArray { get; set; } 

        [DataPinDefinition(
        Id = "c28d392b-fcc9-4728-865a-222d96560f1a",
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