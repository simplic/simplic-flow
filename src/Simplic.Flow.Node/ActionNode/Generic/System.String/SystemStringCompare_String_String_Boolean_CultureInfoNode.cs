// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringCompare_String_String_Boolean_CultureInfo), DisplayName = "Compare(String,String,Boolean,CultureInfo)", Category = "System/String")]
    public class SystemStringCompare_String_String_Boolean_CultureInfo : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Compare(
                scope.GetValue<System.String>(InPinStrA),
                scope.GetValue<System.String>(InPinStrB),
                scope.GetValue<System.Boolean>(InPinIgnoreCase),
                scope.GetValue<System.Globalization.CultureInfo>(InPinCulture));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringCompare_String_String_Boolean_CultureInfo: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringCompare_String_String_Boolean_CultureInfo); 
        public override string FriendlyName => nameof(SystemStringCompare_String_String_Boolean_CultureInfo); 

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
        Id = "ea90688f-f480-42c5-8df6-43f0f89bfba2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrA),
        DisplayName = "StrA",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrA { get; set; } 

        [DataPinDefinition(
        Id = "47cd94f8-d220-44b5-b636-cf62ab022067",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrB),
        DisplayName = "StrB",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrB { get; set; } 

        [DataPinDefinition(
        Id = "7d05a607-846c-48b4-82ba-3749abb208d9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.In,
        Name = nameof(InPinIgnoreCase),
        DisplayName = "IgnoreCase",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIgnoreCase { get; set; } 

        [DataPinDefinition(
        Id = "82d6856c-9be7-4da5-b27b-c7e5abca9129",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.CultureInfo),
        Direction = PinDirection.In,
        Name = nameof(InPinCulture),
        DisplayName = "Culture",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinCulture { get; set; } 

        [DataPinDefinition(
        Id = "b3cfccae-5fde-44e3-81b0-2673daf9dfb1",
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