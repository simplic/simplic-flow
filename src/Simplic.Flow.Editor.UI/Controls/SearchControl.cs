using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Simplic.Flow.Editor.UI
{
    public class SearchControl : Control
    {
        public static readonly DependencyProperty SearchTermProperty = DependencyProperty.Register("SearchTerm",
                                                                                     typeof(string),
                                                                                     typeof(SearchControl),
                                                                                     new PropertyMetadata(null));


        public string SearchTerm { get; set; }

        static SearchControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchControl), new FrameworkPropertyMetadata(typeof(SearchControl)));
        }
    }
}
