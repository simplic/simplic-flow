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
        Id = "83bde8c3-3e26-4afd-9050-3a5f1b57a405",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrA),
        DisplayName = "StrA",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrA { get; set; } 

        [DataPinDefinition(
        Id = "f00c5750-d7e2-4865-85ea-4d20237f8756",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndexA),
        DisplayName = "IndexA",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndexA { get; set; } 

        [DataPinDefinition(
        Id = "22a0a917-94c0-4f69-a9bd-ee0d7bd77a85",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrB),
        DisplayName = "StrB",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrB { get; set; } 

        [DataPinDefinition(
        Id = "93177a37-b0cc-4eef-a2bf-29892a6cfab6",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndexB),
        DisplayName = "IndexB",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndexB { get; set; } 

        [DataPinDefinition(
        Id = "4a73c846-da7f-4afc-8db8-cf2dc15e9dcc",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "9098a2be-bb76-425c-a81a-163da2de835f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.In,
        Name = nameof(InPinIgnoreCase),
        DisplayName = "IgnoreCase",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIgnoreCase { get; set; } 

        [DataPinDefinition(
        Id = "6b00c888-27e1-4e69-b77c-b82c8075b418",
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