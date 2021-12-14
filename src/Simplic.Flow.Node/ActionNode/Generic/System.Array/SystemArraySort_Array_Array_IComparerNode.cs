// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArraySort_Array_Array_IComparer), DisplayName = "Sort(Array,Array,IComparer)", Category = "System/Array")]
    public class SystemArraySort_Array_Array_IComparer : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Array.Sort(
                scope.GetValue<System.Array>(InPinKeys),
                scope.GetValue<System.Array>(InPinItems),
                scope.GetValue<System.Collections.IComparer>(InPinComparer));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArraySort_Array_Array_IComparer: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArraySort_Array_Array_IComparer); 
        public override string FriendlyName => nameof(SystemArraySort_Array_Array_IComparer); 

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
        Id = "7fb1c682-be1e-4fcf-8d74-d0b365c8d972",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinKeys),
        DisplayName = "Keys",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinKeys { get; set; } 

        [DataPinDefinition(
        Id = "b6e3b118-ed46-4078-84df-50f0ac37184f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinItems),
        DisplayName = "Items",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinItems { get; set; } 

        [DataPinDefinition(
        Id = "6b479786-0721-40b4-8c3c-aa75a3ce5d64",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Collections.IComparer),
        Direction = PinDirection.In,
        Name = nameof(InPinComparer),
        DisplayName = "Comparer",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinComparer { get; set; } 

    }
}