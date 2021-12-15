// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_Text_RegularExpressionsRegexReplace_String_String_String_RegexOptions), DisplayName = "Replace(String,String,String,RegexOptions)", Category = "System/Regex")]
    public class System_Text_RegularExpressionsRegexReplace_String_String_String_RegexOptions : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Text.RegularExpressions.Regex.Replace(
                scope.GetValue<System.String>(InPinInput),
                scope.GetValue<System.String>(InPinPattern),
                scope.GetValue<System.String>(InPinReplacement),
                scope.GetValue<System.Text.RegularExpressions.RegexOptions>(InPinOptions));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_Text_RegularExpressionsRegexReplace_String_String_String_RegexOptions: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_Text_RegularExpressionsRegexReplace_String_String_String_RegexOptions); 
        public override string FriendlyName => nameof(System_Text_RegularExpressionsRegexReplace_String_String_String_RegexOptions); 

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
        Id = "c3aa5692-ad62-487c-af72-0d4da4f3468e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinInput),
        DisplayName = "Input",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinInput { get; set; } 

        [DataPinDefinition(
        Id = "d778a646-3dc9-4d14-a627-25d64f0460e8",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPattern),
        DisplayName = "Pattern",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPattern { get; set; } 

        [DataPinDefinition(
        Id = "78c6ea4c-eb83-4daa-9c9b-474cc9e2aad2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinReplacement),
        DisplayName = "Replacement",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinReplacement { get; set; } 

        [DataPinDefinition(
        Id = "57f2d7f6-6051-4454-9af6-b2bc9e67c877",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.RegularExpressions.RegexOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "71caaa3d-9e8a-4da9-a508-f12a8630e754",
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