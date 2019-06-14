using Simplic.Flow.Editor.Definition;

namespace Simplic.Flow.Editor.UI
{
    public abstract class ConnectorViewModel : Simplic.UI.MVC.ViewModelBase
    {        
        public abstract string Name { get; }
        public abstract string DisplayName { get; }
        public abstract PinDirectionDefinition PinDirection { get; set; }
        public abstract bool CanConnect();
        public abstract bool CanConnectTo(ConnectorViewModel targetConnectorViewModel);
        public abstract bool IsConnected { get; set; }
    }
}
