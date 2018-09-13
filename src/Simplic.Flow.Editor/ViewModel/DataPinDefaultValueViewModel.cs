using Simplic.Flow.Configuration;
using Simplic.UI.MVC;

namespace Simplic.Flow.Editor
{
    public class DataPinDefaultValueViewModel : ViewModelBase
    {
        private string pinName;
        private object defaultValue;
        public DataPinDefaultValueViewModel(NodePinConfiguration pinConfiguration)
        {
            PinName = pinConfiguration.Name;
            DefaultValue = pinConfiguration.DefaultValue;
        }        
        public string PinName { get => pinName; set => pinName = value; }
        public object DefaultValue { get => defaultValue; set => defaultValue = value; }
    }
}