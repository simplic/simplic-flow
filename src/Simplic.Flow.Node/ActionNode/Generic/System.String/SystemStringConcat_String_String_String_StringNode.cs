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
        Id = "21d21ba3-0e31-481e-9cd6-2808aa0468c3",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr0),
        DisplayName = "Str0",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr0 { get; set; } 

        [DataPinDefinition(
        Id = "6e3ebe73-0217-4841-9c02-11554dc06139",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr1),
        DisplayName = "Str1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr1 { get; set; } 

        [DataPinDefinition(
        Id = "b4928425-d9cb-4597-9de9-89e89bdb875b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr2),
        DisplayName = "Str2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr2 { get; set; } 

        [DataPinDefinition(
        Id = "1a0bc915-e8e6-42b6-b09d-e27a3ca0d01a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr3),
        DisplayName = "Str3",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr3 { get; set; } 

        [DataPinDefinition(
        Id = "47a7d34c-4659-40ff-8e9d-047257b56236",
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