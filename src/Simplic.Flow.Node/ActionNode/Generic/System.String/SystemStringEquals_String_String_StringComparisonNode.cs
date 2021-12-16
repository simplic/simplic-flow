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
        Id = "34e9dbac-05c7-422b-8a24-3930a76b1f75",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinA),
        DisplayName = "A",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinA { get; set; } 

        [DataPinDefinition(
        Id = "0f666cf0-5c8a-4cff-9496-239a3fcf0bdd",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinB),
        DisplayName = "B",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinB { get; set; } 

        [DataPinDefinition(
        Id = "24546025-96cb-43a5-8624-ebb4c916d1b9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.StringComparison),
        Direction = PinDirection.In,
        Name = nameof(InPinComparisonType),
        DisplayName = "ComparisonType",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinComparisonType { get; set; } 

        [DataPinDefinition(
        Id = "2dd4ab7f-4921-46c2-9d09-43efb66d3f05",
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