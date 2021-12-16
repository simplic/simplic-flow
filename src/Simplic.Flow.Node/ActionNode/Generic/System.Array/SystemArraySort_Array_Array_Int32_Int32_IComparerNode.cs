// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArraySort_Array_Array_Int32_Int32_IComparer), DisplayName = "Sort(Array,Array,Int32,Int32,IComparer)", Category = "System/Array")]
    public class SystemArraySort_Array_Array_Int32_Int32_IComparer : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Array.Sort(
                scope.GetValue<System.Array>(InPinKeys),
                scope.GetValue<System.Array>(InPinItems),
                scope.GetValue<System.Int32>(InPinIndex),
                scope.GetValue<System.Int32>(InPinLength),
                scope.GetValue<System.Collections.IComparer>(InPinComparer));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArraySort_Array_Array_Int32_Int32_IComparer: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArraySort_Array_Array_Int32_Int32_IComparer); 
        public override string FriendlyName => nameof(SystemArraySort_Array_Array_Int32_Int32_IComparer); 

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
        Id = "5e9627e2-ca16-4e97-956f-3be68a496952",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinKeys),
        DisplayName = "Keys",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinKeys { get; set; } 

        [DataPinDefinition(
        Id = "0c6b0361-bb46-4cd9-abb1-1d0c4025c026",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinItems),
        DisplayName = "Items",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinItems { get; set; } 

        [DataPinDefinition(
        Id = "5e78fe9b-72d7-4cd8-96a5-3d23ce357efa",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinIndex),
        DisplayName = "Index",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIndex { get; set; } 

        [DataPinDefinition(
        Id = "c808e199-fbb8-49c7-839b-3552ddd91a25",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int32),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

        [DataPinDefinition(
        Id = "eb96e941-f4fb-4251-a4f4-7d69f148ae31",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Collections.IComparer),
        Direction = PinDirection.In,
        Name = nameof(InPinComparer),
        DisplayName = "Comparer",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinComparer { get; set; } 

    }
}