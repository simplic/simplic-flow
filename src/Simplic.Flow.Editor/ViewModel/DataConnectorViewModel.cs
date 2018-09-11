using Simplic.Flow.Editor.Definition;
using System;

namespace Simplic.Flow.Editor
{
    public class DataConnectorViewModel : ConnectorViewModel
    {
        private DataPinDefinition pinDefinition;

        public DataConnectorViewModel(DataPinDefinition pinDefinition)
        {
            this.pinDefinition = pinDefinition;            
        }

        public Guid Id => pinDefinition.Id;

        public string Name => pinDefinition.Name;
        public string DisplayName => pinDefinition.DisplayName;

        public Type Type => pinDefinition.Type;
        public PinDirectionDefinition PinDirection
        {
            get { return pinDefinition.PinDirection; }
            set { pinDefinition.PinDirection = value; RaisePropertyChanged(nameof(PinDirection)); }
        }
    }
}