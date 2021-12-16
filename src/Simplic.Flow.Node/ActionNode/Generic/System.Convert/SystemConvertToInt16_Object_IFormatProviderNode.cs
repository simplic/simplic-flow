// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemConvertToInt16_Object_IFormatProvider), DisplayName = "ToInt16(Object,IFormatProvider)", Category = "System/Convert")]
    public class SystemConvertToInt16_Object_IFormatProvider : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Convert.ToInt16(
                scope.GetValue<System.Object>(InPinValue),
                scope.GetValue<System.IFormatProvider>(InPinProvider));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemConvertToInt16_Object_IFormatProvider: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemConvertToInt16_Object_IFormatProvider); 
        public override string FriendlyName => nameof(SystemConvertToInt16_Object_IFormatProvider); 

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
        Id = "1c0ba221-28e6-4b1f-835a-3d60657cbb6b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "0a3124ed-9399-4a42-a290-c4ff407c6168",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinProvider),
        DisplayName = "Provider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinProvider { get; set; } 

        [DataPinDefinition(
        Id = "8de3a81c-9c1a-4d1e-8bdc-4bc926cd2fd4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int16),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}