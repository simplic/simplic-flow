using Simplic.Flow.Configuration;
using Simplic.UI.MVC;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// DataPinDefaultValueViewModel
    /// </summary>
    public class DataPinDefaultValueViewModel : ViewModelBase
    {
        #region Private Members
        private NodePinConfiguration pinConfiguration;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinConfiguration">NodePinConfiguration</param>
        /// <param name="displayName">displayName</param>
        public DataPinDefaultValueViewModel(NodePinConfiguration pinConfiguration, string displayName)
        {
            this.pinConfiguration = pinConfiguration;
            DisplayName = displayName;
        }
        #endregion

        #region Public Properties

        #region [DisplayName]
        public string DisplayName { get; private set; }
        #endregion

        #region [PinName]
        public string PinName { get { return pinConfiguration.Name; } }
        #endregion

        #region [DefaultValue]
        public string DefaultValue
        {
            get => pinConfiguration.DefaultValue?.ToString();
            set
            {
                pinConfiguration.DefaultValue = value;
                RaisePropertyChanged(nameof(DefaultValue));
            }
        } 
        #endregion

        #endregion
    }
}