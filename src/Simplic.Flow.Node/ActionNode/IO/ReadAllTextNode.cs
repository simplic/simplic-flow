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
    [ActionNodeDefinition(DisplayName = "ReadAllText", Name = "ReadAllTextNode", Category = "IO", Tooltip = "Read text")]
    public class ReadAllTextNode : ActionNode
    {
        /// <summary>
        /// Execute read all text
        /// </summary>
        /// <param name="runtime">Runtime instance</param>
        /// <param name="scope">Scope instance</param>
        /// <returns>True if successfull</returns>
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            try
            {
                var text = File.ReadAllText(scope.GetValue<string>(InPinFilePath));
                scope.SetValue(OutPinText, text);

                if (OutNode != null)
                    runtime.EnqueueNode(OutNode, scope);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Read all text failed {ex}");

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
            Id = "d2f4b402-0a5e-49a5-bc43-78e008fcbc18",
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
            Id = "feb33e74-6b53-4821-9116-51e3cfb7b2cd",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = "OutPinText",
            DisplayName = "Text")]
        public DataPin OutPinText { get; set; }

        /// <summary>
        /// Gets or sets the friendly name
        /// </summary>
        public override string FriendlyName
        {
            get
            {
                return nameof(ReadAllTextNode);
            }
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public override string Name
        {
            get
            {
                return nameof(ReadAllTextNode);
            }
        }
    }
}
