using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Controls.SyntaxEditor.Taggers;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// Interaction logic for FlowEditorControl.xaml.
    /// </summary>
    public partial class FlowEditorControl : UserControl
    {
        private BaseConnector sourceConnector;
        private WorkflowEditorViewModel diagramViewModel;
        private bool isInitialized = false;

        /// <summary>
        /// Instantiates the flow editor control.
        /// </summary>
        public FlowEditorControl()
        {
            InitializeComponent();
            this.Loaded += EditorControl_Loaded;

            var foldingTagger = new BracketFoldingTagger(this.syntaxEditor);
            this.syntaxEditor.TaggersRegistry.RegisterTagger(foldingTagger);

            var tagger = new JavaScriptTagger(syntaxEditor);
            this.syntaxEditor.TaggersRegistry.RegisterTagger(tagger);
        }

        /// <summary>
        /// Checks if editor is initialized. Throws exception if editor is not initialized.
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

        /// <summary>
        /// When linking nodes, adds source connector to the diagram's view model for information access.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyDiagram_ConnectionManipulationStarted(object sender, ManipulationRoutedEventArgs e)
        {
            if (e.ManipulationStatus == ManipulationStatus.Attaching)
            {
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
                    else
                        viewModel.IsConnected = false;
                }
            }

            sourceConnector = null;
            diagramViewModel.SourceConnector = null;
            diagramViewModel.TargetConnector = null;

            e.Handled = true;
        }

        /// <summary>
        /// When linking nodes, updates source's and target's data contexts.
        /// If linking is possible, updates target connector of diagram view model with target's view model.
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

            (sourceConnector.DataContext as ConnectorViewModel).IsConnected = false;
            sourceConnector = null;
            diagramViewModel.SourceConnector = null;
            diagramViewModel.TargetConnector = null;
            e.Handled = true;
        }

        /// <summary>
        /// Initializes the editor.
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

        /// <summary>
        /// Check for implicit conversion.
        /// </summary>
        /// <param name="baseType">Base type</param>
        /// <param name="targetType">Target type</param>
        /// <returns>True if it has an implicit conversion</returns>
        public static bool HasImplicitConversion(Type baseType, Type targetType)
        {
            return baseType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(mi => mi.Name == "op_Implicit" && mi.ReturnType == targetType)
                .Any(mi =>
                {
                    ParameterInfo pi = mi.GetParameters().FirstOrDefault();
                    return pi != null && pi.ParameterType == baseType;
                });
        }

        /// <summary>
        /// Gets the flow configuration of the diagram.
        /// </summary>
        /// <returns>Flow configuration</returns>
        public Configuration.FlowConfiguration GetFlowConfiguration()
        {
            return diagramViewModel.GetFlowConfiguration();
        }
    }
}
