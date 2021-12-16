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
        Id = "2969cdfb-56f7-499c-bf0c-1d8a77b5f6f6",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "921fed9e-2eb8-4912-bb00-90988ff45c46",
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