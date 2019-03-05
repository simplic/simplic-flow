using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Node
{
    /// <summary>
    /// Read all text node
    /// </summary>
    [ActionNodeDefinition(DisplayName = "ReadAllBytes", Name = "ReadAllBytesNode", Category = "IO", Tooltip = "Read bytes")]
    public class ReadAllBytesNode : ActionNode
    {
        /// <summary>
        /// Execute read all bytes
        /// </summary>
        /// <param name="runtime">Runtime instance</param>
        /// <param name="scope">Scope instance</param>
        /// <returns>True if successfull</returns>
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            try
            {
                var blob = File.ReadAllBytes(scope.GetValue<string>(InPinFilePath));
                scope.SetValue(OutPinBlob, blob);

                if (OutNode != null)
                    runtime.EnqueueNode(OutNode, scope);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Read all bytes failed {ex}");

                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }

            return true;
        }

        /// <summary>
        /// Gets or sets the flow out node (success)
        /// </summary>
        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        /// <summary>
        /// Gets or sets the flow out node failed
        /// </summary>
        [FlowPinDefinition(DisplayName = "Failed", Name = "OutNodeFailed", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeFailed { get; set; }

        /// <summary>
        /// Gets or sets the in pin file path
        /// </summary>
        [DataPinDefinition(
            Id = "0932042e-d24c-4b44-8032-1a4e04172d5e",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = "InPinFilePath",
            DisplayName = "File Path")]
        public DataPin InPinFilePath { get; set; }

        /// <summary>
        /// Gets or sets the out pin text
        /// </summary>
        [DataPinDefinition(
            Id = "2b78d26c-c69f-480c-9725-05a8e813fb0b",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(byte[]),
            Direction = PinDirection.Out,
            Name = "OutPinBlob",
            DisplayName = "Blob")]
        public DataPin OutPinBlob { get; set; }

        /// <summary>
        /// Gets or sets the friendly name
        /// </summary>
        public override string FriendlyName
        {
            get
            {
                return nameof(ReadAllBytesNode);
            }
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public override string Name
        {
            get
            {
                return nameof(ReadAllBytesNode);
            }
        }
    }
}
