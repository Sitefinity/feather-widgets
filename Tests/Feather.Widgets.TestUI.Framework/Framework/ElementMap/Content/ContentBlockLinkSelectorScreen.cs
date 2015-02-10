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
        /// <value>The text to display.</value>
        public HtmlInputText TextToDisplay
        {
            get
            {
                return this.Get<HtmlInputText>("ng-model=sfSelectedItem.displayText");
            }
        }

        /// <summary>
        /// Gets the open in new window.
        /// </summary>
        /// <value>The open in new window.</value>
        public HtmlInputCheckBox OpenInNewWindow
        {
            get
            {
                return this.Get<HtmlInputCheckBox>("ng-model=sfSelectedItem.openInNewWindow");
            }
        }

        /// <summary>
        /// Gets the insert link.
        /// </summary>
        /// <value>The insert link.</value>
        public HtmlButton InsertLink
        {
            get
            {
                return this.Get<HtmlButton>("InnerText=Insert link");
            }
        }

        /// <summary>
        /// Gets the insert link disabled button.
        /// </summary>
        /// <value>The insert link disabled button.</value>
        public HtmlButton InsertLinkDisabledButton
        {
            get
            {
                return this.Get<HtmlButton>("InnerText=Insert link", "disabled=disabled");
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
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public HtmlInputText Email
        {
            get
            {
                return this.Get<HtmlInputText>("ng-model=sfSelectedItem.emailAddress");
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
    }
}