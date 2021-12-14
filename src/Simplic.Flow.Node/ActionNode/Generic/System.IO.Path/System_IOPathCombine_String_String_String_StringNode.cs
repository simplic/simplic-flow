// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOPathCombine_String_String_String_String), DisplayName = "Combine(String,String,String,String)", Category = "System/Path")]
    public class System_IOPathCombine_String_String_String_String : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.Path.Combine(
                scope.GetValue<System.String>(InPinPath1),
                scope.GetValue<System.String>(InPinPath2),
                scope.GetValue<System.String>(InPinPath3),
                scope.GetValue<System.String>(InPinPath4));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOPathCombine_String_String_String_String: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOPathCombine_String_String_String_String); 
        public override string FriendlyName => nameof(System_IOPathCombine_String_String_String_String); 

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
        Id = "92f46580-e270-4211-9e07-d8da0c7070b3",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath1),
        DisplayName = "Path1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath1 { get; set; } 

        [DataPinDefinition(
        Id = "921ab7d3-6040-4c1d-8642-9860a0e7c303",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath2),
        DisplayName = "Path2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath2 { get; set; } 

        [DataPinDefinition(
        Id = "89df5d7d-d0fc-4757-9e4f-254446fd22af",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath3),
        DisplayName = "Path3",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath3 { get; set; } 

        [DataPinDefinition(
        Id = "f563034f-b956-40c3-bb47-94ddfd3c5aea",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath4),
        DisplayName = "Path4",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath4 { get; set; } 

        [DataPinDefinition(
        Id = "623df5c4-39d8-4cf5-a0cd-321a9da8e392",
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