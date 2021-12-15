// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringConcat_String_String_String), DisplayName = "Concat(String,String,String)", Category = "System/String")]
    public class SystemStringConcat_String_String_String : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Concat(
                scope.GetValue<System.String>(InPinStr0),
                scope.GetValue<System.String>(InPinStr1),
                scope.GetValue<System.String>(InPinStr2));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringConcat_String_String_String: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringConcat_String_String_String); 
        public override string FriendlyName => nameof(SystemStringConcat_String_String_String); 

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
        Id = "99fb5635-f93f-4726-bb4b-7178bcf7bef4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr0),
        DisplayName = "Str0",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr0 { get; set; } 

        [DataPinDefinition(
        Id = "6415cc69-69f4-479b-902c-852bfd315a56",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr1),
        DisplayName = "Str1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr1 { get; set; } 

        [DataPinDefinition(
        Id = "c23467ba-e016-400e-a46b-9b81276b6b2c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr2),
        DisplayName = "Str2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr2 { get; set; } 

        [DataPinDefinition(
        Id = "88150a4b-d75b-483e-9ba7-5d5c3793ca2d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}