// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemDateTimeCompare_DateTime_DateTime), DisplayName = "Compare(DateTime,DateTime)", Category = "System/DateTime")]
    public class SystemDateTimeCompare_DateTime_DateTime : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.DateTime.Compare(
                scope.GetValue<System.DateTime>(InPinT1),
                scope.GetValue<System.DateTime>(InPinT2));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemDateTimeCompare_DateTime_DateTime: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemDateTimeCompare_DateTime_DateTime); 
        public override string FriendlyName => nameof(SystemDateTimeCompare_DateTime_DateTime); 

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
        Id = "52a3052e-3d80-41cd-8bd4-fa6bed479827",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.In,
        Name = nameof(InPinT1),
        DisplayName = "T1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinT1 { get; set; } 

        [DataPinDefinition(
        Id = "c05661ad-bd10-4cf9-a538-08db93134c04",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.In,
        Name = nameof(InPinT2),
        DisplayName = "T2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinT2 { get; set; } 

        [DataPinDefinition(
        Id = "017056d5-427b-4586-930b-3d8fac30181e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}