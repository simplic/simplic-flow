using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
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
        private Dictionary<string, ObservableCollection<GalleryItem>> sortedGallleries = new Dictionary<string, ObservableCollection<GalleryItem>>();
        private WorkflowEditorViewModel diagramViewModel;

        public MainWindow()
        {
            InitializeComponent();

            diagramViewModel = new WorkflowEditorViewModel();
            this.DataContext = diagramViewModel;

            this.MyDiagram.ConnectionManipulationStarted += MyDiagram_ConnectionManipulationStarted;            
            this.MyDiagram.ConnectionManipulationCompleted += MyDiagram_ConnectionManipulationCompleted;

            // Init toolbox on the right side
            LoadFlowNodeDefinitions();
            InitializeToolBox();            
        }

        private void LoadFlowNodeDefinitions()
        {
            var asm = Assembly.LoadFrom(@"C:\dev\simplic-flow\src\Simplic.Flow.Node\bin\Debug\Simplic.Flow.Node.dll");

            var defService = new Simplic.Flow.Editor.Definition.Service.DefinitionService();
            diagramViewModel.NodeDefinitions = defService.Create(new List<Assembly>() { asm });
        }

        private void CreateTempNodes()
        {
            //var vm = new WorkflowEditorViewModel();

            //for (int i = 0; i < 10; i++)
            //{
            //    var inFlowPins = new List<FlowPinDefinition>();
            //    inFlowPins.Add(new FlowPinDefinition { Id = Guid.NewGuid(), Name = "FlowIn", PinDirection = PinDirectionDefinition.In });

            //    var inDataPins = new List<DataPinDefinition>();
            //    inDataPins.Add(new DataPinDefinition { Id = Guid.NewGuid(), Name = "EmailAdressIn", Type = typeof(string), PinDirection = PinDirectionDefinition.In });
            //    inDataPins.Add(new DataPinDefinition { Id = Guid.NewGuid(), Name = "SubjectIn", Type = typeof(string), PinDirection = PinDirectionDefinition.In });
            //    inDataPins.Add(new DataPinDefinition { Id = Guid.NewGuid(), Name = "DoSend", Type = typeof(bool), PinDirection = PinDirectionDefinition.In });

            //    var outFlowPins = new List<FlowPinDefinition>();
            //    outFlowPins.Add(new FlowPinDefinition { Id = Guid.NewGuid(), Name = "FlowOut", PinDirection = PinDirectionDefinition.Out });

            //    var outDataPins = new List<DataPinDefinition>();
            //    outDataPins.Add(new DataPinDefinition { Id = Guid.NewGuid(), Name = "ResultOut", Type = typeof(bool), PinDirection = PinDirectionDefinition.Out });

            //    outDataPins.Add(new DataPinDefinition { Id = Guid.NewGuid(), Name = "ResultStrOut", Type = typeof(string), PinDirection = PinDirectionDefinition.Out });


            //    var emailNodeDefinition = new ActionNodeDefinition
            //    {
            //        Name = "Send Email",
            //        InFlowPins = inFlowPins,
            //        InDataPins = inDataPins,
            //        OutFlowPins = outFlowPins,
            //        OutDataPins = outDataPins
            //    };

            //    var emailNodeDefinition2 = new EventNodeDefinition
            //    {
            //        Name = "Send Fax",
            //        InFlowPins = inFlowPins,
            //        InDataPins = inDataPins,
            //        OutFlowPins = outFlowPins,
            //        OutDataPins = outDataPins
            //    };

            //    vm.Nodes.Add(new ActionNodeViewModel(emailNodeDefinition, null) { Position = new Point(650, 320) });
            //    vm.Nodes.Add(new EventNodeViewModel(emailNodeDefinition2, null) { Position = new Point(950, 320) });
            //}

            //this.DataContext = vm;
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

        private void InitializeToolBox()
        {
            galleryItems = new ObservableCollection<Gallery>();

            var actionGallery = new Gallery();
            actionGallery.Header = "Aktion";

            var eventGallery = new Gallery();
            eventGallery.Header = "Event";

            foreach (var item in diagramViewModel.NodeDefinitions)
            {
                var galleryItem = new GalleryItem(item.Name, new ActionNodeShape {
                    Name = item.Name,
                    HeaderText = item.Name
                });

                actionGallery.Items.Add(galleryItem);
            }

            galleryItems.Add(actionGallery);
            galleryItems.Add(eventGallery);
            this.toolbox.ItemsSource = galleryItems;


            //// Add items to workflow gallery
            //actionGallery.Items.Add(new GalleryItem("SendEmail", new ActionNodeShape()));
            //actionGallery.Items.Add(new GalleryItem("Gateway", new RadDiagramShape()
            //{
            //    Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.RectangleShape),
            //    Background = new SolidColorBrush(Colors.LightYellow)
            //}));
            //actionGallery.Items.Add(new GalleryItem("Start/Ende", new RadDiagramShape()
            //{
            //    Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.RectangleShape),
            //    Background = new SolidColorBrush(Colors.LightGreen)
            //}));

            //galleryItems.Add(actionGallery);

            //var eventGallery = new Gallery();
            //eventGallery.Header = "Event";
            //galleryItems.Add(eventGallery);
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

        
    }
}
