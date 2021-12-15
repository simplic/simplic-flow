// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemInt32TryParse_String_Int32_), DisplayName = "TryParse(String,Int32&)", Category = "System/Int32")]
    public class SystemInt32TryParse_String_Int32_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Int32.TryParse(
                scope.GetValue<System.String>(InPinS)
                , out System.Int32 Resultvar);
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemInt32TryParse_String_Int32_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemInt32TryParse_String_Int32_); 
        public override string FriendlyName => nameof(SystemInt32TryParse_String_Int32_); 

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
        Id = "10b54425-6214-4ff7-a754-97eb870e21d8",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinS),
        DisplayName = "S",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinS { get; set; } 

        [DataPinDefinition(
        Id = "bda3b487-e26a-474f-8fad-1c587c65f470",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "d9749e0e-6bb8-4920-b4b4-8790cbfcc5a1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutParameterPinResult),
        DisplayName = "Result",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutParameterPinResult { get; set; } 

    }
}