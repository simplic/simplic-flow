using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Node.IO
{
    public class DeleteFileNode : ActionNode
    {
        public DeleteFileNode()
        {
            InPinFilePath = CreateInNode<string>(nameof(InPinFilePath), Guid.Parse("701a7e15-9ed0-4fe2-a641-031c461b1aaf"));
        }

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            try
            {
                File.Delete(scope.GetValue<string>(InPinFilePath));

                if (OutNode != null)
                    runtime.EnqueueNode(OutNode, scope);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Removing file failed {ex}");

                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }

            return true;
        }

        public ActionNode OutNode { get; set; }
        public ActionNode OutNodeFailed { get; set; }
        public DataPin InPinFilePath { get; set; }

        public override string FriendlyName
        {
            get
            {
                return nameof(DeleteFileNode);
            }
        }

        public override string Name
        {
            get
            {
                return nameof(DeleteFileNode);
            }
        }
    }
}
