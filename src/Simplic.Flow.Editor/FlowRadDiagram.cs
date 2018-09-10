using Telerik.Windows.Controls;

namespace Simplic.Flow.Editor
{
    public class FlowRadDiagram : RadDiagram
    {
        //protected override bool IsItemItsOwnShapeContainerOverride(object item)
        //{
        //    return item is ActionNodeViewModel || item is EventNodeViewModel;
        //}

        protected override Telerik.Windows.Diagrams.Core.IShape GetShapeContainerForItemOverride(object item)
        {
            if (item is ActionNodeViewModel)
            {
                var actionNodeViewModel = item as ActionNodeViewModel;

                var shape = new ActionNodeShape()
                {                    
                    HeaderText = actionNodeViewModel.DisplayName
                };

                shape.DataContext = item;
                shape.CreateConnectors();

                return shape;
            }
            else if (item is EventNodeViewModel)
            {
                var eventNodeViewModel = item as EventNodeViewModel;
                    
                var shape = new EventNodeShape
                {
                    HeaderText = eventNodeViewModel.DisplayName
                };

                shape.DataContext = item;
                shape.CreateConnectors();

                return shape;
            }
            else
                return null;
        }
    }
}
