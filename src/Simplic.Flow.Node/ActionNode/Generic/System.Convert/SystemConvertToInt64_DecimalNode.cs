// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemConvertToInt64_Decimal), DisplayName = "ToInt64(Decimal)", Category = "System/Convert")]
    public class SystemConvertToInt64_Decimal : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Convert.ToInt64(
                scope.GetValue<System.Decimal>(InPinValue));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemConvertToInt64_Decimal: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemConvertToInt64_Decimal); 
        public override string FriendlyName => nameof(SystemConvertToInt64_Decimal); 

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
        Id = "562e1cf3-c007-4c87-ae5a-e1fc4b722cae",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Decimal),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "5f9cbc57-238b-4efa-8638-10caf5c13ab5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}