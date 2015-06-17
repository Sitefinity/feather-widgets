using System;
using System.Linq;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Classifications
{
    /// <summary>
    /// Provides access to Tags widget designer elements.
    /// </summary>
    public class TagsWidgetEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListsWidgetEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public TagsWidgetEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets tags sort drop down.
        /// </summary>
        /// <value>Sorting drop down</value>
        public HtmlSelect SortDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=sortOptions");
            }
        }

        /// <summary>
        /// Gets tags template drop down.
        /// </summary>
        /// <value>Tags template drop down</value>
        public HtmlSelect TagsTemplateDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=navTemplateName");
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
    }
}
