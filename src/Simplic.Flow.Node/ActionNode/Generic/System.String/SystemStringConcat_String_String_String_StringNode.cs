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
        Id = "b073c381-533a-4cd1-9a86-0f4279874172",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr0),
        DisplayName = "Str0",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr0 { get; set; } 

        [DataPinDefinition(
        Id = "70f9462d-87e7-4226-a4b0-41cbd7b55683",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr1),
        DisplayName = "Str1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr1 { get; set; } 

        [DataPinDefinition(
        Id = "7b266b56-e9bb-4d1e-be25-d1e6bf22fb5e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr2),
        DisplayName = "Str2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr2 { get; set; } 

        [DataPinDefinition(
        Id = "4558924f-57fd-441a-84ec-9403242812db",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr3),
        DisplayName = "Str3",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr3 { get; set; } 

        [DataPinDefinition(
        Id = "e0d630d1-b516-40b4-85fe-0841a8380256",
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