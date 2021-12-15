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
        Id = "e5807d6c-7eaa-4f76-a8ce-c7a7bf12d8f6",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "84e796c9-3b4e-45da-b9f3-6583e856d5ae",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSearchPattern),
        DisplayName = "SearchPattern",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSearchPattern { get; set; } 

        [DataPinDefinition(
        Id = "8a2184bb-54a1-4896-b8a6-b39b9f323f05",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.SearchOption),
        Direction = PinDirection.In,
        Name = nameof(InPinSearchOption),
        DisplayName = "SearchOption",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSearchOption { get; set; } 

        [DataPinDefinition(
        Id = "544e9023-dd21-48fb-9f2a-832a79f6982d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String[]),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

        [DataPinDefinition(
        Id = "78cea42e-3e89-4095-820a-b5e34221203c",
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