using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Lists
{
    /// <summary>
    /// This is an entry point for ListsWrapper.
    /// </summary>
    public class ListsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies simple list template
        /// </summary>
        /// <param name="listTitle">list title</param>
        /// <param name="listItemsToVerify">list items to be verified</param>
        public void VerifySimpleListTemplate(string listTitle, string[] listItemsToVerify)
        {
            HtmlContainerControl listTitleH3 = this.EM.Lists.SimpleListsFrontend.ListTitleLabel.AssertIsPresent("list title");

            HtmlUnorderedList listItems = this.EM.Lists.SimpleListsFrontend.ListItemsUnorderedList.AssertIsPresent("unordered list of list items");
            Assert.AreEqual(listItems.Items.Count(), listItemsToVerify.Length, "Expected and actual count of list items are not equal");

            for (int i = 0; i < listItemsToVerify.Length; i++)
            {
                Assert.AreEqual(listItemsToVerify[i], listItems.Items[i].InnerText, "list item");
            }
        }
    }
}
