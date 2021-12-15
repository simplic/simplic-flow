// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeNow), DisplayName = "DateTime.Now", Category = "System/DateTime")]
    public class SystemDateTimeNow : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.Now;
                scope.SetValue(OutPinStaticValue, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeNow: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeNow); 
        public override string FriendlyName => nameof(SystemDateTimeNow); 

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
        Id = "a21c017c-7487-40e5-a003-77a2b154db2f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinStaticValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinStaticValue { get; set; } 

        [DataPinDefinition(
        Id = "d26beb8d-b46c-4293-af26-e17c63ae5a6b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDate),
        DisplayName = "Date",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDate { get; set; } 

        [DataPinDefinition(
        Id = "6d5840c2-89ef-480f-a855-19fe8e5f2152",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDay),
        DisplayName = "Day",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDay { get; set; } 

        [DataPinDefinition(
        Id = "be5bd302-29fc-41f4-9992-b53e9de61744",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DayOfWeek),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDayOfWeek),
        DisplayName = "DayOfWeek",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDayOfWeek { get; set; } 

        [DataPinDefinition(
        Id = "1915acd2-ab57-4491-8e0e-78a9cb71c26c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubDayOfYear),
        DisplayName = "DayOfYear",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubDayOfYear { get; set; } 

        [DataPinDefinition(
        Id = "f2589a19-8177-4869-97b6-a82c0b06853b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubHour),
        DisplayName = "Hour",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubHour { get; set; } 

        [DataPinDefinition(
        Id = "18a42677-a32d-44e3-a789-14c1d03ab1a3",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTimeKind),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubKind),
        DisplayName = "Kind",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubKind { get; set; } 

        [DataPinDefinition(
        Id = "43b0107d-f5e9-4e6e-8b60-2eb76b17de1e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMillisecond),
        DisplayName = "Millisecond",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMillisecond { get; set; } 

        [DataPinDefinition(
        Id = "93a08298-78aa-448b-a2f8-cbafb2fcd2b4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMinute),
        DisplayName = "Minute",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMinute { get; set; } 

        [DataPinDefinition(
        Id = "8563aec7-9938-42bb-bb99-689477ce3605",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubMonth),
        DisplayName = "Month",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubMonth { get; set; } 

        [DataPinDefinition(
        Id = "e0559b80-9617-4136-969a-8aab486d6089",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubSecond),
        DisplayName = "Second",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubSecond { get; set; } 

        [DataPinDefinition(
        Id = "8f4ac622-59a8-4b7e-923f-ace7377b4168",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubTicks),
        DisplayName = "Ticks",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubTicks { get; set; } 

        [DataPinDefinition(
        Id = "43007cac-5633-4f30-ab27-c4fd881cf513",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.Out,
        Name = nameof(OutPinSubTimeOfDay),
        DisplayName = "TimeOfDay",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinSubTimeOfDay { get; set; } 

        [DataPinDefinition(
        Id = "b2312f7d-2747-4583-a452-89ae02685f56",
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