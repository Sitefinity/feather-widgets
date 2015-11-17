using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.CardWidget
{
    /// <summary>
    /// Elements from Card edit screen.
    /// </summary>
    public class CardEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public CardEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Heading field.
        /// </summary>
        public HtmlInputText HeadingTextField
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "id=heading-text");
            }
        }

        /// <summary>
        /// Gets the text area
        /// </summary>
        public HtmlTextArea TextArea
        {
            get
            {
                return this.Get<HtmlTextArea>("TagName=textarea");
            }
        }

        /// <summary>
        /// Label field.
        /// </summary>
        public HtmlInputText LabelTextField
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "id=label-text");
            }
        }

        /// <summary>
        /// Gets select image button.
        /// </summary>
        public HtmlButton SelectImage
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Select image");
            }
        }

        /// <summary>
        /// Selected extrenal url radio button.
        /// </summary>
        public HtmlInputRadioButton ExternalURLRadioButton
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("tagname=input", "ng-model=isPageSelectMode", "class=ng-pristine ng-untouched ng-valid", "value=false");
            }
        }

        /// <summary>
        /// External url field.
        /// </summary>
        public HtmlInputText ExternalURLInput
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "ng-model=properties.LinkedUrl.PropertyValue", "class=form-control ng-pristine ng-untouched ng-valid");
            }
        }
    }
}
