// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeTryParseExact_String_String__IFormatProvider_DateTimeStyles_DateTime_), DisplayName = "TryParseExact(String,String[],IFormatProvider,DateTimeStyles,DateTime&)", Category = "System/DateTime")]
    public class SystemDateTimeTryParseExact_String_String__IFormatProvider_DateTimeStyles_DateTime_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.TryParseExact(
                scope.GetValue<System.String>(InPinS),
                scope.GetValue<System.String[]>(InPinFormats),
                scope.GetValue<System.IFormatProvider>(InPinProvider),
                scope.GetValue<System.Globalization.DateTimeStyles>(InPinStyle)
                , out System.DateTime Resultvar);
                scope.SetValue(OutPinReturn, returnValue);

                scope.SetValue(OutParameterPinResult, Resultvar);
                if (OutNodeTrue != null && returnValue)
                {
                    runtime.EnqueueNode(OutNodeTrue, scope);
                } 
                else if (OutNodeFalse != null && !returnValue)
                {
                    runtime.EnqueueNode(OutNodeFalse, scope);
                }
                    
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeTryParseExact_String_String__IFormatProvider_DateTimeStyles_DateTime_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeTryParseExact_String_String__IFormatProvider_DateTimeStyles_DateTime_); 
        public override string FriendlyName => nameof(SystemDateTimeTryParseExact_String_String__IFormatProvider_DateTimeStyles_DateTime_); 

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
        Id = "0e01bc5d-8234-488d-983f-85e3393e613c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinS),
        DisplayName = "S",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinS { get; set; } 

        [DataPinDefinition(
        Id = "c3050f67-f355-4047-820f-314357980779",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.In,
        Name = nameof(InPinFormats),
        DisplayName = "Formats",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormats { get; set; } 

        [DataPinDefinition(
        Id = "c2773dd6-3052-4520-8d52-324655bd8379",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinProvider),
        DisplayName = "Provider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinProvider { get; set; } 

        [DataPinDefinition(
        Id = "dfb2fe78-e540-4e00-920e-107ed1921d19",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.DateTimeStyles),
        Direction = PinDirection.In,
        Name = nameof(InPinStyle),
        DisplayName = "Style",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStyle { get; set; } 

        [DataPinDefinition(
        Id = "5d6ebea9-58de-442f-b973-8bf2191d5e7c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "492626ad-c41f-443b-bbfc-ca67e23d338b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutParameterPinResult),
        DisplayName = "Result",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutParameterPinResult { get; set; } 

    }
}