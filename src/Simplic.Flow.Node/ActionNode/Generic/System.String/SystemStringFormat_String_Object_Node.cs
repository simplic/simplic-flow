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
        Id = "81a35411-ca1a-4c45-977d-b302df1a3145",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinFormat),
        DisplayName = "Format",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormat { get; set; } 

        [DataPinDefinition(
        Id = "e8dea5bd-5c42-4ee5-b687-e7e87ab87e61",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object[]),
        Direction = PinDirection.In,
        Name = nameof(InPinArgs),
        DisplayName = "Args",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArgs { get; set; } 

        [DataPinDefinition(
        Id = "04cc75e7-4635-4977-944b-65d8db137f7b",
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