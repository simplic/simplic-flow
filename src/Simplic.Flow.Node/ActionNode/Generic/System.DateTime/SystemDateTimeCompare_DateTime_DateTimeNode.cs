// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeCompare_DateTime_DateTime), DisplayName = "Compare(DateTime,DateTime)", Category = "System/DateTime")]
    public class SystemDateTimeCompare_DateTime_DateTime : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.Compare(
                scope.GetValue<System.DateTime>(InPinT1),
                scope.GetValue<System.DateTime>(InPinT2));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeCompare_DateTime_DateTime: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeCompare_DateTime_DateTime); 
        public override string FriendlyName => nameof(SystemDateTimeCompare_DateTime_DateTime); 

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
        Id = "87e45bc0-bca0-4f61-a4e1-8f8e59874c19",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.In,
        Name = nameof(InPinT1),
        DisplayName = "T1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinT1 { get; set; } 

        [DataPinDefinition(
        Id = "ab09b0b3-42c1-4284-957b-99a270d00103",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.In,
        Name = nameof(InPinT2),
        DisplayName = "T2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinT2 { get; set; } 

        [DataPinDefinition(
        Id = "a93056c3-ecf9-4751-a5d9-6fbd6d229ca5",
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