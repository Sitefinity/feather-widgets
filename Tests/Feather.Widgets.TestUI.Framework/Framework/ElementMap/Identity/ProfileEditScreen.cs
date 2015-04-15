using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Identity
{
    /// <summary>
    /// Elements from ProfileEditScreen.
    /// </summary>
    public class ProfileEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ProfileEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets Read mode only button.
        /// </summary>
        public HtmlInputRadioButton ReadModeOnly
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("tagname=input", "value=ReadOnly");
            }
        }

        /// <summary>
        /// Gets both mode button.
        /// </summary>
        public HtmlInputRadioButton BothReadModeCanBeEditedMode
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("tagname=input", "value=Both");
            }
        }

        /// <summary>
        /// Gets open specially prepared page button.
        /// </summary>
        public HtmlDiv WhenChangesAreSavedDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "InnerText=~When the changes are saved...");
            }
        }
    }
}
