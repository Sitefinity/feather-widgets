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
    /// Elements from Login status frontend.
    /// </summary>
    public class LoginStatusFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginStatusFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public LoginStatusFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the user logged in name element.
        /// </summary>
        public HtmlAnchor LoggedInName
        {
            get
            {
                return this.Find.ByExpression<HtmlAnchor>("TagName=a", "data-sf-role=sf-logged-in-name");
            } 
        }

        /// <summary>
        /// Gets the logged in user email element.
        /// </summary>
        public HtmlContainerControl LoggedInEmail
        {
            get
            {
                return this.Get<HtmlContainerControl>("TagName=p", "data-sf-role=sf-logged-in-email");
            }
        }

        /// <summary>
        /// Gets the login button.
        /// </summary>
        public HtmlButton LoginButton
        {
            get
            {
                return this.Get<HtmlButton>("TagName=button", "data-sf-role=login-status-button", "InnerText=Login");
            }
        }

        /// <summary>
        /// Gets the logout button.
        /// </summary>
        public HtmlAnchor LogoutButton
        {
            get
            {
                return this.LoggedInView.Find.AllByExpression<HtmlAnchor>("InnerText=~Logout").SingleOrDefault();
            }
        }

        public HtmlDiv LoggedInView
        {
            get
            {
                return this.Find.ByExpression<HtmlDiv>("data-sf-role=sf-logged-in-view");
            }
        }
    }
}
