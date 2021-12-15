// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder__String), DisplayName = "CompileToAssembly(RegexCompilationInfo[],AssemblyName,CustomAttributeBuilder[],String)", Category = "System/Regex")]
    public class System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder__String : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Text.RegularExpressions.Regex.CompileToAssembly(
                scope.GetValue<System.Text.RegularExpressions.RegexCompilationInfo[]>(InPinRegexinfos),
                scope.GetValue<System.Reflection.AssemblyName>(InPinAssemblyname),
                scope.GetValue<System.Reflection.Emit.CustomAttributeBuilder[]>(InPinAttributes),
                scope.GetValue<System.String>(InPinResourceFile));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder__String: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder__String); 
        public override string FriendlyName => nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder__String); 

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
        Id = "d3072647-ee99-4041-bde0-7ddd915321dd",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.RegularExpressions.RegexCompilationInfo[]),
        Direction = PinDirection.In,
        Name = nameof(InPinRegexinfos),
        DisplayName = "Regexinfos",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinRegexinfos { get; set; } 

        [DataPinDefinition(
        Id = "6614deb4-856f-4613-a939-5ce07ebb8709",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Reflection.AssemblyName),
        Direction = PinDirection.In,
        Name = nameof(InPinAssemblyname),
        DisplayName = "Assemblyname",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinAssemblyname { get; set; } 

        [DataPinDefinition(
        Id = "eb5cb47d-ff4a-4757-a9f1-17be45666200",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Reflection.Emit.CustomAttributeBuilder[]),
        Direction = PinDirection.In,
        Name = nameof(InPinAttributes),
        DisplayName = "Attributes",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinAttributes { get; set; } 

        [DataPinDefinition(
        Id = "ebedcd27-82c9-4fbe-beeb-85eae214e323",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinResourceFile),
        DisplayName = "ResourceFile",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinResourceFile { get; set; } 

    }
}