// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IODirectoryEnumerateDirectories_String_String_SearchOption), DisplayName = "EnumerateDirectories(String,String,SearchOption)", Category = "System/Directory")]
    public class System_IODirectoryEnumerateDirectories_String_String_SearchOption : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.Directory.EnumerateDirectories(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.String>(InPinSearchPattern),
                scope.GetValue<System.IO.SearchOption>(InPinSearchOption));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IODirectoryEnumerateDirectories_String_String_SearchOption: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IODirectoryEnumerateDirectories_String_String_SearchOption); 
        public override string FriendlyName => nameof(System_IODirectoryEnumerateDirectories_String_String_SearchOption); 

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

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "Each item", 
        Name = nameof(OutNodeEachItem), 
        AllowMultiple = false)] 
        public ActionNode OutNodeEachItem { get; set; } 

        [DataPinDefinition(
        Id = "41a2d432-8915-4566-920a-b251741bd97f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "c13bc1fb-4320-4ea6-8b42-c16046e57805",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSearchPattern),
        DisplayName = "SearchPattern",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSearchPattern { get; set; } 

        [DataPinDefinition(
        Id = "d6f655ed-be8e-42de-997e-4a258ab6e605",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.SearchOption),
        Direction = PinDirection.In,
        Name = nameof(InPinSearchOption),
        DisplayName = "SearchOption",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSearchOption { get; set; } 

        [DataPinDefinition(
        Id = "a74a9898-232a-4c3e-bb4e-177837c43eb5",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Collections.Generic.IEnumerable<System.String>),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "5ccc2968-756f-4311-8e95-b5a3494aad3c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.Out,
        Name = nameof(OutPinCurrent),
        DisplayName = "Current",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinCurrent { get; set; } 

    }
}