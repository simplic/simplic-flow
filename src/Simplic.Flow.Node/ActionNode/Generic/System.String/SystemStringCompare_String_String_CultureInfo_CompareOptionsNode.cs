// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemStringCompare_String_String_CultureInfo_CompareOptions), DisplayName = "Compare(String,String,CultureInfo,CompareOptions)", Category = "System/String")]
    public class SystemStringCompare_String_String_CultureInfo_CompareOptions : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.String.Compare(
                scope.GetValue<System.String>(InPinStrA),
                scope.GetValue<System.String>(InPinStrB),
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemStringCompare_String_String_CultureInfo_CompareOptions: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemStringCompare_String_String_CultureInfo_CompareOptions); 
        public override string FriendlyName => nameof(SystemStringCompare_String_String_CultureInfo_CompareOptions); 

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
        Id = "e1c876cb-014e-4e0b-84c0-721482996d3f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrA),
        DisplayName = "StrA",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrA { get; set; } 

        [DataPinDefinition(
        Id = "245b15b3-1147-404e-9f6e-5767d4750369",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrB),
        DisplayName = "StrB",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrB { get; set; } 

        [DataPinDefinition(
        Id = "33b6f45b-b0b2-4d97-843b-d246f9da93dc",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.CultureInfo),
        Direction = PinDirection.In,
        Name = nameof(InPinCulture),
        DisplayName = "Culture",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinCulture { get; set; } 

        [DataPinDefinition(
        Id = "c3aa725b-59f6-4819-91b0-f02993b4919c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.CompareOptions),
        Direction = PinDirection.In,
        Name = nameof(InPinOptions),
        DisplayName = "Options",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOptions { get; set; } 

        [DataPinDefinition(
        Id = "f06d62ff-da89-48f5-8bd9-d9929ff4f007",
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