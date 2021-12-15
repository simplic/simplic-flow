// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemConvertChangeType_Object_TypeCode), DisplayName = "ChangeType(Object,TypeCode)", Category = "System/Convert")]
    public class SystemConvertChangeType_Object_TypeCode : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Convert.ChangeType(
                scope.GetValue<System.Object>(InPinValue),
                scope.GetValue<System.TypeCode>(InPinTypeCode));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemConvertChangeType_Object_TypeCode: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemConvertChangeType_Object_TypeCode); 
        public override string FriendlyName => nameof(SystemConvertChangeType_Object_TypeCode); 

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
        Id = "c7eda170-068a-4b29-a653-ef69c2c94a8f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "6845eb53-ff5c-40e4-a2bb-f0202dc9b1e5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TypeCode),
        Direction = PinDirection.In,
        Name = nameof(InPinTypeCode),
        DisplayName = "TypeCode",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinTypeCode { get; set; } 

        [DataPinDefinition(
        Id = "33eadf1a-c719-49ee-afd4-ad9646c46a3c",
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