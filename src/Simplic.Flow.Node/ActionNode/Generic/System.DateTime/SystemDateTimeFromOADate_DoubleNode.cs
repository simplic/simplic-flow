// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeFromOADate_Double), DisplayName = "FromOADate(Double)", Category = "System/DateTime")]
    public class SystemDateTimeFromOADate_Double : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.FromOADate(
                scope.GetValue<System.Double>(InPinD));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeFromOADate_Double: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeFromOADate_Double); 
        public override string FriendlyName => nameof(SystemDateTimeFromOADate_Double); 

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
        Id = "649f707a-be96-418b-824b-2bb861afb945",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Double),
        Direction = PinDirection.In,
        Name = nameof(InPinD),
        DisplayName = "D",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinD { get; set; } 

        [DataPinDefinition(
        Id = "ff055bc6-01eb-4e53-a3de-e311c3202bfc",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}