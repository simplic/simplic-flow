// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemConvertToBase64String_Byte_), DisplayName = "ToBase64String(Byte[])", Category = "System/Convert")]
    public class SystemConvertToBase64String_Byte_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Convert.ToBase64String(
                scope.GetValue<System.Byte[]>(InPinInArray));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemConvertToBase64String_Byte_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemConvertToBase64String_Byte_); 
        public override string FriendlyName => nameof(SystemConvertToBase64String_Byte_); 

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
        Id = "1c0dab6b-ded0-466e-9b34-6eb6d8a6111e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Byte[]),
        Direction = PinDirection.In,
        Name = nameof(InPinInArray),
        DisplayName = "InArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInArray { get; set; } 

        [DataPinDefinition(
        Id = "be590723-e953-4469-ac7e-3cb3388a5605",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}