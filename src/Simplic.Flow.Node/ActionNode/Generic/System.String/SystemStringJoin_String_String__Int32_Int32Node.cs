// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringJoin_String_String__Int32_Int32), DisplayName = "Join(String,String[],Int32,Int32)", Category = "System/String")]
    public class SystemStringJoin_String_String__Int32_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Join(
                scope.GetValue<System.String>(InPinSeparator),
                scope.GetValue<System.String[]>(InPinValue),
                scope.GetValue<System.Int32>(InPinStartIndex),
                scope.GetValue<System.Int32>(InPinCount));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringJoin_String_String__Int32_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringJoin_String_String__Int32_Int32); 
        public override string FriendlyName => nameof(SystemStringJoin_String_String__Int32_Int32); 

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
        Id = "2c3f011c-a077-489f-b202-62556f40e377",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSeparator),
        DisplayName = "Separator",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSeparator { get; set; } 

        [DataPinDefinition(
        Id = "cb8132db-cd88-458b-953f-7db50f06ac38",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "e0e4f3a7-e26d-4a38-bde2-2eaec60484f3",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinStartIndex),
        DisplayName = "StartIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStartIndex { get; set; } 

        [DataPinDefinition(
        Id = "9ae25a95-9e30-4d7b-a0ae-eefe1475d7e8",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinCount),
        DisplayName = "Count",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinCount { get; set; } 

        [DataPinDefinition(
        Id = "78efa708-de9c-4b38-85a3-636a8bc000ba",
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