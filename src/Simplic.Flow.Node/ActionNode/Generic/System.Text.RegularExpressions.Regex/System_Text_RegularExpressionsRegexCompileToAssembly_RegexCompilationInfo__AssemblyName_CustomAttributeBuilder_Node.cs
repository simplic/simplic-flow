// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder_), DisplayName = "CompileToAssembly(RegexCompilationInfo[],AssemblyName,CustomAttributeBuilder[])", Category = "System/Regex")]
    public class System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Text.RegularExpressions.Regex.CompileToAssembly(
                scope.GetValue<System.Text.RegularExpressions.RegexCompilationInfo[]>(InPinRegexinfos),
                scope.GetValue<System.Reflection.AssemblyName>(InPinAssemblyname),
                scope.GetValue<System.Reflection.Emit.CustomAttributeBuilder[]>(InPinAttributes));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder_); 
        public override string FriendlyName => nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder_); 

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
        Id = "66966e20-0cd9-44ca-b4a7-e250ddf7370b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.RegularExpressions.RegexCompilationInfo[]),
        Direction = PinDirection.In,
        Name = nameof(InPinRegexinfos),
        DisplayName = "Regexinfos",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinRegexinfos { get; set; } 

        [DataPinDefinition(
        Id = "65b66dbf-c1d3-4212-b02a-1605b7d9f61c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Reflection.AssemblyName),
        Direction = PinDirection.In,
        Name = nameof(InPinAssemblyname),
        DisplayName = "Assemblyname",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinAssemblyname { get; set; } 

        [DataPinDefinition(
        Id = "7fbd3c51-e700-43eb-b085-9b0b1bd50c91",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Reflection.Emit.CustomAttributeBuilder[]),
        Direction = PinDirection.In,
        Name = nameof(InPinAttributes),
        DisplayName = "Attributes",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinAttributes { get; set; } 

    }
}