// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeUtcNow), DisplayName = "DateTime.UtcNow", Category = "System/DateTime")]
    public class SystemDateTimeUtcNow : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.UtcNow;
                scope.SetValue(OutPinStaticValue, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeUtcNow: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeUtcNow); 
        public override string FriendlyName => nameof(SystemDateTimeUtcNow); 

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
        Id = "f6f1f79e-1e37-47c9-929a-283b05105adc",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinStaticValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinStaticValue { get; set; } 

        [DataPinDefinition(
        Id = "74318c8b-2ef1-414b-a8c2-b2087d31c6dd",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDate),
        DisplayName = "Date",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDate { get; set; } 

        [DataPinDefinition(
        Id = "9d30a511-72c0-4922-ad77-bbbd27c44877",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDay),
        DisplayName = "Day",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDay { get; set; } 

        [DataPinDefinition(
        Id = "12084b3c-4166-4c74-868e-a40739ed52ed",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DayOfWeek),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDayOfWeek),
        DisplayName = "DayOfWeek",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDayOfWeek { get; set; } 

        [DataPinDefinition(
        Id = "1488a402-2859-4c9f-ba14-054fe525904e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDayOfYear),
        DisplayName = "DayOfYear",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDayOfYear { get; set; } 

        [DataPinDefinition(
        Id = "378df2fe-1f89-4a6c-a962-e6560cf897b1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubHour),
        DisplayName = "Hour",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubHour { get; set; } 

        [DataPinDefinition(
        Id = "aa7d8716-a48b-4e7b-9b04-21a7a1cf8a21",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTimeKind),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubKind),
        DisplayName = "Kind",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubKind { get; set; } 

        [DataPinDefinition(
        Id = "cb5a792d-c781-480a-a342-0e76c7fe8a29",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMillisecond),
        DisplayName = "Millisecond",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMillisecond { get; set; } 

        [DataPinDefinition(
        Id = "11751917-653f-4aed-9857-7a07e7a2ebf9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMinute),
        DisplayName = "Minute",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMinute { get; set; } 

        [DataPinDefinition(
        Id = "eb5393c2-ecee-4c38-9311-792b53dee4ce",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMonth),
        DisplayName = "Month",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMonth { get; set; } 

        [DataPinDefinition(
        Id = "74566ef6-50c3-4775-a2ff-ca2dd00933eb",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubSecond),
        DisplayName = "Second",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubSecond { get; set; } 

        [DataPinDefinition(
        Id = "70a890ef-fc35-45f6-8211-43f2aa84efda",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubTicks),
        DisplayName = "Ticks",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubTicks { get; set; } 

        [DataPinDefinition(
        Id = "ffa84452-17de-4f06-9406-f5e5fbee6c90",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubTimeOfDay),
        DisplayName = "TimeOfDay",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubTimeOfDay { get; set; } 

        [DataPinDefinition(
        Id = "f75e958b-4054-4c36-93f9-5136c84bc604",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubYear),
        DisplayName = "Year",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubYear { get; set; } 

    }
}