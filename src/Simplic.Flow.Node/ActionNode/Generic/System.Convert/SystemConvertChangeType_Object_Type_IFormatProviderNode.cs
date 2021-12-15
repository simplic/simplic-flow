// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemConvertChangeType_Object_Type_IFormatProvider), DisplayName = "ChangeType(Object,Type,IFormatProvider)", Category = "System/Convert")]
    public class SystemConvertChangeType_Object_Type_IFormatProvider : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Convert.ChangeType(
                scope.GetValue<System.Object>(InPinValue),
                scope.GetValue<System.Type>(InPinConversionType),
                scope.GetValue<System.IFormatProvider>(InPinProvider));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemConvertChangeType_Object_Type_IFormatProvider: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemConvertChangeType_Object_Type_IFormatProvider); 
        public override string FriendlyName => nameof(SystemConvertChangeType_Object_Type_IFormatProvider); 

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
        Id = "eef37be5-499f-463f-9a0c-16d47c3bdf97",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "95e97546-59b3-4aa8-9265-3a14d78a60e4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Type),
        Direction = PinDirection.In,
        Name = nameof(InPinConversionType),
        DisplayName = "ConversionType",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinConversionType { get; set; } 

        [DataPinDefinition(
        Id = "ce23defe-23ba-4131-8540-e08c1da9b3f2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinProvider),
        DisplayName = "Provider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinProvider { get; set; } 

        [DataPinDefinition(
        Id = "444c2ed8-3cd7-4347-8425-d4183d6fbae4",
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