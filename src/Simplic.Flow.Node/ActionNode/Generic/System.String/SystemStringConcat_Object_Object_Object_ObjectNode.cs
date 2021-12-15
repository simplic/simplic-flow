// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringConcat_Object_Object_Object_Object), DisplayName = "Concat(Object,Object,Object,Object)", Category = "System/String")]
    public class SystemStringConcat_Object_Object_Object_Object : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Concat(
                scope.GetValue<System.Object>(InPinArg0),
                scope.GetValue<System.Object>(InPinArg1),
                scope.GetValue<System.Object>(InPinArg2),
                scope.GetValue<System.Object>(InPinArg3));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringConcat_Object_Object_Object_Object: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringConcat_Object_Object_Object_Object); 
        public override string FriendlyName => nameof(SystemStringConcat_Object_Object_Object_Object); 

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
        Id = "ae9cd49a-c820-43b0-a6a1-085aa6c24ed9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg0),
        DisplayName = "Arg0",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg0 { get; set; } 

        [DataPinDefinition(
        Id = "df6dfdb6-b2b3-4168-bb87-64ea4348a455",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg1),
        DisplayName = "Arg1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg1 { get; set; } 

        [DataPinDefinition(
        Id = "0285e746-5a5c-4983-939d-42b6ddd4e94f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg2),
        DisplayName = "Arg2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg2 { get; set; } 

        [DataPinDefinition(
        Id = "3452ec2e-750c-431a-8a57-1ec641c26d42",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg3),
        DisplayName = "Arg3",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg3 { get; set; } 

        [DataPinDefinition(
        Id = "e57afd6e-b0dd-445b-8fa2-339d523f694f",
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