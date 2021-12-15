// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileCreate_String_Int32_FileOptions), DisplayName = "Create(String,Int32,FileOptions)", Category = "System/File")]
    public class System_IOFileCreate_String_Int32_FileOptions : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.File.Create(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.Int32>(InPinBufferSize),
                scope.GetValue<System.IO.FileOptions>(InPinOptions));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileCreate_String_Int32_FileOptions: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileCreate_String_Int32_FileOptions); 
        public override string FriendlyName => nameof(System_IOFileCreate_String_Int32_FileOptions); 

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
        Id = "5cfc914a-a4d9-4f56-8d0b-8d23136b5e83",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "560a8103-a330-4e7e-9f4d-d52b7119597a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinBufferSize),
        DisplayName = "BufferSize",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinBufferSize { get; set; } 

        [DataPinDefinition(
        Id = "6da8a7dc-1aa8-460f-91fa-a80c5d885896",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "b8881485-4fdf-42c1-9311-2c72cf5ea827",
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