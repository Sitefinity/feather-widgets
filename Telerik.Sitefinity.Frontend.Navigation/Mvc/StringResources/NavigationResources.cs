using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Navigation widget
    /// </summary>
    [ObjectInfo(typeof(NavigationResources), Title = "NavigationResourcesTitle", Description = "NavigationResourcesDescription")]
    public class NavigationResources : Resource
    {
        #region Constructions

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationResources"/> class.
        /// </summary>
        public NavigationResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public NavigationResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        /// <summary>
        /// Gets Title for the Navigation widget resources class.
        /// </summary>
        [ResourceEntry("NavigationResourcesTitle",
            Value = "Navigation widget resources",
            Description = "Title for the Navigation widget resources class.",
            LastModified = "2014/05/27")]
        public string NavigationResourcesTitle
        {
            get
            {
                return this["NavigationResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the Navigation widget resources class.
        /// </summary>
        [ResourceEntry("NavigationResourcesDescription",
            Value = "Localizable strings for the Navigation widget.",
            Description = "Description for the Navigation widget resources class.",
            LastModified = "2014/05/27")]
        public string NavigationResourcesDescription
        {
            get
            {
                return this["NavigationResourcesDescription"];
            }
        }

        /// <summary>
        /// Gets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        [ResourceEntry("Display",
            Value = "Display...",
            Description = "word : Display...",
            LastModified = "2014/05/27")]
        public string Display
        {
            get
            {
                return this["Display"];
            }
        }

        /// <summary>
        /// Gets the top level pages.
        /// </summary>
        /// <value>
        /// The top level pages.
        /// </value>
        [ResourceEntry("TopLevelPages",
            Value = "Top-level pages (and their child-pages if template allows)",
            Description = "Description for the SelectionMode option TopLevelPages.",
            LastModified = "2014/05/27")]
        public string TopLevelPages
        {
            get
            {
                return this["TopLevelPages"];
            }
        }

        /// <summary>
        /// Gets the current page children.
        /// </summary>
        /// <value>
        /// The current page children.
        /// </value>
        [ResourceEntry("CurrentPageChildren",
            Value = "All pages under currently opened page",
            Description = "Description for the SelectionMode option CurrentPageChildren.",
            LastModified = "2014/05/27")]
        public string CurrentPageChildren
        {
            get
            {
                return this["CurrentPageChildren"];
            }
        }

        /// <summary>
        /// Gets Description for the SelectionMode option CurrentPageSiblings.
        /// </summary>
        [ResourceEntry("CurrentPageSiblings",
            Value = "All sibling pages of currently opened page",
            Description = "Description for the SelectionMode option CurrentPageSiblings.",
            LastModified = "2014/05/27")]
        public string CurrentPageSiblings
        {
            get
            {
                return this["CurrentPageSiblings"];
            }
        }

        /// <summary>
        /// Gets phrase : Levels to include
        /// </summary>
        [ResourceEntry("LevelsToInclude",
            Value = "Levels to include",
            Description = "phrase : Levels to include",
            LastModified = "2014/05/27")]
        public string LevelsToInclude
        {
            get
            {
                return this["LevelsToInclude"];
            }
        }

        /// <summary>
        /// Gets phrase : Responsive design
        /// </summary>
        [ResourceEntry("ResponsiveDesignSectionTitle",
            Value = "Responsive design",
            Description = "phrase : Responsive design",
            LastModified = "2014/05/27")]
        public string ResponsiveDesignSectionTitle
        {
            get
            {
                return this["ResponsiveDesignSectionTitle"];
            }
        }

        /// <summary>
        /// Gets the description of the Responsive design section.
        /// </summary>
        [ResourceEntry("ResponsiveDesignDescription",
            Value = "You can define how navigation is transformed for different screens and devices in the template package. Check your package documentation for more information.",
            Description = "The description of the Responsive design section",
            LastModified = "2014/05/27")]
        public string ResponsiveDesignDescription
        {
            get
            {
                return this["ResponsiveDesignDescription"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2014/05/27")]
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
            LastModified = "2014/05/27")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets word : Template
        /// </summary>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "word : Template",
            LastModified = "2014/05/27")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Gets word: Simple
        /// </summary>
        [ResourceEntry("Simple",
            Value = "Simple",
            Description = "Simple",
            LastModified = "2014/05/27")]
        public string Simple
        {
            get
            {
                return this["Simple"];
            }
        }

        /// <summary>
        /// Description for the SelectionMode option SelectedPageChildren.
        /// </summary>
        [ResourceEntry("SelectedPageChildren",
            Value = "All pages under particular page",
            Description = "Description for the SelectionMode option SelectedPageChildren.",
            LastModified = "2015/01/20")]
        public string SelectedPageChildren
        {
            get
            {
                return this["SelectedPageChildren"];
            }
        }

        /// <summary>
        /// Description for the SelectionMode option SelectedPages.
        /// </summary>
        [ResourceEntry("SelectedPages",
            Value = "Custom selection of pages...",
            Description = "Description for the SelectionMode option SelectedPages.",
            LastModified = "2015/01/20")]
        public string SelectedPages
        {
            get
            {
                return this["SelectedPages"];
            }
        }
    }
}
