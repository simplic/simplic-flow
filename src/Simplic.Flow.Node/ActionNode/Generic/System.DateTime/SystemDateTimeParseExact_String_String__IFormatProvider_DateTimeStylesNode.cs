// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeParseExact_String_String__IFormatProvider_DateTimeStyles), DisplayName = "ParseExact(String,String[],IFormatProvider,DateTimeStyles)", Category = "System/DateTime")]
    public class SystemDateTimeParseExact_String_String__IFormatProvider_DateTimeStyles : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.ParseExact(
                scope.GetValue<System.String>(InPinS),
                scope.GetValue<System.String[]>(InPinFormats),
                scope.GetValue<System.IFormatProvider>(InPinProvider),
                scope.GetValue<System.Globalization.DateTimeStyles>(InPinStyle));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeParseExact_String_String__IFormatProvider_DateTimeStyles: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeParseExact_String_String__IFormatProvider_DateTimeStyles); 
        public override string FriendlyName => nameof(SystemDateTimeParseExact_String_String__IFormatProvider_DateTimeStyles); 

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
        Id = "1fa2155d-068e-4044-8a88-da91a6910890",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinS),
        DisplayName = "S",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinS { get; set; } 

        [DataPinDefinition(
        Id = "efabc79d-5123-4bc0-83ac-ffa967b05f6e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.In,
        Name = nameof(InPinFormats),
        DisplayName = "Formats",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormats { get; set; } 

        [DataPinDefinition(
        Id = "aa9688ae-b7e8-4d43-98d4-cb4b098d4a55",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinProvider),
        DisplayName = "Provider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinProvider { get; set; } 

        [DataPinDefinition(
        Id = "6935ea55-b023-4951-bc20-20493f6be4ef",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.DateTimeStyles),
        Direction = PinDirection.In,
        Name = nameof(InPinStyle),
        DisplayName = "Style",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStyle { get; set; } 

        [DataPinDefinition(
        Id = "9ff18c1c-a6fd-4977-b5f1-fd6adccdbf75",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}