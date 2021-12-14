// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemTimeSpanCompare_TimeSpan_TimeSpan), DisplayName = "Compare(TimeSpan,TimeSpan)", Category = "System/TimeSpan")]
    public class SystemTimeSpanCompare_TimeSpan_TimeSpan : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.TimeSpan.Compare(
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemTimeSpanCompare_TimeSpan_TimeSpan: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemTimeSpanCompare_TimeSpan_TimeSpan); 
        public override string FriendlyName => nameof(SystemTimeSpanCompare_TimeSpan_TimeSpan); 

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
        Id = "a0cfabcf-7b19-4739-8304-6ce505ab7511",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.In,
        Name = nameof(InPinT1),
        DisplayName = "T1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinT1 { get; set; } 

        [DataPinDefinition(
        Id = "d492a5b3-e377-4e57-afd7-305fed26622a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.In,
        Name = nameof(InPinT2),
        DisplayName = "T2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinT2 { get; set; } 

        [DataPinDefinition(
        Id = "fe756476-1659-49bc-af00-05a776b10072",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}