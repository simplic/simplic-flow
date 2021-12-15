// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeFromFileTimeUtc_Int64), DisplayName = "FromFileTimeUtc(Int64)", Category = "System/DateTime")]
    public class SystemDateTimeFromFileTimeUtc_Int64 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.FromFileTimeUtc(
                scope.GetValue<System.Int64>(InPinFileTime));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeFromFileTimeUtc_Int64: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeFromFileTimeUtc_Int64); 
        public override string FriendlyName => nameof(SystemDateTimeFromFileTimeUtc_Int64); 

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
        Id = "d63997ad-45d3-44c1-aedc-5cc6eab6857d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.In,
        Name = nameof(InPinFileTime),
        DisplayName = "FileTime",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFileTime { get; set; } 

        [DataPinDefinition(
        Id = "4582c305-d46c-44c5-b84a-3771a8ae13bd",
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