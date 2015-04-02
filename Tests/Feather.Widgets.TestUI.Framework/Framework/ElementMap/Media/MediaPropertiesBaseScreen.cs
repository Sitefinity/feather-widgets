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
    /// Provides access to MediaPropertiesScreen
    /// </summary>
    public class MediaPropertiesBaseScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPropertiesBaseScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public MediaPropertiesBaseScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the done button.
        /// </summary>
        /// <value>The done button.</value>
        public HtmlButton DoneButton
        {
            get
            {
                return this.MediaPropertiesModalDialog.Find.ByExpression<HtmlButton>("tagName=button", "InnerText=Done");
            }
        }

        /// <summary>
        /// Gets the save button in media widget.
        /// </summary>
        /// <value>The save button in media widget.</value>
        public HtmlButton SaveButtonInMediaWidget
        {
            get
            {
                return this.Get<HtmlButton>("tagName=button", "InnerText=Save");
            }
        }

        /// <summary>
        /// Gets the change button.
        /// </summary>
        /// <value>The change button.</value>
        public HtmlButton ChangeButton
        {
            get
            {
                return this.MediaPropertiesModalDialog.Find.ByExpression<HtmlButton>("tagName=button", "InnerText=~Change");
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
                return this.MediaPropertiesModalDialog.Find.ByExpression<HtmlButton>("tagName=button", "InnerText=Edit all properties");
            }
        }

        private HtmlDiv MediaPropertiesModalDialog
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=~modal-dialog-1");
            }
        }
    }
}
