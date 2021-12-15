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
        Id = "3d1bd806-f437-40eb-9628-1202f9cefd36",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.In,
        Name = nameof(InPinT1),
        DisplayName = "T1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinT1 { get; set; } 

        [DataPinDefinition(
        Id = "a5b2a21d-f2bf-462a-8dbe-f51632384f9f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.In,
        Name = nameof(InPinT2),
        DisplayName = "T2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinT2 { get; set; } 

        [DataPinDefinition(
        Id = "27c1811f-5ffd-45c8-a157-a6840a4324d6",
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