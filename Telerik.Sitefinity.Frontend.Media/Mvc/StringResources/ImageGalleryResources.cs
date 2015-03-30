using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Image Gallery widget
    /// </summary>
    [ObjectInfo(typeof(ImageGalleryResources), ResourceClassId = "ImageGalleryResources", Title = "ImageGalleryResourcesTitle", Description = "ImageGalleryResourcesDescription")]
    public class ImageGalleryResources : Resource
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageGalleryResources"/> class. 
        /// Initializes new instance of <see cref="ImageGalleryResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ImageGalleryResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageGalleryResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public ImageGalleryResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description

        /// <summary>
        /// Gets Title for the Image Gallery widget resources class.
        /// </summary>
        [ResourceEntry("ImageGalleryResourcesTitle",
            Value = "Image Gallery widget resources",
            Description = "Title for the Image Gallery widget resources class.",
            LastModified = "2015/03/26")]
        public string ImageGalleryResourcesTitle
        {
            get
            {
                return this["ImageGalleryResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the Image Gallery widget resources class.
        /// </summary>
        [ResourceEntry("ImageGalleryResourcesDescription",
            Value = "Localizable strings for the Image Gallery widget.",
            Description = "Description for the Image Gallery widget resources class.",
            LastModified = "2015/03/26")]
        public string ImageGalleryResourcesDescription
        {
            get
            {
                return this["ImageGalleryResourcesDescription"];
            }
        }

        #endregion

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/02/23")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/02/23")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        #region Designer
        /// <summary>
        /// Phrase: All published images
        /// </summary>
        /// <value>All published images</value>
        [ResourceEntry("AllPublishedImages",
            Value = "All published images",
            Description = "Phrase: All published images",
            LastModified = "2015/02/23")]
        public string AllPublishedImages
        {
            get
            {
                return this["AllPublishedImages"];
            }
        }

        /// <summary>
        /// Phrase: From selected libraries only...
        /// </summary>
        /// <value>From selected libraries only...</value>
        [ResourceEntry("FromSelectedLibraries",
            Value = "From selected libraries only...",
            Description = "Phrase: From selected libraries only...",
            LastModified = "2015/02/23")]
        public string FromSelectedLibraries
        {
            get
            {
                return this["FromSelectedLibraries"];
            }
        }

        /// <summary>
        /// Phrase: From currently opened library
        /// </summary>
        /// <value>From currently opened library</value>
        [ResourceEntry("FromCurrentlyOpen",
            Value = "From currently opened library",
            Description = "Phrase: From currently opened library",
            LastModified = "2015/02/23")]
        public string FromCurrentlyOpen
        {
            get
            {
                return this["FromCurrentlyOpen"];
            }
        }

        /// <summary>
        /// Phrase: Selection of images...
        /// </summary>
        /// <value>Selection of images...</value>
        [ResourceEntry("SelectionOfImages",
            Value = "Selection of images...",
            Description = "Phrase: Selection of images...",
            LastModified = "2015/02/23")]
        public string SelectionOfImages
        {
            get
            {
                return this["SelectionOfImages"];
            }
        }

        /// <summary>
        /// phrase : Narrow selection by...
        /// </summary>
        /// <value>Narrow selection by...</value>
        [ResourceEntry("NarrowSelection",
            Value = "Narrow selection by...",
            Description = "phrase : Narrow selection by...",
            LastModified = "2015/02/23")]
        public string NarrowSelection
        {
            get
            {
                return this["NarrowSelection"];
            }
        }

        /// <summary>
        /// phrase: List settings
        /// </summary>
        /// <value>List settings</value>
        [ResourceEntry("ListSettings",
            Value = "List settings",
            Description = "phrase: List settings",
            LastModified = "2015/02/23")]
        public string ListSettings
        {
            get
            {
                return this["ListSettings"];
            }
        }

        /// <summary>
        /// Phrase: Use paging
        /// </summary>
        /// <value>Use paging</value>
        [ResourceEntry("UsePaging",
            Value = "Use paging",
            Description = "Phrase: Use paging",
            LastModified = "2015/02/23")]
        public string UsePaging
        {
            get
            {
                return this["UsePaging"];
            }
        }

        /// <summary>
        /// Phrase: Divide the list on pages up to {0} items per page
        /// </summary>
        /// <value>Divide the list on pages up to {0} items per page</value>
        [ResourceEntry("DivideToItemPerPage",
            Value = "Divide the list on pages up to {0} items per page",
            Description = "Phrase: Divide the list on pages up to {0} items per page",
            LastModified = "2015/02/23")]
        public string DivideToItemPerPage
        {
            get
            {
                return this["DivideToItemPerPage"];
            }
        }

        /// <summary>
        /// Phrase: Show only limited number of items {0} items in total
        /// </summary>
        /// <value>Show only limited number of items {0} items in total</value>
        [ResourceEntry("ShowLimitedItems",
            Value = "Show only limited number of items {0} items in total",
            Description = "Phrase: Show only limited number of items {0} items in total",
            LastModified = "2015/02/23")]
        public string ShowLimitedItems
        {
            get
            {
                return this["ShowLimitedItems"];
            }
        }

        /// <summary>
        /// Phrase: No limit and paging
        /// </summary>
        /// <value>No limit and paging</value>
        [ResourceEntry("NoLimitPaging",
            Value = "No limit and paging",
            Description = "Phrase: No limit and paging",
            LastModified = "2015/02/23")]
        public string NoLimitPaging
        {
            get
            {
                return this["NoLimitPaging"];
            }
        }

        /// <summary>
        /// Phrase: Show all published items at once
        /// </summary>
        /// <value>Show all published items at once</value>
        [ResourceEntry("ShowAllItems",
            Value = "Show all published items at once",
            Description = "Phrase: Show all published items at once",
            LastModified = "2015/02/23")]
        public string ShowAllItems
        {
            get
            {
                return this["ShowAllItems"];
            }
        }

        /// <summary>
        /// Phrase: Sort {0}
        /// </summary>
        /// <value>Sort {0}</value>
        [ResourceEntry("SortItems",
            Value = "Sort images",
            Description = "Phrase: Sort images",
            LastModified = "2015/02/23")]
        public string SortItems
        {
            get
            {
                return this["SortItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Last published
        /// </summary>
        [ResourceEntry("LastPublished",
            Value = "Last published",
            Description = "phrase : Last published",
            LastModified = "2015/02/23")]
        public string LastPublished
        {
            get
            {
                return this["LastPublished"];
            }
        }

        /// <summary>
        /// Gets phrase : Last modified
        /// </summary>
        [ResourceEntry("LastModified",
            Value = "Last modified",
            Description = "phrase : Last modified",
            LastModified = "2015/02/23")]
        public string LastModified
        {
            get
            {
                return this["LastModified"];
            }
        }

        /// <summary>
        /// Gets phrase : By Title (A-Z)
        /// </summary>
        [ResourceEntry("ByTitleAZ",
            Value = "By Title (A-Z)",
            Description = "phrase : By Title (A-Z)",
            LastModified = "2015/02/23")]
        public string ByTitleAZ
        {
            get
            {
                return this["ByTitleAZ"];
            }
        }

        /// <summary>
        /// Gets phrase : By Title (Z-A)
        /// </summary>
        [ResourceEntry("ByTitleZA",
            Value = "By Title (Z-A)",
            Description = "phrase : By Title (Z-A)",
            LastModified = "2015/02/23")]
        public string ByTitleZA
        {
            get
            {
                return this["ByTitleZA"];
            }
        }

        /// <summary>
        /// phrase: As set manually
        /// </summary>
        /// <value>As set manually</value>
        [ResourceEntry("AsSetManually",
            Value = "As set manually",
            Description = "phrase: As set manually",
            LastModified = "2015/02/23")]
        public string AsSetManually
        {
            get
            {
                return this["AsSetManually"];
            }
        }

        /// <summary>
        /// phrase: As set in Advanced mode
        /// </summary>
        /// <value>As set in Advanced mode</value>
        [ResourceEntry("AsSetInAdvancedMode",
            Value = "As set in Advanced mode",
            Description = "phrase: As set in Advanced mode",
            LastModified = "2015/02/23")]
        public string AsSetInAdvancedMode
        {
            get
            {
                return this["AsSetInAdvancedMode"];
            }
        }

        /// <summary>
        /// Gets phrase : List template
        /// </summary>
        [ResourceEntry("ListTemplate",
            Value = "List template",
            Description = "phrase : List template",
            LastModified = "2015/02/23")]
        public string ListTemplate
        {
            get
            {
                return this["ListTemplate"];
            }
        }

        /// <summary>
        /// Gets phrase : Single item settings
        /// </summary>
        [ResourceEntry("SingleItemSettings",
            Value = "Single item settings",
            Description = "phrase : Single item settings",
            LastModified = "2015/02/23")]
        public string SingleItemSettings
        {
            get
            {
                return this["SingleItemSettings"];
            }
        }

        /// <summary>
        /// Gets phrase : Open single item in...
        /// </summary>
        [ResourceEntry("OpenSingleItem",
            Value = "Open single item in...",
            Description = "phrase : Open single item in...",
            LastModified = "2015/02/23")]
        public string OpenSingleItem
        {
            get
            {
                return this["OpenSingleItem"];
            }
        }

        /// <summary>
        /// Gets phrase : Auto-generated page
        /// </summary>
        [ResourceEntry("ShowInSamePage",
            Value = "Auto-generated page",
            Description = "phrase : Auto-generated page (with the same layout as the list page)",
            LastModified = "2015/02/23")]
        public string ShowInSamePage
        {
            get
            {
                return this["ShowInSamePage"];
            }
        }

        /// <summary>
        /// Gets phrase : Detail template
        /// </summary>
        [ResourceEntry("DetailTemplate",
            Value = "Detail template",
            Description = "phrase : Detail template",
            LastModified = "2015/02/23")]
        public string DetailTemplate
        {
            get
            {
                return this["DetailTemplate"];
            }
        }

        /// <summary>
        /// Gets word : Content
        /// </summary>
        [ResourceEntry("Content",
            Value = "Content",
            Description = "word: Content",
            LastModified = "2015/02/23")]
        public string Content
        {
            get
            {
                return this["Content"];
            }
        }

        /// <summary>
        /// Gets phrase : Which images to display?
        /// </summary>
        [ResourceEntry("ItemsToDisplay",
            Value = "Which images to display?",
            Description = "phrase : Which images to display?",
            LastModified = "2015/02/23")]
        public string ItemsToDisplay
        {
            get
            {
                return this["ItemsToDisplay"];
            }
        }

        /// <summary>
        /// Gets phrase : Use limit
        /// </summary>
        [ResourceEntry("UseLimit",
            Value = "Use limit",
            Description = "phrase : Use limit",
            LastModified = "2015/02/23")]
        public string UseLimit
        {
            get
            {
                return this["UseLimit"];
            }
        }

        /// <summary>
        /// Gets phrase : Selected existing page...
        /// </summary>
        [ResourceEntry("ShowInExistingPage",
            Value = "Selected existing page...",
            Description = "phrase : Selected existing page...",
            LastModified = "2015/02/23")]
        public string ShowInExistingPage
        {
            get
            {
                return this["ShowInExistingPage"];
            }
        }

        /// <summary>
        /// Gets the Provider
        /// </summary>
        [ResourceEntry("Provider",
            Value = "Provider",
            Description = "Provider",
            LastModified = "2015/02/23")]
        public string Provider
        {
            get
            {
                return this["Provider"];
            }
        }

        /// <summary>
        /// phrase: Thumbnail size
        /// </summary>
        /// <value>Thumbnail size</value>
        [ResourceEntry("ThumbnailSize",
            Value = "Thumbnail size",
            Description = "phrase: Thumbnail size",
            LastModified = "2015/02/23")]
        public string ThumbnailSize
        {
            get
            {
                return this["ThumbnailSize"];
            }
        }

        /// <summary>
        /// phrase: Image size
        /// </summary>
        /// <value>Image size</value>
        [ResourceEntry("ImageSize",
            Value = "Image size",
            Description = "phrase: Image size",
            LastModified = "2015/02/24")]
        public string ImageSize
        {
            get
            {
                return this["ImageSize"];
            }
        }
        #endregion

        /// <summary>
        /// phrase: Next image
        /// </summary>
        [ResourceEntry("NextImage",
            Value = "Next image",
            Description = "phrase: Next image",
            LastModified = "2015/02/24")]
        public string NextImage
        {
            get
            {
                return this["NextImage"];
            }
        }

        /// <summary>
        /// phrase: Previous image
        /// </summary>
        /// <value>Previous image</value>
        [ResourceEntry("PreviousImage",
            Value = "Previous image",
            Description = "phrase: Previous image",
            LastModified = "2015/02/24")]
        public string PreviousImage
        {
            get
            {
                return this["PreviousImage"];
            }
        }

        /// <summary>
        /// word: by
        /// </summary>
        /// <value>by</value>
        [ResourceEntry("By",
            Value = "by",
            Description = "word: by",
            LastModified = "2015/02/24")]
        public string By
        {
            get
            {
                return this["By"];
            }
        }

        /// <summary>
        /// phrase: Back to all images
        /// </summary>
        /// <value>Back to all images</value>
        [ResourceEntry("BackToAllImages",
            Value = "Back to all images",
            Description = "phrase: Back to all images",
            LastModified = "2015/02/24")]
        public string BackToAllImages
        {
            get
            {
                return this["BackToAllImages"];
            }
        }

        /// <summary>
        /// phrase: [Index] of [TotalCount]
        /// </summary>
        /// <value>{0} of {1}</value>
        [ResourceEntry("IndexOfTotal",
            Value = "{0} of {1}",
            Description = "phrase: [Index] of [TotalCount]",
            LastModified = "2015/02/25")]
        public string IndexOfTotal
        {
            get
            {
                return this["IndexOfTotal"];
            }
        }

        /// <summary>
        /// phrase: From all libraries
        /// </summary>
        [ResourceEntry("AllLibraries",
            Value = "From all libraries",
            Description = "phrase: From all libraries",
            LastModified = "2015/02/25")]
        public string AllLibraries
        {
            get
            {
                return this["AllLibraries"];
            }
        }

        /// <summary>
        /// phrase: Some of the selected libraries were moved or deleted.
        /// </summary>
        /// <value>Some of the selected libraries were moved or deleted.</value>
        [ResourceEntry("MissingLibraries",
            Value = "Some of the selected libraries were moved or deleted.",
            Description = "phrase: Some of the selected libraries were moved or deleted.",
            LastModified = "2015/02/26")]
        public string MissingLibraries
        {
            get
            {
                return this["MissingLibraries"];
            }
        }
    }
}
