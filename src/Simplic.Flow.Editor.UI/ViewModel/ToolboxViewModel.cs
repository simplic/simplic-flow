using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls.Diagrams.Extensions;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// ToolboxViewModel.
    /// </summary>
    public class ToolboxViewModel : Simplic.UI.MVC.ViewModelBase
    {
        #region Private Members
        private Gallery selectedGallery;
        private string searchTerm;
        private ObservableCollection<Gallery> galleryItems;
        private ObservableCollection<Gallery> galleryItemsFiltered;
        #endregion

        #region Constructor
        /// <summary>
        /// Instantiates the ToolboxViewModel.
        /// </summary>
        /// <param name="nodeDefinitions">List of node definitions</param>
        public ToolboxViewModel(IList<Definition.NodeDefinition> nodeDefinitions)
        {
            galleryItems = new ObservableCollection<Gallery>();
            galleryItemsFiltered = new ObservableCollection<Gallery>();
            NodeDefinitions = nodeDefinitions;

            LoadGalleryItems();
        }
        #endregion

        #region Private Methods

        #region [LoadGalleryItems]
        private void LoadGalleryItems()
        {
            var categories = NodeDefinitions.OrderBy(x => x.Category).GroupBy(x => x.Category);

            foreach (var category in categories)
            {
                var gallery = new Gallery
                {
                    Header = category.Key
                };

                if (string.IsNullOrEmpty(gallery.Header))
                {
                    gallery.Header = "General";
                }

                foreach (var item in category.OrderBy(x => x.DisplayName))
                {
                    var galleryItem = new GalleryItem
                    {
                        Header = item.DisplayName
                    };

                    if (item is Definition.ActionNodeDefinition)
                    {
                        galleryItem.Shape = new ActionNodeShape
                        {
                            Name = item.Name,
                            DataContext = item,
                        };
                    }
                    else
                    {
                        galleryItem.Shape = new EventNodeShape
                        {
                            Name = item.Name,
                            DataContext = item,
                        };
                    }

                    gallery.Items.Add(galleryItem);
                }

                GalleryItems.Add(gallery);
            }

            SelectedGallery = GalleryItems.FirstOrDefault();
        }
        #endregion

        #endregion

        #region Public Properties

        #region [GalleryItems]
        public ObservableCollection<Gallery> GalleryItems
        {
            get
            {
                if (searchTerm != string.Empty)
                    return galleryItemsFiltered;
                return galleryItems;
            }
            set
            {
                if (value != null)
                    galleryItems = value;
            }
        }
        #endregion

        #region [NodeDefinitions]
        public IList<Definition.NodeDefinition> NodeDefinitions { get; }
        #endregion

        #region [SelectedGallery]
        public Gallery SelectedGallery
        {
            get { return selectedGallery; }
            set { selectedGallery = value; RaisePropertyChanged(nameof(SelectedGallery)); }
        }
        #endregion

        public string SearchTerm
        {
            get => searchTerm;
            set
            {
                if (value != null)
                {
                    // current problem:
                    // does not update binding or at least GalleryItems
                    searchTerm = value;
                    applyFilter(value);
                }
            }
        }

        /// <summary>
        /// Select relevant items from the gallery and load them into the filtered gallery.
        /// </summary>
        /// <param name="term">Term by which is filtered</param>
        private void applyFilter(string term)
        {
            galleryItemsFiltered = new ObservableCollection<Gallery>();
            // TODO filter by proper relevance measures
            foreach (var (category, node) in GalleryItems.SelectMany(
                category => category.Items
                    .Where(node => node.Header.Contains(term))
                    .Select(node => (category, node))))
            {
                if (!galleryItemsFiltered.Contains(category))
                    galleryItemsFiltered.Add(category);
                category.Items.Add(node);
            }
            RaisePropertyChanged(nameof(GalleryItems));
        }

        #endregion
    }
}
