using Simplic.Flow.Event;

namespace Simplic.Flow.Node.IO
{
    public class OnCheckDirectoryContentEventArgs : FlowEventArgs
    {
        public string DirectoryPath { get; set; }
    }
}
