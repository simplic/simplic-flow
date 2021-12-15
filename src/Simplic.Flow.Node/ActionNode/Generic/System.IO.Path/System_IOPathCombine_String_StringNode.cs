// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOPathCombine_String_String), DisplayName = "Combine(String,String)", Category = "System/Path")]
    public class System_IOPathCombine_String_String : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.Path.Combine(
                scope.GetValue<System.String>(InPinPath1),
                scope.GetValue<System.String>(InPinPath2));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOPathCombine_String_String: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOPathCombine_String_String); 
        public override string FriendlyName => nameof(System_IOPathCombine_String_String); 

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
        Id = "d866ce9f-c5bf-4fa6-8926-66cf2f944cbf",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath1),
        DisplayName = "Path1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath1 { get; set; } 

        [DataPinDefinition(
        Id = "3fc51e9c-3d65-472c-bda9-89a10431a68a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath2),
        DisplayName = "Path2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath2 { get; set; } 

        [DataPinDefinition(
        Id = "5859cee3-68a4-4d37-b532-9ef4a98cfa1c",
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