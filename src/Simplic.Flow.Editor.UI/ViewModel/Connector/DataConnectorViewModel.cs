using Simplic.Flow.Editor.Definition;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Simplic.Flow.Editor.UI
{

    public class DataConnectorViewModel : ConnectorViewModel
    {
        #region Private Members
        private DataPinDefinition pinDefinition;
        private bool isConnected;
        #endregion

        public DataConnectorViewModel(DataPinDefinition pinDefinition)
        {
            this.pinDefinition = pinDefinition;
        }

        public Guid Id => pinDefinition.Id;

        public override string Name => pinDefinition.Name;
        public string DisplayName => pinDefinition.DisplayName;

        public Type Type => pinDefinition.Type;
        public PinDirectionDefinition PinDirection
        {
            get { return pinDefinition.PinDirection; }
            set { pinDefinition.PinDirection = value; RaisePropertyChanged(nameof(PinDirection)); }
        }

        public Type DataConnectorType
        {
            get { return pinDefinition.Type; }
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
                RaisePropertyChanged(nameof(HighlightStrokeColor));
                RaisePropertyChanged(nameof(HighlightFillColor));
            }
        }

        public string StrokeColor
        {
            get
            {
                if (pinDefinition.Type.IsPrimitive)
                    return Constants.StrokeColors[pinDefinition.Type.Name];
                else if (pinDefinition.Type.IsValueType)
                    return Constants.StrokeColors["ValueType"];
                else if (pinDefinition.Type.IsClass)
                    return Constants.StrokeColors["ClassType"];
                else
                    return "Gray";
            }
        }

        public string FillColor
        {
            get
            {
                if (IsConnected)
                {
                    if (pinDefinition.Type.IsPrimitive)
                        return Constants.StrokeColors[pinDefinition.Type.Name];
                    else if (pinDefinition.Type.IsValueType)
                        return Constants.StrokeColors["ValueType"];
                    else if (pinDefinition.Type.IsClass)
                        return Constants.StrokeColors["ClassType"];
                    else
                        return "Gray";
                }
                else
                    return "Transparent";                
            }
        }

        public string HighlightStrokeColor
        {
            get
            {
                if (pinDefinition.Type.IsPrimitive)
                    return Constants.HighlightColors[pinDefinition.Type.Name];
                else if (pinDefinition.Type.IsValueType)
                    return Constants.HighlightColors["ValueType"];
                else if (pinDefinition.Type.IsClass)
                    return Constants.HighlightColors["ClassType"];
                else
                    return "White";
            }
        }

        public string HighlightFillColor
        {
            get
            {
                if (IsConnected)
                {
                    if (pinDefinition.Type.IsPrimitive)
                        return Constants.HighlightColors[pinDefinition.Type.Name];
                    else if (pinDefinition.Type.IsValueType)
                        return Constants.HighlightColors["ValueType"];
                    else if (pinDefinition.Type.IsClass)
                        return Constants.HighlightColors["ClassType"];
                    else
                        return "White";
                }
                else
                    return "Transparent";
            }
        }
    }
}