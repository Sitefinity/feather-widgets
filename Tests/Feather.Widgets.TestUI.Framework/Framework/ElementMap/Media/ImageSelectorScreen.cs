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
        /// Gets Done button.
        /// </summary>
        public HtmlButton DoneButton
        {
            get
            {
                return this.ImageSelectorModalDialog.Find.ByExpression<HtmlButton>("tagname=button", "InnerText=Done");
            }
        }

        private HtmlDiv EmptyImageElementsParentDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=Media-upload-text text-center");
            }
        }

        private HtmlDiv ImageSelectorModalDialog
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=~modal-dialog-2");
            }
        }
    }
}
