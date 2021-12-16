// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayLastIndexOf_Array_Object_Int32_Int32), DisplayName = "LastIndexOf(Array,Object,Int32,Int32)", Category = "System/Array")]
    public class SystemArrayLastIndexOf_Array_Object_Int32_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Array.LastIndexOf(
                scope.GetValue<System.Array>(InPinArray),
                scope.GetValue<System.Object>(InPinValue),
                scope.GetValue<System.Int32>(InPinStartIndex),
                scope.GetValue<System.Int32>(InPinCount));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayLastIndexOf_Array_Object_Int32_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayLastIndexOf_Array_Object_Int32_Int32); 
        public override string FriendlyName => nameof(SystemArrayLastIndexOf_Array_Object_Int32_Int32); 

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
        Id = "3d7c628c-487e-421e-8cf8-b4656e95c230",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinArray),
        DisplayName = "Array",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArray { get; set; } 

        [DataPinDefinition(
        Id = "a95d9a4d-1666-49b5-a326-f6c93989e604",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "ef5441a2-5acd-40dc-ae80-2623282adb77",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinStartIndex),
        DisplayName = "StartIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStartIndex { get; set; } 

        [DataPinDefinition(
        Id = "61a8d9b4-ffb9-4eba-a56b-0ab24f7236b5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinCount),
        DisplayName = "Count",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinCount { get; set; } 

        [DataPinDefinition(
        Id = "04ac035d-1f97-4c63-b20d-549d8a19feab",
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