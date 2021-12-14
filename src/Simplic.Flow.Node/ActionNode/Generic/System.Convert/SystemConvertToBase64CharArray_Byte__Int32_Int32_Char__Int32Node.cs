// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32), DisplayName = "ToBase64CharArray(Byte[],Int32,Int32,Char[],Int32)", Category = "System/Convert")]
    public class SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Convert.ToBase64CharArray(
                scope.GetValue<System.Byte[]>(InPinInArray),
                scope.GetValue<System.Int32>(InPinOffsetIn),
                scope.GetValue<System.Int32>(InPinLength),
                scope.GetValue<System.Char[]>(InPinOutArray),
                scope.GetValue<System.Int32>(InPinOffsetOut));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32); 
        public override string FriendlyName => nameof(SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32); 

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
        Id = "dd294b85-598b-4840-bd48-68806e5fc643",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Byte[]),
        Direction = PinDirection.In,
        Name = nameof(InPinInArray),
        DisplayName = "InArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInArray { get; set; } 

        [DataPinDefinition(
        Id = "4198a5cd-5e17-4e19-9b42-211c368417c7",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffsetIn),
        DisplayName = "OffsetIn",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffsetIn { get; set; } 

        [DataPinDefinition(
        Id = "23f6c95e-7fd3-44f3-82a1-1e7ae83e060c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "6aebe064-0ea9-4284-b094-b1a9ff0de33b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Char[]),
        Direction = PinDirection.In,
        Name = nameof(InPinOutArray),
        DisplayName = "OutArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOutArray { get; set; } 

        [DataPinDefinition(
        Id = "e6ee3d94-0bd5-417e-9af7-b9c9363aa72f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffsetOut),
        DisplayName = "OffsetOut",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffsetOut { get; set; } 

        [DataPinDefinition(
        Id = "e281b864-4ea6-4a25-9be1-b18c8c19c8ff",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}