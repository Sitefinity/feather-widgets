using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Forms
{
    /// <summary>
    /// Elements from Forms frontend.
    /// </summary>
    public class FormsFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormsFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public FormsFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the textbox field on frontend
        /// </summary>
        public HtmlDiv TextboxField
        {
            get
            {
                return this.Get<HtmlDiv>("data-sf-role=text-field-container");
            }
        }

        /// <summary>
        /// Gets the checkboxes field on frontend
        /// </summary>
        public HtmlDiv CheckboxesField
        {
            get
            {
                return this.Find.ByExpression<HtmlDiv>("data-sf-role=checkboxes-field-container");
            }
        }

        /// <summary>
        /// Gets the multiple choice field on frontend
        /// </summary>
        public HtmlDiv MultipleChoiceField
        {
            get
            {
                return this.Find.ByExpression<HtmlDiv>("data-sf-role=multiple-choice-field-container");
            }
        }

        /// <summary>
        /// Gets the dropdown list field on frontend
        /// </summary>
        public HtmlDiv DropdownListField
        {
            get
            {
                return this.Find.ByExpression<HtmlDiv>("data-sf-role=dropdown-list-field-container");
            }
        }

        /// <summary>
        /// Gets the paragraph text field on frontend
        /// </summary>
        public HtmlDiv ParagraphTextField
        {
            get
            {
                return this.Find.ByExpression<HtmlDiv>("data-sf-role=paragraph-text-field-container");
            }
        }

        /// <summary>
        /// Gets the text field on frontend
        /// </summary>
        public HtmlInputText TextField
        {
            get
            {
                return this.Get<HtmlInputText>("TagName=input", "Class=form-control");
            }
        }

        /// <summary>
        /// Gets the paragraph text box on frontend
        /// </summary>
        public HtmlTextArea ParagraphTextBox
        {
            get
            {
                return this.Get<HtmlTextArea>("TagName=textarea");
            }
        }

        /// <summary>
        /// Gets the submit button on frontend
        /// </summary>
        public HtmlButton SubmitButton
        {
            get
            {
                return this.Get<HtmlButton>("TagName=button", "type=submit");
            }
        }

        /// <summary>
        /// Gets the success message after submit on frontend
        /// </summary>
        public HtmlDiv SuccessMessage
        {
            get
            {
                return this.Find.ByExpression<HtmlDiv>("TagName=div", "innertext=Success! Thanks for filling out our form!");
            }
        }
        /// <summary>
        /// Gets the message after form is deleted "The specified form no longer exists or is currently unpublished. "
        /// </summary>
        public HtmlDiv DeleteFormInUseMessage
        {
            get
            {
                return this.Get<HtmlDiv>("id=PublicWrapper", "class=sfPublicWrapper");
                    //, "innertext= The specified form no longer exists or is currently unpublished. ");
            }
        }
   
        /// <summary>
        /// Gets the captcha field on frontend
        /// </summary>
        public HtmlDiv CaptchaField
        {
            get
            {
                return this.Find.ByExpression<HtmlDiv>("data-sf-role=field-captcha-container");
            }
        }
        /// <summary>
        /// Gets the fil upload field on frontend
        /// </summary>
        public HtmlDiv FileUploadField
        {
            get
            {
                return this.Find.ByExpression<HtmlDiv>("data-sf-role=file-field-inputs");
            }
        }
    }
}
