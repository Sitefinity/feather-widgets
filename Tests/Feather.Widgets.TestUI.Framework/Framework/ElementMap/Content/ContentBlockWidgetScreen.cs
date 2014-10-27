using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;
using Telerik.TestUI.Core.ElementMap;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Content
{
    /// <summary>
    /// Provides access to content block widget screen
    /// </summary>
    public class ContentBlockWidgetScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockWidgetScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ContentBlockWidgetScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets Content block widget body.
        /// </summary>
        public HtmlDiv ContentBlockWidgetPlaceholder
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "id=viewsPlaceholder");
            }
        }

        /// <summary>
        /// Gets changes button.
        /// </summary>
        public HtmlButton SaveChangesButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Save");
            }
        }

        /// <summary>
        /// Gets editable area.
        /// </summary>
        public HtmlTableCell EditableArea
        {
            get
            {
                return this.Get<HtmlTableCell>("tagname=td", "class=k-editable-area");
            }
        }

        /// <summary>
        /// Gets share content title.
        /// </summary>
        public HtmlInputText ShareContentTitle
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "id=sharedContentTitle");
            }
        }

        /// <summary>
        /// Gets share button.
        /// </summary>
        public HtmlButton ShareButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Share this content");
            }
        }

        /// <summary>
        /// Gets unshare button.
        /// </summary>
        public HtmlButton UnshareButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Yes, Unshare this content");
            }
        }

        /// <summary>
        /// Gets Content block list with shared content blocks.
        /// </summary>
        public HtmlDiv ContentBlockList
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=list-group s-items-list-wrp");
            }
        }

        /// <summary>
        /// Gets done selecting button.
        /// </summary>
        public HtmlButton DoneSelectingButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Done selecting");
            }
        }

        /// <summary>
        /// Gets create content.
        /// </summary>
        public HtmlAnchor CreateContent
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "class=sfAddContentLnk");
            }
        }

        /// <summary>
        /// Gets the select provider dropdown.
        /// </summary>
        /// <value>The select provider dropdown.</value>
        public HtmlAnchor SelectProviderDropdown
        {
            get
            {
                return this.Get<HtmlAnchor>("class=btn btn-default dropdown-toggle ng-binding");
            }
        }

        /// <summary>
        /// Gets title is required.
        /// </summary>
        public HtmlControl TitleIsRequired
        {
            get
            {
                return this.Get<HtmlControl>("tagname=p", "InnerText=Title is required!");
            }
        }

        /// <summary>
        /// Gets cancel button.
        /// </summary>
        public HtmlAnchor CancelButton
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "class=btn btn-link ng-scope");
            }
        }
    }
}
