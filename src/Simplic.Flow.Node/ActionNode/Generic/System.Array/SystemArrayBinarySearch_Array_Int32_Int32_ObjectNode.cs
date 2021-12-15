// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayBinarySearch_Array_Int32_Int32_Object), DisplayName = "BinarySearch(Array,Int32,Int32,Object)", Category = "System/Array")]
    public class SystemArrayBinarySearch_Array_Int32_Int32_Object : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Array.BinarySearch(
                scope.GetValue<System.Array>(InPinArray),
                scope.GetValue<System.Int32>(InPinIndex),
                scope.GetValue<System.Int32>(InPinLength),
                scope.GetValue<System.Object>(InPinValue));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayBinarySearch_Array_Int32_Int32_Object: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayBinarySearch_Array_Int32_Int32_Object); 
        public override string FriendlyName => nameof(SystemArrayBinarySearch_Array_Int32_Int32_Object); 

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
        Id = "1ec5948c-3bd4-4503-a4d8-5efe96a9961d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinArray),
        DisplayName = "Array",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArray { get; set; } 

        [DataPinDefinition(
        Id = "7e508c32-a5bf-4566-bb14-fd29384c009c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndex),
        DisplayName = "Index",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndex { get; set; } 

        [DataPinDefinition(
        Id = "f133ab10-ffbc-4275-9187-e98469d6efa1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "d56463f7-1e3d-434b-b7a0-51fb8ea8063b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "5ce26375-f0ff-4997-b955-4fd8f0cc7fcc",
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