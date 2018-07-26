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

                if (OutNodeRemoved != null)
                    runtime.EnqueueNode(OutNodeRemoved, scope);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Removing file failed {ex}");

                if (OutNodeRemoveFailed != null)
                    runtime.EnqueueNode(OutNodeRemoveFailed, scope);
            }

            return true;
        }

        public ActionNode OutNodeRemoved { get; set; }
        public ActionNode OutNodeRemoveFailed { get; set; }
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
