// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemConvertToUInt32_String_Int32), DisplayName = "ToUInt32(String,Int32)", Category = "System/Convert")]
    public class SystemConvertToUInt32_String_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Convert.ToUInt32(
                scope.GetValue<System.String>(InPinValue),
                scope.GetValue<System.Int32>(InPinFromBase));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemConvertToUInt32_String_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemConvertToUInt32_String_Int32); 
        public override string FriendlyName => nameof(SystemConvertToUInt32_String_Int32); 

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
        Id = "269b051e-7b2b-4acc-b13e-03855e0eff06",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinValue),
        DisplayName = "Value",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinValue { get; set; } 

        [DataPinDefinition(
        Id = "112cdc03-bc7f-4c89-92af-7036f933dce5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinFromBase),
        DisplayName = "FromBase",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFromBase { get; set; } 

        [DataPinDefinition(
        Id = "92c1ffd5-576b-49f0-a31a-8007387e9799",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.UInt32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}