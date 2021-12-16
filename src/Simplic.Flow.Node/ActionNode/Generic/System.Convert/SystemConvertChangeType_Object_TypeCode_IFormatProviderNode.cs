// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemConvertChangeType_Object_TypeCode_IFormatProvider), DisplayName = "ChangeType(Object,TypeCode,IFormatProvider)", Category = "System/Convert")]
    public class SystemConvertChangeType_Object_TypeCode_IFormatProvider : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Convert.ChangeType(
                scope.GetValue<System.Object>(InPinValue),
                scope.GetValue<System.TypeCode>(InPinTypeCode),
                scope.GetValue<System.IFormatProvider>(InPinProvider));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemConvertChangeType_Object_TypeCode_IFormatProvider: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemConvertChangeType_Object_TypeCode_IFormatProvider); 
        public override string FriendlyName => nameof(SystemConvertChangeType_Object_TypeCode_IFormatProvider); 

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
        Id = "161033ac-a44a-49e5-8c0e-a6702ca57280",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "c98bce54-4a2e-420d-8a4d-cf22df3495c6",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TypeCode),
        Direction = PinDirection.In,
        Name = nameof(InPinTypeCode),
        DisplayName = "TypeCode",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinTypeCode { get; set; } 

        [DataPinDefinition(
        Id = "e9b5cf17-e56c-4512-a1c5-d6108a88c908",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinProvider),
        DisplayName = "Provider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinProvider { get; set; } 

        [DataPinDefinition(
        Id = "c8b39939-50b6-4edd-ba98-a27c39eeaf5a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}