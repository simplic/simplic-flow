using Simplic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// Interaction logic for FlowEditorControl.xaml
    /// </summary>
    public partial class FlowEditorControl : UserControl
    {
        #region Private Members
        private BaseConnector sourceConnector;
        private WorkflowEditorViewModel diagramViewModel;
        private bool isInitialized = false; 
        #endregion

        #region Constructor
        public FlowEditorControl()
        {
            InitializeComponent();
            this.Loaded += EditorControl_Loaded;
        } 
        #endregion

        #region Private Methods

        #region [EditorControl_Loaded]
        /// <summary>
        /// EditorControl_Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!isInitialized)
            {
                throw new System.Exception("Editor is not initialized. Please use initialize method.");
            }

            this.Loaded -= EditorControl_Loaded;
        }
        #endregion

        #region [HasImplicitConversion]
        public static bool HasImplicitConversion(Type baseType, Type targetType)
        {
            return baseType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(mi => mi.Name == "op_Implicit" && mi.ReturnType == targetType)
                .Any(mi => {
                    ParameterInfo pi = mi.GetParameters().FirstOrDefault();
                    return pi != null && pi.ParameterType == baseType;
                });
        }
        #endregion

        #region [MyDiagram_ConnectionManipulationStarted]
        private void MyDiagram_ConnectionManipulationStarted(object sender, ManipulationRoutedEventArgs e)
        {
            // only accept connections from out nodes
            if (e.ManipulationStatus == ManipulationStatus.Attaching)
            {
                /* 
                 *  add source connector to the diagram's view model, so it can use the connector information
                    when linking the connectors we need this information.
                */

                var connector = e.Connector as BaseConnector;
                if (connector != null && connector.DataContext is ConnectorViewModel)
                {
                    var viewModel = connector.DataContext as ConnectorViewModel;

                    if (viewModel.CanConnect())
                    {
                        sourceConnector = connector;
                        diagramViewModel.SourceConnector = viewModel;                        

                        return;
                    }
                }    
                

                //if (e.Connector is FlowConnector)
                //{
                //    sourceConnector = e.Connector as FlowConnector;
                //    var flowConnector = sourceConnector as FlowConnector;
                //    var flowConnectorViewModel = sourceConnector.DataContext as FlowConnectorViewModel;

                //    var d = diagramViewModel.Connections.Any(x => x.SourceConnectorViewModel == sourceConnector.DataContext);
                //    if (d && !flowConnectorViewModel.IsList)
                //    {
                //        e.Handled = true;
                //        diagramViewModel.SourceConnector = null;
                //        diagramViewModel.TargetConnector = null;
                //        return;
                //    }

                //    diagramViewModel.SourceConnector = flowConnectorViewModel;
                //}
                //else if (e.Connector is DataConnector)
                //{                    
                //    var dataConnector = e.Connector as DataConnector;
                //    var dataConnectorViewModel = dataConnector.DataContext as DataConnectorViewModel;

                //    // skip if connector does not have any type assigned yet
                //    if (dataConnector.ConnectorDataType == null)
                //    {
                //        sourceConnector = null;
                //        e.Handled = true;
                //        return;
                //    }

                //    sourceConnector = dataConnector;
                //    diagramViewModel.SourceConnector = dataConnectorViewModel;
                //}
            }

            // skip            
            sourceConnector = null;
            diagramViewModel.SourceConnector = null;
            diagramViewModel.TargetConnector = null;

            e.Handled = true;
        }
        #endregion
        
        #region [MyDiagram_ConnectionManipulationCompleted]
        /// <summary>
        /// MyDiagram_ConnectionManipulationCompleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyDiagram_ConnectionManipulationCompleted(object sender, ManipulationRoutedEventArgs e)
        {
            if (sourceConnector != null && e.Connector != null && e.Connector != sourceConnector
                && e.ManipulationStatus == ManipulationStatus.Attaching)
            {
                var sourceConnectorViewModel = sourceConnector.DataContext as ConnectorViewModel;

                var targetConnector = e.Connector as BaseConnector;
                var targetConnectorViewModel = targetConnector.DataContext as ConnectorViewModel;

                if (targetConnectorViewModel.CanConnectTo(sourceConnectorViewModel))
                {
                    diagramViewModel.TargetConnector = targetConnectorViewModel;

                    return;
                }
            }

            sourceConnector = null;
            diagramViewModel.SourceConnector = null;
            diagramViewModel.TargetConnector = null;
            e.Handled = true;                        

            //if (sourceConnector == null
            //    || sourceConnector.GetType() != targetConnector.GetType()
            //    || (targetConnector as BaseConnector).ConnectorDirection == ConnectorDirection.Out
            //    || (
            //        sourceConnector is DataConnector
            //        && targetConnector is DataConnector
            //        && (// if target connector data type is object type, then allow it, otherwise check if the types match 
            //                (targetConnector as DataConnector).ConnectorDataType != typeof(object)

            //                && (sourceConnector as DataConnector).ConnectorDataType != (e.Connector as DataConnector).ConnectorDataType
            //           )
            //        ))
            //{
            //    // bypass
            //    e.Handled = true;

            //    diagramViewModel.SourceConnector = null;
            //    diagramViewModel.TargetConnector = null;
            //}
            //else
            //{
            //    /* 
            //    *   add target connector to the diagram's view model, so it can use the connector information
            //        when linking the connectors we need this information.
            //    */
            //    if (e.Connector is FlowConnector)
            //    {
            //        var flowConnector = e.Connector as FlowConnector;
            //        var flowConnectorViewModel = flowConnector.DataContext as FlowConnectorViewModel;
            //        diagramViewModel.TargetConnector = flowConnectorViewModel;
            //    }
            //    else if (e.Connector is DataConnector)
            //    {
            //        var dataConnector = e.Connector as DataConnector;
            //        var dataConnectorViewModel = dataConnector.DataContext as DataConnectorViewModel;

            //        var d = diagramViewModel.Connections.Any(x => x.TargetConnectorViewModel == dataConnector.DataContext);

            //        if (d)
            //        {
            //            e.Handled = true;
            //            diagramViewModel.SourceConnector = null;
            //            diagramViewModel.TargetConnector = null;
            //            return;
            //        }

            //        diagramViewModel.TargetConnector = dataConnectorViewModel;
            //    }
            //}

        }
        #endregion

        #endregion

        #region Public Methods

        #region [Initialize]
        /// <summary>
        /// Initializes the editor
        /// </summary>
        /// <param name="nodeDefinitions">Node definitions</param>
        /// <param name="flowConfiguration">Flow configuration</param>
        public void Initialize(IList<Definition.NodeDefinition> nodeDefinitions, Configuration.FlowConfiguration flowConfiguration)
        {
            isInitialized = true;

            toolbox.DataContext = new ToolboxViewModel(nodeDefinitions);
            diagramViewModel = new WorkflowEditorViewModel(nodeDefinitions, flowConfiguration);
            this.DataContext = diagramViewModel;

            this.MyDiagram.ConnectionManipulationStarted += MyDiagram_ConnectionManipulationStarted;
            this.MyDiagram.ConnectionManipulationCompleted += MyDiagram_ConnectionManipulationCompleted;
        }
        #endregion

        #region [GetFlowConfiguration]
        public Configuration.FlowConfiguration GetFlowConfiguration()
        {
            return diagramViewModel.GetFlowConfiguration();
        }
        #endregion

        #endregion
    }
}
