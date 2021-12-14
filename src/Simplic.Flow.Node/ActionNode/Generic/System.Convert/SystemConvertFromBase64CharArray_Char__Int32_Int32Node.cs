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
        Id = "3aa9b8f3-69fb-421a-8926-ad0d0fbbd212",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Char[]),
        Direction = PinDirection.In,
        Name = nameof(InPinInArray),
        DisplayName = "InArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInArray { get; set; } 

        [DataPinDefinition(
        Id = "e93f1e8b-e77f-4493-be44-7c6d6c2f1832",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffset),
        DisplayName = "Offset",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffset { get; set; } 

        [DataPinDefinition(
        Id = "ab5f46d8-36f2-4799-a431-3f19a08d92f0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "e07c9dd5-f6b4-4a00-a95e-5e1ce45576f0",
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