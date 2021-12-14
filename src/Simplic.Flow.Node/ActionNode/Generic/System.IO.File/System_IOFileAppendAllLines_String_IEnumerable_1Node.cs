// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileAppendAllLines_String_IEnumerable_1), DisplayName = "AppendAllLines(String,IEnumerable`1)", Category = "System/File")]
    public class System_IOFileAppendAllLines_String_IEnumerable_1 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.AppendAllLines(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.Collections.Generic.IEnumerable<System.String > >(InPinContents));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileAppendAllLines_String_IEnumerable_1: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileAppendAllLines_String_IEnumerable_1); 
        public override string FriendlyName => nameof(System_IOFileAppendAllLines_String_IEnumerable_1); 

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
        Id = "e9acad8d-4a73-42d1-b071-877a0cfff069",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "6494e4cb-1e2f-4aad-bff0-ce1d50f550c0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Collections.Generic.IEnumerable<System.String>),
        Direction = PinDirection.In,
        Name = nameof(InPinContents),
        DisplayName = "Contents",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinContents { get; set; } 

    }
}