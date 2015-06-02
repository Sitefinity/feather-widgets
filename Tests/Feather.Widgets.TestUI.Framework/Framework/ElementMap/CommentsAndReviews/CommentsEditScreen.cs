using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.CommentsAndReviews
{
    /// <summary>
    /// Elements from Comments edit screen.
    /// </summary>
    public class CommentsEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public CommentsEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets thread is closed
        /// </summary>
        public HtmlInputControl ThreadIdsClosed
        {
            get
            {
                return this.Get<HtmlInputControl>("TagName=input", "id=prop-ThreadIsClosed");
            }
        }
    }
}
