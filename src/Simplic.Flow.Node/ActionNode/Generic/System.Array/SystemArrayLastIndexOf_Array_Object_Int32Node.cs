// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayLastIndexOf_Array_Object_Int32), DisplayName = "LastIndexOf(Array,Object,Int32)", Category = "System/Array")]
    public class SystemArrayLastIndexOf_Array_Object_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Array.LastIndexOf(
                scope.GetValue<System.Array>(InPinArray),
                scope.GetValue<System.Object>(InPinValue),
                scope.GetValue<System.Int32>(InPinStartIndex));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayLastIndexOf_Array_Object_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayLastIndexOf_Array_Object_Int32); 
        public override string FriendlyName => nameof(SystemArrayLastIndexOf_Array_Object_Int32); 

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
        Id = "493bde52-52de-4a76-b509-1747aa76aa35",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinArray),
        DisplayName = "Array",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArray { get; set; } 

        [DataPinDefinition(
        Id = "cf105d0b-6422-4f01-8842-58ad8a82fd20",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "88dfd52b-50be-41d2-9062-6275a2ec1e10",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinStartIndex),
        DisplayName = "StartIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStartIndex { get; set; } 

        [DataPinDefinition(
        Id = "2ce212b3-0cc1-4cde-844c-e27588121760",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}