using System;
using System.Linq;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Classifications
{
    /// <summary>
    /// Provides access to Categories widget designer elements.
    /// </summary>
    public class CategoriesWidgetEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesWidgetEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public CategoriesWidgetEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the Settings tab.
        /// </summary>
        /// <value>The list settings.</value>
        public HtmlAnchor SettingsTab
        {
            get
            {
                return this.Get<HtmlAnchor>("class=~ng-binding", "Innertext=Settings");
            }
        }

        /// <summary>
        /// Gets the Show Empty Categpries checkbox.
        /// </summary>
        public HtmlInputCheckBox ShowEmptyCategories
        {
            get
            {
                return this.Get<HtmlInputCheckBox>("id=showEmpty");
            }
        }

        /// <summary>
        /// Gets the used by content type dropdown.
        /// </summary>
        /// <value>The used by content type dropdown.</value>
        public HtmlSelect UsedByContentTypeDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("ng-show=properties.TaxaToDisplay.PropertyValue === 'UsedByContentType'");
            }
        }

        /// <summary>
        /// Gets categories sort drop down.
        /// </summary>
        /// <value>Sorting drop down</value>
        public HtmlSelect SortDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=sortOptions");
            }
        }

    }
}
