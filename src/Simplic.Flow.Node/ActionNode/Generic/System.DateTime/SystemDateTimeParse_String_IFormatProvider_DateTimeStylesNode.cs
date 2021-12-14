// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeParse_String_IFormatProvider_DateTimeStyles), DisplayName = "Parse(String,IFormatProvider,DateTimeStyles)", Category = "System/DateTime")]
    public class SystemDateTimeParse_String_IFormatProvider_DateTimeStyles : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.Parse(
                scope.GetValue<System.String>(InPinS),
                scope.GetValue<System.IFormatProvider>(InPinProvider),
                scope.GetValue<System.Globalization.DateTimeStyles>(InPinStyles));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeParse_String_IFormatProvider_DateTimeStyles: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeParse_String_IFormatProvider_DateTimeStyles); 
        public override string FriendlyName => nameof(SystemDateTimeParse_String_IFormatProvider_DateTimeStyles); 

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
        Id = "7faafcf9-43b6-4304-8501-7819109f9b1c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinS),
        DisplayName = "S",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinS { get; set; } 

        [DataPinDefinition(
        Id = "dd3dd9a2-703a-4c48-bcb1-fa4d469d2a3d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinProvider),
        DisplayName = "Provider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinProvider { get; set; } 

        [DataPinDefinition(
        Id = "8aa06010-1807-4e9b-ad18-faad23990495",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.DateTimeStyles),
        Direction = PinDirection.In,
        Name = nameof(InPinStyles),
        DisplayName = "Styles",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStyles { get; set; } 

        [DataPinDefinition(
        Id = "0f1894d8-9737-4696-b88b-740ead9092cf",
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