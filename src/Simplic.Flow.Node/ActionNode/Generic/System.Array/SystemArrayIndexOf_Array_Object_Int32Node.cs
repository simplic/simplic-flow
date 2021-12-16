// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayIndexOf_Array_Object_Int32), DisplayName = "IndexOf(Array,Object,Int32)", Category = "System/Array")]
    public class SystemArrayIndexOf_Array_Object_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Array.IndexOf(
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayIndexOf_Array_Object_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayIndexOf_Array_Object_Int32); 
        public override string FriendlyName => nameof(SystemArrayIndexOf_Array_Object_Int32); 

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
        Id = "750948d5-d1b6-403a-977b-70bcb3b5cb9f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinArray),
        DisplayName = "Array",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArray { get; set; } 

        [DataPinDefinition(
        Id = "04f3755e-d0fd-4a96-b446-cb69360181f9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "482a616c-5ba5-4527-90a4-ee170e52484c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinStartIndex),
        DisplayName = "StartIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStartIndex { get; set; } 

        [DataPinDefinition(
        Id = "346b09ce-273f-42a9-9342-fb857c201008",
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