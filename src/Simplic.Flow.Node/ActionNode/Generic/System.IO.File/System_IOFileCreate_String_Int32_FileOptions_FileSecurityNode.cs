// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileCreate_String_Int32_FileOptions_FileSecurity), DisplayName = "Create(String,Int32,FileOptions,FileSecurity)", Category = "System/File")]
    public class System_IOFileCreate_String_Int32_FileOptions_FileSecurity : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.File.Create(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.Int32>(InPinBufferSize),
                scope.GetValue<System.IO.FileOptions>(InPinOptions),
                scope.GetValue<System.Security.AccessControl.FileSecurity>(InPinFileSecurity));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileCreate_String_Int32_FileOptions_FileSecurity: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileCreate_String_Int32_FileOptions_FileSecurity); 
        public override string FriendlyName => nameof(System_IOFileCreate_String_Int32_FileOptions_FileSecurity); 

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
        Id = "93763c8b-821d-4992-8f33-cec3d0687805",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "48ecc657-cdfe-4171-8cab-979053d0a9ed",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinBufferSize),
        DisplayName = "BufferSize",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinBufferSize { get; set; } 

        [DataPinDefinition(
        Id = "36040e61-6a91-4166-b6d6-3eab6074af70",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "959f9c35-46b3-4d05-8e3c-7db6e24b0de1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Security.AccessControl.FileSecurity),
        Direction = PinDirection.In,
        Name = nameof(InPinFileSecurity),
        DisplayName = "FileSecurity",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFileSecurity { get; set; } 

        [DataPinDefinition(
        Id = "5539f237-a272-4a6d-ab0e-e7f723fc71e5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileStream),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}