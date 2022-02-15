using System.Windows;
using System.Windows.Controls;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// Control for search box.
    /// </summary>
    public class SearchControl : Control
    {
        /// <summary>
        /// DependencyProperty for SearchTerm string.
        /// </summary>
        public static readonly DependencyProperty SearchTermProperty
            = DependencyProperty.Register("SearchTerm", typeof(string), typeof(SearchControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// DependencyProperty for Watermark string.
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty
            = DependencyProperty.Register("Watermark", typeof(string), typeof(SearchControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// DependencyProperty for MatchCase string.
        /// </summary>
        public static readonly DependencyProperty MatchCaseProperty
            = DependencyProperty.Register("MatchCase", typeof(string), typeof(SearchControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// DependencyProperty for MatchWholeWord string.
        /// </summary>
        public static readonly DependencyProperty MatchWholeWordProperty
            = DependencyProperty.Register("MatchWholeWord", typeof(string), typeof(SearchControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the search term.
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// Gets or sets the watermark.
        /// </summary>
        public string Watermark { get; set; }

        /// <summary>
        /// Gets or sets if case should be matched.
        /// </summary>
        public string MatchCase { get; set; }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public string MatchWholeWord { get; set; }

        static SearchControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchControl), new FrameworkPropertyMetadata(typeof(SearchControl)));
        }
    }
}
