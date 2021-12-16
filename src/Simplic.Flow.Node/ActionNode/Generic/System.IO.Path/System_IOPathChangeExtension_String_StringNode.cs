// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOPathChangeExtension_String_String), DisplayName = "ChangeExtension(String,String)", Category = "System/Path")]
    public class System_IOPathChangeExtension_String_String : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.Path.ChangeExtension(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.String>(InPinExtension));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOPathChangeExtension_String_String: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOPathChangeExtension_String_String); 
        public override string FriendlyName => nameof(System_IOPathChangeExtension_String_String); 

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
        Id = "42979858-1478-4044-97e6-057ca00c94b7",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "dd0c2e94-7796-4cfa-af56-17e2e3c68f62",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinExtension),
        DisplayName = "Extension",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinExtension { get; set; } 

        [DataPinDefinition(
        Id = "f8c91dfd-66b2-493c-aa4a-65d7d490c706",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}