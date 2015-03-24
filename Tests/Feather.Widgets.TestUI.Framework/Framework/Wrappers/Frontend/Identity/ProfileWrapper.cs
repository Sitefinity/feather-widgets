using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Identity
{
    /// <summary>
    /// This is the entry point class for profile on the frontend.
    /// </summary>
    public class ProfileWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify user names
        /// </summary>
        /// <param name="userNames">User first and last names</param>
        public void VerifyUserFirstAndLastName(string userNames)
        {
           ActiveBrowser.Find.ByExpression<HtmlControl>("tagname=h3", "InnerText=" + userNames)
                                                    .AssertIsPresent("User with this name" + userNames + "not found");
        }

        /// <summary>
        /// Verify user email address
        /// </summary>
        /// <param name="emailAddress">User email address</param>
        public void VerifyUserEmailAddress(string emailAddress)
        {
            ActiveBrowser.Find.ByExpression<HtmlControl>("tagname=p", "InnerText=" + emailAddress)
                                                     .AssertIsPresent("User with this email" + emailAddress + "not found");
        }
    }
}
