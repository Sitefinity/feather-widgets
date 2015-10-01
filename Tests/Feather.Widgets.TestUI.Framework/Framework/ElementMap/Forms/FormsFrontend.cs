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
        /// Gets the form on frontend
        /// </summary>
        public HtmlDiv TextboxLabel
        {
            get
            {
                return this.Get<HtmlDiv>("TagName=div", "Class=form-group");
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
        /// Gets the submit button on frontend
        /// </summary>
        public HtmlButton SubmitButton
        {
            get
            {
                return this.Get<HtmlButton>("TagName=button", "innertext=Submit");
            }
        }

        /// <summary>
        /// Gets the success message after submit on frontend
        /// </summary>
        public HtmlDiv SuccessMessage
        {
            get
            {
                return this.Get<HtmlDiv>("TagName=div", "innertext=Success! Thanks for filling out our form!");
            }
        }
    }
}
