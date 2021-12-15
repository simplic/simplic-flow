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
        Id = "4e6aa842-cc81-46d2-9150-9ac0960322b3",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Byte[]),
        Direction = PinDirection.In,
        Name = nameof(InPinInArray),
        DisplayName = "InArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInArray { get; set; } 

        [DataPinDefinition(
        Id = "f0cccf55-184c-40b0-bb8b-1e8f6eef1591",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffsetIn),
        DisplayName = "OffsetIn",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffsetIn { get; set; } 

        [DataPinDefinition(
        Id = "b2ee9e94-09b2-4d50-a71d-884e953d255b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "7634a658-2d56-4953-807f-db16a34e6823",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Char[]),
        Direction = PinDirection.In,
        Name = nameof(InPinOutArray),
        DisplayName = "OutArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOutArray { get; set; } 

        [DataPinDefinition(
        Id = "080e5d5c-528a-4255-bd41-ded0f8de89a4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffsetOut),
        DisplayName = "OffsetOut",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffsetOut { get; set; } 

        [DataPinDefinition(
        Id = "15e3d3bb-cc4b-4cca-834c-706ca6bd84e9",
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