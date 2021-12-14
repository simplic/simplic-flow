// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemTimeSpanTryParseExact_String_String__IFormatProvider_TimeSpanStyles_TimeSpan_), DisplayName = "TryParseExact(String,String[],IFormatProvider,TimeSpanStyles,TimeSpan&)", Category = "System/TimeSpan")]
    public class SystemTimeSpanTryParseExact_String_String__IFormatProvider_TimeSpanStyles_TimeSpan_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.TimeSpan.TryParseExact(
                scope.GetValue<System.String>(InPinInput),
                scope.GetValue<System.String[]>(InPinFormats),
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemTimeSpanTryParseExact_String_String__IFormatProvider_TimeSpanStyles_TimeSpan_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemTimeSpanTryParseExact_String_String__IFormatProvider_TimeSpanStyles_TimeSpan_); 
        public override string FriendlyName => nameof(SystemTimeSpanTryParseExact_String_String__IFormatProvider_TimeSpanStyles_TimeSpan_); 

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
        Id = "4c0bfa12-1ee0-4ff0-8ccf-9b0a20fe7974",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinInput),
        DisplayName = "Input",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInput { get; set; } 

        [DataPinDefinition(
        Id = "b9ee6030-9274-4113-9a61-1fc3d8f0b1f8",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.In,
        Name = nameof(InPinFormats),
        DisplayName = "Formats",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormats { get; set; } 

        [DataPinDefinition(
        Id = "64dced59-0bb6-4ced-98d3-d94b4b25bfd7",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinFormatProvider),
        DisplayName = "FormatProvider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormatProvider { get; set; } 

        [DataPinDefinition(
        Id = "7cfba8cf-9551-4b64-9480-7edee9943fe1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.TimeSpanStyles),
        Direction = PinDirection.In,
        Name = nameof(InPinStyles),
        DisplayName = "Styles",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStyles { get; set; } 

        [DataPinDefinition(
        Id = "c6e46241-c8b5-4dae-888b-dcf1a5719840",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "00b6d245-a461-417c-b4a2-c3e1cdc6f3ea",
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