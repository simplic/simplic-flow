using System.Windows;
using System.Windows.Controls;

namespace Simplic.Flow.Editor
{
    public class NodeStyleSelector : StyleSelector
    {
        public Style ActionNodeStyle { get; set; }
        public Style EventNodeStyle { get; set; }
        public Style IfNodeStyle { get; set; }
        public Style ForEachNodeStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {            
            if (item is ActionNodeViewModel)
                return ActionNodeStyle;
            else if (item is EventNodeViewModel)
                return EventNodeStyle;            
            else
                return base.SelectStyle(item, container);
        }
    }
}
