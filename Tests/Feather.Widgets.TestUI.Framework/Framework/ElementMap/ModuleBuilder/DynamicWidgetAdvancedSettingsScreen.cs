using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.ModuleBuilder
{
    public class DynamicWidgetAdvancedSettingsScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicWidgetAdvancedSettingsScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public DynamicWidgetAdvancedSettingsScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets dynamic widget footer.
        /// </summary>
        public HtmlDiv DynamicWidgetFooter
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "placeholder=modal-footer");
            }
        }

        /// <summary>
        /// Gets the model button.
        /// </summary>
        public HtmlButton ModelButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "class=btn btn-default btn-xs ng-binding");
            }
        }

        /// <summary>
        /// Gets the items per page
        /// </summary>
        public HtmlInputControl ItemsPerPage
        {
            get
            {
                return this.Get<HtmlInputControl>("tagname=input", "id=prop-ItemsPerPage");
            }
        }

        /// <summary>
        /// Gets the sort expression
        /// </summary>
        public HtmlInputControl SortExpression
        {
            get
            {
                return this.Get<HtmlInputControl>("tagname=input", "id=prop-SortExpression");
            }
        }
    }
}
