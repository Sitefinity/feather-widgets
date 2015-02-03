using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Navigation
{
    /// <summary>
    /// Elements from Navigation widget on the frontend.
    /// </summary>
    public class NavigationWidgetFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationWidgetFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public NavigationWidgetFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the Toggle button on the frontend when toggle menu is present
        /// </summary>
        public HtmlButton ToggleButton
        {
            get
            {
                return this.Get<HtmlButton>("class=^navbar-toggle", "InnerText=~Toggle navigation");
            }
        }

        /// <summary>
        /// Gets the Bootstrap Navigation unordered list element on the frontend.
        /// </summary>
        public HtmlUnorderedList GetBootstrapNavigation(string cssClass)
        {
            return this.Get<HtmlUnorderedList>("class=" + cssClass);          
        }

        /// <summary>
        /// Gets the Foundation Navigation unordered list element on the frontend.
        /// </summary>
        public HtmlControl GetFoundationNavigation(string cssClass)
        {
            HtmlControl section = this.Find.ByExpression<HtmlControl>("tagname=section", "class=" + cssClass);
            HtmlUnorderedList list = null;

            if (section != null && section.IsVisible())
            {
                list = section.Find.AllByTagName<HtmlUnorderedList>("ul").First();
            }
            else
            {
                list = this.Find.ByExpression<HtmlUnorderedList>("TagName=ul", "class=" + cssClass);
            }

            return list;          
        }

        /// <summary>
        /// Gets the Foundation Navigation unordered list of child pages on the frontend.
        /// </summary>
        public HtmlUnorderedList GetFoundationNavigationChild(string parentCssClass)
        {
            var parentList = this.GetFoundationNavigation(parentCssClass);

            var list = parentList.Find.AllByTagName<HtmlUnorderedList>("ul").First();

            return list;        
        }

        /// <summary>
        /// Gets the Semantic Navigation list on the frontend.
        /// </summary>
        public HtmlControl GetSemanticNavigation(string cssClass)
        {
            return this.Find.ByExpression<HtmlControl>("tagname=nav", "class=" + cssClass);
        }

        /// <summary>
        /// Gets the Semantic Navigation list of child pages
        /// </summary>
        /// <param name="cssClass">The css class of the group of pages.</param>
        /// <returns>The div element.</returns>
        public HtmlDiv GetSemanticNavigationChild(string cssClass)
        {
            return this.Get<HtmlDiv>("tagname=div", "class=" + cssClass);
        }

        /// <summary>
        /// Gets the navigation select element
        /// </summary>
        public HtmlSelect NavigationDropDown
        {
            get
            {
                return this.Get<HtmlSelect>("class=nav-select form-control");
            }
        }

        /// <summary>
        /// Gets the menu link from transformation for Foundation template. 
        /// </summary>
        public HtmlAnchor FoundationMenuLink
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "InnerText=Menu");
            }
        }
    }
}
