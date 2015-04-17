using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Identity
{
    /// <summary>
    /// Elements from Login form edit.
    /// </summary>
    public class LoginFormEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFormEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public LoginFormEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the select button in redirect to section.
        /// </summary>
        public HtmlButton RedirectToPageSelectButton
        {
            get
            {
                return this.RedirectPageSection.Find.ByExpression<HtmlButton>("tagname=button", "class=~openSelectorBtn");
            }
        }

        /// <summary>
        /// Gets the select button in registration to section.
        /// </summary>
        public HtmlButton RegistrationPageSelectButton
        {
            get
            {
                return this.RegistrationPageSection.Find.ByExpression<HtmlButton>("tagname=button", "class=~openSelectorBtn");
            }
        }

        private HtmlDiv RedirectPageSection
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "InnerText=~After login users will be redirected to...", "class=form-group");
            }
        }

        private HtmlDiv RegistrationPageSection
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "InnerText=~This is the page where you have dropped Registration widget", "class=form-group");
            }
        }
    }
}
