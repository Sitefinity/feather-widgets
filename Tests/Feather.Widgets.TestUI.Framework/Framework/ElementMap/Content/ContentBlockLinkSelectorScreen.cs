using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Content
{
    /// <summary>
    /// Provides access to ContentBlock -> Link Selector Screen
    /// </summary>
    public class ContentBlockLinkSelectorScreen : HtmlElementContainer
    { 
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockLinkSelectorScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ContentBlockLinkSelectorScreen(Find find) : base(find)
        {
        }

        /// <summary>
        /// Gets the web address.
        /// </summary>
        /// <value>The web address.</value>
        public HtmlInputText WebAddress
        {
            get
            {
                return this.Get<HtmlInputText>("ng-model=sfSelectedItem.webAddress");
            }
        }

        /// <summary>
        /// Gets the text to display.
        /// </summary>
        /// <param name="tabIndex">the index of the link selector tab</param>
        /// <returns>The text to display.</returns>
        public HtmlInputText TextToDisplay(int tabIndex)
        {
            string textToDisplayId = "textToDisplay" + tabIndex.ToString();

            return this.Get<HtmlInputText>("id=" + textToDisplayId, "ng-model=sfSelectedItem.displayText");
        }

        /// <summary>
        /// Gets the open in new window.
        /// </summary>
        /// <param name="tabIndex">The index of the link selector tab.</param>
        /// <returns>The open in new window.</returns>
        public HtmlInputCheckBox OpenInNewWindow(int tabIndex)
        {
            string openInNewWinId = "openInNewWin" + tabIndex.ToString();
            
            return this.Get<HtmlInputCheckBox>("id=" + openInNewWinId, "ng-model=sfSelectedItem.openInNewWindow");
        }

        /// <summary>
        /// Gets the insert link.
        /// </summary>
        /// <value>The insert link.</value>
        public HtmlButton InsertLinkButton
        {
            get
            {
                return this.Get<HtmlButton>("InnerText=Insert link");
            }
        }

        /// <summary>
        /// Gets the cancel button.
        /// </summary>
        /// <value>The cancel button.</value>
        public HtmlButton CancelButton
        {
            get
            {
                return this.Get<HtmlButton>("type=button", "ng-click=cancel()", "InnerText=Cancel");
            }
        }

        /// <summary>
        /// Gets the anchor selector.
        /// </summary>
        /// <value>The anchor selector.</value>
        public HtmlSelect AnchorSelector
        {
            get
            {
                return this.Get<HtmlSelect>("ng-model=sfSelectedItem.selectedAnchor");
            }
        }

        /// <summary>
        /// Gets the how to insert an anchor link.
        /// </summary>
        /// <value>The how to insert an anchor link.</value>
        public HtmlAnchor HowToInsertAnAnchorLink
        {
            get
            {
                return this.Get<HtmlAnchor>("href=https://github.com/Sitefinity/feather/wiki/How-to-insert-an-anchor", "InnerText=How to insert an anchor?");
            }
        }

        /// <summary>
        /// Gets the anchor alert info container.
        /// </summary>
        /// <value>The anchor alert info container.</value>
        public HtmlDiv AnchorAlertInfoContainer
        {
            get
            {
                return this.Get<HtmlDiv>("class=alert alert-info", "role=alert");
            }
        }

        /// <summary>
        /// Gets the no anchors have been inserted text.
        /// </summary>
        /// <value>The no anchors have been inserted text.</value>
        public HtmlSpan NoAnchorsHaveBeenInsertedText
        {
            get
            {
                return this.AnchorAlertInfoContainer.Find.ByExpression<HtmlSpan>("tagname=span", "InnerText=~No anchors have been inserted in the text.");
            }
        }

        /// <summary>
        /// Gets the how to insert an anchor link in empty anchor dialog.
        /// </summary>
        /// <value>The how to insert an anchor link in empty anchor dialog.</value>
        public HtmlAnchor HowToInsertAnAnchorLinkInEmptyAnchorDialog
        {
            get
            {
                return this.AnchorAlertInfoContainer.Find.ByExpression<HtmlAnchor>("href=https://github.com/Sitefinity/feather/wiki/How-to-insert-an-anchor", "InnerText=How to insert an anchor?");
            }
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public HtmlInputEmail Email
        {
            get
            {
                return this.Get<HtmlInputEmail>("ng-model=sfSelectedItem.emailAddress");
            }
        }

        /// <summary>
        /// Gets the invalid email message.
        /// </summary>
        /// <value>The invalid email message.</value>
        public HtmlSpan InvalidEmailMessage
        {
            get
            {
                return this.Get<HtmlSpan>("InnerText=You have entered an invalid email address");
            }
        }

        /// <summary>
        /// Gets the test this link.
        /// </summary>
        /// <value>The test this link.</value>
        public HtmlDiv TestThisLink
        {
            get
            {
                return this.Get<HtmlDiv>("class=~form-group", "InnerText=~Test this link:");
            }
        }

        /// <summary>
        /// Gets the tabs navigation wrapper element.
        /// </summary>
        public HtmlDiv TabsNavigation
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=form-group nav-wrapper");
            }
        }
    }
}