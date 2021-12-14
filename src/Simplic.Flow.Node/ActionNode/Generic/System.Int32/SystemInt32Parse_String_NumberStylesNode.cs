// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemInt32Parse_String_NumberStyles), DisplayName = "Parse(String,NumberStyles)", Category = "System/Int32")]
    public class SystemInt32Parse_String_NumberStyles : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Int32.Parse(
                scope.GetValue<System.String>(InPinS),
                scope.GetValue<System.Globalization.NumberStyles>(InPinStyle));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemInt32Parse_String_NumberStyles: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemInt32Parse_String_NumberStyles); 
        public override string FriendlyName => nameof(SystemInt32Parse_String_NumberStyles); 

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
        Id = "015aa871-2517-495d-948d-a49703e6cd0f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinS),
        DisplayName = "S",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinS { get; set; } 

        [DataPinDefinition(
        Id = "bd81afe5-f67e-447b-aee2-a1265fce8991",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.NumberStyles),
        Direction = PinDirection.In,
        Name = nameof(InPinStyle),
        DisplayName = "Style",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStyle { get; set; } 

        [DataPinDefinition(
        Id = "ff632b36-7137-4bf9-89a5-a1602c4749c7",
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