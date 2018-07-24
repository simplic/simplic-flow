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

            connector = e.Connection.SourceConnectorResult as FlowConnector;
            if (connector != null)
            {
                connector.Connection = e.Connection;
            }
        }

        private void MyDiagram_ConnectionManipulationStarted(object sender, ManipulationRoutedEventArgs e)
        {
            var connector = e.Connector as FlowConnector;
            if (connector != null)
            {
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
            }
            this.isManipulating = true;
        }

        private void OnConnectorActivationChanged(object sender, RadRoutedEventArgs e)
        {
            var connector = e.OriginalSource as FlowConnector;
            if (connector == null) return;
            if ((connector as IConnector).IsActive)
            {
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
            else
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
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
    }
}
