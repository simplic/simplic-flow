// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayCreateInstance_Type_Int32_Int32), DisplayName = "CreateInstance(Type,Int32,Int32)", Category = "System/Array")]
    public class SystemArrayCreateInstance_Type_Int32_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Array.CreateInstance(
                scope.GetValue<System.Type>(InPinElementType),
                scope.GetValue<System.Int32>(InPinLength1),
                scope.GetValue<System.Int32>(InPinLength2));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayCreateInstance_Type_Int32_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayCreateInstance_Type_Int32_Int32); 
        public override string FriendlyName => nameof(SystemArrayCreateInstance_Type_Int32_Int32); 

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
        Id = "86ed7d85-b4da-4151-af1f-b56c4c9a504c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Type),
        Direction = PinDirection.In,
        Name = nameof(InPinElementType),
        DisplayName = "ElementType",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinElementType { get; set; } 

        [DataPinDefinition(
        Id = "7e6f31e5-8eca-4728-acfd-47b340b4413f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength1),
        DisplayName = "Length1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength1 { get; set; } 

        [DataPinDefinition(
        Id = "1739eb06-a4db-4222-aae1-5d83b77870ec",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength2),
        DisplayName = "Length2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength2 { get; set; } 

        [DataPinDefinition(
        Id = "87e4e55e-fb69-4b66-85de-bc61fcd22a0b",
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