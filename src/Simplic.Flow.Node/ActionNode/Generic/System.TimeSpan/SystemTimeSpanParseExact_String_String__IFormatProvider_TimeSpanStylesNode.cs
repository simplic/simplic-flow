// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemTimeSpanParseExact_String_String__IFormatProvider_TimeSpanStyles), DisplayName = "ParseExact(String,String[],IFormatProvider,TimeSpanStyles)", Category = "System/TimeSpan")]
    public class SystemTimeSpanParseExact_String_String__IFormatProvider_TimeSpanStyles : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.TimeSpan.ParseExact(
                scope.GetValue<System.String>(InPinInput),
                scope.GetValue<System.String[]>(InPinFormats),
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemTimeSpanParseExact_String_String__IFormatProvider_TimeSpanStyles: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemTimeSpanParseExact_String_String__IFormatProvider_TimeSpanStyles); 
        public override string FriendlyName => nameof(SystemTimeSpanParseExact_String_String__IFormatProvider_TimeSpanStyles); 

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
        Id = "de457d7f-7d77-43d7-acee-a43e3e1be7d7",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinInput),
        DisplayName = "Input",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInput { get; set; } 

        [DataPinDefinition(
        Id = "7d1451d0-ce53-49d7-aba0-bd05a1cdcbc1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.In,
        Name = nameof(InPinFormats),
        DisplayName = "Formats",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormats { get; set; } 

        [DataPinDefinition(
        Id = "23087bc6-4c14-4d9c-920c-fd629ab7e6cd",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinFormatProvider),
        DisplayName = "FormatProvider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormatProvider { get; set; } 

        [DataPinDefinition(
        Id = "db75a1ff-4cd1-4613-9e74-9a2295965a88",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.TimeSpanStyles),
        Direction = PinDirection.In,
        Name = nameof(InPinStyles),
        DisplayName = "Styles",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStyles { get; set; } 

        [DataPinDefinition(
        Id = "2db8e83e-6795-4569-8eb0-f11b1c5a6335",
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