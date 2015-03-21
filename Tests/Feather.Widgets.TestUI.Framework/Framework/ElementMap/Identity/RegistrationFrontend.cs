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
    }
}
