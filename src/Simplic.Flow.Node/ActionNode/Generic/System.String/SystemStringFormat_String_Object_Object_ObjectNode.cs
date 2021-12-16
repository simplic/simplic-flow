// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringFormat_String_Object_Object_Object), DisplayName = "Format(String,Object,Object,Object)", Category = "System/String")]
    public class SystemStringFormat_String_Object_Object_Object : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Format(
                scope.GetValue<System.String>(InPinFormat),
                scope.GetValue<System.Object>(InPinArg0),
                scope.GetValue<System.Object>(InPinArg1),
                scope.GetValue<System.Object>(InPinArg2));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringFormat_String_Object_Object_Object: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringFormat_String_Object_Object_Object); 
        public override string FriendlyName => nameof(SystemStringFormat_String_Object_Object_Object); 

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
        Id = "b7484e59-713c-4fcb-8ac9-3ed1d0c94df8",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinFormat),
        DisplayName = "Format",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormat { get; set; } 

        [DataPinDefinition(
        Id = "1e76bd32-088e-4951-8420-c975c7605cb9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg0),
        DisplayName = "Arg0",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg0 { get; set; } 

        [DataPinDefinition(
        Id = "dc00fe9d-bf7d-4b31-9687-960f2b2a1f28",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg1),
        DisplayName = "Arg1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg1 { get; set; } 

        [DataPinDefinition(
        Id = "029f37f4-9a93-4dd6-a0f2-7051e2c0b75f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg2),
        DisplayName = "Arg2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg2 { get; set; } 

        [DataPinDefinition(
        Id = "c3e14454-7c13-4812-a00b-8d75f7e36d30",
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