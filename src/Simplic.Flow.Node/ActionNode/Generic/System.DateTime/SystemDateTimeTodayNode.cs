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
        Id = "db2496eb-e4d1-4c36-b906-5464489141d3",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinStaticValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinStaticValue { get; set; } 

        [DataPinDefinition(
        Id = "c45e8d35-2070-4523-8e47-17efcff0b289",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDate),
        DisplayName = "Date",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDate { get; set; } 

        [DataPinDefinition(
        Id = "d9e31f6b-72ba-44e3-8e88-26dc7ec27d5c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDay),
        DisplayName = "Day",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDay { get; set; } 

        [DataPinDefinition(
        Id = "963eac17-2397-47d4-8ddc-ed2f9dd20841",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DayOfWeek),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDayOfWeek),
        DisplayName = "DayOfWeek",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDayOfWeek { get; set; } 

        [DataPinDefinition(
        Id = "41490c65-2a7d-44e1-afa5-74fd6b9cac35",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDayOfYear),
        DisplayName = "DayOfYear",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDayOfYear { get; set; } 

        [DataPinDefinition(
        Id = "92b82ddb-b508-4c2f-a246-13e4bc84de2e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubHour),
        DisplayName = "Hour",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubHour { get; set; } 

        [DataPinDefinition(
        Id = "98467263-75dc-411e-b015-63c65d5d99cd",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTimeKind),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubKind),
        DisplayName = "Kind",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubKind { get; set; } 

        [DataPinDefinition(
        Id = "bfd92a1c-ee4c-487f-b394-0da6143e6a9b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMillisecond),
        DisplayName = "Millisecond",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMillisecond { get; set; } 

        [DataPinDefinition(
        Id = "4b46fc85-c8c6-48d7-bafd-5ec001ae8282",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMinute),
        DisplayName = "Minute",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMinute { get; set; } 

        [DataPinDefinition(
        Id = "97541616-3d0d-44d8-9bdc-5e35e1a4d211",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMonth),
        DisplayName = "Month",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMonth { get; set; } 

        [DataPinDefinition(
        Id = "17f5b7e9-cf50-4165-9a32-02155ee15daf",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubSecond),
        DisplayName = "Second",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubSecond { get; set; } 

        [DataPinDefinition(
        Id = "cfa34fee-8b9d-4547-8a4e-5b92bd9dada7",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubTicks),
        DisplayName = "Ticks",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubTicks { get; set; } 

        [DataPinDefinition(
        Id = "1f9c79a2-a827-4466-bf44-e70d27d8d9d4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubTimeOfDay),
        DisplayName = "TimeOfDay",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubTimeOfDay { get; set; } 

        [DataPinDefinition(
        Id = "e572ff98-63a5-463d-abdd-bd2763e60fa0",
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