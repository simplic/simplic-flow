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
        Id = "865a25f6-022e-42a4-919e-46536b141f92",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr0),
        DisplayName = "Str0",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr0 { get; set; } 

        [DataPinDefinition(
        Id = "62f471ec-47b0-4457-8a8d-eb925f70ed95",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr1),
        DisplayName = "Str1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr1 { get; set; } 

        [DataPinDefinition(
        Id = "d760b2aa-bdf5-43b2-afd4-cefdc9c6a29b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStr2),
        DisplayName = "Str2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStr2 { get; set; } 

        [DataPinDefinition(
        Id = "ed1ea238-941f-400a-be66-589b84029677",
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