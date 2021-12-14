// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArraySort_Array_Int32_Int32_IComparer), DisplayName = "Sort(Array,Int32,Int32,IComparer)", Category = "System/Array")]
    public class SystemArraySort_Array_Int32_Int32_IComparer : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Array.Sort(
                scope.GetValue<System.Array>(InPinArray),
                scope.GetValue<System.Int32>(InPinIndex),
                scope.GetValue<System.Int32>(InPinLength),
                scope.GetValue<System.Collections.IComparer>(InPinComparer));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArraySort_Array_Int32_Int32_IComparer: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArraySort_Array_Int32_Int32_IComparer); 
        public override string FriendlyName => nameof(SystemArraySort_Array_Int32_Int32_IComparer); 

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
        Id = "7f2f0056-a74a-41ce-8145-3918d92de2e7",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinArray),
        DisplayName = "Array",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArray { get; set; } 

        [DataPinDefinition(
        Id = "8ee267f2-8571-4b2f-8d33-a7844d743a44",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndex),
        DisplayName = "Index",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndex { get; set; } 

        [DataPinDefinition(
        Id = "07f610a2-33df-4a4b-a2c6-16fd72030dc8",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "67ec946c-c6d5-4543-80d1-9fae6f07ce7b",
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