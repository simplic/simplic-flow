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
        Id = "2a0f6c12-bf56-4acb-83e9-bb0287a4c3a3",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Byte[]),
        Direction = PinDirection.In,
        Name = nameof(InPinInArray),
        DisplayName = "InArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInArray { get; set; } 

        [DataPinDefinition(
        Id = "6634829a-a6bf-44f8-b851-b724dc78d1e6",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffsetIn),
        DisplayName = "OffsetIn",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffsetIn { get; set; } 

        [DataPinDefinition(
        Id = "acb37ab2-4ba5-45d4-a694-68439b93931c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "2144e174-790b-4162-a700-6985ce157f14",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Char[]),
        Direction = PinDirection.In,
        Name = nameof(InPinOutArray),
        DisplayName = "OutArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOutArray { get; set; } 

        [DataPinDefinition(
        Id = "88c81de5-0997-4b15-9931-4650024f4a12",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffsetOut),
        DisplayName = "OffsetOut",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffsetOut { get; set; } 

        [DataPinDefinition(
        Id = "1ad3a057-6eda-4019-ab27-e84879ad65de",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Base64FormattingOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "bc393af2-9de1-4513-ad92-0fbfb12a8ca2",
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