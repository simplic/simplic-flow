// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringEquals_String_String_StringComparison), DisplayName = "Equals(String,String,StringComparison)", Category = "System/String")]
    public class SystemStringEquals_String_String_StringComparison : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Equals(
                scope.GetValue<System.String>(InPinA),
                scope.GetValue<System.String>(InPinB),
                scope.GetValue<System.StringComparison>(InPinComparisonType));
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringEquals_String_String_StringComparison: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringEquals_String_String_StringComparison); 
        public override string FriendlyName => nameof(SystemStringEquals_String_String_StringComparison); 

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
        Id = "fa6e9fc4-fad0-4fd2-af7c-a79a43f400e2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinA),
        DisplayName = "A",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinA { get; set; } 

        [DataPinDefinition(
        Id = "bda15f7a-54ac-4e65-b76b-2ddc4f25ada7",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinB),
        DisplayName = "B",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinB { get; set; } 

        [DataPinDefinition(
        Id = "3df1b5c8-54e2-4df8-95a7-7ea31bcf1a96",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.StringComparison),
        Direction = PinDirection.In,
        Name = nameof(InPinComparisonType),
        DisplayName = "ComparisonType",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinComparisonType { get; set; } 

        [DataPinDefinition(
        Id = "3a7a2528-c6b1-446b-a450-226d47e091ce",
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