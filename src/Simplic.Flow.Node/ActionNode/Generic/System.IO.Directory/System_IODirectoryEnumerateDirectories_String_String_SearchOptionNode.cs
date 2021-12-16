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

                foreach (var item in returnValue)
                {
                    var childScope = scope.CreateChild();
                    childScope.SetValue(OutPinCurrent, item);

                    if (OutNodeEachItem != null)
                        runtime.EnqueueNode(OutNodeEachItem, childScope);
                }
                    
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
        Id = "15f4667a-ab47-41f6-bc39-9a63ae48567e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "39ef0ac2-d56d-4cf9-993b-b259df08fcb2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSearchPattern),
        DisplayName = "SearchPattern",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSearchPattern { get; set; } 

        [DataPinDefinition(
        Id = "78bca5a8-d188-46c3-954b-0f4edd55ca0b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.SearchOption),
        Direction = PinDirection.In,
        Name = nameof(InPinSearchOption),
        DisplayName = "SearchOption",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSearchOption { get; set; } 

        [DataPinDefinition(
        Id = "ba357d3b-41ce-4117-9fc8-5e94af1e5cb2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Collections.Generic.IEnumerable<System.String>),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "6648a998-b6b3-4f59-a53d-6d8c548f9c0c",
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