// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeToday), DisplayName = "DateTime.Today", Category = "System/DateTime")]
    public class SystemDateTimeToday : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.Today;
                scope.SetValue(OutPinStaticValue, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeToday: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeToday); 
        public override string FriendlyName => nameof(SystemDateTimeToday); 

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
        Id = "32820ad5-95aa-49a2-88eb-58d54cd37b1e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinStaticValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinStaticValue { get; set; } 

        [DataPinDefinition(
        Id = "cad82fe7-ffd6-4ff0-b103-8651a114fe56",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDate),
        DisplayName = "Date",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDate { get; set; } 

        [DataPinDefinition(
        Id = "14fe2ec6-fa59-4c25-a745-e6ba0662c551",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDay),
        DisplayName = "Day",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDay { get; set; } 

        [DataPinDefinition(
        Id = "929e5196-89da-4b54-8921-be06b56ced8e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DayOfWeek),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDayOfWeek),
        DisplayName = "DayOfWeek",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDayOfWeek { get; set; } 

        [DataPinDefinition(
        Id = "d490ae21-8cf1-49ac-bb05-8ece9011da9a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDayOfYear),
        DisplayName = "DayOfYear",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDayOfYear { get; set; } 

        [DataPinDefinition(
        Id = "cb119809-566b-4922-aae2-16dfc33202d5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubHour),
        DisplayName = "Hour",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubHour { get; set; } 

        [DataPinDefinition(
        Id = "81f1b0e9-9e90-4c86-aa95-aba252c818cb",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTimeKind),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubKind),
        DisplayName = "Kind",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubKind { get; set; } 

        [DataPinDefinition(
        Id = "b24c0d3c-2a68-4942-b566-d050bdfac598",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMillisecond),
        DisplayName = "Millisecond",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMillisecond { get; set; } 

        [DataPinDefinition(
        Id = "be957d82-63d3-4a5f-9531-1b447f5fed9f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMinute),
        DisplayName = "Minute",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMinute { get; set; } 

        [DataPinDefinition(
        Id = "3b02dd07-a208-48e8-a738-83aead2e52d9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMonth),
        DisplayName = "Month",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMonth { get; set; } 

        [DataPinDefinition(
        Id = "f80b3c12-eb52-443e-b49d-1d61180763b7",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubSecond),
        DisplayName = "Second",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubSecond { get; set; } 

        [DataPinDefinition(
        Id = "01dc764f-6d1a-47b5-8d08-880cfc7feb62",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubTicks),
        DisplayName = "Ticks",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubTicks { get; set; } 

        [DataPinDefinition(
        Id = "3e456e09-0591-4fd2-aec1-2de0813fb630",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubTimeOfDay),
        DisplayName = "TimeOfDay",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubTimeOfDay { get; set; } 

        [DataPinDefinition(
        Id = "5d90ec87-c160-4602-8942-753c2ed63b55",
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