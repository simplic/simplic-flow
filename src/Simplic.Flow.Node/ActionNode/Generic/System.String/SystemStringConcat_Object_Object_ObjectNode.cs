// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringConcat_Object_Object_Object), DisplayName = "Concat(Object,Object,Object)", Category = "System/String")]
    public class SystemStringConcat_Object_Object_Object : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Concat(
                scope.GetValue<System.Object>(InPinArg0),
                scope.GetValue<System.Object>(InPinArg1),
                scope.GetValue<System.Object>(InPinArg2));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringConcat_Object_Object_Object: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringConcat_Object_Object_Object); 
        public override string FriendlyName => nameof(SystemStringConcat_Object_Object_Object); 

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
        Id = "d8fab77b-2cd9-4c44-b9b5-6e845d83c574",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg0),
        DisplayName = "Arg0",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg0 { get; set; } 

        [DataPinDefinition(
        Id = "2fd3c0ba-db5c-417c-8da1-f10763bbaf85",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg1),
        DisplayName = "Arg1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg1 { get; set; } 

        [DataPinDefinition(
        Id = "99a4bb00-edac-4eb5-978a-7c426a031c92",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg2),
        DisplayName = "Arg2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg2 { get; set; } 

        [DataPinDefinition(
        Id = "0ae3d32a-59df-48e2-bad6-70e1a6c9e9c6",
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