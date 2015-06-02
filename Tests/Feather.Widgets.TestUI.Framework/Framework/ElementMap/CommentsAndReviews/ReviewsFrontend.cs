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

        /// <summary>
        /// Gets alert warning div.
        /// </summary>
        public HtmlDiv AlertWarningDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=^alert alert-warning sf-");
            }
        }

        /// <summary>
        /// Gets subscribe to new review
        /// </summary>
        public HtmlSpan SubscribeToNewReview
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=Subscribe to new reviews");
            }
        }

        /// <summary>
        /// Gets successfully subscribed message to new review
        /// </summary>
        public HtmlSpan SuccessfulySubscribedMessage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=You are successfully subscribed to new reviews");
            }
        }

        /// <summary>
        /// Gets login alert warning div.
        /// </summary>
        public HtmlDiv LoginAlertWarningDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=alert alert-warning");
            }
        }
    }
}
