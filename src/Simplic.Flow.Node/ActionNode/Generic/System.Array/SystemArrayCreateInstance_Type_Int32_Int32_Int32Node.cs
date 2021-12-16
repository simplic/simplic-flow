// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayCreateInstance_Type_Int32_Int32_Int32), DisplayName = "CreateInstance(Type,Int32,Int32,Int32)", Category = "System/Array")]
    public class SystemArrayCreateInstance_Type_Int32_Int32_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Array.CreateInstance(
                scope.GetValue<System.Type>(InPinElementType),
                scope.GetValue<System.Int32>(InPinLength1),
                scope.GetValue<System.Int32>(InPinLength2),
                scope.GetValue<System.Int32>(InPinLength3));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayCreateInstance_Type_Int32_Int32_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayCreateInstance_Type_Int32_Int32_Int32); 
        public override string FriendlyName => nameof(SystemArrayCreateInstance_Type_Int32_Int32_Int32); 

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

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "Each item", 
        Name = nameof(OutNodeEachItem), 
        AllowMultiple = false)] 
        public ActionNode OutNodeEachItem { get; set; } 

        [DataPinDefinition(
        Id = "6dbceba8-d5c5-4fb7-9052-166b1e3dd314",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Type),
        Direction = PinDirection.In,
        Name = nameof(InPinElementType),
        DisplayName = "ElementType",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinElementType { get; set; } 

        [DataPinDefinition(
        Id = "2faddef4-e224-4e8c-8ccc-7f6c85491703",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength1),
        DisplayName = "Length1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength1 { get; set; } 

        [DataPinDefinition(
        Id = "f47e55d5-471c-438a-ac8a-0e792274dd61",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength2),
        DisplayName = "Length2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength2 { get; set; } 

        [DataPinDefinition(
        Id = "779994b1-fc69-4eed-81cc-be3eef3f70bf",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength3),
        DisplayName = "Length3",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength3 { get; set; } 

        [DataPinDefinition(
        Id = "70b2700e-1b0b-4e49-ae1a-b0bb901d7801",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}