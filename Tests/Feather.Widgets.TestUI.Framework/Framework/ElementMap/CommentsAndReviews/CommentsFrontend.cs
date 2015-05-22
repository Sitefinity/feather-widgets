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
    /// Elements from Comments frontend.
    /// </summary>
    public class CommentsFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public CommentsFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets comments header
        /// </summary>
        /// <value>Gets comments header</value>
        public HtmlSpan CommentsHeader
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "InnerText=Leave a comment");
            }
        }

        /// <summary>
        /// Gets leave a comment link.
        /// </summary>
        public HtmlAnchor LeaveAComment
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "InnerText=Leave a comment");
            }
        }

        /// <summary>
        /// Gets leave a comment area.
        /// </summary>
        public HtmlDiv LeaveACommentArea
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=media-body sf-media-body");
            }
        }

        /// <summary>
        /// Gets submit button.
        /// </summary>
        public HtmlButton SubmitButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Submit");
            }
        }

        /// <summary>
        /// Gets comments results list.
        /// </summary>
        public HtmlDiv ResultsCommentsDivList 
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=media-list sf-Comments-list");
            }
        }
    }
}
