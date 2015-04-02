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
        /// Gets the editable HTML area.
        /// </summary>
        /// <value>The editable HTML area.</value>
        public HtmlTextArea EditableHtmlArea
        {
            get
            {
                return this.EditableArea.Find.ByExpression<HtmlTextArea>("class=html k-content ng-pristine ng-untouched ng-valid ng-scope");
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
                return this.Get<HtmlAnchor>("tagname=a", "class=~sfAddContentLnk");
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

        /// <summary>
        /// Gets advanced button.
        /// </summary>
        public HtmlAnchor AdvancedButton
        {
            get
            {
                return this.Get<HtmlAnchor>("ng-hide=isCurrentView('PropertyGrid')");
            }
        }

        /// <summary>
        /// Gets enable social share.
        /// </summary>
        public HtmlInputText EnableSocialSharing
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "id=prop-EnableSocialSharing");
            }
        }

        /// <summary>
        /// Gets Content block widget footer.
        /// </summary>
        public HtmlDiv ContentBlockWidgetFooter
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "placeholder=modal-footer");
            }
        }

        /// <summary>
        /// Gets the HTML button.
        /// </summary>
        /// <value>The HTML button.</value>
        public HtmlButton HtmlButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=HTML");
            }
        }

        /// <summary>
        /// Gets the design button.
        /// </summary>
        /// <value>The design button.</value>
        public HtmlButton DesignButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Design");
            }
        }

        /// <summary>
        /// Gets the link selector.
        /// </summary>
        /// <value>The link selector.</value>
        public HtmlAnchor LinkSelector
        {
            get
            {
                return this.Get<HtmlAnchor>("title=Insert hyperlink");
            }
        }

        /// <summary>
        /// Gets the full screen.
        /// </summary>
        /// <value>The full screen.</value>
        public HtmlAnchor FullScreen
        {
            get
            {
                return this.Get<HtmlAnchor>("ng-click=toggleFullScreen()");
            }
        }

        /// <summary>
        /// Gets the modal dialog not full screen div.
        /// </summary>
        /// <value>The modal dialog not full screen div.</value>
        public HtmlDiv ModalDialogNotFullScreenDiv
        {
            get
            {
                return this.Get<HtmlDiv>("class=modal-dialog modal-lg", "ng-class={'modal-sm': size == 'sm', 'modal-lg': size == 'lg'}");
            }
        }

        /// <summary>
        /// Gets the modal dialog full screen div.
        /// </summary>
        /// <value>The modal dialog full screen div.</value>
        public HtmlDiv ModalDialogFullScreenDiv
        {
            get
            {
                return this.Get<HtmlDiv>("class=modal-dialog modal-lg modal-full-screen", "ng-class={'modal-sm': size == 'sm', 'modal-lg': size == 'lg'}");
            }
        }

        /// <summary>
        /// Gets the buttons container.
        /// </summary>
        /// <value>The buttons container.</value>
        public ICollection<HtmlUnorderedList> ButtonsContainers
        {
            get
            {
                return this.Find.AllByExpression<HtmlUnorderedList>("class=k-editor-toolbar ng-scope", "data-role=editortoolbar");
            }
        }

        /// <summary>
        /// Gets the image selector.
        /// </summary>
        /// <value>The image selector anchor link.</value>
        public HtmlAnchor ImageSelector
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "title=Insert image");
            }
        }

        /// <summary>
        /// Gets the document selector.
        /// </summary>
        /// <value>The document selector.</value>
        public HtmlAnchor DocumentSelector
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "title=Insert file");
            }
        }
    }
}
