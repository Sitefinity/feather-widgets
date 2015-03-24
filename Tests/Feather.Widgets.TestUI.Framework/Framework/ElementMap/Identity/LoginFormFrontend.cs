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
    /// Elements from Login form frontend.
    /// </summary>
    public class LoginFormFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFormFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public LoginFormFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the Already logged in message.
        /// </summary>
        public HtmlDiv AlreadyLoggedInMessage
        {
            get
            {
                return this.Get<HtmlDiv>("TagName=div", "InnerText=You are already logged in");
            }
        }

        /// <summary>
        /// Gets the UserName input text field.
        /// </summary>
        public HtmlInputText UserName
        {
            get
            {
                return this.Get<HtmlInputText>("TagName=input", "id=UserName", "name=UserName");
            } 
        }

        /// <summary>
        /// Gets the Password input text field.
        /// </summary>
        public HtmlInputPassword Password
        {
            get
            {
                return this.Get<HtmlInputPassword>("TagName=input", "id=Password", "name=Password");
            }
        }

        /// <summary>
        /// Log in button.
        /// </summary>
        public HtmlButton LoginButton
        {
            get
            {
                return this.Get<HtmlButton>("TagName=button", "type=submit", "InnerText=Log in");
            }
        }

        /// <summary>
        /// Username required field message span.
        /// </summary>
        public HtmlSpan UserNameRequiredMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=The Username field is required.");
            } 
        }

        /// <summary>
        /// Password required field message span.
        /// </summary>
        public HtmlSpan PasswordRequiredMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=The Password field is required.");
            }
        }

        /// <summary>
        /// Gets the incorrect user name and password message span.
        /// </summary>
        public HtmlSpan IncorrectUserNamePasswordMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=Incorrect Username/Password Combination");
            }
        }
    }
}
