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
        Id = "b5c1cc2e-07d1-467f-916e-d5a495f4d6d2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "db7bb057-7b32-4df1-8bb2-0cc3d5c5e807",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTimeKind),
        Direction = PinDirection.In,
        Name = nameof(InPinKind),
        DisplayName = "Kind",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinKind { get; set; } 

        [DataPinDefinition(
        Id = "b5e7121f-0741-4132-8518-e6ab8e315f8d",
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