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
    /// Elements from UsersListEditScreen.
    /// </summary>
    public class UsersListEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersListEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public UsersListEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets list title.
        /// </summary>
        public HtmlContainerControl WhichProfileToDisplayLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("tagname=label", "innerText=Which profile type to use?");
            }
        }

        /// <summary>
        /// Gets list title.
        /// </summary>
        public HtmlContainerControl WhichUsersToDisplayLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("tagname=h4", "innerText=Which users to display?");
            }
        }

        /// <summary>
        /// Gets user profiles drop down.
        /// </summary>
        /// <value>Profiles drop down</value>
        public HtmlSelect ProfileDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("tagname=select", "ng-model=properties.ProfileTypeFullName.PropertyValue");
            }
        }

        /// <summary>
        /// All registered users radio button.
        /// </summary>
        /// <value>All registered users radio button</value>
        public HtmlInputRadioButton AllRegisteredUsersRadioButton
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=allUsers");
            }
        }

        /// <summary>
        /// Selected users radio button.
        /// </summary>
        /// <value>Selected users radio button</value>
        public HtmlInputRadioButton SelectedUsersRadioButton
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=selectedUsers");
            }
        }

        /// <summary>
        /// Users by roles radio button.
        /// </summary>
        /// <value>Users by roles radio button</value>
        public HtmlInputRadioButton UsersByRolesRadioButton
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=filterUsers");
            }
        }
    }
}
