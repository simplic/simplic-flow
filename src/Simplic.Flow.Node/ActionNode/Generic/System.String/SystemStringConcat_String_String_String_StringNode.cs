// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringConcat_String_String_String_String), DisplayName = "Concat(String,String,String,String)", Category = "System/String")]
    public class SystemStringConcat_String_String_String_String : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Concat(
                scope.GetValue<System.String>(InPinStr0),
                scope.GetValue<System.String>(InPinStr1),
                scope.GetValue<System.String>(InPinStr2),
                scope.GetValue<System.String>(InPinStr3));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringConcat_String_String_String_String: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringConcat_String_String_String_String); 
        public override string FriendlyName => nameof(SystemStringConcat_String_String_String_String); 

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
        Id = "ffe2e1c3-3e4b-4c63-a554-071e8a9d14f9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr0),
        DisplayName = "Str0",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr0 { get; set; } 

        [DataPinDefinition(
        Id = "f40ac42f-1410-4da3-a239-30faf3ff8c1c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr1),
        DisplayName = "Str1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr1 { get; set; } 

        [DataPinDefinition(
        Id = "744a71b8-8937-4de4-8b2e-6cf1268f1eb5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr2),
        DisplayName = "Str2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr2 { get; set; } 

        [DataPinDefinition(
        Id = "822148fe-98d6-4d60-a38c-c27468b0f6d5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr3),
        DisplayName = "Str3",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr3 { get; set; } 

        [DataPinDefinition(
        Id = "acf24fb0-3a45-4b21-a80c-db2e83f624c5",
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