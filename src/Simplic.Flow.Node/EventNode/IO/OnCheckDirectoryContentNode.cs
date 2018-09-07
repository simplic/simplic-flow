using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Node.IO
{
    public class OnCheckDirectoryContentNode : EventNode
    {
        public OnCheckDirectoryContentNode()
        {
            OutPinDirectoryPath = CreateOutNode<string>(nameof(OutPinDirectoryPath), Guid.Parse("be1a31c7-4f82-417b-b199-221af87dac68"));
        }

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var args = runtime.FlowEventArgs as OnCheckDirectoryContentEventArgs;

            if (args == null)
            {
                Console.WriteLine($"Arguments not found in {nameof(OnCheckDirectoryContentNode)}");
                return false;
            }
            if (string.IsNullOrWhiteSpace(args.DirectoryPath))
            {
                Console.WriteLine($"Path null or mpety in {nameof(OnCheckDirectoryContentNode)}");
                return false;
            }

            scope.SetValue(OutPinDirectoryPath, args.DirectoryPath);

            if (!Directory.Exists(args.DirectoryPath))
            {
                Console.WriteLine($"Directory not found `{args.DirectoryPath}` {nameof(OnCheckDirectoryContentNode)}");
                return false;
            }

            if (OutPinDirectoryPath != null && Directory.GetFiles(args.DirectoryPath).Any())
            {
                runtime.EnqueueNode(OutNode, scope);
            }

            return true;
        }


        public ActionNode OutNode { get; set; }
        public DataPin OutPinDirectoryPath { get; set; }

        public override string EventName
        {
            get
            {
                return nameof(OnCheckDirectoryContentNode);
            }
        }

        public override string FriendlyName
        {
            get
            {
                return nameof(OnCheckDirectoryContentNode);
            }
        }

        public override string Name
        {
            get
            {
                return nameof(OnCheckDirectoryContentNode);
            }
        }
    }
}
