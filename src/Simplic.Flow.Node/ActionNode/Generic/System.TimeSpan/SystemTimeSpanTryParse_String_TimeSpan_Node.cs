// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemTimeSpanTryParse_String_TimeSpan_), DisplayName = "TryParse(String,TimeSpan&)", Category = "System/TimeSpan")]
    public class SystemTimeSpanTryParse_String_TimeSpan_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.TimeSpan.TryParse(
                scope.GetValue<System.String>(InPinS)
                , out System.TimeSpan Resultvar);
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemTimeSpanTryParse_String_TimeSpan_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemTimeSpanTryParse_String_TimeSpan_); 
        public override string FriendlyName => nameof(SystemTimeSpanTryParse_String_TimeSpan_); 

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
        Id = "6ec55e54-c8ea-4e9a-9c7b-4e4d67f7b400",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinS),
        DisplayName = "S",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinS { get; set; } 

        [DataPinDefinition(
        Id = "8a6c98bd-a05b-4cca-848c-7f6b3f176c28",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "d50e19de-f4f7-45e9-a297-00fcc9691157",
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