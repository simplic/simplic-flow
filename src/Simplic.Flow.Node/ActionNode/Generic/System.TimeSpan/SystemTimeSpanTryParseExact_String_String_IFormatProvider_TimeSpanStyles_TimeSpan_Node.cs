// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemTimeSpanTryParseExact_String_String_IFormatProvider_TimeSpanStyles_TimeSpan_), DisplayName = "TryParseExact(String,String,IFormatProvider,TimeSpanStyles,TimeSpan&)", Category = "System/TimeSpan")]
    public class SystemTimeSpanTryParseExact_String_String_IFormatProvider_TimeSpanStyles_TimeSpan_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.TimeSpan.TryParseExact(
                scope.GetValue<System.String>(InPinInput),
                scope.GetValue<System.String>(InPinFormat),
                scope.GetValue<System.IFormatProvider>(InPinFormatProvider),
                scope.GetValue<System.Globalization.TimeSpanStyles>(InPinStyles)
                , out System.TimeSpan Resultvar);
                scope.SetValue(OutPinReturn, returnValue);

                scope.SetValue(OutParameterPinResult, Resultvar);
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemTimeSpanTryParseExact_String_String_IFormatProvider_TimeSpanStyles_TimeSpan_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemTimeSpanTryParseExact_String_String_IFormatProvider_TimeSpanStyles_TimeSpan_); 
        public override string FriendlyName => nameof(SystemTimeSpanTryParseExact_String_String_IFormatProvider_TimeSpanStyles_TimeSpan_); 

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

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "True", 
        Name = nameof(OutNodeTrue), 
        AllowMultiple = false)] 
        public ActionNode OutNodeTrue { get; set; } 

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "False", 
        Name = nameof(OutNodeFalse), 
        AllowMultiple = false)] 
        public ActionNode OutNodeFalse { get; set; } 

        [DataPinDefinition(
        Id = "181bdec3-1c13-4c40-98a6-ffc43cfc6a43",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinInput),
        DisplayName = "Input",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInput { get; set; } 

        [DataPinDefinition(
        Id = "6e7d540a-cd7f-44f1-b31e-83b221f6e6bb",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinFormat),
        DisplayName = "Format",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormat { get; set; } 

        [DataPinDefinition(
        Id = "bd1ae338-6bda-47c8-bdd1-e85db0f56cf5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinFormatProvider),
        DisplayName = "FormatProvider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormatProvider { get; set; } 

        [DataPinDefinition(
        Id = "7360c807-c4d4-4f9d-a877-a18d504711f4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.TimeSpanStyles),
        Direction = PinDirection.In,
        Name = nameof(InPinStyles),
        DisplayName = "Styles",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStyles { get; set; } 

        [DataPinDefinition(
        Id = "49c2638b-99f8-4a97-8bd2-53b01eea17cc",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "db443543-940d-4946-9b3f-39351978f803",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.Out,
        Name = nameof(OutParameterPinResult),
        DisplayName = "Result",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutParameterPinResult { get; set; } 

    }
}