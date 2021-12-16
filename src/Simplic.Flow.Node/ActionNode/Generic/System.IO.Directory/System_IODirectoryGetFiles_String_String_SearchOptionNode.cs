// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IODirectoryGetFiles_String_String_SearchOption), DisplayName = "GetFiles(String,String,SearchOption)", Category = "System/Directory")]
    public class System_IODirectoryGetFiles_String_String_SearchOption : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.Directory.GetFiles(
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
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IODirectoryGetFiles_String_String_SearchOption: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IODirectoryGetFiles_String_String_SearchOption); 
        public override string FriendlyName => nameof(System_IODirectoryGetFiles_String_String_SearchOption); 

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
        Id = "2cce0f68-b2a8-483d-be41-f893fae133d3",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "a9de4350-29de-4d12-bf7f-60a7aeea76c4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSearchPattern),
        DisplayName = "SearchPattern",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSearchPattern { get; set; } 

        [DataPinDefinition(
        Id = "736d8ba0-b7ae-414e-9d48-dfec557f8d7f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.SearchOption),
        Direction = PinDirection.In,
        Name = nameof(InPinSearchOption),
        DisplayName = "SearchOption",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSearchOption { get; set; } 

        [DataPinDefinition(
        Id = "37f4a0ee-8152-4163-90c4-145fadc4555c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "6cb53b42-cce5-4027-b611-e087d705dbe5",
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