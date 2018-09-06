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
using Telerik.Windows.Controls;
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

        private BaseConnector sourceConnector;

        private ObservableCollection<Gallery> galleryItems;

        Dictionary<string, ObservableCollection<GalleryItem>> sortedGallleries = new Dictionary<string, ObservableCollection<GalleryItem>>();

        public MainWindow()
        {
            InitializeComponent();            
            
            this.MyDiagram.ConnectionManipulationStarted += MyDiagram_ConnectionManipulationStarted;            
            this.MyDiagram.ConnectionManipulationCompleted += MyDiagram_ConnectionManipulationCompleted;

            CreateTempNodes();

            // Init toolbox on the right side
            InitializeToolBox();
        }

        private void CreateTempNodes()
        {
            var vm = new WorkflowEditorViewModel();

            var inFlowPins = new List<FlowPinDefinition>();
            inFlowPins.Add(new FlowPinDefinition { Id = Guid.NewGuid(), Name = "FlowIn", PinDirection = PinDirectionDefinition.In });

            var inDataPins = new List<DataPinDefinition>();
            inDataPins.Add(new DataPinDefinition { Id = Guid.NewGuid(), Name = "EmailAdressIn", Type = typeof(string), PinDirection = PinDirectionDefinition.In });
            inDataPins.Add(new DataPinDefinition { Id = Guid.NewGuid(), Name = "SubjectIn", Type = typeof(string), PinDirection = PinDirectionDefinition.In });

            var outFlowPins = new List<FlowPinDefinition>();
            outFlowPins.Add(new FlowPinDefinition { Id = Guid.NewGuid(), Name = "FlowOut", PinDirection = PinDirectionDefinition.Out });

            var outDataPins = new List<DataPinDefinition>();
            outDataPins.Add(new DataPinDefinition { Id = Guid.NewGuid(), Name = "ResultOut", Type = typeof(bool), PinDirection = PinDirectionDefinition.Out });

            var emailNodeDefinition = new ActionNodeDefinition {
                Name = "Send Email",
                InFlowPins = inFlowPins,
                InDataPins = inDataPins,
                OutFlowPins = outFlowPins,
                OutDataPins = outDataPins
            };

            var emailNodeDefinition2 = new ActionNodeDefinition
            {
                Name = "Send Fax",
                InFlowPins = inFlowPins,
                InDataPins = inDataPins,
                OutFlowPins = outFlowPins,
                OutDataPins = outDataPins
            };

            vm.Nodes.Add(new ActionNodeViewModel(emailNodeDefinition, null) { Position = new Point(650, 320) });
            vm.Nodes.Add(new ActionNodeViewModel(emailNodeDefinition2, null) { Position = new Point(950, 320) });

            this.DataContext = vm;
        }

        private void MyDiagram_ConnectionManipulationCompleted(object sender, ManipulationRoutedEventArgs e)
        {
            // Check whether the connection was not attached
            if (e.Connector == null)
            {
                DiagramViewModel.SourceConnector = null;
                DiagramViewModel.TargetConnector = null;

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
                    || e.Connector.Name.EndsWith("Out")                    
                    || (
                        sourceConnector is DataConnector && e.Connector is DataConnector 
                        && (sourceConnector as DataConnector).ConnectorDataType 
                                != (e.Connector as DataConnector).ConnectorDataType
                        ))
                {
                    // bypass
                    e.Handled = true;

                    DiagramViewModel.SourceConnector = null;
                    DiagramViewModel.TargetConnector = null;
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
                        DiagramViewModel.TargetConnector = new FlowConnectorViewModel(new FlowPinDefinition
                        {
                            Id = Guid.NewGuid(),
                            Name = flowConnector.Name,
                            PinDirection = flowConnector.ConnectorDirection == ConnectorDirection.In ? PinDirectionDefinition.In : PinDirectionDefinition.Out
                        });
                    }
                    else if (e.Connector is DataConnector)
                    {
                        var dataConnector = e.Connector as DataConnector;
                        DiagramViewModel.TargetConnector = new DataConnectorViewModel(new DataPinDefinition
                        {
                            Id = Guid.NewGuid(),
                            Name = dataConnector.Name,
                            PinDirection = dataConnector.ConnectorDirection == ConnectorDirection.In ? PinDirectionDefinition.In : PinDirectionDefinition.Out,
                            Type = dataConnector.ConnectorDataType
                        });
                    }
                }
            }
        }

        private void MyDiagram_ConnectionManipulationStarted(object sender, ManipulationRoutedEventArgs e)
        {
            // only accept connections from out nodes
            if (e.ManipulationStatus == ManipulationStatus.Attaching
                    && e.Connector.Name.EndsWith("Out"))
            {                
                /* 
                 *  add source connector to the diagram's view model, so it can use the connector information
                    when linking the connectors we need this information.
                */

                if (e.Connector is FlowConnector)
                {
                    sourceConnector = e.Connector as FlowConnector;
                    var flowConnector = sourceConnector as FlowConnector;
                    DiagramViewModel.SourceConnector = new FlowConnectorViewModel(new FlowPinDefinition
                    {
                        Id = Guid.NewGuid(),
                        Name = flowConnector.Name,
                        PinDirection = flowConnector.ConnectorDirection == ConnectorDirection.In ? PinDirectionDefinition.In : PinDirectionDefinition.Out
                    });
                }
                else if (e.Connector is DataConnector)
                {
                    sourceConnector = e.Connector as DataConnector;
                    var dataConnector = sourceConnector as DataConnector;
                    DiagramViewModel.SourceConnector = new DataConnectorViewModel(new DataPinDefinition
                    {
                        Id = Guid.NewGuid(),
                        Name = dataConnector.Name,
                        PinDirection = dataConnector.ConnectorDirection == ConnectorDirection.In ? PinDirectionDefinition.In : PinDirectionDefinition.Out,
                        Type = dataConnector.ConnectorDataType
                    });
                }
            }
            else
            {
                sourceConnector = null;
                e.Handled = true;
            }
                
        }

        private void InitializeToolBox()
        {
            galleryItems = new ObservableCollection<Gallery>();
            this.toolbox.ItemsSource = galleryItems;

            // Workflow gallery
            var actionGallery = new Gallery();
            actionGallery.Header = "Aktion";

            // Add items to workflow gallery
            actionGallery.Items.Add(new GalleryItem("SendEmail", new RadDiagramShape()));
            actionGallery.Items.Add(new GalleryItem("Gateway", new RadDiagramShape()
            {
                Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.RectangleShape),
                Background = new SolidColorBrush(Colors.LightYellow)
            }));
            actionGallery.Items.Add(new GalleryItem("Start/Ende", new RadDiagramShape()
            {
                Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.RectangleShape),
                Background = new SolidColorBrush(Colors.LightGreen)
            }));

            galleryItems.Add(actionGallery);

            var eventGallery = new Gallery();
            eventGallery.Header = "Event";
            galleryItems.Add(eventGallery);
            //var autoCompleteNodes = new ObservableCollection<string>();

            //var galleries = new HierarchicalGalleryItemsCollection();
            //galleries.Clear();

            //var eventGallery = new Gallery() { Header = "Events" };
            //var actionGallery = new Gallery() { Header = "Actions" };

            //for (int i = 0; i < 5; i++)
            //{
            //    var galleryItem = new GalleryItem() { ItemType = "Events" };
            //    galleryItem.Header = "EVENT-" + i;
            //    galleryItem.Shape = new FunctionNode();

            //    autoCompleteNodes.Add("EVENT-" + i);
            //    eventGallery.Items.Add(galleryItem);

            //    // Gallery item has no Tag property so we will use the shapes' one. We will sort the gallery items by the this tag (hex of the glyph).
            //    //galleryItem.Shape = new RadDiagramTextShape() { Content = dict[item], Tag = categoryCode + categoryCode2 };                
            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    var galleryItem = new GalleryItem() { ItemType = "Actions" };
            //    galleryItem.Header = "ACTION-" + i;
            //    galleryItem.Shape = new FunctionNode();

            //    autoCompleteNodes.Add("ACTION-" + i);
            //    actionGallery.Items.Add(galleryItem);
            //}

            //galleries.Insert(0, eventGallery);
            //galleries.Add(actionGallery);

            //this.toolbox.ItemsSource = galleries;
            //this.autoComplete.ItemsSource = autoCompleteNodes;
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

        private WorkflowEditorViewModel DiagramViewModel
        {
            get { return this.MyDiagram.DataContext as WorkflowEditorViewModel; }
        }


    }
}
