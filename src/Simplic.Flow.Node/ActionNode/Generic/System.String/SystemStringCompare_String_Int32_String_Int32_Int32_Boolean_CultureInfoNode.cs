// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringCompare_String_Int32_String_Int32_Int32_Boolean_CultureInfo), DisplayName = "Compare(String,Int32,String,Int32,Int32,Boolean,CultureInfo)", Category = "System/String")]
    public class SystemStringCompare_String_Int32_String_Int32_Int32_Boolean_CultureInfo : ActionNode 
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringCompare_String_Int32_String_Int32_Int32_Boolean_CultureInfo: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringCompare_String_Int32_String_Int32_Int32_Boolean_CultureInfo); 
        public override string FriendlyName => nameof(SystemStringCompare_String_Int32_String_Int32_Int32_Boolean_CultureInfo); 

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
        Id = "2400dec7-ece1-4b89-a9ed-9d8ac9900f1e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrA),
        DisplayName = "StrA",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrA { get; set; } 

        [DataPinDefinition(
        Id = "190f30f4-b66c-4232-984d-78199df4c0aa",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndexA),
        DisplayName = "IndexA",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndexA { get; set; } 

        [DataPinDefinition(
        Id = "9d6c913b-c5fc-4fde-97e4-990818b94142",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrB),
        DisplayName = "StrB",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrB { get; set; } 

        [DataPinDefinition(
        Id = "df56782f-9449-4f2f-bedc-54a88c5908d2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndexB),
        DisplayName = "IndexB",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndexB { get; set; } 

        [DataPinDefinition(
        Id = "e650c937-31c3-4934-b82a-508fe6057549",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "6aec3350-0def-4f40-a95a-314aeab254cd",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.In,
        Name = nameof(InPinIgnoreCase),
        DisplayName = "IgnoreCase",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIgnoreCase { get; set; } 

        [DataPinDefinition(
        Id = "910671e2-649e-4379-8c78-af6708d486cb",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.CultureInfo),
        Direction = PinDirection.In,
        Name = nameof(InPinCulture),
        DisplayName = "Culture",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinCulture { get; set; } 

        [DataPinDefinition(
        Id = "6fef6fdc-b6ec-4e96-bde6-1b037a8aeccd",
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