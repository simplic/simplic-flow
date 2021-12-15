// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringEquals_String_String), DisplayName = "Equals(String,String)", Category = "System/String")]
    public class SystemStringEquals_String_String : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Equals(
                scope.GetValue<System.String>(InPinA),
                scope.GetValue<System.String>(InPinB));
                scope.SetValue(OutPinReturn, returnValue);

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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringEquals_String_String: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringEquals_String_String); 
        public override string FriendlyName => nameof(SystemStringEquals_String_String); 

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
        Id = "f94383d7-ac2a-456b-a442-6b0632f1aab5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinA),
        DisplayName = "A",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinA { get; set; } 

        [DataPinDefinition(
        Id = "55eea41f-709c-409e-b447-87ba7cdb4bfb",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinB),
        DisplayName = "B",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinB { get; set; } 

        [DataPinDefinition(
        Id = "eee1ae19-0b6d-4271-970a-61c08babd531",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}