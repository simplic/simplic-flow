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
            if (item is ActionNode)
                return ActionNodeStyle;
            else if (item is EventNode)
                return EventNodeStyle;
            //else if (item is EllipseNode)
            //{
            //    switch (((EllipseNode)item).Type)
            //    {
            //        case EllipseNodeType.Start:
            //            return StartNodeStyle;
            //        case EllipseNodeType.End:
            //            return EndNodeStyle;
            //        default:
            //            return base.SelectStyle(item, container);
            //    }
            //}
            else return base.SelectStyle(item, container);
        }
    }
}
