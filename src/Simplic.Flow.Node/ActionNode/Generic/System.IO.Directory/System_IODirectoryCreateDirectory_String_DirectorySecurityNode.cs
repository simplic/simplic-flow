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
        Id = "0892939a-4d6c-4028-ab68-43b654b37653",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "95cdc34c-a62d-433a-a41f-50368cee5411",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Security.AccessControl.DirectorySecurity),
        Direction = PinDirection.In,
        Name = nameof(InPinDirectorySecurity),
        DisplayName = "DirectorySecurity",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDirectorySecurity { get; set; } 

        [DataPinDefinition(
        Id = "19afa433-dc9a-46e4-9c0f-f3155e8254a6",
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