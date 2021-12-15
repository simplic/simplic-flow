// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayCreateInstance_Type_Int32__Int32_), DisplayName = "CreateInstance(Type,Int32[],Int32[])", Category = "System/Array")]
    public class SystemArrayCreateInstance_Type_Int32__Int32_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Array.CreateInstance(
                scope.GetValue<System.Type>(InPinElementType),
                scope.GetValue<System.Int32[]>(InPinLengths),
                scope.GetValue<System.Int32[]>(InPinLowerBounds));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayCreateInstance_Type_Int32__Int32_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayCreateInstance_Type_Int32__Int32_); 
        public override string FriendlyName => nameof(SystemArrayCreateInstance_Type_Int32__Int32_); 

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
        Id = "0dbce3d5-82e6-4d81-8c9e-6110503a833b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Type),
        Direction = PinDirection.In,
        Name = nameof(InPinElementType),
        DisplayName = "ElementType",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinElementType { get; set; } 

        [DataPinDefinition(
        Id = "8080fa58-76ce-428a-a649-862a0daddcb9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32[]),
        Direction = PinDirection.In,
        Name = nameof(InPinLengths),
        DisplayName = "Lengths",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLengths { get; set; } 

        [DataPinDefinition(
        Id = "97c4b693-63ab-4aeb-b797-2bddb2b5e3fb",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32[]),
        Direction = PinDirection.In,
        Name = nameof(InPinLowerBounds),
        DisplayName = "LowerBounds",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLowerBounds { get; set; } 

        [DataPinDefinition(
        Id = "c628c53b-062a-4260-a07b-b2252dfcd9f8",
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