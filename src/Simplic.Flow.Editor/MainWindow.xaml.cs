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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isManipulating;

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
            this.MyDiagram.ConnectionManipulationStarted += MyDiagram_ConnectionManipulationStarted;
            this.MyDiagram.ConnectionManipulationCompleted += MyDiagram_ConnectionManipulationCompleted;
            
            EventManager.RegisterClassHandler(typeof(MainWindow),
                RadDiagramConnector.ActivationChangedEvent, new RadRoutedEventHandler(OnConnectorActivationChanged));
        }

        private void MyDiagram_ConnectionManipulationCompleted(object sender, ManipulationRoutedEventArgs e)
        {
            this.isManipulating = false;

            // Check whether the connection was not attached
            if (e.ManipulationStatus != ManipulationStatus.Attaching)
            {
                e.Handled = true;
                return;
            }

            var connector = e.Connector as FlowConnector;
            if (connector != null && e.ManipulationStatus == ManipulationStatus.Attaching)
            {
                if (connector.FlowConnectorDirection == FlowConnectorDirection.Out || connector.Connection != null)
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    connector.Connection = e.Connection;
                }
            }

            var flowConnector = e.Connection.SourceConnectorResult as FlowConnector;
            var dataConnector = e.Connection.SourceConnectorResult as DataConnector;
            if (flowConnector != null)
            {
                flowConnector.Connection = e.Connection;
            }
            else if (dataConnector != null)
            {
                dataConnector.Connection = e.Connection;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void MyDiagram_ConnectionManipulationStarted(object sender, ManipulationRoutedEventArgs e)
        {
            if (e.Connector is FlowConnector)
            {
                var connector = e.Connector as FlowConnector;

                if (e.ManipulationStatus == ManipulationStatus.Attaching)
                {
                    if (connector.Connection != null || connector.FlowConnectorDirection == FlowConnectorDirection.In)
                    {
                        e.Handled = true;
                        return;
                    }
                    else if (e.ManipulationStatus == ManipulationStatus.Detaching)
                    {
                        connector.Connection = null;
                    }
                }

                this.isManipulating = true;
            }
            else if (e.Connector is DataConnector)
            {
                var connector = e.Connector as DataConnector;

                if (e.ManipulationStatus == ManipulationStatus.Attaching)
                {
                    if (connector.Connection != null
                        || connector.FlowConnectorDirection == FlowConnectorDirection.In)
                    {
                        e.Handled = true;
                        return;
                    }
                    else if (e.ManipulationStatus == ManipulationStatus.Detaching)
                    {
                        connector.Connection = null;
                    }
                }

                this.isManipulating = true;
            }
        }

        private void OnConnectorActivationChanged(object sender, RadRoutedEventArgs e)
        {
            if ((e.OriginalSource as IConnector).IsActive)
            {
                if (e.OriginalSource is FlowConnector)
                {
                    var connector = e.OriginalSource as FlowConnector;
                    if (this.isManipulating && (connector.FlowConnectorDirection == FlowConnectorDirection.Out
                        || connector.Connection != null))
                    {
                        Mouse.OverrideCursor = Cursors.No;
                    }
                    else
                    {
                        Mouse.OverrideCursor = Cursors.Pen;
                    }
                }
                else if (e.OriginalSource is DataConnector)
                {
                    var connector = e.OriginalSource as DataConnector;
                    if (this.isManipulating && (connector.FlowConnectorDirection == FlowConnectorDirection.Out
                        || connector.Connection != null))
                    {
                        Mouse.OverrideCursor = Cursors.No;
                    }
                    else
                    {
                        Mouse.OverrideCursor = Cursors.Pen;
                    }
                }
            }
            else
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.MyDiagram.ConnectionBridge = BridgeType.Bow;
            //BrushConverter bc = new BrushConverter();

            //var flowIn = new RadDiagramConnector()
            //{
            //    Offset = new Point(0, 0.23),
            //    Name = "flowIn"                
            //};

            //var flowOut = new RadDiagramConnector()
            //{
            //    Offset = new Point(1, 0.23),
            //    Name = "flowOut"                
            //};

            //eventShape.Connectors.Add(flowOut);

            //functionShape.Connectors.Add(flowIn);
            //functionShape.Connectors.Add(flowOut);

            //eventShape.Background = (Brush)bc.ConvertFrom("#0f0f0f");
            //functionShape.Background = (Brush)bc.ConvertFrom("#0f0f0f");            

            this.MyDiagram.BringIntoView(this.eventShape);

            //#416177
            //            header.Background = (Brush)bc.ConvertFrom("#252e33"); 
        }

        private void MyDiagram_CommandExecuted(object sender, CommandRoutedEventArgs e)
        {
            if (e.Command.Name == "Delete Items")
            {
                var compositeCommand = ((e.Command as CompositeCommand).Commands.FirstOrDefault() as CompositeCommand).Commands.Where(x => x.Name == "Remove Connection").FirstOrDefault();
                if (compositeCommand != null)
                {
                    
                    
                }
            }            
        }
    }
}
