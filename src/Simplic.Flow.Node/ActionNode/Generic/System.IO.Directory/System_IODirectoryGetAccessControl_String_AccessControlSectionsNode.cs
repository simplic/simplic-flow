// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IODirectoryGetAccessControl_String_AccessControlSections), DisplayName = "GetAccessControl(String,AccessControlSections)", Category = "System/Directory")]
    public class System_IODirectoryGetAccessControl_String_AccessControlSections : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.Directory.GetAccessControl(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.Security.AccessControl.AccessControlSections>(InPinIncludeSections));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IODirectoryGetAccessControl_String_AccessControlSections: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IODirectoryGetAccessControl_String_AccessControlSections); 
        public override string FriendlyName => nameof(System_IODirectoryGetAccessControl_String_AccessControlSections); 

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
        Id = "5fbed190-e8a1-4113-8649-cf67a668c6b7",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "1e57da55-13e7-4653-9cf6-c25f3122f6a0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Security.AccessControl.AccessControlSections),
        Direction = PinDirection.In,
        Name = nameof(InPinIncludeSections),
        DisplayName = "IncludeSections",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIncludeSections { get; set; } 

        [DataPinDefinition(
        Id = "1faee01d-826a-42ec-ae0a-6b79429ea1d0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Security.AccessControl.DirectorySecurity),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}