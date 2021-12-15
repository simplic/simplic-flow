// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemInt32TryParse_String_NumberStyles_IFormatProvider_Int32_), DisplayName = "TryParse(String,NumberStyles,IFormatProvider,Int32&)", Category = "System/Int32")]
    public class SystemInt32TryParse_String_NumberStyles_IFormatProvider_Int32_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Int32.TryParse(
                scope.GetValue<System.String>(InPinS),
                scope.GetValue<System.Globalization.NumberStyles>(InPinStyle),
                scope.GetValue<System.IFormatProvider>(InPinProvider)
                , out System.Int32 Resultvar);
                scope.SetValue(OutPinReturn, returnValue);

                scope.SetValue(OutParameterPinResult, Resultvar);
                if (OutNodeTrue != null && returnValue)
                {
                    runtime.EnqueueNode(OutNodeTrue, scope);
                } 
                else if (OutNodeFalse != null && !returnValue)
                {
                    runtime.EnqueueNode(OutNodeFalse, scope);
                }
                    
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemInt32TryParse_String_NumberStyles_IFormatProvider_Int32_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemInt32TryParse_String_NumberStyles_IFormatProvider_Int32_); 
        public override string FriendlyName => nameof(SystemInt32TryParse_String_NumberStyles_IFormatProvider_Int32_); 

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

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "True", 
        Name = nameof(OutNodeTrue), 
        AllowMultiple = false)] 
        public ActionNode OutNodeTrue { get; set; } 

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "False", 
        Name = nameof(OutNodeFalse), 
        AllowMultiple = false)] 
        public ActionNode OutNodeFalse { get; set; } 

        [DataPinDefinition(
        Id = "406c18a6-39d1-4ab7-820b-9f0e7e649c2c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinS),
        DisplayName = "S",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinS { get; set; } 

        [DataPinDefinition(
        Id = "66fe66c1-a45c-4131-9761-b0ef90571a8f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.NumberStyles),
        Direction = PinDirection.In,
        Name = nameof(InPinStyle),
        DisplayName = "Style",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStyle { get; set; } 

        [DataPinDefinition(
        Id = "e9158512-0105-4a94-81f3-3414188ad5dc",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinProvider),
        DisplayName = "Provider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinProvider { get; set; } 

        [DataPinDefinition(
        Id = "30e077c8-54ce-4b4f-91a9-66b6b84aa6e8",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "7d7fe8e3-5b8f-4c4e-ad18-b49a2cf2c4bf",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutParameterPinResult),
        DisplayName = "Result",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutParameterPinResult { get; set; } 

    }
}