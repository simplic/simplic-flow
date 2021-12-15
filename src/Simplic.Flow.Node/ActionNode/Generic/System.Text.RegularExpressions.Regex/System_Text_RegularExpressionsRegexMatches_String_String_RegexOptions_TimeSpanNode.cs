// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_Text_RegularExpressionsRegexMatches_String_String_RegexOptions_TimeSpan), DisplayName = "Matches(String,String,RegexOptions,TimeSpan)", Category = "System/Regex")]
    public class System_Text_RegularExpressionsRegexMatches_String_String_RegexOptions_TimeSpan : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Text.RegularExpressions.Regex.Matches(
                scope.GetValue<System.String>(InPinInput),
                scope.GetValue<System.String>(InPinPattern),
                scope.GetValue<System.Text.RegularExpressions.RegexOptions>(InPinOptions),
                scope.GetValue<System.TimeSpan>(InPinMatchTimeout));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_Text_RegularExpressionsRegexMatches_String_String_RegexOptions_TimeSpan: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_Text_RegularExpressionsRegexMatches_String_String_RegexOptions_TimeSpan); 
        public override string FriendlyName => nameof(System_Text_RegularExpressionsRegexMatches_String_String_RegexOptions_TimeSpan); 

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
        DisplayName = "Each item", 
        Name = nameof(OutNodeEachItem), 
        AllowMultiple = false)] 
        public ActionNode OutNodeEachItem { get; set; } 

        [DataPinDefinition(
        Id = "4a3cd559-54d2-43d8-a66d-4c61e057292e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinInput),
        DisplayName = "Input",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInput { get; set; } 

        [DataPinDefinition(
        Id = "fd5998c6-4595-4540-8d36-91ac95990b28",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPattern),
        DisplayName = "Pattern",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPattern { get; set; } 

        [DataPinDefinition(
        Id = "cafe3680-ee0b-4289-a379-1295dbdcac4e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.RegularExpressions.RegexOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "cda6cea4-f978-47a8-9d5b-e20bfe1ad917",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.In,
        Name = nameof(InPinMatchTimeout),
        DisplayName = "MatchTimeout",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinMatchTimeout { get; set; } 

        [DataPinDefinition(
        Id = "a6a8f962-c56f-4014-84a5-b8afb9832f26",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.RegularExpressions.MatchCollection),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}