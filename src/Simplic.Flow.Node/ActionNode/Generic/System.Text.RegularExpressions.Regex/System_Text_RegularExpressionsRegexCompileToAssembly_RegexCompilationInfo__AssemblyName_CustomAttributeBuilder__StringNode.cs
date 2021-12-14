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
        Id = "2c2c75f5-4518-48c1-afc9-6c54bf179089",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.RegularExpressions.RegexCompilationInfo[]),
        Direction = PinDirection.In,
        Name = nameof(InPinRegexinfos),
        DisplayName = "Regexinfos",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinRegexinfos { get; set; } 

        [DataPinDefinition(
        Id = "68f4416a-182d-4b3f-bde5-82acd9ac0723",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Reflection.AssemblyName),
        Direction = PinDirection.In,
        Name = nameof(InPinAssemblyname),
        DisplayName = "Assemblyname",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinAssemblyname { get; set; } 

        [DataPinDefinition(
        Id = "0e1a024d-4a2d-41d2-b050-343a26256eaf",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Reflection.Emit.CustomAttributeBuilder[]),
        Direction = PinDirection.In,
        Name = nameof(InPinAttributes),
        DisplayName = "Attributes",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinAttributes { get; set; } 

        [DataPinDefinition(
        Id = "881ceda4-15b0-4940-8685-f3db5eff9966",
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