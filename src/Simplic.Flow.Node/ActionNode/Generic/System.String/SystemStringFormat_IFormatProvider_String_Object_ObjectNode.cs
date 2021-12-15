// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringFormat_IFormatProvider_String_Object_Object), DisplayName = "Format(IFormatProvider,String,Object,Object)", Category = "System/String")]
    public class SystemStringFormat_IFormatProvider_String_Object_Object : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Format(
                scope.GetValue<System.IFormatProvider>(InPinProvider),
                scope.GetValue<System.String>(InPinFormat),
                scope.GetValue<System.Object>(InPinArg0),
                scope.GetValue<System.Object>(InPinArg1));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringFormat_IFormatProvider_String_Object_Object: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringFormat_IFormatProvider_String_Object_Object); 
        public override string FriendlyName => nameof(SystemStringFormat_IFormatProvider_String_Object_Object); 

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
        Id = "47c6bdd8-cc0d-41d8-88d0-64f86ae6afa5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinProvider),
        DisplayName = "Provider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinProvider { get; set; } 

        [DataPinDefinition(
        Id = "ddc87af2-36ee-4b7d-b6ff-14e2316c9759",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinFormat),
        DisplayName = "Format",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormat { get; set; } 

        [DataPinDefinition(
        Id = "de479140-c385-4131-9422-f910e470a004",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg0),
        DisplayName = "Arg0",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg0 { get; set; } 

        [DataPinDefinition(
        Id = "9c9f70f5-a16b-4178-bc8e-4ef0cc1a79e0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object),
        Direction = PinDirection.In,
        Name = nameof(InPinArg1),
        DisplayName = "Arg1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArg1 { get; set; } 

        [DataPinDefinition(
        Id = "98013f04-a0da-4754-8711-3a5505777388",
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