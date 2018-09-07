using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Node.IO
{
    public class GetDirectoryContentNode : ActionNode
    {
        public GetDirectoryContentNode()
        {
            OutPinFilePath = CreateOutNode<string>(nameof(OutPinFilePath), Guid.Parse("0b87f0d0-ab56-460d-b4c2-8d1460921fd6"));
            InPinDirectoryPath = CreateInNode<string>(nameof(InPinDirectoryPath), Guid.Parse("fc1eb96c-e17d-4d0e-868c-5d74850fbfe1"));
        }

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var path = scope.GetValue<string>(InPinDirectoryPath);
            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    var childScope = scope.CreateChild();
                    childScope.SetValue(OutPinFilePath, file);

                    if (OutNodeEachFile != null)
                        runtime.EnqueueNode(OutNodeEachFile, childScope);
                }

                if (OutNodeCompleted != null)
                    runtime.EnqueueNode(OutNodeCompleted, scope);

                return true;
            }
            else
            {
                Console.WriteLine($"Could not find direcotry {path}");
                return false;
            }
        }

        public ActionNode OutNodeEachFile { get; set; }
        public ActionNode OutNodeCompleted { get; set; }
        public DataPin OutPinFilePath { get; set; }
        public DataPin InPinDirectoryPath { get; set; }

        public override string FriendlyName
        {
            get
            {
                return nameof(GetDirectoryContentNode);
            }
        }

        public override string Name
        {
            get
            {
                return nameof(GetDirectoryContentNode);
            }
        }
    }
}
