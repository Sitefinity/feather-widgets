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
    /// Provides access to ImagePropertiesScreen
    /// </summary>
    public class ImagePropertiesScreen : MediaPropertiesBaseScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertiesScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ImagePropertiesScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the image title text field.
        /// </summary>
        public HtmlInputText ImageTitleField
        {
            get
            {
                return this.Get<HtmlInputText>("tagName=input", "name=title");
            }
        }

        /// <summary>
        /// Gets the image alt text field.
        /// </summary>
        public HtmlInputText ImageAltTextField
        {
            get
            {
                return this.Get<HtmlInputText>("tagName=input", "name=alternativeText");
            }
        }

        /// <summary>
        /// Gets the image None alignment.
        /// </summary>
        public HtmlInputRadioButton AlignmentNone
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("tagName=input", "ng-model=model.alignment", "value=None");
            }
        }

        /// <summary>
        /// Gets the image Left alignment.
        /// </summary>
        public HtmlInputRadioButton AlignmentLeft
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("tagName=input", "ng-model=model.alignment", "value=Left");
            }
        }

        /// <summary>
        /// Gets the image Center alignment.
        /// </summary>
        public HtmlInputRadioButton AlignmentCenter
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("tagName=input", "ng-model=model.alignment", "value=Center");
            }
        }

        /// <summary>
        /// Gets the image Right alignment.
        /// </summary>
        public HtmlInputRadioButton AlignmentRight
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("tagName=input", "ng-model=model.alignment", "value=Right");
            }
        }

        /// <summary>
        /// Gets the image thumbnail selector.
        /// </summary>
        public HtmlSelect ThumbnailSelector
        {
            get
            {
                return this.Get<HtmlSelect>("tagName=select", "ng-model=sizeSelection");
            }
        }

        /// <summary>
        /// Gets the max width number.
        /// </summary>
        /// <value>The max width number.</value>
        public HtmlInputNumber MaxWidthNumber
        {
            get
            {
                return this.Get<HtmlInputNumber>("tagName=input", "ng-model=model.MaxWidth");
            }
        }

        /// <summary>
        /// Gets the max height number.
        /// </summary>
        /// <value>The max height number.</value>
        public HtmlInputNumber MaxHeightNumber
        {
            get
            {
                return this.Get<HtmlInputNumber>("tagName=input", "ng-model=model.MaxHeight");
            }
        }

        /// <summary>
        /// Gets the resize image selector.
        /// </summary>
        /// <value>The resize image selector.</value>
        public HtmlSelect ResizeImageSelector
        {
            get
            {
                return this.Get<HtmlSelect>("tagName=select", "ng-model=model.Method");
            }
        }

        /// <summary>
        /// Gets the quality selector.
        /// </summary>
        /// <value>The quality selector.</value>
        public HtmlSelect QualitySelector
        {
            get
            {
                return this.Get<HtmlSelect>("tagName=select", "ng-model=model.Quality");
            }
        }

        /// <summary>
        /// Gets the max width is required message.
        /// </summary>
        /// <value>The max width is required message.</value>
        public HtmlContainerControl WidthIsRequiredMessage
        {
            get
            {
                return this.Get<HtmlContainerControl>("tagName=p", "InnerText=Max width is required", "class=text-danger");
            }
        }

        /// <summary>
        /// Gets the max height is required message.
        /// </summary>
        /// <value>The max height is required message.</value>
        public HtmlContainerControl HeightIsRequiredMessage
        {
            get
            {
                return this.Get<HtmlContainerControl>("tagName=p", "InnerText=Max height is required", "class=text-danger");
            }
        }

        /// <summary>
        /// Gets the done resizing button.
        /// </summary>
        /// <value>The done resizing button.</value>
        public HtmlButton DoneResizingButton
        {
            get
            {
                return this.Get<HtmlButton>("tagName=button", "ng-disabled=~areCustomSizeOptionsValid()", "InnerText=Done");
            }
        }

        /// <summary>
        /// Gets the this image is A link check box.
        /// </summary>
        /// <value>The this image is A link check box.</value>
        public HtmlInputCheckBox ThisImageIsALinkCheckBox
        {
            get
            {
                return this.Get<HtmlInputCheckBox>("type=checkbox", "ng-model=properties.UseAsLink.PropertyValue");
            }
        }
    }
}
