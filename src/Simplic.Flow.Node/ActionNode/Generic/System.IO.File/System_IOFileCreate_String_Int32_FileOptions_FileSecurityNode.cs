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
        Id = "ed0ea2ad-bf1e-4fc9-99e1-3ab9b9cb8e8d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "b08092ba-5c4c-4671-a3dd-98e5c43d0e6f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinBufferSize),
        DisplayName = "BufferSize",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinBufferSize { get; set; } 

        [DataPinDefinition(
        Id = "d13572df-a497-42df-bdab-d78575ef3762",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "7dc3eebf-a616-4ac0-8928-bdad48e42e4a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Security.AccessControl.FileSecurity),
        Direction = PinDirection.In,
        Name = nameof(InPinFileSecurity),
        DisplayName = "FileSecurity",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFileSecurity { get; set; } 

        [DataPinDefinition(
        Id = "ddb6a789-e997-4861-9680-6154cbe88371",
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