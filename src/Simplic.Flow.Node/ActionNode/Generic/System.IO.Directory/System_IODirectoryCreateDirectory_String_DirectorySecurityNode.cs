// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IODirectoryCreateDirectory_String_DirectorySecurity), DisplayName = "CreateDirectory(String,DirectorySecurity)", Category = "System/Directory")]
    public class System_IODirectoryCreateDirectory_String_DirectorySecurity : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.Directory.CreateDirectory(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.Security.AccessControl.DirectorySecurity>(InPinDirectorySecurity));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IODirectoryCreateDirectory_String_DirectorySecurity: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IODirectoryCreateDirectory_String_DirectorySecurity); 
        public override string FriendlyName => nameof(System_IODirectoryCreateDirectory_String_DirectorySecurity); 

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
        Id = "50db2aef-5287-4104-8315-6e47b3c6f905",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "2bf3640d-9756-4d27-b8a7-21a664267854",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Security.AccessControl.DirectorySecurity),
        Direction = PinDirection.In,
        Name = nameof(InPinDirectorySecurity),
        DisplayName = "DirectorySecurity",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDirectorySecurity { get; set; } 

        [DataPinDefinition(
        Id = "88935abb-7941-444d-89e5-36ff489321f1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.DirectoryInfo),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}