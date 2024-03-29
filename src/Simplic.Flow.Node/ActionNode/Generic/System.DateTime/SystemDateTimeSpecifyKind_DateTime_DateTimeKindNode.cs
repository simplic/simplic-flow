// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeSpecifyKind_DateTime_DateTimeKind), DisplayName = "SpecifyKind(DateTime,DateTimeKind)", Category = "System/DateTime")]
    public class SystemDateTimeSpecifyKind_DateTime_DateTimeKind : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.SpecifyKind(
                scope.GetValue<System.DateTime>(InPinValue),
                scope.GetValue<System.DateTimeKind>(InPinKind));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeSpecifyKind_DateTime_DateTimeKind: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeSpecifyKind_DateTime_DateTimeKind); 
        public override string FriendlyName => nameof(SystemDateTimeSpecifyKind_DateTime_DateTimeKind); 

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
        Id = "7fb0ecfb-cf47-4e4f-ad20-c3aadf181969",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "b7647a72-2868-4b8d-ba59-3a03c3de1921",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTimeKind),
        Direction = PinDirection.In,
        Name = nameof(InPinKind),
        DisplayName = "Kind",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinKind { get; set; } 

        [DataPinDefinition(
        Id = "05a923e4-867a-4cc6-8eb8-bd3c121baef1",
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