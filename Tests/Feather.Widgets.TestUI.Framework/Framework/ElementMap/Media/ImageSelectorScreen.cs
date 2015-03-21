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
    /// Provides access to ImageSelectorScreen
    /// </summary>
    public class ImageSelectorScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSelectorScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ImageSelectorScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets no images icon element.
        /// </summary>
        public HtmlDiv NoImagesIcon
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=icon-no-image");
            }
        }

        /// <summary>
        /// Gets no images paragraph text.
        /// </summary>
        public HtmlControl NoImagesText
        {
            get
            {
                return this.Get<HtmlControl>("tagName=p", "class=text-muted", "InnerText=No images");
            }
        }        

        /// <summary>
        /// Gets select image from your computer anchor link.
        /// </summary>
        public HtmlAnchor SelectImageFromYourComputerLink
        {
            get
            {
                return this.EmptyImageElementsParentDiv.Find.ByExpression<HtmlAnchor>("tagName=a", "InnerText=Select image from your computer");
            }
        }

        /// <summary>
        /// Gets the drag and drop label div.
        /// </summary>
        public HtmlDiv DragAndDropLabel
        {
            get
            {
                return this.EmptyImageElementsParentDiv.Find.ByExpression<HtmlDiv>("tagName=div", "InnerText=or simply drag & drop here");
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
        public HtmlAnchor UploadImage
        {
            get
            {
                return this.ImageSelectorModalDialog.Find.ByExpression<HtmlAnchor>("ng-click=switchToUploadMode()", "InnerText=Upload image");
            }
        }

        /// <summary>
        /// Gets Done button.
        /// </summary>
        public HtmlButton DoneButton
        {
            get
            {
                return this.ImageSelectorModalDialog.Find.ByExpression<HtmlButton>("tagname=button", "InnerText=Done");
            }
        }

        /// <summary>
        /// Gets the image selector media file divs.
        /// </summary>
        /// <value>The image selector media file divs.</value>
        public ICollection<HtmlDiv> ImageSelectorMediaImageFileDivs
        {
            get
            {
                return this.Find.AllByExpression<HtmlDiv>("tagName=div", "class=~Media-file ng-scope");
            }
        }

        /// <summary>
        /// Gets the image selector media folder divs.
        /// </summary>
        /// <value>The image selector media folder divs.</value>
        public ICollection<HtmlDiv> ImageSelectorMediaFolderDivs
        {
            get
            {
                return this.Find.AllByExpression<HtmlDiv>("tagName=div", "class=Media-folder ng-scope");
            }
        }

        /// <summary>
        /// Gets the empty image elements parent div.
        /// </summary>
        /// <value>The empty image elements parent div.</value>
        private HtmlDiv EmptyImageElementsParentDiv
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
        private HtmlDiv ImageSelectorModalDialog
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=~modal-dialog-2");
            }
        }

        /// <summary>
        /// Gets the image selector thumbnail holder div.
        /// </summary>
        /// <value>The image selector thumbnail holder div.</value>
        public HtmlDiv ImageSelectorThumbnailHolderDiv
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
    }
}
