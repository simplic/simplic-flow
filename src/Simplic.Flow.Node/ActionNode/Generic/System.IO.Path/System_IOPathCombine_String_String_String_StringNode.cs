// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOPathCombine_String_String_String_String), DisplayName = "Combine(String,String,String,String)", Category = "System/Path")]
    public class System_IOPathCombine_String_String_String_String : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.Path.Combine(
                scope.GetValue<System.String>(InPinPath1),
                scope.GetValue<System.String>(InPinPath2),
                scope.GetValue<System.String>(InPinPath3),
                scope.GetValue<System.String>(InPinPath4));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOPathCombine_String_String_String_String: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOPathCombine_String_String_String_String); 
        public override string FriendlyName => nameof(System_IOPathCombine_String_String_String_String); 

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
        Id = "2e999dac-f259-46d5-b7da-a6d5950ea97c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath1),
        DisplayName = "Path1",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath1 { get; set; } 

        [DataPinDefinition(
        Id = "97e4433d-867d-4636-bc9a-a30ea83cf2f0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath2),
        DisplayName = "Path2",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath2 { get; set; } 

        [DataPinDefinition(
        Id = "b4db47ae-5e4b-4291-999d-c48df4f9454a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath3),
        DisplayName = "Path3",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath3 { get; set; } 

        [DataPinDefinition(
        Id = "ec0ccb64-6b13-4063-999b-369cf7a12c43",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath4),
        DisplayName = "Path4",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath4 { get; set; } 

        [DataPinDefinition(
        Id = "3ba8688a-0978-456d-ad1d-8341a1ce989f",
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