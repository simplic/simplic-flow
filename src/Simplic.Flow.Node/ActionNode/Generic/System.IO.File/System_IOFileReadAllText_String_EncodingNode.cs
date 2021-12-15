// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileReadAllText_String_Encoding), DisplayName = "ReadAllText(String,Encoding)", Category = "System/File")]
    public class System_IOFileReadAllText_String_Encoding : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.File.ReadAllText(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.Text.Encoding>(InPinEncoding));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileReadAllText_String_Encoding: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileReadAllText_String_Encoding); 
        public override string FriendlyName => nameof(System_IOFileReadAllText_String_Encoding); 

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
        Id = "80b4df9f-8b4b-4308-9e67-2fc224f82393",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "36ab2b8c-199c-4f55-b0a9-2bae7bcaf9db",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.Encoding),
        Direction = PinDirection.In,
        Name = nameof(InPinEncoding),
        DisplayName = "Encoding",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinEncoding { get; set; } 

        [DataPinDefinition(
        Id = "c462cf80-5550-4f23-b031-120dc6eba25a",
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