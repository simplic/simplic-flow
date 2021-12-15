// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName), DisplayName = "CompileToAssembly(RegexCompilationInfo[],AssemblyName)", Category = "System/Regex")]
    public class System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Text.RegularExpressions.Regex.CompileToAssembly(
                scope.GetValue<System.Text.RegularExpressions.RegexCompilationInfo[]>(InPinRegexinfos),
                scope.GetValue<System.Reflection.AssemblyName>(InPinAssemblyname));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName); 
        public override string FriendlyName => nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName); 

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
        Id = "3b826349-8935-408f-91b0-8ddc652898d7",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.RegularExpressions.RegexCompilationInfo[]),
        Direction = PinDirection.In,
        Name = nameof(InPinRegexinfos),
        DisplayName = "Regexinfos",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinRegexinfos { get; set; } 

        [DataPinDefinition(
        Id = "000996ac-7327-4d66-a42a-a690156a344b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Reflection.AssemblyName),
        Direction = PinDirection.In,
        Name = nameof(InPinAssemblyname),
        DisplayName = "Assemblyname",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinAssemblyname { get; set; } 

    }
}