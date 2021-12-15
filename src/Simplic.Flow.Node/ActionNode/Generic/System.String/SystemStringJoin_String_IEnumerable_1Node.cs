// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringJoin_String_IEnumerable_1), DisplayName = "Join(String,IEnumerable`1)", Category = "System/String")]
    public class SystemStringJoin_String_IEnumerable_1 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Join(
                scope.GetValue<System.String>(InPinSeparator),
                scope.GetValue<System.Collections.Generic.IEnumerable<System.String > >(InPinValues));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringJoin_String_IEnumerable_1: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringJoin_String_IEnumerable_1); 
        public override string FriendlyName => nameof(SystemStringJoin_String_IEnumerable_1); 

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
        Id = "57106f13-39a1-4606-a2d1-7a98a5455c21",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSeparator),
        DisplayName = "Separator",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSeparator { get; set; } 

        [DataPinDefinition(
        Id = "408c33aa-3fce-4795-bcd2-c629e659bbbd",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Collections.Generic.IEnumerable<System.String>),
        Direction = PinDirection.In,
        Name = nameof(InPinValues),
        DisplayName = "Values",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValues { get; set; } 

        [DataPinDefinition(
        Id = "1cab7acc-0c16-4391-9c6f-a6836e5fce6f",
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