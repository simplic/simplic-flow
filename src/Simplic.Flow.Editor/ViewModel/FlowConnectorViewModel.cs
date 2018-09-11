using Simplic.Flow.Editor.Definition;
using System;

namespace Simplic.Flow.Editor
{
    public class FlowConnectorViewModel : ConnectorViewModel
    {
        private FlowPinDefinition pinDefinition;

        public FlowConnectorViewModel(FlowPinDefinition pinDefinition)
        {
            this.pinDefinition = pinDefinition;
        }

        public Guid Id => pinDefinition.Id;

        public string Name => pinDefinition.Name;
        public string DisplayName => pinDefinition.DisplayName;

        public bool IsList { get { return pinDefinition.AllowMultiple; } }

        public PinDirectionDefinition PinDirection
        {
            get { return pinDefinition.PinDirection; }
            set { pinDefinition.PinDirection = value; RaisePropertyChanged(nameof(PinDirection)); }
        }
    }
}
