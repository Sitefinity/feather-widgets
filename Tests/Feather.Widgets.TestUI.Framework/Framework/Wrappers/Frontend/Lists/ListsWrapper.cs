using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

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
        public void VerifySimpleListTemplate(string listTitleToVerify, string[] listItemsToVerify)
        {
            HtmlContainerControl listTitle = this.EM.Lists.SimpleListFrontend.ListTitleLabel.AssertIsPresent("list title");
            Assert.AreEqual(listTitleToVerify, listTitle.InnerText, "list title");

            HtmlUnorderedList listItems = this.EM.Lists.SimpleListFrontend.ListItemsUnorderedList.AssertIsPresent("unordered list of list items");
            Assert.AreEqual(listItems.Items.Count(), listItemsToVerify.Length, "Expected and actual count of list items are not equal");

            for (int i = 0; i < listItemsToVerify.Length; i++)
            {
                Assert.AreEqual(listItemsToVerify[i], listItems.Items[i].InnerText, "list item");
            }
        }

        /// <summary>
        /// Verifies expanded list template
        /// </summary>
        /// <param name="listTitle">list title</param>
        /// <param name="listItemsToVerify">list items to be verified</param>
        public void VerifyExpandedListTemplate(string listTitleToVerify, Dictionary<string, string> listItemsToVerify)
        {
            HtmlContainerControl listTitle = this.EM.Lists.ExpandedListFrontend.ListTitleLabel.AssertIsPresent("list title");
            Assert.AreEqual(listTitleToVerify, listTitle.InnerText, "list title");

            List<HtmlDiv> listItemDivs = this.EM.Lists.ExpandedListFrontend.ListItemsDivs;
            Assert.IsNotNull(listItemDivs, "List of div elements is null");
            Assert.AreEqual(listItemsToVerify.Count, listItemDivs.Count, "Expected and actual count of list items are not equal");

            for (int i = 0; i < listItemsToVerify.Count; i++)
            {
                Assert.AreEqual(listItemsToVerify.Keys.ElementAt(i), listItemDivs[i].ChildNodes[0].InnerText, "list item title");
                Assert.AreEqual(listItemsToVerify.Values.ElementAt(i), listItemDivs[i].ChildNodes[1].InnerText, "list item content");
            }
        }

        /// <summary>
        /// Verifies expanded list template on Bootstrap
        /// </summary>
        /// <param name="listTitle">list title</param>
        /// <param name="listItemsToVerify">list items to be verified</param>
        public void VerifyExpandedListTemplateOnBootstrap(string listTitleToVerify, Dictionary<string, string> listItemsToVerify)
        {
            HtmlContainerControl listTitle = this.EM.Lists.ExpandedListFrontend.ListTitleLabel.AssertIsPresent("list title");
            Assert.AreEqual(listTitleToVerify, listTitle.InnerText, "list title");

            List<HtmlDiv> listItemDivs = this.EM.Lists.ExpandedListFrontend.ListItemsDivsOnBootstrap;
            Assert.IsNotNull(listItemDivs, "List of div elements is null");
            Assert.AreEqual(listItemsToVerify.Count, listItemDivs.Count, "Expected and actual count of list items are not equal");

            for (int i = 0; i < listItemsToVerify.Count; i++)
            {
                Assert.AreEqual(listItemsToVerify.Keys.ElementAt(i), listItemDivs[i].ChildNodes[0].InnerText, "list item title");
                Assert.AreEqual(listItemsToVerify.Values.ElementAt(i), listItemDivs[i].ChildNodes[1].InnerText, "list item content");
            }
        }

        /// <summary>
        /// Verifies expandable list template
        /// </summary>
        /// <param name="listTitle">list title</param>
        /// <param name="listItemsToVerify">list items to be verified</param>
        public void VerifyExpandableListTemplate(string listTitleToVerify, Dictionary<string, string> listItemsToVerify)
        {
            HtmlContainerControl listTitle = this.EM.Lists.ExpandableListFrontend.ListTitleLabel.AssertIsPresent("list title");
            Assert.AreEqual(listTitleToVerify, listTitle.InnerText, "list title");

            List<HtmlDiv> listItemDivs = this.EM.Lists.ExpandableListFrontend.ListItemsDivs;
            Assert.IsNotNull(listItemDivs, "List of div elements is null");
            Assert.AreEqual(listItemsToVerify.Count, listItemDivs.Count, "Expected and actual count of list items are not equal");

            HtmlContainerControl expandAll = this.EM.Lists.ExpandableListFrontend.ExpandAll;
            HtmlContainerControl collapseAll = this.EM.Lists.ExpandableListFrontend.CollapseAll;

            //// verify list items before expanding all items
            expandAll.AssertIsPresent("expand all");
            collapseAll.AssertIsNotVisible("collapse all");

            for (int i = 0; i < listItemsToVerify.Count; i++)
            {
                Assert.AreEqual(listItemsToVerify.Keys.ElementAt(i), listItemDivs[i].ChildNodes[0].InnerText, "list item title");
                listItemDivs[i].ChildNodes[0].As<HtmlSpan>().AssertIsPresent("list item title");
                listItemDivs[i].ChildNodes[1].As<HtmlDiv>().AssertIsNotVisible("list item content");
            }

            expandAll.Click();

            //// verify list items after expanding all items
            expandAll.AssertIsNotVisible("expand all");
            collapseAll.AssertIsPresent("collapse all");

            for (int i = 0; i < listItemsToVerify.Count; i++)
            {
                Assert.AreEqual(listItemsToVerify.Keys.ElementAt(i), listItemDivs[i].ChildNodes[0].InnerText, "list item title");
                Assert.AreEqual(listItemsToVerify.Values.ElementAt(i), listItemDivs[i].ChildNodes[1].InnerText, "list item content");
                listItemDivs[i].ChildNodes[0].As<HtmlSpan>().AssertIsPresent("list item title");
                listItemDivs[i].ChildNodes[1].As<HtmlDiv>().AssertIsPresent("list item content");
            }

            collapseAll.Click();

            for (int i = 0; i < listItemsToVerify.Count; i++)
            {
                listItemDivs[i].ChildNodes[0].As<HtmlSpan>().AssertIsPresent("list item title");
                listItemDivs[i].ChildNodes[1].As<HtmlDiv>().AssertIsNotVisible("list item content");
            }
        }

        /// <summary>
        /// Verifies expandable list template on Bootstrap
        /// </summary>
        /// <param name="listTitle">list title</param>
        /// <param name="listItemsToVerify">list items to be verified</param>
        public void VerifyExpandableListTemplateOnBootstrap(string listTitleToVerify, Dictionary<string, string> listItemsToVerify)
        {
            HtmlContainerControl listTitle = this.EM.Lists.ExpandableListFrontend.ListTitleLabel.AssertIsPresent("list title");
            Assert.AreEqual(listTitleToVerify, listTitle.InnerText, "list title");

            List<HtmlDiv> listItemDivs = this.EM.Lists.ExpandableListFrontend.ListItemsDivsOnBootstrap;
            Assert.IsNotNull(listItemDivs, "List of div elements is null");
            Assert.AreEqual(listItemsToVerify.Count, listItemDivs.Count, "Expected and actual count of list items are not equal");

            HtmlContainerControl expandAll = this.EM.Lists.ExpandableListFrontend.ExpandAll;
            HtmlContainerControl collapseAll = this.EM.Lists.ExpandableListFrontend.CollapseAll;

            //// verify list items before expanding all items
            expandAll.AssertIsPresent("expand all");
            collapseAll.AssertIsNotVisible("collapse all");

            for (int i = 0; i < listItemsToVerify.Count; i++)
            {
                Assert.AreEqual(listItemsToVerify.Keys.ElementAt(i), listItemDivs[i].ChildNodes[0].InnerText, "list item title");
                listItemDivs[i].ChildNodes[0].As<HtmlAnchor>().AssertIsPresent("list item title");
                listItemDivs[i].ChildNodes[1].As<HtmlDiv>().AssertIsNotVisible("list item content");
            }

            expandAll.Click();

            //// verify list items after expanding all items
            expandAll.AssertIsNotVisible("expand all");
            collapseAll.AssertIsPresent("collapse all");

            for (int i = 0; i < listItemsToVerify.Count; i++)
            {
                Assert.AreEqual(listItemsToVerify.Keys.ElementAt(i), listItemDivs[i].ChildNodes[0].InnerText, "list item title");
                Assert.AreEqual(listItemsToVerify.Values.ElementAt(i), listItemDivs[i].ChildNodes[1].InnerText, "list item content");
                listItemDivs[i].ChildNodes[0].As<HtmlAnchor>().AssertIsPresent("list item title");
                listItemDivs[i].ChildNodes[1].As<HtmlDiv>().AssertIsPresent("list item content");
            }

            collapseAll.Click();

            for (int i = 0; i < listItemsToVerify.Count; i++)
            {
                listItemDivs[i].ChildNodes[0].As<HtmlAnchor>().AssertIsPresent("list item title");
                listItemDivs[i].ChildNodes[1].As<HtmlDiv>().AssertIsNotVisible("list item content");
            }
        }

        /// <summary>
        /// Verifies anchor list template
        /// </summary>
        /// <param name="listTitle">list title</param>
        /// <param name="listItemsToVerify">list items to be verified</param>
        public void VerifyAnchorListTemplate(string listTitleToVerify, Dictionary<string, string> listItemsToVerify)
        {
            //// verify list title
            HtmlContainerControl listTitleAnchor = this.EM.Lists.AnchorListFrontend.ListTitleWithAnchor.AssertIsPresent("list title anchor");
            Assert.AreEqual(listTitleToVerify, listTitleAnchor.InnerText, "list title anchor");

            //// verify unordered list of anchors
            HtmlUnorderedList listItemAnchors = this.EM.Lists.AnchorListFrontend.ListItemsUnorderedList.AssertIsPresent("unordered list of list items");
            Assert.AreEqual(listItemAnchors.Items.Count(), listItemsToVerify.Count, "Expected and actual count of list items are not equal");

            for (int i = 0; i < listItemsToVerify.Count; i++)
            {
                Assert.AreEqual(listItemsToVerify.Keys.ElementAt(i), listItemAnchors.Items[i].InnerText, "list item");
                Assert.IsTrue(listItemAnchors.Items[i].ChildNodes[0].TagName.Equals("a"));
            }

            //// verify expanded list
            List<HtmlDiv> listItemDivs = this.EM.Lists.AnchorListFrontend.ListItemsDivs;
            Assert.IsNotNull(listItemDivs, "List of div elements is null");
            Assert.AreEqual(listItemsToVerify.Count, listItemDivs.Count, "Expected and actual count of list items are not equal");

            for (int i = 0; i < listItemsToVerify.Count; i++)
            {
                Assert.AreEqual(listItemsToVerify.Keys.ElementAt(i), listItemDivs[i].ChildNodes[0].InnerText, "list item title");
                Assert.AreEqual(listItemsToVerify.Values.ElementAt(i), listItemDivs[i].ChildNodes[1].InnerText, "list item content");
                Assert.AreEqual("Back to top", listItemDivs[i].ChildNodes[2].InnerText, "Back to top link");
                listItemDivs[i].ChildNodes[0].As<HtmlContainerControl>().AssertIsPresent("list item title");
                listItemDivs[i].ChildNodes[1].As<HtmlDiv>().AssertIsPresent("list item content");
                listItemDivs[i].ChildNodes[2].As<HtmlContainerControl>().AssertIsPresent("list item content");
            }
        }

        /// <summary>
        /// Verifies pages list template
        /// </summary>
        /// <param name="listTitle">list title</param>
        /// <param name="listItemsToVerify">list items to be verified</param>
        public void VerifyPagesListTemplate(string listTitleToVerify, Dictionary<string, string> listItemsToVerify)
        {
            //// verify list title
            HtmlContainerControl listTitle = this.EM.Lists.PagesListFrontend.ListTitleLabel.AssertIsPresent("list title");
            Assert.AreEqual(listTitleToVerify, listTitle.InnerText, "list title");

            //// verify unordered list of links
            HtmlUnorderedList listItemLinks = this.EM.Lists.PagesListFrontend.ListItemsUnorderedList.AssertIsPresent("unordered list of list items");
            Assert.AreEqual(listItemLinks.Items.Count(), listItemsToVerify.Count, "Expected and actual count of list items are not equal");

            for (int i = 0; i < listItemsToVerify.Count; i++)
            {
                Assert.AreEqual(listItemsToVerify.Keys.ElementAt(i), listItemLinks.Items[i].InnerText, "list item title");
                Assert.IsTrue(listItemLinks.Items[i].ChildNodes[0].TagName.Equals("a"));

                //// verify details of list item
                listItemLinks.Items[i].ChildNodes[0].As<HtmlAnchor>().Click();
                ActiveBrowser.WaitForUrl("/lists/", true);

                HtmlDiv listItemDetails = this.EM.Lists.PagesListFrontend.ListItemsDiv.AssertIsPresent("list item details");
                Assert.AreEqual(listItemsToVerify.Keys.ElementAt(i), listItemDetails.ChildNodes[0].InnerText, "list item title in details");
                Assert.AreEqual(listItemsToVerify.Values.ElementAt(i), listItemDetails.ChildNodes[1].InnerText, "list item content in details");

                //// go back to the main list page
                ActiveBrowser.GoBack();
            }
        }
    }
}
