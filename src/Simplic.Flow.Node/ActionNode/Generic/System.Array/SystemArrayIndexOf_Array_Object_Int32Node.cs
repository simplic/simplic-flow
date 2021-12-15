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
        Id = "e6ff716d-0e94-49ab-bf10-628cbba2fed9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinArray),
        DisplayName = "Array",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArray { get; set; } 

        [DataPinDefinition(
        Id = "6e61e814-f152-437d-9694-017b17f69561",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "66cfb335-fb12-4d6e-867f-5455359323b6",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinStartIndex),
        DisplayName = "StartIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStartIndex { get; set; } 

        [DataPinDefinition(
        Id = "cb6d7771-2d57-493d-b763-64fecea72d1e",
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