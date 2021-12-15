// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemEnumGetNames_Type), DisplayName = "GetNames(Type)", Category = "System/Enum")]
    public class SystemEnumGetNames_Type : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.Enum.GetNames(
                scope.GetValue<System.Type>(InPinEnumType));
                scope.SetValue(OutPinReturn, returnValue);

                foreach (var item in returnValue)
                {
                    var childScope = scope.CreateChild();
                    childScope.SetValue(OutPinCurrent, item);

                    if (OutNodeEachItem != null)
                        runtime.EnqueueNode(OutNodeEachItem, childScope);
                }
                    
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemEnumGetNames_Type: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemEnumGetNames_Type); 
        public override string FriendlyName => nameof(SystemEnumGetNames_Type); 

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

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "Each item", 
        Name = nameof(OutNodeEachItem), 
        AllowMultiple = false)] 
        public ActionNode OutNodeEachItem { get; set; } 

        [DataPinDefinition(
        Id = "d12ae40f-8b5e-4033-be65-486e71e04a7c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Type),
        Direction = PinDirection.In,
        Name = nameof(InPinEnumType),
        DisplayName = "EnumType",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinEnumType { get; set; } 

        [DataPinDefinition(
        Id = "b02c67d2-1087-4024-9507-b4d6999edc69",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "9925cd50-f737-43f6-bc74-798fd6602e52",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.Out,
        Name = nameof(OutPinCurrent),
        DisplayName = "Current",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinCurrent { get; set; } 

    }
}