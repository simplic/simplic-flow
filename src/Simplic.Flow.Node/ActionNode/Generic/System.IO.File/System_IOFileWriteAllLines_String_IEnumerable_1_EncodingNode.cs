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
        Id = "21895161-f575-4c7d-9f63-fb0bb90a9c68",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "ed82ed5e-6ba2-4c72-bfa3-d80a311d2593",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Collections.Generic.IEnumerable<System.String>),
        Direction = PinDirection.In,
        Name = nameof(InPinContents),
        DisplayName = "Contents",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinContents { get; set; } 

        [DataPinDefinition(
        Id = "22fec6e2-a8aa-4cfd-a3b9-4540f3522507",
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