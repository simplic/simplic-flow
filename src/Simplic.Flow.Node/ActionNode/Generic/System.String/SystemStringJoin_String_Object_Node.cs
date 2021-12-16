// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringJoin_String_Object_), DisplayName = "Join(String,Object[])", Category = "System/String")]
    public class SystemStringJoin_String_Object_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Join(
                scope.GetValue<System.String>(InPinSeparator),
                scope.GetValue<System.Object[]>(InPinValues));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringJoin_String_Object_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringJoin_String_Object_); 
        public override string FriendlyName => nameof(SystemStringJoin_String_Object_); 

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
        Id = "08a03cd8-6766-4e05-8c6a-9cba971a63da",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSeparator),
        DisplayName = "Separator",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSeparator { get; set; } 

        [DataPinDefinition(
        Id = "020d72ee-627b-4633-9c17-ff2d02d8efac",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object[]),
        Direction = PinDirection.In,
        Name = nameof(InPinValues),
        DisplayName = "Values",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValues { get; set; } 

        [DataPinDefinition(
        Id = "2372fe08-9a8c-43a6-88ab-9d70fe985378",
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