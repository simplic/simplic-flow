using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Simplic.Flow.Editor
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var list = new List<NodeDefinition>();

            #region ConsoleWriteLineNode
            var flowInPins = new List<FlowPinDefinition>();
            flowInPins.Add(new FlowPinDefinition {
                Id = Guid.Parse("884aadb6-a3f7-4555-83c7-27d9de31c855"),
                Name = "FlowIn",    
                DisplayName = "In",
                PinDirection = PinDirectionDefinition.In
            });

            var flowOutPins = new List<FlowPinDefinition>();
            flowOutPins.Add(new FlowPinDefinition
            {
                Id = Guid.Parse("880d6cf0-8d2a-4974-85b2-e18f9d463c40"),
                Name = "OutNode",
                DisplayName = "Out",
                PinDirection = PinDirectionDefinition.Out
            });

            var dataInPins = new List<DataPinDefinition>();
            dataInPins.Add(new DataPinDefinition
            {
                Id = Guid.Parse("e1515d02-9fc9-473d-a3a8-a320ef005cf6"),
                Name = "InPinToPrint",
                DisplayName = "ToPrint",
                Type = typeof(string),
                PinDirection = PinDirectionDefinition.In
            });

            var consoleWriteLineNode = new ActionNodeDefinition
            {
                Name = "ConsoleWriteLineNode",
                DisplayName = "Console Write Line",
                InFlowPins = flowInPins,
                OutFlowPins = flowOutPins,
                InDataPins = dataInPins,
                OutDataPins = new List<DataPinDefinition>()
            };

            list.Add(consoleWriteLineNode);
            #endregion

            #region DeleteFileNode
            var flowInPins1 = new List<FlowPinDefinition>();
            flowInPins1.Add(new FlowPinDefinition
            {
                Id = Guid.Parse("da345491-f72a-40dd-a628-e05190f6702c"),
                Name = "FlowIn",
                DisplayName = "In",
                PinDirection = PinDirectionDefinition.In
            });

            var flowOutPins1 = new List<FlowPinDefinition>();
            flowOutPins1.Add(new FlowPinDefinition
            {
                Id = Guid.Parse("880d6cf0-8d2a-4974-85b2-e18f9d463c40"),
                Name = "OutNode",
                DisplayName = "Success",
                PinDirection = PinDirectionDefinition.Out
            });
            flowOutPins1.Add(new FlowPinDefinition
            {
                Id = Guid.Parse("dae8dc3c-52b1-4cd4-8b2d-9167259c142c"),
                Name = "OutNodeFailed",
                DisplayName = "Failed",
                PinDirection = PinDirectionDefinition.Out
            });
            

            var dataInPins1 = new List<DataPinDefinition>();
            dataInPins1.Add(new DataPinDefinition
            {
                Id = Guid.Parse("701a7e15-9ed0-4fe2-a641-031c461b1aaf"),
                Name = "InPinFilePath",
                DisplayName = "File Path",
                Type = typeof(string),
                PinDirection = PinDirectionDefinition.In
            });

            var deleteFileNode = new ActionNodeDefinition
            {
                Name = "DeleteFileNode",
                DisplayName = "Delete File",
                InFlowPins = flowInPins1,
                OutFlowPins = flowOutPins1,
                InDataPins = dataInPins1,
                OutDataPins = new List<DataPinDefinition>()
            };

            list.Add(deleteFileNode);
            #endregion

            
            string output = JsonConvert.SerializeObject(list);
        }
    }
}
