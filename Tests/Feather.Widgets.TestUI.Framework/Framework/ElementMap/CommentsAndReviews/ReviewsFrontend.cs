using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.CommentsAndReviews
{
    /// <summary>
    /// Elements from Reviews frontend.
    /// </summary>
    public class ReviewsFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewsFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ReviewsFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets raiting stars div.
        /// </summary>
        public HtmlDiv RaitingStars
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=sf-Ratings-stars");
            }
        }

        /// <summary>
        /// Gets the reviews div
        /// </summary>
        public ICollection<HtmlDiv> ReviewsDivs
        {
            get
            {
                return this.Find.AllByExpression<HtmlDiv>("tagName=div", "class=media-body sf-media-body");
            }
        }
    }
}
