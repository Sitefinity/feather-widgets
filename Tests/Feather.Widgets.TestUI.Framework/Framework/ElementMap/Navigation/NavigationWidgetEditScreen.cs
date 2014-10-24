using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Navigation
{
    /// <summary>
    /// Provides access to navigation widget screen
    /// </summary>
    public class NavigationWidgetEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationWidgetEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public NavigationWidgetEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets display mode list.
        /// </summary>
        public HtmlDiv DislayModeList
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=form-group ng-scope");
            }
        }

        /// <summary>
        /// Gets Save changes button.
        /// </summary>
        public HtmlButton SaveChangesButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Save");
            }
        }

        /// <summary>
        /// Gets template selector.
        /// </summary>
        public HtmlSelect TemplateSelector
        {
            get
            {
                return this.Get<HtmlSelect>("tagname=select", "id=navTemplateName");
            }
        }

        /// <summary>
        /// Gets levels to include.
        /// </summary>
        public HtmlSelect LevelesToIncludeSelector
        {
            get
            {
                return this.Get<HtmlSelect>("tagname=select", "id=navLevelsToInclude");
            }
        }

        /// <summary>
        /// Gets css class.
        /// </summary>
        public HtmlInputText CSSClass
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "id=navCssClass");
            }
        }

        /// <summary>
        /// Gets More options.
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
