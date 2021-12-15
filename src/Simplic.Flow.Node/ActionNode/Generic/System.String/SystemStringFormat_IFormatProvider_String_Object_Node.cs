// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringFormat_IFormatProvider_String_Object_), DisplayName = "Format(IFormatProvider,String,Object[])", Category = "System/String")]
    public class SystemStringFormat_IFormatProvider_String_Object_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Format(
                scope.GetValue<System.IFormatProvider>(InPinProvider),
                scope.GetValue<System.String>(InPinFormat),
                scope.GetValue<System.Object[]>(InPinArgs));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringFormat_IFormatProvider_String_Object_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringFormat_IFormatProvider_String_Object_); 
        public override string FriendlyName => nameof(SystemStringFormat_IFormatProvider_String_Object_); 

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
        Id = "2d87e7af-810d-4469-9e0a-d523bec99e06",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IFormatProvider),
        Direction = PinDirection.In,
        Name = nameof(InPinProvider),
        DisplayName = "Provider",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinProvider { get; set; } 

        [DataPinDefinition(
        Id = "f576d555-8168-42fb-bd43-f5f957277e07",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinFormat),
        DisplayName = "Format",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFormat { get; set; } 

        [DataPinDefinition(
        Id = "04d76f5a-6fb0-4a70-b395-86629e1740bf",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Object[]),
        Direction = PinDirection.In,
        Name = nameof(InPinArgs),
        DisplayName = "Args",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinArgs { get; set; } 

        [DataPinDefinition(
        Id = "0c946ae1-9a0a-4846-8668-8e4d8de135f0",
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