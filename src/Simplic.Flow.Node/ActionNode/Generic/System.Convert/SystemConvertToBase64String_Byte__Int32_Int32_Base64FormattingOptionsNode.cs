// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemConvertToBase64String_Byte__Int32_Int32_Base64FormattingOptions), DisplayName = "ToBase64String(Byte[],Int32,Int32,Base64FormattingOptions)", Category = "System/Convert")]
    public class SystemConvertToBase64String_Byte__Int32_Int32_Base64FormattingOptions : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Convert.ToBase64String(
                scope.GetValue<System.Byte[]>(InPinInArray),
                scope.GetValue<System.Int32>(InPinOffset),
                scope.GetValue<System.Int32>(InPinLength),
                scope.GetValue<System.Base64FormattingOptions>(InPinOptions));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemConvertToBase64String_Byte__Int32_Int32_Base64FormattingOptions: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemConvertToBase64String_Byte__Int32_Int32_Base64FormattingOptions); 
        public override string FriendlyName => nameof(SystemConvertToBase64String_Byte__Int32_Int32_Base64FormattingOptions); 

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
        Id = "d7e9dd16-cbf8-428d-9c2a-3a4dcf3513c0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Byte[]),
        Direction = PinDirection.In,
        Name = nameof(InPinInArray),
        DisplayName = "InArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInArray { get; set; } 

        [DataPinDefinition(
        Id = "c0594114-b347-4697-b2ab-982d57c5acea",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinOffset),
        DisplayName = "Offset",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOffset { get; set; } 

        [DataPinDefinition(
        Id = "7e7dc889-f3f8-4778-8b98-8c7a5cf01708",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "59f35a4f-f7db-42c0-a810-639d6f1a53b0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Base64FormattingOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "16067b98-28a9-4651-98e7-5a5ee0abffdd",
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