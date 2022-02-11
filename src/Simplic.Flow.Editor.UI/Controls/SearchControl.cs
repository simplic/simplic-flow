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

        public string SearchTerm
        {
            get { return (string)GetValue(SearchTermProperty); }
            set { SetValue(SearchTermProperty, value); }
        }

        static SearchControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchControl), new FrameworkPropertyMetadata(typeof(SearchControl)));
        }
    }
}
