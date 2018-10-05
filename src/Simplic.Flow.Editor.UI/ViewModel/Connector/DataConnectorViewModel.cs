using Simplic.Flow.Editor.Definition;
using System;

namespace Simplic.Flow.Editor.UI
{

    public class DataConnectorViewModel : ConnectorViewModel
    {
        #region Private Members
        private DataPinDefinition pinDefinition;
        private bool isConnected;
        #endregion

        #region Constructor
        public DataConnectorViewModel(DataPinDefinition pinDefinition)
        {
            this.pinDefinition = pinDefinition;
        }
        #endregion

        #region Private Methods

        #region [NotifyColors]
        /// <summary>
        /// Notifies color properties so the gui stays up to date with changes
        /// </summary>
        private void NotifyColors()
        {
            RaisePropertyChanged(nameof(StrokeColor));
            RaisePropertyChanged(nameof(FillColor));
            RaisePropertyChanged(nameof(HighlightStrokeColor));
            RaisePropertyChanged(nameof(HighlightFillColor));
        }
        #endregion

        #endregion

        #region Private Properties
        
        #region [ParentViewModel]
        /// <summary>
        /// Gets the parent view model as <see cref="ActionNodeViewModel"/>
        /// </summary>
        private ActionNodeViewModel ParentViewModel
        {
            get
            {
                return this.Parent as ActionNodeViewModel;
            }
        }
        #endregion

        #endregion

        #region Public Properties

        #region [Id]
        /// <summary>
        /// Gets the id of the pin
        /// </summary>
        public Guid Id { get { return pinDefinition.Id; } }
        #endregion

        #region [Name]
        /// <summary>
        /// Gets the name of the pin
        /// </summary>
        public override string Name { get { return pinDefinition.Name; } }
        #endregion

        #region [DisplayName]
        /// <summary>
        /// Gets the display name of the pin
        /// </summary>
        public override string DisplayName { get { return pinDefinition.DisplayName; } }
        #endregion

        #region [PinDirection]
        /// <summary>
        /// Gets or sets the pin direction
        /// </summary>
        public override PinDirectionDefinition PinDirection
        {
            get { return pinDefinition.PinDirection; }
            set { pinDefinition.PinDirection = value; RaisePropertyChanged(nameof(PinDirection)); }
        }
        #endregion

        #region [DataConnectorType]
        /// <summary>
        /// Gets or sets the pin data type
        /// </summary>
        public Type DataConnectorType
        {
            get { return pinDefinition.Type; }
            set
            {
                pinDefinition.Type = value;
                RaisePropertyChanged(nameof(DataConnectorType));
                NotifyColors();
            }
        }
        #endregion

        #region [IsConnected]
        /// <summary>
        /// Gets or sets if the pin is connected
        /// </summary>
        public override bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                RaisePropertyChanged(nameof(IsConnected));
                NotifyColors();
            }
        }
        #endregion

        #region [StrokeColor]
        /// <summary>
        /// Gets the stroke color of the pin
        /// </summary>
        public string StrokeColor
        {
            get
            {
                if (pinDefinition.Type == null)
                    return "Gray";

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
        #endregion

        #region [FillColor]
        /// <summary>
        /// Gets the fill color of the pin
        /// </summary>
        public string FillColor
        {
            get
            {
                if (pinDefinition.Type == null)
                    return "Transparent";

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
        #endregion

        #region [HighlightStrokeColor]
        /// <summary>
        /// Gets the highlighted stroke color
        /// </summary>
        public string HighlightStrokeColor
        {
            get
            {
                if (pinDefinition.Type == null)
                    return "White";

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
        #endregion

        #region [HighlightFillColor]
        /// <summary>
        /// Gets the highlighted fill color
        /// </summary>
        public string HighlightFillColor
        {
            get
            {
                if (pinDefinition.Type == null)
                    return "Transparent";

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
        #endregion

        #region [IsGeneric]
        /// <summary>
        /// Gets if the pin is a generic data type ( this is used to overwrite the data type)
        /// </summary>
        public bool IsGeneric
        {
            get { return pinDefinition.IsGeneric; }
        }
        #endregion

        #region [AllowedTypes]
        /// <summary>
        /// Gets a comma seperate string of allowed types (Type.Name)
        /// </summary>
        public string AllowedTypes
        {
            get { return pinDefinition.AllowedTypes; }
        } 
        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Decides if this pin can be connected to others
        /// </summary>
        /// <returns>True if successfull</returns>
        public override bool CanConnect()
        {
            return DataConnectorType != null && PinDirection == PinDirectionDefinition.Out;
        }

        /// <summary>
        /// Decides if this pin can be connected to a target pin
        /// </summary>
        /// <param name="targetConnectorViewModel">Target pin's view model</param>
        /// <returns>True if successfull</returns>
        public override bool CanConnectTo(ConnectorViewModel targetConnectorViewModel)
        {
            if (targetConnectorViewModel is DataConnectorViewModel 
                && PinDirection == PinDirectionDefinition.In)
            {
                var target = targetConnectorViewModel as DataConnectorViewModel;

                if (DataConnectorType != null)
                {
                    if (target.DataConnectorType.IsAssignableFrom(DataConnectorType) 
                        || DataConnectorType == typeof(object))
                    {
                        return true;
                    }
                }
                else
                {                    
                    if (AllowedTypes.Contains(target.DataConnectorType.Name))
                    {
                        // call parent node to update connector types
                        ParentViewModel.UpdateDataTypes(target.DataConnectorType);

                        return true;
                    }                    
                }
            }

            return false;
        }
        #endregion

    }
}