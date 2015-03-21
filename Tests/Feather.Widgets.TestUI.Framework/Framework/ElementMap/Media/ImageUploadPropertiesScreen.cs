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
    /// Provides access to ImageUploadPropertiesScreen
    /// </summary>
    public class ImageUploadPropertiesScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertiesScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ImageUploadPropertiesScreen(Find find)
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
                return this.Get<HtmlInputText>("tagName=input", "ng-model=model.title", "class=form-control ng-pristine ng-untouched ng-valid");
            }
        }

        /// <summary>
        /// Gets the image alt text field.
        /// </summary>
        public ICollection<HtmlInputText> ImageAltTextFields
        {
            get
            {
                return this.Find.AllByExpression<HtmlInputText>("tagName=input", "ng-model=model.alternativeText");
            }
        }

        /// <summary>
        /// Gets the upload button.
        /// </summary>
        /// <value>The upload button.</value>
        public HtmlButton UploadButton
        {
            get
            {
                return this.Get<HtmlButton>("tagName=button", "InnerText=Upload", "ng-click=uploadImage()");
            }
        }

        /// <summary>
        /// Gets the upload disabled button.
        /// </summary>
        /// <value>The upload disabled button.</value>
        public HtmlButton UploadDisabledButton
        {
            get
            {
                return this.Get<HtmlButton>("tagName=button", "InnerText=Upload", "disabled=disabled");
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
                return this.Get<HtmlButton>("tagName=button", "InnerText=Cancel", "ng-click=cancelUpload()");
            }
        }

        /// <summary>
        /// Gets the activated title is required message.
        /// </summary>
        /// <value>The activated title is required message.</value>
        public HtmlDiv VisibleTitleIsRequiredMessage
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "InnerText=~Title is required!", "class=text-danger");
            }
        }

        /// <summary>
        /// Gets the cancel upload icon.
        /// </summary>
        /// <value>The cancel upload icon.</value>
        public HtmlAnchor CancelUploadIcon
        {
            get
            {
                return this.Get<HtmlAnchor>("tagName=a", "ng-click=cancelUpload()");
            }
        }

        /// <summary>
        /// Gets you must select library message.
        /// </summary>
        /// <value>You must select library message.</value>
        public HtmlDiv YouMustSelectLibraryMessage
        {
            get
            {
                return this.Get<HtmlDiv>("InnerText=~You must select the library in which the files ought to be uploaded.");
            }
        }

        /// <summary>
        /// Gets the select library button.
        /// </summary>
        /// <value>The select library button.</value>
        public ICollection<HtmlSpan> SelectButtons
        {
            get
            {
                return this.Find.AllByExpression<HtmlSpan>("ng-hide=isItemSelected()", "InnerText=Select");
            }
        }

        /// <summary>
        /// Gets the change library button.
        /// </summary>
        /// <value>The change library button.</value>
        public ICollection<HtmlSpan> ChangeButtons
        {
            get
            {
                return this.Find.AllByExpression<HtmlSpan>("ng-show=isItemSelected()", "InnerText=Change");
            }
        }

        /// <summary>
        /// Gets the categories and tags arrow.
        /// </summary>
        /// <value>The categories and tags arrow.</value>
        public HtmlSpan CategoriesAndTagsArrow
        {
            get
            {
                return this.Get<HtmlSpan>("InnerText=Categories and tags");
            }
        }

        /// <summary>
        /// Gets the add tag.
        /// </summary>
        /// <value>The add tag.</value>
        public HtmlInputText AddTag
        {
            get
            {
                return this.Get<HtmlInputText>("tagName=input", "placeholder=Add a tag");
            }
        }

        /// <summary>
        /// Gets the image upload properties modal dialog.
        /// </summary>
        /// <value>The image upload properties modal dialog.</value>
        private HtmlDiv ImageUploadPropertiesModalDialog
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=modal-content");
            }
        }

    }
}
