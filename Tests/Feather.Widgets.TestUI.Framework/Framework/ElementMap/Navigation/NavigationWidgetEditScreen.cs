using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Navigation
{
    public class NavigationWidgetEditScreen : HtmlElementContainer
    {
        public NavigationWidgetEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Provides access to display mode list.
        /// </summary>
        public HtmlUnorderedList DislayModeList
        {
            get
            {
                return this.Get<HtmlUnorderedList>("tagname=ul", "class=form-group ng-scope");
            }
        }

        /// <summary>
        /// Provides access to Save changes button.
        /// </summary>
        public HtmlButton SaveChangesButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Save");
            }
        }

        /// <summary>
        /// Provides access to template selector.
        /// </summary>
        public HtmlSelect TemplateSelector
        {
            get
            {
                return this.Get<HtmlSelect>("tagname=select", "id=navTemplateName");
            }
        }

        /// <summary>
        /// Provides access to levels to include.
        /// </summary>
        public HtmlSelect LevelesToIncludeSelector
        {
            get
            {
                return this.Get<HtmlSelect>("tagname=select", "id=navLevelsToInclude");
            }
        }

        /// <summary>
        /// Provides access to css class.
        /// </summary>
        public HtmlInputText CSSClass
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "id=navCssClass");
            }
        }

        /// <summary>
        /// Provides access to More options.
        /// </summary>
        public HtmlSpan MoreOptions
        {
            get
            {
                return this.Get<HtmlSpan>("tagname=span", "InnerText=More options");
            }
        }
    }
}
