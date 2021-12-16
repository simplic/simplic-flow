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
        Id = "d730f505-bd2d-4010-b31f-341c1a1c9629",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Type),
        Direction = PinDirection.In,
        Name = nameof(InPinEnumType),
        DisplayName = "EnumType",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinEnumType { get; set; } 

        [DataPinDefinition(
        Id = "e6c10c3e-edb9-4fbc-b58d-e17ceef5b8db",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "c198960e-42e6-4a45-aed7-6d18016eb604",
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