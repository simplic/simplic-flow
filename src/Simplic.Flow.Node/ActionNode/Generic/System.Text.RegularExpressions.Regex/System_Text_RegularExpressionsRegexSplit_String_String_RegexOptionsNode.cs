// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_Text_RegularExpressionsRegexSplit_String_String_RegexOptions), DisplayName = "Split(String,String,RegexOptions)", Category = "System/Regex")]
    public class System_Text_RegularExpressionsRegexSplit_String_String_RegexOptions : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Text.RegularExpressions.Regex.Split(
                scope.GetValue<System.String>(InPinInput),
                scope.GetValue<System.String>(InPinPattern),
                scope.GetValue<System.Text.RegularExpressions.RegexOptions>(InPinOptions));
                scope.SetValue(OutPinReturn, returnValue);

                foreach (var item in returnValue)
                {
                    var childScope = scope.CreateChild();
                    childScope.SetValue(OutPinCurrent, item);

                    if (OutNodeEachItem != null)
                        runtime.EnqueueNode(OutNodeEachItem, childScope);
                }
                    
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_Text_RegularExpressionsRegexSplit_String_String_RegexOptions: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_Text_RegularExpressionsRegexSplit_String_String_RegexOptions); 
        public override string FriendlyName => nameof(System_Text_RegularExpressionsRegexSplit_String_String_RegexOptions); 

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
        Id = "cfa35abd-ec46-4d5d-a5f4-52191dc19ec4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinInput),
        DisplayName = "Input",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInput { get; set; } 

        [DataPinDefinition(
        Id = "35a536dc-4e6f-4497-b6c4-c66acc75d1f1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPattern),
        DisplayName = "Pattern",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPattern { get; set; } 

        [DataPinDefinition(
        Id = "c7d2952f-5544-4f01-b7e6-d747b025efab",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.RegularExpressions.RegexOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "673d825c-5950-40ee-83be-307da4afccb2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "08d15f38-f390-467c-91f8-7baed5939d56",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.Out,
        Name = nameof(OutPinCurrent),
        DisplayName = "Current",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinCurrent { get; set; } 

    }
}