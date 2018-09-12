using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        private BaseConnector sourceConnector;                
        private WorkflowEditorViewModel diagramViewModel;        

        public MainWindow()
        {
            InitializeComponent();

            #region Load Available Node Definitions
            var asm = Assembly.LoadFrom(@"G:\SimplicRepo\simplic-flow\src\Simplic.Flow.Node\bin\Debug\Simplic.Flow.Node.dll");

            var defService = new Simplic.Flow.Editor.Definition.Service.DefinitionService();
            var nodeDefinitions = defService.Create(new List<Assembly>() { asm });
            #endregion

            var config = LoadTempConfiguration();

            toolbox.DataContext = new ToolboxViewModel(nodeDefinitions);
            diagramViewModel = new WorkflowEditorViewModel(nodeDefinitions, config);
            this.DataContext = diagramViewModel;

            this.MyDiagram.ConnectionManipulationStarted += MyDiagram_ConnectionManipulationStarted;            
            this.MyDiagram.ConnectionManipulationCompleted += MyDiagram_ConnectionManipulationCompleted;         
        }

        private Configuration.FlowConfiguration LoadTempConfiguration()
        {
            //var jsonText = File.ReadAllText(@"C:\Users\guenay\Desktop\HBTS.json");

            //return Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration.FlowConfiguration>(jsonText);            
            return null;
        }

        private void MyDiagram_ConnectionManipulationStarted(object sender, ManipulationRoutedEventArgs e)
        {
            // only accept connections from out nodes
            if (e.ManipulationStatus == ManipulationStatus.Attaching
                    && (e.Connector as BaseConnector).ConnectorDirection == ConnectorDirection.Out)
            {                
                /* 
                 *  add source connector to the diagram's view model, so it can use the connector information
                    when linking the connectors we need this information.
                */

                if (e.Connector is FlowConnector)
                {
                    sourceConnector = e.Connector as FlowConnector;
                    var flowConnector = sourceConnector as FlowConnector;
                    var flowConnectorViewModel = sourceConnector.DataContext as FlowConnectorViewModel;
                   
                    var d = diagramViewModel.Connections.Any(x => x.SourceConnectorViewModel == sourceConnector.DataContext);
                    if (d && !flowConnectorViewModel.IsList)
                    {
                        e.Handled = true;
                        diagramViewModel.SourceConnector = null;
                        diagramViewModel.TargetConnector = null;
                        return;
                    }

                    diagramViewModel.SourceConnector = flowConnectorViewModel;
                }
                else if (e.Connector is DataConnector)
                {
                    sourceConnector = e.Connector as DataConnector;
                    var dataConnector = sourceConnector as DataConnector;
                    var dataConnectorViewModel = sourceConnector.DataContext as DataConnectorViewModel;
                    diagramViewModel.SourceConnector = dataConnectorViewModel;
                }
            }
            else
            {
                sourceConnector = null;
                e.Handled = true;
            }                
        }

        private void MyDiagram_ConnectionManipulationCompleted(object sender, ManipulationRoutedEventArgs e)
        {
            // Check whether the connection was not attached
            if (e.Connector == null)
            {
                diagramViewModel.SourceConnector = null;
                diagramViewModel.TargetConnector = null;

                e.Handled = true;
                return;
            }

            if (e.ManipulationStatus == ManipulationStatus.Attaching)
            {
                /*
                    ignore any connection attempt on wrong connection and data types.
                    e.Connector is the target
                 */
                if (sourceConnector == null
                    || sourceConnector.GetType() != e.Connector.GetType()
                    || (e.Connector as BaseConnector).ConnectorDirection == ConnectorDirection.Out
                    || (
                        sourceConnector is DataConnector && e.Connector is DataConnector
                        && (sourceConnector as DataConnector).ConnectorDataType
                                != (e.Connector as DataConnector).ConnectorDataType
                        ))
                {
                    // bypass
                    e.Handled = true;

                    diagramViewModel.SourceConnector = null;
                    diagramViewModel.TargetConnector = null;
                }
                else
                {
                    /* 
                    *   add target connector to the diagram's view model, so it can use the connector information
                        when linking the connectors we need this information.
                    */
                    if (e.Connector is FlowConnector)
                    {
                        var flowConnector = e.Connector as FlowConnector;
                        var flowConnectorViewModel = flowConnector.DataContext as FlowConnectorViewModel;
                        diagramViewModel.TargetConnector = flowConnectorViewModel;
                    }
                    else if (e.Connector is DataConnector)
                    {
                        var dataConnector = e.Connector as DataConnector;
                        var dataConnectorViewModel = dataConnector.DataContext as DataConnectorViewModel;

                        var d = diagramViewModel.Connections.Any(x => x.TargetConnectorViewModel == dataConnector.DataContext);

                        if (d)
                        {
                            e.Handled = true;
                            diagramViewModel.SourceConnector = null;
                            diagramViewModel.TargetConnector = null;
                            return;
                        }

                        diagramViewModel.TargetConnector = dataConnectorViewModel;
                    }
                }
            }
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            var json = diagramViewModel.Serialize();
        }
    }
}
