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
        Id = "a7d337ae-c494-4e41-b2be-63702782d9a5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "8792acc1-78fd-4c50-af1f-b42e1c8332ae",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Security.AccessControl.AccessControlSections),
        Direction = PinDirection.In,
        Name = nameof(InPinIncludeSections),
        DisplayName = "IncludeSections",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIncludeSections { get; set; } 

        [DataPinDefinition(
        Id = "7c1e31f2-a1e5-414e-acb0-49111c5b5fed",
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