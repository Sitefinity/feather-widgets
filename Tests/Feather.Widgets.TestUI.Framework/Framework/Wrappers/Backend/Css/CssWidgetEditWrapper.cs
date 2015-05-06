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
    /// This is the entry point class for css widget edit wrapper.
    /// </summary>
    public class CssWidgetEditWrapper : BaseWrapper
    {
        /// <summary>
        /// Fill css to the css widget
        /// </summary>
        /// <param name="css">The css value</param>
        public void FillCssToCssWidget(string css)
        {
            HtmlDiv editable = EM.Css
                                       .CssWidgetEditScreen
                                       .CodeMirrorLines
                                       .AssertIsPresent("Editable area");

            editable.ScrollToVisible();
            editable.Focus();
            editable.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(css);
        }

        /// <summary>
        /// Switch to Link to Css file
        /// </summary>
        public void SwitchToLinkToCssFile()
        {
            HtmlInputRadioButton linkToCssFile = EM.Css.CssWidgetEditScreen.LinkToCss
            .AssertIsPresent("Link to css file button");
            linkToCssFile.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Click select button
        /// </summary>
        public void ClickSelectButton()
        {
            HtmlButton selectButton = EM.Css.CssWidgetEditScreen.SelectButton
                .AssertIsPresent("Select button");
            selectButton.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Expand folder
        /// </summary>
        public void ExpandFolder(string folderName)
        {
            HtmlUnorderedList folderTree = EM.Css.CssWidgetEditScreen.FolderTree
                .AssertIsPresent("Folder tree");

            HtmlListItem listItem = folderTree.Find.ByExpression<HtmlListItem>("InnerText=" + folderName);
            var anchorExpand = listItem.Find.ByExpression<HtmlAnchor>("tagname=a");

            anchorExpand.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Select css file
        /// </summary>
        public void SelectCssFile(string fileName)
        {
            ActiveBrowser.RefreshDomTree();
            HtmlUnorderedList fileTree = EM.Css.CssWidgetEditScreen.FileTree
                .AssertIsPresent("File tree");

            HtmlListItem listItem = fileTree.Find.ByExpression<HtmlListItem>("InnerText=" + fileName);
            listItem.Focus();
            listItem.MouseClick();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
        }
    }
}
