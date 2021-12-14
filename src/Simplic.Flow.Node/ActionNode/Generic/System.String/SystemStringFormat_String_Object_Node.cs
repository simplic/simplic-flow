// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringFormat_String_Object_), DisplayName = "Format(String,Object[])", Category = "System/String")]
    public class SystemStringFormat_String_Object_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Format(
                scope.GetValue<System.String>(InPinFormat),
                scope.GetValue<System.Object[]>(InPinArgs));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringFormat_String_Object_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringFormat_String_Object_); 
        public override string FriendlyName => nameof(SystemStringFormat_String_Object_); 

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
        Id = "3f2450ad-1596-47ce-916a-791e56b571b3",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinFormat),
        DisplayName = "Format",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormat { get; set; } 

        [DataPinDefinition(
        Id = "cd72b4a2-6d23-4bff-94bb-10d9f5fd1e8b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object[]),
        Direction = PinDirection.In,
        Name = nameof(InPinArgs),
        DisplayName = "Args",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArgs { get; set; } 

        [DataPinDefinition(
        Id = "60f4f758-001f-48a9-ad6f-c615a53d0edd",
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