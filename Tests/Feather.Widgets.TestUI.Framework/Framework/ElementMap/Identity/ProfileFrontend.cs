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
    /// Elements from Profile Frontend.
    /// </summary>
    public class ProfileFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ProfileFrontend(Find find)
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
        /// Gets old password field.
        /// </summary>
        public HtmlInputPassword OldPassword
        {
            get
            {
                return this.Get<HtmlInputPassword>("tagname=input", "id=OldPassword");
            }
        }

        /// <summary>
        /// Gets new password field.
        /// </summary>
        public HtmlInputPassword NewPassword
        {
            get
            {
                return this.Get<HtmlInputPassword>("tagname=input", "id=NewPassword");
            }
        }

        /// <summary>
        /// Gets re type password field.
        /// </summary>
        public HtmlInputPassword ReTypePassword
        {
            get
            {
                return this.Get<HtmlInputPassword>("tagname=input", "id=RepeatPassword");
            }
        }

        /// <summary>
        /// Gets the change password link 
        /// </summary>
        public HtmlAnchor ChangePasswordLink
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "InnerText=Change password");
            }
        }

        /// <summary>
        /// Gets save changes button.
        /// </summary>
        public HtmlButton SaveChanges
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Save changes");
            }
        }

        /// <summary>
        /// First name field message span.
        /// </summary>
        public HtmlSpan FirstNameRequiredMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=The FirstName profile field is required!");
            }
        }

        /// <summary>
        /// Last name field message span.
        /// </summary>
        public HtmlSpan LastNameRequiredMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=The LastName profile field is required!");
            }
        }

        /// <summary>
        /// Old password field message span.
        /// </summary>
        public HtmlSpan OldPasswordInvalidMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=The field OldPassword is invalid.");
            }
        }

        /// <summary>
        /// Password retype field message span.
        /// </summary>
        public HtmlSpan NewPasswordInvalidMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=The field NewPassword is invalid.");
            }
        }

        /// <summary>
        /// Password retype field message span.
        /// </summary>
        public HtmlSpan PasswordRetypeInvalidMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=The field RepeatPassword is invalid.");
            }
        }

        /// <summary>
        /// New and retype password do not match field message span.
        /// </summary>
        public HtmlSpan NewAndRetypePasswordDoNotMatchMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=~'NewPassword' and 'RepeatPassword' do not match.");
            }
        }

        /// <summary>
        /// Retype and new password do not match field message span.
        /// </summary>
        public HtmlSpan RetypeAndNewPasswordDoNotMatchMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=~'RepeatPassword' and 'NewPassword' do not match.");
            }
        }

        /// <summary>
        /// Password length message span.
        /// </summary>
        public HtmlSpan PasswordLengthMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=Password length must be at least 7 symbols long!");
            }
        }

        /// <summary>
        /// Successfully saved first and last name message div.
        /// </summary>
        public HtmlDiv SuccessfullySavedMessage
        {
            get
            {
                return this.Get<HtmlDiv>("TagName=div", "InnerText=Your changes are saved");
            }
        }

        /// <summary>
        /// Gets the edit profile link 
        /// </summary>
        public HtmlAnchor EditProfileLink
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "InnerText=Edit profile");
            }
        }

        /// <summary>
        /// Gets the upload photo link 
        /// </summary>
        public HtmlAnchor UploadPhotoLink
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "InnerText=Upload photo");
            }
        }

        /// <summary>
        /// Gets img of user avatar 
        /// </summary>
        public HtmlImage UserAvatar
        {
            get
            {
                return this.Get<HtmlImage>("tagname=img", "data-sf-role=edit-profile-user-image");
            }
        }

        /// <summary>
        /// Gets default user avatar 
        /// </summary>
        public HtmlImage DefaultUserAvatar
        {
            get
            {
                return this.Get<HtmlImage>("tagname=img", "src=/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png");
            }
        }
    }
}
