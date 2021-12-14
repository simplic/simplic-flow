// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemTimeSpanParseExact_String_String_IFormatProvider_TimeSpanStyles), DisplayName = "ParseExact(String,String,IFormatProvider,TimeSpanStyles)", Category = "System/TimeSpan")]
    public class SystemTimeSpanParseExact_String_String_IFormatProvider_TimeSpanStyles : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.TimeSpan.ParseExact(
                scope.GetValue<System.String>(InPinInput),
                scope.GetValue<System.String>(InPinFormat),
                scope.GetValue<System.IFormatProvider>(InPinFormatProvider),
                scope.GetValue<System.Globalization.TimeSpanStyles>(InPinStyles));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemTimeSpanParseExact_String_String_IFormatProvider_TimeSpanStyles: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemTimeSpanParseExact_String_String_IFormatProvider_TimeSpanStyles); 
        public override string FriendlyName => nameof(SystemTimeSpanParseExact_String_String_IFormatProvider_TimeSpanStyles); 

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
        Id = "048018ec-8674-4b5a-827a-ccb30f9f79cf",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinInput),
        DisplayName = "Input",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInput { get; set; } 

        [DataPinDefinition(
        Id = "c007953c-f356-490b-8539-d318664f5481",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinFormat),
        DisplayName = "Format",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormat { get; set; } 

        [DataPinDefinition(
        Id = "f8fce05d-1d20-4b81-bd80-96d3ca6a8eed",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinFormatProvider),
        DisplayName = "FormatProvider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormatProvider { get; set; } 

        [DataPinDefinition(
        Id = "39a290da-43f5-4f55-803b-8782aada906c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.TimeSpanStyles),
        Direction = PinDirection.In,
        Name = nameof(InPinStyles),
        DisplayName = "Styles",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStyles { get; set; } 

        [DataPinDefinition(
        Id = "148090a2-aab5-4d3f-b002-9dbe79df5bd9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}