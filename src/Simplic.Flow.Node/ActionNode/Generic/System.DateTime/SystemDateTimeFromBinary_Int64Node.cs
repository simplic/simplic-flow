// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeFromBinary_Int64), DisplayName = "FromBinary(Int64)", Category = "System/DateTime")]
    public class SystemDateTimeFromBinary_Int64 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.FromBinary(
                scope.GetValue<System.Int64>(InPinDateData));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeFromBinary_Int64: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeFromBinary_Int64); 
        public override string FriendlyName => nameof(SystemDateTimeFromBinary_Int64); 

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
        Id = "842b4164-cd68-499d-aabd-df063ba30dad",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.In,
        Name = nameof(InPinDateData),
        DisplayName = "DateData",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDateData { get; set; } 

        [DataPinDefinition(
        Id = "90f93402-5d5e-469f-909c-097fb7d0b233",
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