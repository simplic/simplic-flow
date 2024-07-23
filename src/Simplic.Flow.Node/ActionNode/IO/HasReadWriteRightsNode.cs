using Simplic.Flow;
using System;
using System.IO;

namespace Simplic.Flow.Node.IO
{
    [ActionNodeDefinition(DisplayName = "Has Read&Write Rights", Name = "HasReadWriteRightsNode", Category ="IO")]
    public class HasReadWriteRightsNode : ActionNode
    {
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            try
            {
                string filePath = scope.GetValue<string>(InPinFilePath);
                var attributes = System.IO.File.GetAttributes(filePath);

                if (attributes == FileAttributes.ReadOnly)
                {
                    // File is read-only
                    Console.WriteLine("File is readonly.");
                    runtime.EnqueueNode(OutNodeFalse, scope);
                    return false;
                }
                else
                {
                    // File is read-write
                    runtime.EnqueueNode(OutNodeTrue, scope);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not check file if its readonly.", ex);
                return false;
            }
        }


        #region Flow Pins
        [FlowPinDefinition(DisplayName = "True", Name = "OutNodeTrue", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeTrue { get; set; }

        [FlowPinDefinition(DisplayName = "False", Name = "OutNodeFalse", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeFalse { get; set; }
        #endregion

        #region Data Pins
        [DataPinDefinition(
            Id = "66d72cea-b6d9-4672-acfa-e5a66a6e9e83",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            DisplayName = "File path",
            Name = "InPinFilePath")]
        public DataPin InPinFilePath { get; set; }
        #endregion

        public override string FriendlyName
        {
            get
            {
                return nameof(HasReadWriteRightsNode);
            }
        }

        public override string Name
        {
            get
            {
                return nameof(HasReadWriteRightsNode);
            }
        }
    }
}

