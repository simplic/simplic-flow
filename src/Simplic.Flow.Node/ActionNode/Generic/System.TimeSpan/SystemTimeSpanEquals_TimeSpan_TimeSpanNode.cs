// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemTimeSpanEquals_TimeSpan_TimeSpan), DisplayName = "Equals(TimeSpan,TimeSpan)", Category = "System/TimeSpan")]
    public class SystemTimeSpanEquals_TimeSpan_TimeSpan : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.TimeSpan.Equals(
                scope.GetValue<System.TimeSpan>(InPinT1),
                scope.GetValue<System.TimeSpan>(InPinT2));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemTimeSpanEquals_TimeSpan_TimeSpan: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemTimeSpanEquals_TimeSpan_TimeSpan); 
        public override string FriendlyName => nameof(SystemTimeSpanEquals_TimeSpan_TimeSpan); 

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

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "True", 
        Name = nameof(OutNodeTrue), 
        AllowMultiple = false)] 
        public ActionNode OutNodeTrue { get; set; } 

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "False", 
        Name = nameof(OutNodeFalse), 
        AllowMultiple = false)] 
        public ActionNode OutNodeFalse { get; set; } 

        [DataPinDefinition(
        Id = "c9f28466-d456-4bc0-a211-82af309a464c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.In,
        Name = nameof(InPinT1),
        DisplayName = "T1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinT1 { get; set; } 

        [DataPinDefinition(
        Id = "482c8fde-80f2-4e5e-9c22-3df96803692d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.In,
        Name = nameof(InPinT2),
        DisplayName = "T2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinT2 { get; set; } 

        [DataPinDefinition(
        Id = "8f3eeb17-8e73-4f56-b3f8-a5ebf83d859d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}