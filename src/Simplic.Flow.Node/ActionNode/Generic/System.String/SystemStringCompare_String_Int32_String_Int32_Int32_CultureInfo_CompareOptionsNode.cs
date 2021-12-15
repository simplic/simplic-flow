// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringCompare_String_Int32_String_Int32_Int32_CultureInfo_CompareOptions), DisplayName = "Compare(String,Int32,String,Int32,Int32,CultureInfo,CompareOptions)", Category = "System/String")]
    public class SystemStringCompare_String_Int32_String_Int32_Int32_CultureInfo_CompareOptions : ActionNode 
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
                scope.GetValue<System.Globalization.CultureInfo>(InPinCulture),
                scope.GetValue<System.Globalization.CompareOptions>(InPinOptions));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringCompare_String_Int32_String_Int32_Int32_CultureInfo_CompareOptions: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringCompare_String_Int32_String_Int32_Int32_CultureInfo_CompareOptions); 
        public override string FriendlyName => nameof(SystemStringCompare_String_Int32_String_Int32_Int32_CultureInfo_CompareOptions); 

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
        Id = "0d965bc2-0ac9-418e-9423-bb1ed7c953ab",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrA),
        DisplayName = "StrA",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrA { get; set; } 

        [DataPinDefinition(
        Id = "7e2838ef-580b-4c86-84c8-2b35b77dbd24",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndexA),
        DisplayName = "IndexA",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndexA { get; set; } 

        [DataPinDefinition(
        Id = "cd49713a-fc9a-4a6e-a900-cbd8e75b3302",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrB),
        DisplayName = "StrB",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrB { get; set; } 

        [DataPinDefinition(
        Id = "9cd09746-07a3-401b-8e6a-c25ad571ba39",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndexB),
        DisplayName = "IndexB",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndexB { get; set; } 

        [DataPinDefinition(
        Id = "78f15ee5-bfa1-4185-92b8-4fc264dbc36c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "dbfd4922-228a-46f3-83d6-95ed2099459c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.CultureInfo),
        Direction = PinDirection.In,
        Name = nameof(InPinCulture),
        DisplayName = "Culture",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinCulture { get; set; } 

        [DataPinDefinition(
        Id = "b52bd2d9-15b9-4e52-ba57-90f4f6f12603",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.CompareOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "c4aa70aa-d6e8-499f-bcb1-c33a36ff2c12",
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