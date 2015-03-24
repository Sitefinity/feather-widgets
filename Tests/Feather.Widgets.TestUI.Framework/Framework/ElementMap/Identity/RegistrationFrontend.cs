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
    /// Elements from RegistrationFrontend.
    /// </summary>
    public class RegistrationFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public RegistrationFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets first name field.
        /// </summary>
        public HtmlInputText FirstName
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "id=Profile_FirstName_");
            }
        }

        /// <summary>
        /// Gets last name field.
        /// </summary>
        public HtmlInputText LastName
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "id=Profile_LastName_");
            }
        }

        /// <summary>
        /// Gets email field.
        /// </summary>
        public HtmlInputText Email
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "id=Email");
            }
        }

        /// <summary>
        /// Gets username field.
        /// </summary>
        public HtmlInputText Username
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "id=UserName");
            }
        }

        /// <summary>
        /// Gets password field.
        /// </summary>
        public HtmlInputPassword Password
        {
            get
            {
                return this.Get<HtmlInputPassword>("tagname=input", "id=Password");
            }
        }

        /// <summary>
        /// Gets re type password field.
        /// </summary>
        public HtmlInputPassword ReTypePassword
        {
            get
            {
                return this.Get<HtmlInputPassword>("tagname=input", "id=ReTypePassword");
            }
        }

        /// <summary>
        /// Gets register button.
        /// </summary>
        public HtmlButton RegisterButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Register");
            }
        }

        /// <summary>
        /// Email required field message span.
        /// </summary>
        public HtmlSpan EmailRequiredMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=The Email field is required.");
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
        /// Password retype field message span.
        /// </summary>
        public HtmlSpan PasswordRetypeMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=~'Re-type password' and 'Password' do not match.");
            }
        }

        /// <summary>
        /// Password length message div.
        /// </summary>
        public HtmlDiv PasswordLengthMessage
        {
            get
            {
                return this.Get<HtmlDiv>("TagName=div", "InnerText=Password length minimum: 7. Non-alphanumeric characters required: 0.");
            }
        }

        /// <summary>
        /// Duplicate user name message div.
        /// </summary>
        public HtmlDiv DuplicateUserNameMessage
        {
            get
            {
                return this.Get<HtmlDiv>("TagName=div", "InnerText=Please enter a different user name.");
            }
        }

        /// <summary>
        /// Duplicate email message div.
        /// </summary>
        public HtmlDiv DuplicateEmailMessage
        {
            get
            {
                return this.Get<HtmlDiv>("TagName=div", "InnerText=The email address that you entered is already in use. Please enter a different email address.");
            }
        }
    }
}
