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
        Id = "806c5f06-b53d-45b4-9e85-36ddd74db40f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinS),
        DisplayName = "S",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinS { get; set; } 

        [DataPinDefinition(
        Id = "3eecdf78-7b8a-40fa-882f-7a9ed4d70664",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.In,
        Name = nameof(InPinFormats),
        DisplayName = "Formats",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormats { get; set; } 

        [DataPinDefinition(
        Id = "b36faf50-d6fd-4939-b3f1-44bf802a4e46",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinProvider),
        DisplayName = "Provider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinProvider { get; set; } 

        [DataPinDefinition(
        Id = "2bb40129-e687-4a4e-a389-1e63323559ce",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.DateTimeStyles),
        Direction = PinDirection.In,
        Name = nameof(InPinStyle),
        DisplayName = "Style",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStyle { get; set; } 

        [DataPinDefinition(
        Id = "8973145a-f0da-4147-828f-f21009a37b35",
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