﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using Telerik.Windows.Controls.Diagrams.Extensions;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// ToolboxViewModel.
    /// </summary>
    public class ToolboxViewModel : Simplic.UI.MVC.ViewModelBase
    {
        private Gallery selectedGallery;
        private string searchTerm;
        private DispatcherTimer timer;
        private int keyCounter;
        private bool loaded;

        /// <summary>
        /// Instantiates the ToolboxViewModel.
        /// </summary>
        /// <param name="nodeDefinitions">List of node definitions</param>
        public ToolboxViewModel(IList<Definition.NodeDefinition> nodeDefinitions)
        {
            NodeDefinitions = nodeDefinitions;
            Initialize();
            LoadGalleryItems();
        }

        /// <summary>
        /// Ensures only nodes of selected category are loaded when opening the available nodes tab for the first time.
        /// </summary>
        public void LazyLoadAvailableNodes()
        {
            if (loaded)
                return;
            loaded = true;

            GalleryViewSources.Where(x => x.Key == selectedGallery).First().Value.Filter = nodeFilter;
        }

        /// <summary>
        /// Initializes variables.
        /// </summary>
        private void Initialize()
        {
            GalleryItems = new ObservableCollection<Gallery>();
            SearchTerm = string.Empty;
            GalleryItemsViewSource = CollectionViewSource.GetDefaultView(GalleryItems);
            GalleryItemsViewSource.Filter = galleryFilter;
            GalleryViewSources = new Dictionary<Gallery, ICollectionView>();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.3);
            timer.Tick += Timer_Tick;
            keyCounter = 0;
            MatchWholeWord = false;
            MatchCase = false;
        }

        /// <summary>
        /// Extracts all nodes from node definitions and loads them into galleries.
        /// </summary>
        private void LoadGalleryItems()
        {
            var categories = NodeDefinitions.OrderBy(x => x.Category).GroupBy(x => x.Category);

            foreach (var category in categories)
            {
                var gallery = new Gallery() { Header = category.Key };

                if (string.IsNullOrEmpty(gallery.Header))
                    gallery.Header = "General";

                foreach (var item in category.OrderBy(x => x.DisplayName))
                {
                    var galleryItem = new GalleryItem { Header = item.DisplayName };

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
                GalleryViewSources[gallery] = CollectionViewSource.GetDefaultView(gallery.Items);

                // Initialize gallery to not load nodes to enable lazy loading.
                GalleryViewSources[gallery].Filter = (x => false);
            }

            SelectedGallery = GalleryItems.FirstOrDefault();
        }

        /// <summary>
        /// Filters for relevant galleries.
        /// </summary>
        /// <param name="term">Term by which is filtered</param>
        private bool galleryFilter(object obj)
        {
            if (obj is Gallery category)
            {
                bool hasRelevantNode = false;
                foreach (var node in category.Items)
                    if (nodeFilter(node))
                        hasRelevantNode = true;
                return hasRelevantNode;
            }
            return true;
        }

        /// <summary>
        /// Filters for relevant nodes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool nodeFilter(object obj)
        {
            if (!(obj is GalleryItem))
                return true;
            
            var searchTermNormalized = normalizeString(SearchTerm);

            if (string.IsNullOrWhiteSpace(searchTermNormalized))
                return true;

            var node = obj as GalleryItem;

            if (MatchWholeWord)
            {
                if (MatchCase)
                    return node.Header.Split(' ').Contains(searchTermNormalized);

                return node.Header.ToLower().Split(' ').Contains(searchTermNormalized.ToLower());
            }

            if (MatchCase)
                return node.Header.Contains(searchTermNormalized);

            return node.Header.ToLower().Contains(searchTermNormalized.ToLower());
        }

        /// <summary>
        /// Removes whitespaces at the beginning and at the end from given string.
        /// Uses '~' as placeholder to do so.
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>normalized string</returns>
        private string normalizeString(string str)
        {
            var stringBuilder = new StringBuilder(str);
            for (int i = 0; i < stringBuilder.Length; i++)
            {
                if (stringBuilder[i] != ' ')
                    break;
                stringBuilder[i] = '~';
            }
            for (int i = stringBuilder.Length - 1; i >= 0; i--)
            {
                if (stringBuilder[i] != ' ')
                    break;
                stringBuilder[i] = '~';
            }

            return Regex.Replace(str, "~+", "");
        }

        /// <summary>
        /// Refreshes all collection view sources.
        /// </summary>
        /// <returns></returns>
        private Task UpdateToolBox()
        {
            if (GalleryItemsViewSource != null)
                GalleryItemsViewSource.Refresh();

            if (GalleryViewSources != null)
                foreach (var galleryViewSource in GalleryViewSources.Values)
                    galleryViewSource.Refresh();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Event handler for timer tick event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Timer_Tick(object sender, EventArgs e)
        {
            keyCounter++;

            if (keyCounter >= 2)
            {
                await UpdateToolBox();
                keyCounter = 0;
                timer.Stop();
            }
        }

        /// <summary>
        /// Gets or sets the collection of galleries.
        /// </summary>
        public ObservableCollection<Gallery> GalleryItems { get; set; }

        /// <summary>
        /// Gets or sets the ViewSource for the collection of galleries.
        /// </summary>
        public ICollectionView GalleryItemsViewSource { get; set; }

        /// <summary>
        /// Gets or sets the ViewSources for every gallery.
        /// </summary>
        public IDictionary<Gallery, ICollectionView> GalleryViewSources { get; set; }

        /// <summary>
        /// Gets the list of node definitions.
        /// </summary>
        public IList<Definition.NodeDefinition> NodeDefinitions { get; }

        /// <summary>
        /// Gets or sets the currently selected gallery.
        /// </summary>
        public Gallery SelectedGallery
        {
            get => selectedGallery;
            
            set
            {
                selectedGallery = value;
                GalleryViewSources.Where(x => x.Key == value).First().Value.Filter = nodeFilter;
                RaisePropertyChanged(nameof(SelectedGallery));
            }
        }

        /// <summary>
        /// Gets or sets the search term.
        /// </summary>
        public string SearchTerm { get => searchTerm; set { searchTerm = value; if (timer != null) timer.Start(); } }

        /// <summary>
        /// Gets or sets whether the search term should be matched to a whole word.
        /// </summary>
        public bool MatchWholeWord { get; set; }

        /// <summary>
        /// Gets or sets whether the search term should match the exact case.
        /// </summary>
        public bool MatchCase { get; set; }
    }
}
