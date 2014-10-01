using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for news widget edit wrapper.
    /// </summary>
    public class NewsWidgetEditContentScreenWrapper : BaseWrapper
    {
        /// <summary>
        /// Selects which news to display in the widget designer
        /// </summary>
        /// <param name="mode">Which news to display</param>
        public void SelectWhichNewsToDisplay(string mode)
        {
            HtmlUnorderedList optionsList = EM.News.NewsWidgetContentScreen.WhichNewsToDisplayList
                .AssertIsPresent("Which news to display options list");

            HtmlListItem option = optionsList.AllItems.Where(a => a.InnerText.Contains(mode)).FirstOrDefault()
                .AssertIsPresent("Which news to display option" + mode);

            HtmlInputRadioButton optionButton = option.Find.ByExpression<HtmlInputRadioButton>("tagname=input")
                .AssertIsPresent("Which news to display option radio button");

            optionButton.Click();
        }

        /// <summary>
        /// Selects the taxonomy.
        /// </summary>
        /// <param name="taxonomy">The taxonomy.</param>
        public void SelectTaxonomy(string taxonomy)
        {
            ActiveBrowser.WaitForAsyncOperations();

            HtmlInputCheckBox optionButton = ActiveBrowser.Find.ByExpression<HtmlInputCheckBox>("id=" + taxonomy)
                .AssertIsPresent("Taxonomy option");

            optionButton.Click();

            ActiveBrowser.WaitForAsyncOperations();
        }

        /// <summary>
        /// Provide access to done button
        /// </summary>
        public void DoneSelectingButton()
        {
            HtmlButton shareButton = EM.News.NewsWidgetContentScreen.DoneSelectingButton
            .AssertIsPresent("Done selecting button");
            shareButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Select news item
        /// </summary>
        /// <param name="newsTitle">The title of the news item</param>
        public void SelectItem(string newsTitle)
        {
            var selectButtons = ActiveBrowser.Find.AllByExpression<HtmlButton>("class=btn btn-xs btn-default openSelectorBtn");
            foreach (var button in selectButtons)
            {
                if (button.IsVisible())
                {
                    button.Click();
                    break;
                }
            }

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();

            HtmlDiv sharedContentBlockList = EM.News.NewsWidgetContentScreen.NewsList
            .AssertIsPresent("Shared content list");

            var itemSpan = sharedContentBlockList.Find.ByExpression<HtmlSpan>("class=ng-binding", "InnerText=" + newsTitle);

            itemSpan.Wait.ForVisible();
            itemSpan.ScrollToVisible();
            itemSpan.MouseClick();
            this.DoneSelectingButton();
        }

        /// <summary>
        /// Save news widget
        /// </summary>
        public void SaveChanges()
        {
            HtmlButton saveButton = EM.News.NewsWidgetContentScreen.SaveChangesButton
            .AssertIsPresent("Save button");
            saveButton.Click();
        }
    }
}
