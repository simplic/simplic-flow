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
        public Style BooleanDataTypeLinkStyle { get; set; }
        public Style IntDataTypeLinkStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {                        
            var link = item as NodeConnectionViewModel;
            if (link == null || link.SourceConnectorViewModel == null)
                return base.SelectStyle(item, container);

            if (link.SourceConnectorViewModel is FlowConnectorViewModel)
                return FlowLinkStyle;
            else if (link.SourceConnectorViewModel is DataConnectorViewModel)
            {
                var sourceDataType = link.SourceConnectorViewModel as DataConnectorViewModel;

                if (sourceDataType.Type == typeof(string))
                    return StringDataTypeLinkStyle;
                else if (sourceDataType.Type == typeof(int))
                    return IntDataTypeLinkStyle;
                else if (sourceDataType.Type == typeof(bool))
                    return BooleanDataTypeLinkStyle;
            }
            
            return base.SelectStyle(item, container);
        }
    }
}
