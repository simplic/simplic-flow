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
        Id = "66808ec3-3251-4311-9cfc-bf9e23d4ed68",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrA),
        DisplayName = "StrA",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrA { get; set; } 

        [DataPinDefinition(
        Id = "24d5d5d0-7c15-47a1-a3d5-caa029325cc5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinStrB),
        DisplayName = "StrB",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinStrB { get; set; } 

        [DataPinDefinition(
        Id = "c094ffba-b53d-4314-aef1-600f72e6c62f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.In,
        Name = nameof(InPinIgnoreCase),
        DisplayName = "IgnoreCase",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIgnoreCase { get; set; } 

        [DataPinDefinition(
        Id = "728a519f-c4c6-47b3-afc8-d46768825fea",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Globalization.CultureInfo),
        Direction = PinDirection.In,
        Name = nameof(InPinCulture),
        DisplayName = "Culture",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinCulture { get; set; } 

        [DataPinDefinition(
        Id = "c1d85d3e-a49c-4fd8-837e-f0f2e06ff9f1",
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