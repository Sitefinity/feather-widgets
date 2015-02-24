using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Search
{
    /// <summary>
    /// Provides access to Search results widget designer elements.
    /// </summary>
    public class SearchResultsWidgetEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBoxWidgetEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SearchResultsWidgetEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets Sorting options dropdown.
        /// </summary>
        public HtmlSelect SortingOptionsDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=sortOptions");
            }
        }

        /// <summary>
        /// Gets Allow users to sort results checkbox.
        /// </summary>
        public HtmlInputCheckBox AllowUsersToSortResultsCheckbox
        {
            get
            {
                return this.Get<HtmlInputCheckBox>("id=allowSorting");
            }
        }

        /// <summary>
        /// Gets Allow sorting label.
        /// </summary>
        public HtmlContainerControl AllowSortingLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("tagname=label", "innerText=Allow users to sort results");
            }
        }

        /// <summary>
        /// Gets Use paging radio button.
        /// </summary>
        public HtmlInputRadioButton UsePagingRadioButton
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=usePaging");
            }
        }

        /// <summary>
        /// Gets Use paging radio span.
        /// </summary>
        public HtmlSpan UsePagingSpan
        {
            get
            {
                return this.Get<HtmlSpan>("tagname=span", "innerText=~Divide the list on pages up to");
            }
        }

        /// <summary>
        /// Gets Items per page textbox.
        /// </summary>
        public HtmlInputText ItemsPerPage
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "ng-model=properties.ItemsPerPage.PropertyValue");
            }
        }

        /// <summary>
        /// Gets disbaled Items per page textbox.
        /// </summary>
        public HtmlInputText ItemsPerPageDisabled
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "ng-model=properties.ItemsPerPage.PropertyValue", "disabled=disabled");
            }
        }

        /// <summary>
        /// Gets Use limit radio button.
        /// </summary>
        public HtmlInputRadioButton UseLimitRadioButton
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=useLimit");
            }
        }

        /// <summary>
        /// Gets Use limit radio span.
        /// </summary>
        public HtmlSpan UseLimitSpan
        {
            get
            {
                return this.Get<HtmlSpan>("tagname=span", "innerText=~Show only limited number of items");
            }
        }

        /// <summary>
        /// Gets No limit radio button.
        /// </summary>
        public HtmlInputRadioButton NoLimitRadioButton
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=useLimit");
            }
        }

        /// <summary>
        /// Gets No limit radio span.
        /// </summary>
        public HtmlSpan NoLimitSpan
        {
            get
            {
                return this.Get<HtmlSpan>("tagname=span", "innerText=~Show all published items at once");
            }
        }

        /// <summary>
        /// Gets Sort results label.
        /// </summary>
        public HtmlContainerControl SortResultsLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("tagname=label", "innerText=Sort results");
            }
        }

        /// <summary>
        /// Gets Template label.
        /// </summary>
        public HtmlContainerControl TemplateLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("tagname=label", "innerText=Template");
            }
        }

        /// <summary>
        /// Gets Template dropdown.
        /// </summary>
        public HtmlSelect TemplateDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=searchTemplateName");
            }
        }

        /// <summary>
        /// Gets More options div.
        /// </summary>
        public HtmlSpan MoreOptionsSpan
        {
            get
            {
                return this.Get<HtmlSpan>("class=Options-toggler text-muted ng-binding", "innerText=More options");
            }
        }

        /// <summary>
        /// Gets CSS classes label.
        /// </summary>
        public HtmlContainerControl CssClassesLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("tagname=label", "innerText=CSS classes");
            }
        }

        /// <summary>
        /// Gets CSS classes label.
        /// </summary>
        public HtmlInputText CssClassesTextbox
        {
            get
            {
                return this.Get<HtmlInputText>("id=searchCssClass");
            }
        }
    }
}
