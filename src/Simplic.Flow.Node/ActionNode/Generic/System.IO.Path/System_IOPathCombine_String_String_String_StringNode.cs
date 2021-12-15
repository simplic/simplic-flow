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
        Id = "f8c7c5fc-ca29-4942-b889-5bad84872c57",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath1),
        DisplayName = "Path1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath1 { get; set; } 

        [DataPinDefinition(
        Id = "bf8cee3e-2b26-46ef-a5c4-dfd65a24e1a6",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath2),
        DisplayName = "Path2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath2 { get; set; } 

        [DataPinDefinition(
        Id = "4776d0a2-b038-4c10-83c2-6eb0cf4a11a2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath3),
        DisplayName = "Path3",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath3 { get; set; } 

        [DataPinDefinition(
        Id = "9709ab5f-13ca-4711-a634-e85111c77aed",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath4),
        DisplayName = "Path4",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath4 { get; set; } 

        [DataPinDefinition(
        Id = "b5fed894-8b95-46d1-a7fe-97b4ac8e604d",
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