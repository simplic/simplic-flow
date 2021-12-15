// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringCompare_String_Int32_String_Int32_Int32_Boolean), DisplayName = "Compare(String,Int32,String,Int32,Int32,Boolean)", Category = "System/String")]
    public class SystemStringCompare_String_Int32_String_Int32_Int32_Boolean : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Compare(
                scope.GetValue<System.String>(InPinStrA),
                scope.GetValue<System.Int32>(InPinIndexA),
                scope.GetValue<System.String>(InPinStrB),
                scope.GetValue<System.Int32>(InPinIndexB),
                scope.GetValue<System.Int32>(InPinLength),
                scope.GetValue<System.Boolean>(InPinIgnoreCase));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringCompare_String_Int32_String_Int32_Int32_Boolean: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringCompare_String_Int32_String_Int32_Int32_Boolean); 
        public override string FriendlyName => nameof(SystemStringCompare_String_Int32_String_Int32_Int32_Boolean); 

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
        Id = "ebe5be02-1454-46a8-acb1-9a44d51a77df",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrA),
        DisplayName = "StrA",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrA { get; set; } 

        [DataPinDefinition(
        Id = "ca93abd0-b01a-4c8d-9c4b-aa78fce6ab5a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndexA),
        DisplayName = "IndexA",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndexA { get; set; } 

        [DataPinDefinition(
        Id = "3b417f00-3261-4ae9-a3bf-91c6929425ee",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrB),
        DisplayName = "StrB",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrB { get; set; } 

        [DataPinDefinition(
        Id = "6fea5a65-9797-4da8-92b0-92d488999b76",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndexB),
        DisplayName = "IndexB",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndexB { get; set; } 

        [DataPinDefinition(
        Id = "720243dd-2aca-45bf-952f-5dc28c5703e5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "b0856f10-ac83-491a-9ea0-58b213d70ae1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.In,
        Name = nameof(InPinIgnoreCase),
        DisplayName = "IgnoreCase",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIgnoreCase { get; set; } 

        [DataPinDefinition(
        Id = "970c54f8-11da-4563-b77c-2f2022f80359",
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