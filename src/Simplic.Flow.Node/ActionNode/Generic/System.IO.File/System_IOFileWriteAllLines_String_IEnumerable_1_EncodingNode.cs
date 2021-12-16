// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileWriteAllLines_String_IEnumerable_1_Encoding), DisplayName = "WriteAllLines(String,IEnumerable`1,Encoding)", Category = "System/File")]
    public class System_IOFileWriteAllLines_String_IEnumerable_1_Encoding : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.WriteAllLines(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.Collections.Generic.IEnumerable<System.String > >(InPinContents),
                scope.GetValue<System.Text.Encoding>(InPinEncoding));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileWriteAllLines_String_IEnumerable_1_Encoding: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileWriteAllLines_String_IEnumerable_1_Encoding); 
        public override string FriendlyName => nameof(System_IOFileWriteAllLines_String_IEnumerable_1_Encoding); 

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
        Id = "21f43756-6a86-444c-8b5c-ee13806fbc61",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "238952ef-78fe-40ed-a94b-e45b481f534c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Collections.Generic.IEnumerable<System.String>),
        Direction = PinDirection.In,
        Name = nameof(InPinContents),
        DisplayName = "Contents",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinContents { get; set; } 

        [DataPinDefinition(
        Id = "635fccd2-8cf0-4db8-9606-185cfe3814f1",
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