// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArraySort_Array_Array_Int32_Int32), DisplayName = "Sort(Array,Array,Int32,Int32)", Category = "System/Array")]
    public class SystemArraySort_Array_Array_Int32_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Array.Sort(
                scope.GetValue<System.Array>(InPinKeys),
                scope.GetValue<System.Array>(InPinItems),
                scope.GetValue<System.Int32>(InPinIndex),
                scope.GetValue<System.Int32>(InPinLength));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArraySort_Array_Array_Int32_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArraySort_Array_Array_Int32_Int32); 
        public override string FriendlyName => nameof(SystemArraySort_Array_Array_Int32_Int32); 

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
        Id = "bb6b1c4a-4b79-4c94-a925-bf16da7cc2c1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinKeys),
        DisplayName = "Keys",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinKeys { get; set; } 

        [DataPinDefinition(
        Id = "44d09824-9d8b-4430-9233-2d116543bca7",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinItems),
        DisplayName = "Items",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinItems { get; set; } 

        [DataPinDefinition(
        Id = "fa299305-5ab7-4c4e-a942-dcc50daa7b7c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndex),
        DisplayName = "Index",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndex { get; set; } 

        [DataPinDefinition(
        Id = "dc01c1be-b295-4b93-a1cc-ce32a6b297d6",
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