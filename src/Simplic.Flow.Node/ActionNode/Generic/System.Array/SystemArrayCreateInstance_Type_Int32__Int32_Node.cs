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
        Id = "93535083-0ac2-40c8-9967-8b0e5717d8f2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Type),
        Direction = PinDirection.In,
        Name = nameof(InPinElementType),
        DisplayName = "ElementType",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinElementType { get; set; } 

        [DataPinDefinition(
        Id = "fa54b9ee-26a0-416d-b404-634e62b2e832",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32[]),
        Direction = PinDirection.In,
        Name = nameof(InPinLengths),
        DisplayName = "Lengths",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLengths { get; set; } 

        [DataPinDefinition(
        Id = "cab639bc-48d7-472f-bb82-3acac7ff6011",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32[]),
        Direction = PinDirection.In,
        Name = nameof(InPinLowerBounds),
        DisplayName = "LowerBounds",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLowerBounds { get; set; } 

        [DataPinDefinition(
        Id = "01d5c159-7263-45a8-a3b2-46ff55a77814",
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