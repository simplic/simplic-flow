// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeDaysInMonth_Int32_Int32), DisplayName = "DaysInMonth(Int32,Int32)", Category = "System/DateTime")]
    public class SystemDateTimeDaysInMonth_Int32_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.DaysInMonth(
                scope.GetValue<System.Int32>(InPinYear),
                scope.GetValue<System.Int32>(InPinMonth));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeDaysInMonth_Int32_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeDaysInMonth_Int32_Int32); 
        public override string FriendlyName => nameof(SystemDateTimeDaysInMonth_Int32_Int32); 

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
        Id = "74a319ac-8e21-4f87-ba4e-6fd58812c1df",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinYear),
        DisplayName = "Year",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinYear { get; set; } 

        [DataPinDefinition(
        Id = "7db20ca8-49c8-4d87-b5be-a4c3bfc04d94",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinMonth),
        DisplayName = "Month",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinMonth { get; set; } 

        [DataPinDefinition(
        Id = "5168611a-5b1f-47eb-96e9-854361e819c5",
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