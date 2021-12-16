// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32_Base64FormattingOptions), DisplayName = "ToBase64CharArray(Byte[],Int32,Int32,Char[],Int32,Base64FormattingOptions)", Category = "System/Convert")]
    public class SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32_Base64FormattingOptions : ActionNode 
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
                scope.GetValue<System.Int32>(InPinOffsetOut),
                scope.GetValue<System.Base64FormattingOptions>(InPinOptions));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32_Base64FormattingOptions: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32_Base64FormattingOptions); 
        public override string FriendlyName => nameof(SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32_Base64FormattingOptions); 

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
        Id = "ae3dde7b-460f-448d-9b86-446326ceb879",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Byte[]),
        Direction = PinDirection.In,
        Name = nameof(InPinInArray),
        DisplayName = "InArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInArray { get; set; } 

        [DataPinDefinition(
        Id = "b6f1810a-0214-4363-b469-43c0ca1dfbbd",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffsetIn),
        DisplayName = "OffsetIn",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffsetIn { get; set; } 

        [DataPinDefinition(
        Id = "69d38954-748c-488e-99d5-78f59e7e8503",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "28a2a8b0-51c0-4316-be8d-42d30b542006",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Char[]),
        Direction = PinDirection.In,
        Name = nameof(InPinOutArray),
        DisplayName = "OutArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOutArray { get; set; } 

        [DataPinDefinition(
        Id = "675729ac-ab9e-4e86-a234-1b70f25a793c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffsetOut),
        DisplayName = "OffsetOut",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffsetOut { get; set; } 

        [DataPinDefinition(
        Id = "5adfe363-fe29-44c5-be43-0a13f21921be",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Base64FormattingOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "43c6de0d-8ab1-43ed-b7ef-451ce41c0962",
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