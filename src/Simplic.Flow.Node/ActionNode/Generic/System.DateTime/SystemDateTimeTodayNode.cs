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
        Id = "b6564b8b-3e69-4058-bf6e-cc5839290868",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinStaticValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinStaticValue { get; set; } 

        [DataPinDefinition(
        Id = "f0be1186-cbe6-49cd-9148-8bac115d4b9c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDate),
        DisplayName = "Date",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDate { get; set; } 

        [DataPinDefinition(
        Id = "b3115436-4fd0-40a4-bad4-70bf382aa480",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDay),
        DisplayName = "Day",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDay { get; set; } 

        [DataPinDefinition(
        Id = "2e45c4ca-826d-4856-9c91-3ea8106a79c9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DayOfWeek),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDayOfWeek),
        DisplayName = "DayOfWeek",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDayOfWeek { get; set; } 

        [DataPinDefinition(
        Id = "e7bf0075-65d1-486a-9261-8813372cc12c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDayOfYear),
        DisplayName = "DayOfYear",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDayOfYear { get; set; } 

        [DataPinDefinition(
        Id = "8a7a782b-990c-4e03-a90d-2f5374763131",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubHour),
        DisplayName = "Hour",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubHour { get; set; } 

        [DataPinDefinition(
        Id = "d4c85380-44ca-4dda-acae-41cf2ebf9f44",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTimeKind),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubKind),
        DisplayName = "Kind",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubKind { get; set; } 

        [DataPinDefinition(
        Id = "18986238-b7ca-4768-88ed-87d40f6df47b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMillisecond),
        DisplayName = "Millisecond",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMillisecond { get; set; } 

        [DataPinDefinition(
        Id = "3a0dccaf-1b9f-4b44-acc8-56e4fbcd55c9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMinute),
        DisplayName = "Minute",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMinute { get; set; } 

        [DataPinDefinition(
        Id = "f5111de5-4b8c-4b82-a78b-e30378f01ef5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMonth),
        DisplayName = "Month",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMonth { get; set; } 

        [DataPinDefinition(
        Id = "a0a4666e-61d6-4917-881c-06163c9e9841",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubSecond),
        DisplayName = "Second",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubSecond { get; set; } 

        [DataPinDefinition(
        Id = "df9bf02b-eaee-474e-9659-e52651108760",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubTicks),
        DisplayName = "Ticks",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubTicks { get; set; } 

        [DataPinDefinition(
        Id = "2068e321-e4b7-4733-8dab-6376b1c8e51e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubTimeOfDay),
        DisplayName = "TimeOfDay",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubTimeOfDay { get; set; } 

        [DataPinDefinition(
        Id = "14ae01ae-a893-417d-ac25-44a05c96abf9",
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