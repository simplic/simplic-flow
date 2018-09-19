using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls.Diagrams.Extensions;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// ToolboxViewModel
    /// </summary>
    public class ToolboxViewModel : Simplic.UI.MVC.ViewModelBase
    {
        #region Private Members
        private Gallery selectedGallery;
        #endregion

        #region Constructor
        public ToolboxViewModel(IList<Definition.NodeDefinition> nodeDefinitions)
        {
            GalleryItems = new ObservableCollection<Gallery>();
            NodeDefinitions = nodeDefinitions;

            LoadGalleryItems();
        }
        #endregion

        #region Private Methods

        #region [LoadGalleryItems]
        private void LoadGalleryItems()
        {
            var actionGallery = new Gallery();
            actionGallery.Header = "Aktion";

            var eventGallery = new Gallery();
            eventGallery.Header = "Event";

            foreach (var item in NodeDefinitions.Where(x => x is Simplic.Flow.Editor.Definition.ActionNodeDefinition))
            {
                var galleryItem = new GalleryItem(item.Name, new ActionNodeShape
                {
                    Name = item.Name,
                    DataContext = item,
                });
                galleryItem.ItemType = item.GetType().ToString();
                galleryItem.Header = item.DisplayName;

                actionGallery.Items.Add(galleryItem);
            }

            GalleryItems.Add(actionGallery);


            foreach (var item in NodeDefinitions.Where(x => x is Simplic.Flow.Editor.Definition.EventNodeDefinition))
            {
                var galleryItem = new GalleryItem(item.Name, new EventNodeShape
                {
                    Name = item.Name,
                    DataContext = item,
                });
                galleryItem.ItemType = item.GetType().ToString();

                eventGallery.Items.Add(galleryItem);
            }

            GalleryItems.Add(eventGallery);

            SelectedGallery = actionGallery;
        }
        #endregion

        #endregion
        
        #region Public Properties

        #region [GalleryItems]
        public ObservableCollection<Gallery> GalleryItems { get; set; }
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

        #endregion
    }
}
