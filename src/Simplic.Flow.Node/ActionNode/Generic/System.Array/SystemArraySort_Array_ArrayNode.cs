// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArraySort_Array_Array), DisplayName = "Sort(Array,Array)", Category = "System/Array")]
    public class SystemArraySort_Array_Array : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Array.Sort(
                scope.GetValue<System.Array>(InPinKeys),
                scope.GetValue<System.Array>(InPinItems));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArraySort_Array_Array: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArraySort_Array_Array); 
        public override string FriendlyName => nameof(SystemArraySort_Array_Array); 

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
        Id = "3a8651d2-b11c-4a95-a410-916b357956a6",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinKeys),
        DisplayName = "Keys",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinKeys { get; set; } 

        [DataPinDefinition(
        Id = "832d3347-478f-4dd1-8f30-cd7d953b15f4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinItems),
        DisplayName = "Items",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinItems { get; set; } 

    }
}