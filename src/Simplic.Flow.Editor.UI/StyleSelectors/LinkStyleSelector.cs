using System;
using System.Windows;
using System.Windows.Controls;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// LinkStyleSelector
    /// </summary>
    public class LinkStyleSelector : StyleSelector
    {
        public Style FlowLinkStyle { get; set; }
        public Style StandardDataTypeLinkStyle { get; set; }
        public Style StringDataTypeLinkStyle { get; set; }
        public Style BooleanDataTypeLinkStyle { get; set; }
        public Style IntDataTypeLinkStyle { get; set; }
        public Style FloatDataTypeLinkStyle { get; set; }
        public Style GuidDataTypeLinkStyle { get; set; }
        

        /// <summary>
        /// Selects a link style based on the link type
        /// </summary>
        /// <param name="item">NodeConnectionViewModel</param>
        /// <param name="container">DependencyObject</param>
        /// <returns>Style</returns>
        public override Style SelectStyle(object item, DependencyObject container)
        {
            return StandardDataTypeLinkStyle;

            //var link = item as NodeConnectionViewModel;
            //if (link == null || link.SourceConnectorViewModel == null)
            //    return StandardDataTypeLinkStyle;

            //if (link.SourceConnectorViewModel is FlowConnectorViewModel)
            //    return FlowLinkStyle;
            //else if (link.SourceConnectorViewModel is DataConnectorViewModel)
            //{
            //    var sourceDataType = link.SourceConnectorViewModel as DataConnectorViewModel;

            //    if (sourceDataType.Type == typeof(string))
            //        return StringDataTypeLinkStyle;
            //    else if (sourceDataType.Type == typeof(int))
            //        return IntDataTypeLinkStyle;
            //    else if (sourceDataType.Type == typeof(bool))
            //        return BooleanDataTypeLinkStyle;
            //    else if (sourceDataType.Type == typeof(Guid))
            //        return GuidDataTypeLinkStyle;
            //    else if (sourceDataType.Type == typeof(float))
            //        return FloatDataTypeLinkStyle;
            //}

            //return StandardDataTypeLinkStyle;
        }
    }
}
