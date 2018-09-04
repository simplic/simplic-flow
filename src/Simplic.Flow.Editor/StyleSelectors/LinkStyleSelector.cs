using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Simplic.Flow.Editor
{
    public class LinkStyleSelector : StyleSelector
    {
        public Style FlowLinkStyle { get; set; }
        public Style StringDataTypeLinkStyle { get; set; }
        public Style IntDataTypeLinkStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var link = item as RadDiagramConnection;
            if (link == null || link.SourceConnectorResult == null)
                return base.SelectStyle(item, container);

            if (link.SourceConnectorResult is FlowConnector)
                return FlowLinkStyle;
            else if (link.SourceConnectorResult is DataConnector)
            {
                var sourceDataType = link.SourceConnectorResult as DataConnector;

                if (sourceDataType.ConnectorDataType == typeof(string))
                    return StringDataTypeLinkStyle;
                else if (sourceDataType.ConnectorDataType == typeof(int))
                    return IntDataTypeLinkStyle;            
            }
            
            return base.SelectStyle(item, container);

            //else switch (link.Type)
            //    {
            //        case LinkType.RightToLeft:
            //            return LeftCapLinkStyle;
            //        case LinkType.LeftToRight:
            //            return RightCapLinkStyle;
            //        case LinkType.Normal:
            //            return NormalLinkStyle;
            //        default:
            //            return base.SelectStyle(item, container);
            //    }
        }
    }
}
