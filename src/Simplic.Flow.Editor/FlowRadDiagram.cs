using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Simplic.Flow.Editor
{
    public class FlowRadDiagram : RadDiagram
    {
        public FlowRadDiagram()
        {
            AllowCopy = false;
            AllowPaste = false;

            Loaded += FlowRadDiagram_Loaded;
        }

        private void FlowRadDiagram_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (RadDiagramConnection connection in Connections)
            {
                connection.ConnectionType = Telerik.Windows.Diagrams.Core.ConnectionType.Bezier;
                var connectionViewModel = connection.DataContext as NodeConnectionViewModel;

                connection.SourceConnectorPosition = connectionViewModel.SourceConnectorViewModel.Name;
                connection.TargetConnectorPosition = connectionViewModel.TargetConnectorViewModel.Name;

            }
        }
               
        protected override Telerik.Windows.Diagrams.Core.IShape GetShapeContainerForItemOverride(object item)
        {
            if (item is ActionNodeViewModel)
            {
                var actionNodeViewModel = item as ActionNodeViewModel;

                var shape = new ActionNodeShape()
                {
                    DataContext = item
                };

                shape.CreateConnectors();

                return shape;
            }
            else if (item is EventNodeViewModel)
            {
                var eventNodeViewModel = item as EventNodeViewModel;

                var shape = new EventNodeShape
                {
                    DataContext = item
                };

                shape.CreateConnectors();

                return shape;
            }
            else
                return null;
        }
    }
}
