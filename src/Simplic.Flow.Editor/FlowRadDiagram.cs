using Telerik.Windows.Controls;

namespace Simplic.Flow.Editor
{
    public class FlowRadDiagram : RadDiagram
    {       
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
