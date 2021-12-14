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
        Id = "66400f4e-2de5-4754-8b01-3d82c2908f0a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Byte[]),
        Direction = PinDirection.In,
        Name = nameof(InPinInArray),
        DisplayName = "InArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInArray { get; set; } 

        [DataPinDefinition(
        Id = "3139b7c5-c814-41bd-903a-9231e19b00bf",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffsetIn),
        DisplayName = "OffsetIn",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffsetIn { get; set; } 

        [DataPinDefinition(
        Id = "cd79c8d1-7cbc-4167-bee5-e688b233d958",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "90c56db0-3c29-4dee-9f13-692667f3af0f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Char[]),
        Direction = PinDirection.In,
        Name = nameof(InPinOutArray),
        DisplayName = "OutArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOutArray { get; set; } 

        [DataPinDefinition(
        Id = "d390e934-849b-4c84-a50b-c35df6823496",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffsetOut),
        DisplayName = "OffsetOut",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffsetOut { get; set; } 

        [DataPinDefinition(
        Id = "5a738b22-3317-4999-bd49-25c60d2ad30c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Base64FormattingOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "1dd22f92-cd80-4f44-a659-c515a0f457f5",
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