// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemInt32Parse_String_NumberStyles_IFormatProvider), DisplayName = "Parse(String,NumberStyles,IFormatProvider)", Category = "System/Int32")]
    public class SystemInt32Parse_String_NumberStyles_IFormatProvider : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Int32.Parse(
                scope.GetValue<System.String>(InPinS),
                scope.GetValue<System.Globalization.NumberStyles>(InPinStyle),
                scope.GetValue<System.IFormatProvider>(InPinProvider));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemInt32Parse_String_NumberStyles_IFormatProvider: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemInt32Parse_String_NumberStyles_IFormatProvider); 
        public override string FriendlyName => nameof(SystemInt32Parse_String_NumberStyles_IFormatProvider); 

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
        Id = "4c153fa4-aa95-445d-a854-271338020a41",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinS),
        DisplayName = "S",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinS { get; set; } 

        [DataPinDefinition(
        Id = "846b3423-c230-4c8c-a70a-8a95abfd0f9e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.NumberStyles),
        Direction = PinDirection.In,
        Name = nameof(InPinStyle),
        DisplayName = "Style",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStyle { get; set; } 

        [DataPinDefinition(
        Id = "0b5d3a67-f4ff-4800-ab06-b2da08b1674d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinProvider),
        DisplayName = "Provider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinProvider { get; set; } 

        [DataPinDefinition(
        Id = "eec7dda7-aa57-44db-bee8-5e5e2a86d0f1",
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