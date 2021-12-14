// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileGetAccessControl_String_AccessControlSections), DisplayName = "GetAccessControl(String,AccessControlSections)", Category = "System/File")]
    public class System_IOFileGetAccessControl_String_AccessControlSections : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.File.GetAccessControl(
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileGetAccessControl_String_AccessControlSections: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileGetAccessControl_String_AccessControlSections); 
        public override string FriendlyName => nameof(System_IOFileGetAccessControl_String_AccessControlSections); 

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
        Id = "7a0e02bb-c085-4ff6-a80b-054f63e851fd",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "cc0c01c3-0ba6-416e-b3bc-b94c90f580f0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Security.AccessControl.AccessControlSections),
        Direction = PinDirection.In,
        Name = nameof(InPinIncludeSections),
        DisplayName = "IncludeSections",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIncludeSections { get; set; } 

        [DataPinDefinition(
        Id = "ac4fa550-66de-465b-9316-6ef87672b28c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Security.AccessControl.FileSecurity),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}