using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Media
{
    /// <summary>
    /// Provides access to MediaSelectorScreen
    /// </summary>
    public class MediaSelectorScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaSelectorScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public MediaSelectorScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the drag and drop label div.
        /// </summary>
        public HtmlDiv DragAndDropLabel
        {
            get
            {
                return this.EmptyMediaElementsParentDiv.Find.ByExpression<HtmlDiv>("tagName=div", "InnerText=or simply drag & drop it here");
            }
        }

        /// <summary>
        /// Gets the Cancel button element.
        /// Searching by this class is not stable, but since there are lots of Cancel buttons which are not visible
        /// this is necessarry to find the correct element
        /// </summary>
        public HtmlButton CancelButton
        {
            get
            {
                return this.Get<HtmlButton>("tagName=button", "class=btn btn-link  pull-left", "InnerText=Cancel");
            }
        }

        /// <summary>
        /// Gets the tooltip icon.
        /// </summary>
        public HtmlSpan Tooltip
        {
            get
            {
                return this.Get<HtmlSpan>("sf-popover-trigger=hover", "InnerText=i");
            }
        }

        /// <summary>
        /// Gets the upload image.
        /// </summary>
        /// <value>The upload image.</value>
        public HtmlAnchor UploadMediaFile
        {
            get
            {
                return this.MediaSelectorModalDialog.Find.ByExpression<HtmlAnchor>("ng-click=switchToUploadMode()", "InnerText=~Upload");
            }
        }

        /// <summary>
        /// Gets Done button.
        /// </summary>
        public HtmlButton DoneButton
        {
            get
            {
                return this.MediaSelectorModalDialog.Find.ByExpression<HtmlButton>("tagname=button", "InnerText=Done selecting");
            }
        }

        /// <summary>
        /// Gets Done button in media widget.
        /// </summary>
        public HtmlButton DoneButtonInMediaWidget
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Done selecting");
            }
        }

        /// <summary>
        /// Gets the image selector media file divs.
        /// </summary>
        /// <value>The image selector media file divs.</value>
        public ICollection<HtmlDiv> ImageFileDivs
        {
            get
            {
                return this.Find.AllByExpression<HtmlDiv>("tagName=div", "class=~Media-file ng-scope");
            }
        }

        /// <summary>
        /// Gets the document selector media file divs.
        /// </summary>
        /// <value>The document selector media file divs.</value>
        public ICollection<HtmlDiv> DocFileDivs
        {
            get
            {
                return this.Find.AllByExpression<HtmlDiv>("tagName=div", "class=~Media-file Media-file--doc ng-scope");
            }
        }

        /// <summary>
        /// Gets the video selector media file divs.
        /// </summary>
        /// <value>The video selector media file divs.</value>
        public ICollection<HtmlDiv> VideoFileDivs
        {
            get
            {
                return this.Find.AllByExpression<HtmlDiv>("tagName=div", "class=~Media-file Media-file--video ng-scope");
            }
        }

        /// <summary>
        /// Gets the image selector media folder divs.
        /// </summary>
        /// <value>The image selector media folder divs.</value>
        public ICollection<HtmlDiv> MediaFolderDivs
        {
            get
            {
                return this.Find.AllByExpression<HtmlDiv>("tagName=div", "class=Media-folder ng-scope");
            }
        }

        /// <summary>
        /// Gets the media thumbnail divs.
        /// </summary>
        /// <value>The media thumbnail divs.</value>
        public ICollection<HtmlDiv> MediaThumbnailDivs
        {
            get
            {
                return this.Find.AllByExpression<HtmlDiv>("tagName=div", "class=~Media-file-thumb-holder");
            }
        }

        /// <summary>
        /// Gets select image from your computer anchor link.
        /// </summary>
        public HtmlAnchor SelectMediaFileFromYourComputerLink
        {
            get
            {
                return this.EmptyMediaElementsParentDiv.Find.ByExpression<HtmlAnchor>("tagName=a", "InnerText=~from your computer");
            }
        }

        /// <summary>
        /// Gets the media selector thumbnail holder div.
        /// </summary>
        /// <value>The media selector thumbnail holder div.</value>
        public HtmlDiv ThumbnailHolderDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=Media-items ng-isolate-scope");
            }
        }

        /// <summary>
        /// Gets the not expanded arrow.
        /// </summary>
        /// <value>The not expanded arrow.</value>
        public HtmlAnchor NotExpandedArrow
        {
            get
            {
                return this.Get<HtmlAnchor>("class=sf-Tree-expander ng-scope");
            }
        }

        /// <summary>
        /// Gets the search box.
        /// </summary>
        /// <value>The search box.</value>
        public HtmlInputText SearchBox
        {
            get
            {
                return this.Get<HtmlInputText>("ng-change=sfSearchCallback()");
            }
        }

        /// <summary>
        /// Gets the no items found div.
        /// </summary>
        /// <value>The no items found div.</value>
        public HtmlDiv NoItemsFoundDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "innertext=~No items found");
            }
        }

        /// <summary>
        /// Gets no media icon element.
        /// </summary>
        public HtmlDiv NoMediaIcon
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=icon-no-image");
            }
        }

        /// <summary>
        /// Notes the media text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public HtmlControl NoMediaText(string text)
        {
            return this.Get<HtmlControl>("tagName=p", "class=~text-muted", "InnerText=" + text);
        }

        /// <summary>
        /// Gets the empty image elements parent div.
        /// </summary>
        /// <value>The empty image elements parent div.</value>
        private HtmlDiv EmptyMediaElementsParentDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=Media-upload-text text-center");
            }
        }

        /// <summary>
        /// Gets the image selector modal dialog.
        /// </summary>
        /// <value>The image selector modal dialog.</value>
        private HtmlDiv MediaSelectorModalDialog
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=~modal-dialog-2");
            }
        }
    }
}
