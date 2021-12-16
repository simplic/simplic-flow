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
        Id = "6962a120-6a57-4987-a64e-e67eff7c5f51",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinArray),
        DisplayName = "Array",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArray { get; set; } 

        [DataPinDefinition(
        Id = "c65f3067-8f08-4b96-87dd-d0d749f21c2e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndex),
        DisplayName = "Index",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndex { get; set; } 

        [DataPinDefinition(
        Id = "24590ea3-3cac-445e-a577-f42331832c9d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "9f8efc02-3bf3-472d-8dfa-e9df5aa4dfae",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "b2dc065e-dd72-4071-b60e-d44998eb05c5",
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