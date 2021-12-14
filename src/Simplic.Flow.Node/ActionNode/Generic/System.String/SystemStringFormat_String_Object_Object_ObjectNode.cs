// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringFormat_String_Object_Object_Object), DisplayName = "Format(String,Object,Object,Object)", Category = "System/String")]
    public class SystemStringFormat_String_Object_Object_Object : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Format(
                scope.GetValue<System.String>(InPinFormat),
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringFormat_String_Object_Object_Object: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringFormat_String_Object_Object_Object); 
        public override string FriendlyName => nameof(SystemStringFormat_String_Object_Object_Object); 

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
        Id = "b3f29ab6-45cb-4bca-aee2-bbc5c5a84a9d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinFormat),
        DisplayName = "Format",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormat { get; set; } 

        [DataPinDefinition(
        Id = "19627250-5baf-4a43-adc9-040356d46c84",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg0),
        DisplayName = "Arg0",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg0 { get; set; } 

        [DataPinDefinition(
        Id = "4e29fe87-b21e-47fc-b13f-98f90db4feb3",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg1),
        DisplayName = "Arg1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg1 { get; set; } 

        [DataPinDefinition(
        Id = "92dfa7ee-24e3-4336-bdb8-fca045802289",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg2),
        DisplayName = "Arg2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg2 { get; set; } 

        [DataPinDefinition(
        Id = "1001b1b6-6a3b-4a53-92b6-dc0ed5e975c3",
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