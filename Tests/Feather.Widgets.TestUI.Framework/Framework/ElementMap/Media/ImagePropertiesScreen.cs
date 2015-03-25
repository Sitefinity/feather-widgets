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
    public class ImagePropertiesScreen : HtmlElementContainer
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
        /// Gets the done button.
        /// </summary>
        /// <value>The done button.</value>
        public HtmlButton DoneButton
        {
            get
            {
                return this.ImagePropertiesModalDialog.Find.ByExpression<HtmlButton>("tagName=button", "InnerText=Done");
            }
        }

        /// <summary>
        /// Gets the change image button.
        /// </summary>
        /// <value>The change image button.</value>
        public HtmlButton ChangeImageButton
        {
            get
            {
                return this.ImagePropertiesModalDialog.Find.ByExpression<HtmlButton>("tagName=button", "InnerText=Change image");
            }
        }

        /// <summary>
        /// Gets the edit all properties button.
        /// </summary>
        /// <value>The edit all properties button.</value>
        public HtmlButton EditAllPropertiesButton
        {
            get
            {
                return this.ImagePropertiesModalDialog.Find.ByExpression<HtmlButton>("tagName=button", "InnerText=Edit all properties");
            }
        }

        private HtmlDiv ImagePropertiesModalDialog
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=~modal-dialog-1");
            }
        }
    }
}
