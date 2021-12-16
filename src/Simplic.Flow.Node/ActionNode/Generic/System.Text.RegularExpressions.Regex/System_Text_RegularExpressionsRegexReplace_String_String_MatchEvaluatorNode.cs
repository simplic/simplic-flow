// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator), DisplayName = "Replace(String,String,MatchEvaluator)", Category = "System/Regex")]
    public class System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Text.RegularExpressions.Regex.Replace(
                scope.GetValue<System.String>(InPinInput),
                scope.GetValue<System.String>(InPinPattern),
                scope.GetValue<System.Text.RegularExpressions.MatchEvaluator>(InPinEvaluator));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator); 
        public override string FriendlyName => nameof(System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator); 

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
        Id = "6962dd6c-2afc-4f8a-9a45-73ad743cbd84",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinInput),
        DisplayName = "Input",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInput { get; set; } 

        [DataPinDefinition(
        Id = "22c3a8b6-8b2d-4d33-b6d0-192d62f18c05",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPattern),
        DisplayName = "Pattern",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPattern { get; set; } 

        [DataPinDefinition(
        Id = "ca7fcc21-7276-4a58-b054-6decfe92cda5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.RegularExpressions.MatchEvaluator),
        Direction = PinDirection.In,
        Name = nameof(InPinEvaluator),
        DisplayName = "Evaluator",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinEvaluator { get; set; } 

        [DataPinDefinition(
        Id = "158f9273-e068-4a9c-8ef0-c98665f21630",
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