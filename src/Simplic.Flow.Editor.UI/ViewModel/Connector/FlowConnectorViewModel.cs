using Simplic.Flow.Editor.Definition;
using System;

namespace Simplic.Flow.Editor.UI
{
    public class FlowConnectorViewModel : ConnectorViewModel
    {
        private FlowPinDefinition pinDefinition;
        private bool isConnected;

        public FlowConnectorViewModel(FlowPinDefinition pinDefinition)
        {
            this.pinDefinition = pinDefinition;
        }

        public Guid Id => pinDefinition.Id;

        public override string Name => pinDefinition.Name;
        public string DisplayName => pinDefinition.DisplayName;

        public bool IsList { get { return pinDefinition.AllowMultiple; } }

        public PinDirectionDefinition PinDirection
        {
            get { return pinDefinition.PinDirection; }
            set { pinDefinition.PinDirection = value; RaisePropertyChanged(nameof(PinDirection)); }
        }

        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                RaisePropertyChanged(nameof(IsConnected));
                RaisePropertyChanged(nameof(StrokeColor));
                RaisePropertyChanged(nameof(FillColor));                
            }
        }

        public string StrokeColor { get { return Constants.FlowStrokeColor; } }

        public string FillColor
        {
            get
            {
                if (IsConnected)
                    return Constants.FlowStrokeColor;
                else
                    return "Transparent";
            }
        }

        public string HighlightStrokeColor
        {
            get
            {
                return Constants.FlowHighlightColor;
            }
        }

        public string HighlightFillColor
        {
            get
            {
                if (IsConnected)
                {
                    return Constants.FlowHighlightColor;
                }
                else
                    return "Transparent";
            }
        }
    }
}
