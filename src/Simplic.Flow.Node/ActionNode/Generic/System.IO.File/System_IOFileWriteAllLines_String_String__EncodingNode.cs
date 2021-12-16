// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileWriteAllLines_String_String__Encoding), DisplayName = "WriteAllLines(String,String[],Encoding)", Category = "System/File")]
    public class System_IOFileWriteAllLines_String_String__Encoding : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.WriteAllLines(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.String[]>(InPinContents),
                scope.GetValue<System.Text.Encoding>(InPinEncoding));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileWriteAllLines_String_String__Encoding: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileWriteAllLines_String_String__Encoding); 
        public override string FriendlyName => nameof(System_IOFileWriteAllLines_String_String__Encoding); 

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
        Id = "a8b261d0-d9da-40f7-ae61-6d7870ef56e0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "5c218e95-e9e9-47e3-ac9a-45feca85009f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.In,
        Name = nameof(InPinContents),
        DisplayName = "Contents",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinContents { get; set; } 

        [DataPinDefinition(
        Id = "ea64e668-2b38-4175-890e-79306ef1efc5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.Encoding),
        Direction = PinDirection.In,
        Name = nameof(InPinEncoding),
        DisplayName = "Encoding",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinEncoding { get; set; } 

    }
}