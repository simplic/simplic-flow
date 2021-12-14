// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(SystemArrayCopy_Array_Int64_Array_Int64_Int64), DisplayName = "Copy(Array,Int64,Array,Int64,Int64)", Category = "System/Array")]
    public class SystemArrayCopy_Array_Int64_Array_Int64_Int64 : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.Array.Copy(
                scope.GetValue<System.Array>(InPinSourceArray),
                scope.GetValue<System.Int64>(InPinSourceIndex),
                scope.GetValue<System.Array>(InPinDestinationArray),
                scope.GetValue<System.Int64>(InPinDestinationIndex),
                scope.GetValue<System.Int64>(InPinLength));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in SystemArrayCopy_Array_Int64_Array_Int64_Int64: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(SystemArrayCopy_Array_Int64_Array_Int64_Int64); 
        public override string FriendlyName => nameof(SystemArrayCopy_Array_Int64_Array_Int64_Int64); 

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
        Id = "507c28a1-9f43-438c-bc7c-13ec58f8a8e4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceArray),
        DisplayName = "SourceArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceArray { get; set; } 

        [DataPinDefinition(
        Id = "bdab2392-94be-40a5-b654-baebe235811e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceIndex),
        DisplayName = "SourceIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceIndex { get; set; } 

        [DataPinDefinition(
        Id = "df6f8695-2969-4f42-aed2-b8b424651614",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Array),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationArray),
        DisplayName = "DestinationArray",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationArray { get; set; } 

        [DataPinDefinition(
        Id = "a5fe8f3f-66c5-483a-b6ef-704c6bd03974",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationIndex),
        DisplayName = "DestinationIndex",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationIndex { get; set; } 

        [DataPinDefinition(
        Id = "1ae820f2-cd16-4345-8632-bb6225247512",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Int64),
        Direction = PinDirection.In,
        Name = nameof(InPinLength),
        DisplayName = "Length",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLength { get; set; } 

    }
}