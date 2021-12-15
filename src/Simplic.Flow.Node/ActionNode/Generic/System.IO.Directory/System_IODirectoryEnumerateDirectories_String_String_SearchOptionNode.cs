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
        Id = "e0a4c496-f52f-4899-9ce8-57b64fc28196",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "a08cc2a5-e369-4fb2-9707-f9f41c6d469f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSearchPattern),
        DisplayName = "SearchPattern",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSearchPattern { get; set; } 

        [DataPinDefinition(
        Id = "a48bbc5e-e585-4be2-9750-a89f144f7cc8",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.SearchOption),
        Direction = PinDirection.In,
        Name = nameof(InPinSearchOption),
        DisplayName = "SearchOption",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSearchOption { get; set; } 

        [DataPinDefinition(
        Id = "a7801042-daf2-4f9a-95b2-7fb98fac237d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Collections.Generic.IEnumerable<System.String>),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "1ec0a17d-65ac-40aa-baf6-54bd1a28a006",
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