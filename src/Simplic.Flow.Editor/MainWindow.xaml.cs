using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telerik.Windows.Controls.Diagrams.Extensions;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RadDiagramToolboxItem selectedToolBoxItem;
        private IConnector sourceConnector;
        Dictionary<string, ObservableCollection<GalleryItem>> sortedGallleries = new Dictionary<string, ObservableCollection<GalleryItem>>();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            
            this.MyDiagram.ConnectionManipulationStarted += MyDiagram_ConnectionManipulationStarted;            
            this.MyDiagram.ConnectionManipulationCompleted += MyDiagram_ConnectionManipulationCompleted;

            CreateTempNodes();
        }

        private void CreateTempNodes()
        {
            var flowConnectors = new List<FlowConnector>();
            flowConnectors.Add(new FlowConnector("FlowIn", "Flow In", ConnectorDirection.In));
            flowConnectors.Add(new FlowConnector("FlowOut", "Flow Out", ConnectorDirection.Out));

            var dataConnectors = new List<DataConnector>();
            dataConnectors.Add(new DataConnector("DataIn", "Variable In", ConnectorDirection.In, typeof(string)));
            dataConnectors.Add(new DataConnector("DataOut", "Variable Out", ConnectorDirection.Out, typeof(string)));            

            var an = new ActionNode("Temp Action Node 1", flowConnectors, dataConnectors);
            an.Position = new Point(650, 320);

            var flowConnectors2 = new List<FlowConnector>();
            flowConnectors2.Add(new FlowConnector("FlowIn", "Flow In", ConnectorDirection.In));
            flowConnectors2.Add(new FlowConnector("FlowOut", "Flow Out", ConnectorDirection.Out));

            var dataConnectors2 = new List<DataConnector>();
            dataConnectors2.Add(new DataConnector("DataIn", "Variable In", ConnectorDirection.In, typeof(string)));
            dataConnectors2.Add(new DataConnector("DataOut", "Variable Out", ConnectorDirection.Out, typeof(string)));

            var ab = new EventNode("Event Node 1", flowConnectors2, dataConnectors2);
            ab.Position = new Point(950, 320);

            this.MyDiagram.Items.Add(an);
            this.MyDiagram.Items.Add(ab);
        }

        private void MyDiagram_ConnectionManipulationCompleted(object sender, ManipulationRoutedEventArgs e)
        {
            // Check whether the connection was not attached
            if (e.Connector == null)
            {
                e.Handled = true;
                return;
            }
                        
            if (e.ManipulationStatus == ManipulationStatus.Attaching)
            {
                // source connector type and target connector type should match.
                // e.Connector is the target
                if (sourceConnector == null
                    || sourceConnector.GetType() != e.Connector.GetType()
                    || e.Connector.Name.EndsWith("Out"))
                {
                    // bypass
                    e.Handled = true;
                }
            }
        }

        private void MyDiagram_ConnectionManipulationStarted(object sender, ManipulationRoutedEventArgs e)
        {
            // only accept connections from out nodes
            if (e.ManipulationStatus == ManipulationStatus.Attaching 
                    && e.Connector.Name.EndsWith("Out"))
            {
                sourceConnector = e.Connector;

            }                
            else
            {
                sourceConnector = null;
                e.Handled = true;
            }
                
        }

        private void OnConnectorActivationChanged(object sender, RadRoutedEventArgs e)
        {
            //if ((e.OriginalSource as IConnector).IsActive)
            //{
            //    if (e.OriginalSource is FlowConnector)
            //    {
            //        var connector = e.OriginalSource as FlowConnector;
            //        if (this.isManipulating && (connector.FlowConnectorDirection == FlowConnectorDirection.Out
            //            || connector.Connection != null))
            //        {
            //            Mouse.OverrideCursor = Cursors.No;
            //        }
            //        else
            //        {
            //            Mouse.OverrideCursor = Cursors.Pen;
            //        }
            //    }
            //    else if (e.OriginalSource is DataConnector)
            //    {
            //        var connector = e.OriginalSource as DataConnector;
            //        if (this.isManipulating && (connector.FlowConnectorDirection == FlowConnectorDirection.Out
            //            || connector.Connection != null))
            //        {
            //            Mouse.OverrideCursor = Cursors.No;
            //        }
            //        else
            //        {
            //            Mouse.OverrideCursor = Cursors.Pen;
            //        }
            //    }
            //}
            //else
            //{
            //    Mouse.OverrideCursor = null;
            //}
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //this.MyDiagram.ConnectionBridge = BridgeType.Bow;
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

            //this.MyDiagram.BringIntoView(this.eventShape);

            //#416177
            //            header.Background = (Brush)bc.ConvertFrom("#252e33"); 

            InitializeToolBox();
        }

        private void MyDiagram_CommandExecuted(object sender, CommandRoutedEventArgs e)
        {
            //if (e.Command.Name == "Delete Items")
            //{
            //    var compositeCommand = ((e.Command as CompositeCommand).Commands.FirstOrDefault() as CompositeCommand).Commands.Where(x => x.Name == "Remove Connection").FirstOrDefault();
            //    if (compositeCommand != null)
            //    {
                    
                    
            //    }
            //}            
        }

        private void InitializeToolBox()
        {
            var autoCompleteNodes = new ObservableCollection<string>();

            var galleries = new HierarchicalGalleryItemsCollection();
            galleries.Clear();

            var eventGallery = new Gallery() { Header = "Events" };
            var actionGallery = new Gallery() { Header = "Actions" };            

            for (int i = 0; i < 5; i++)
            {
                var galleryItem = new GalleryItem() { ItemType = "Events" };
                galleryItem.Header = "EVENT-" + i;
                galleryItem.Shape = new FunctionNode();

                autoCompleteNodes.Add("EVENT-" + i);
                eventGallery.Items.Add(galleryItem);

                // Gallery item has no Tag property so we will use the shapes' one. We will sort the gallery items by the this tag (hex of the glyph).
                //galleryItem.Shape = new RadDiagramTextShape() { Content = dict[item], Tag = categoryCode + categoryCode2 };                
            }

            for (int i = 0; i < 5; i++)
            {
                var galleryItem = new GalleryItem() { ItemType = "Actions" };
                galleryItem.Header = "ACTION-" + i;
                galleryItem.Shape = new FunctionNode();

                autoCompleteNodes.Add("ACTION-" + i);
                actionGallery.Items.Add(galleryItem);                                
            }

            galleries.Insert(0, eventGallery);
            galleries.Add(actionGallery);

            this.toolbox.ItemsSource = galleries;
            this.autoComplete.ItemsSource = autoCompleteNodes;
        }

        private void BringToolBoxItemIntoView(Gallery gallery, string searchHeader)
        {
            foreach (GalleryItem galleryItem in gallery.Items)
            {
                if (galleryItem.Header == searchHeader)
                {
                    RadDiagramToolboxGroup groupContainer = this.toolbox.ItemContainerGenerator.ContainerFromItem(gallery) as RadDiagramToolboxGroup;
                    if (groupContainer != null)
                    {
                        RadDiagramToolboxItem tbItemContainer = groupContainer.ItemContainerGenerator.ContainerFromItem(galleryItem) as RadDiagramToolboxItem;
                        if (tbItemContainer != null)
                        {
                            if (this.selectedToolBoxItem != null)
                            {
                                this.selectedToolBoxItem.ClearValue(RadDiagramToolboxItem.BackgroundProperty);
                            }
                            this.selectedToolBoxItem = tbItemContainer;                            
                            tbItemContainer.BringIntoView();
                            return;
                        }
                    }
                }
            }
        }


    }
}
