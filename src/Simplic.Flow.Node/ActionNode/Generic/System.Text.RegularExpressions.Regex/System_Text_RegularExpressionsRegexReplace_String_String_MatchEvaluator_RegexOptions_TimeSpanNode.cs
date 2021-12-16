// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator_RegexOptions_TimeSpan), DisplayName = "Replace(String,String,MatchEvaluator,RegexOptions,TimeSpan)", Category = "System/Regex")]
    public class System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator_RegexOptions_TimeSpan : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Text.RegularExpressions.Regex.Replace(
                scope.GetValue<System.String>(InPinInput),
                scope.GetValue<System.String>(InPinPattern),
                scope.GetValue<System.Text.RegularExpressions.MatchEvaluator>(InPinEvaluator),
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator_RegexOptions_TimeSpan: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator_RegexOptions_TimeSpan); 
        public override string FriendlyName => nameof(System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator_RegexOptions_TimeSpan); 

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
        Id = "4cf6ee70-db4a-4c54-ac7c-9fddd1884535",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinInput),
        DisplayName = "Input",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInput { get; set; } 

        [DataPinDefinition(
        Id = "79a236ec-6f69-4b5c-ba00-a8ef7c36ed04",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPattern),
        DisplayName = "Pattern",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPattern { get; set; } 

        [DataPinDefinition(
        Id = "7129185b-2128-4301-b46c-dc733910e681",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.RegularExpressions.MatchEvaluator),
        Direction = PinDirection.In,
        Name = nameof(InPinEvaluator),
        DisplayName = "Evaluator",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinEvaluator { get; set; } 

        [DataPinDefinition(
        Id = "84b5b07e-3a66-4610-bf06-9e9af7058491",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.RegularExpressions.RegexOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "1d9dddfe-26f2-4dc0-b74c-d8ffaff15dd6",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.TimeSpan),
        Direction = PinDirection.In,
        Name = nameof(InPinMatchTimeout),
        DisplayName = "MatchTimeout",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinMatchTimeout { get; set; } 

        [DataPinDefinition(
        Id = "93f285da-2504-4dc6-8e9b-8389dac32ba8",
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