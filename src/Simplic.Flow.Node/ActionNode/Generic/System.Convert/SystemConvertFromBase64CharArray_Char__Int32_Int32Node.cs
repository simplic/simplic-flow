// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemConvertFromBase64CharArray_Char__Int32_Int32), DisplayName = "FromBase64CharArray(Char[],Int32,Int32)", Category = "System/Convert")]
    public class SystemConvertFromBase64CharArray_Char__Int32_Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Convert.FromBase64CharArray(
                scope.GetValue<System.Char[]>(InPinInArray),
                scope.GetValue<System.Int32>(InPinOffset),
                scope.GetValue<System.Int32>(InPinLength));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemConvertFromBase64CharArray_Char__Int32_Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemConvertFromBase64CharArray_Char__Int32_Int32); 
        public override string FriendlyName => nameof(SystemConvertFromBase64CharArray_Char__Int32_Int32); 

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
        Id = "c40989c9-d587-4992-9d91-5ddb0b5c4a35",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Char[]),
        Direction = PinDirection.In,
        Name = nameof(InPinInArray),
        DisplayName = "InArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInArray { get; set; } 

        [DataPinDefinition(
        Id = "ea6e5d4e-4d8f-44b2-a073-6764d8de6e24",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffset),
        DisplayName = "Offset",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffset { get; set; } 

        [DataPinDefinition(
        Id = "475faacd-bb04-423c-9a97-4947bf73f653",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "8c709e33-a5a6-4c75-8bc9-52e4100ef6bf",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Byte[]),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}