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
        Id = "a8938252-72cb-4c33-b509-024dac84df2b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "01c2df6d-107c-4455-8467-6fd43f68c46b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TypeCode),
        Direction = PinDirection.In,
        Name = nameof(InPinTypeCode),
        DisplayName = "TypeCode",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinTypeCode { get; set; } 

        [DataPinDefinition(
        Id = "2c9091b8-02e6-40b1-8f09-b4da269a953b",
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