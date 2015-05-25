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
                return this.Get<HtmlAnchor>("tagname=a", "data-sf-role=comments-count-anchor");
            }
        }

        /// <summary>
        /// Gets leave a comment area.
        /// </summary>
        public HtmlDiv LeaveACommentArea
        {
            get
            {
                return this.Find.AllByTagName<HtmlDiv>("div").Where(d => d.ChildNodes.Count == 2
                                                                        && d.ChildNodes[0].TagName.Equals("label")
                                                                        && d.ChildNodes[1].TagName.Equals("textarea")).FirstOrDefault();
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
        /// Gets comments list.
        /// </summary>
        public HtmlDiv CommentsList
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=media-list sf-Comments-list");
            }
        }

        /// <summary>
        /// Gets comments results list.
        /// </summary>
        public IList<HtmlDiv> ResultsCommentsList
        {
            get
            {
                return this.CommentsList.Find.AllByTagName<HtmlDiv>("div").Where(d => d.ChildNodes.Count == 3
                                                                        && d.ChildNodes[0].TagName.Equals("span")
                                                                        && d.ChildNodes[1].TagName.Equals("span")
                                                                        && d.ChildNodes[2].TagName.Equals("p")).ToList();
            }
        }

        /// <summary>
        /// Gets comments count on page.
        /// </summary>
        public HtmlDiv CommentsCountOnPage
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=row sf-Comments-header");
            }
        }

        /// <summary>
        /// Gets your name field.
        /// </summary>
        public HtmlDiv YourNameField
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "InnerText=Your name");
            }
        }

        /// <summary>
        /// Gets email field.
        /// </summary>
        public HtmlDiv EmailField
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "InnerText=Email (optional)");
            }
        }

        /// <summary>
        /// Gets alert warning div.
        /// </summary>
        public HtmlDiv AlertWarningDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=alert alert-warning");
            }
        }

        /// <summary>
        /// Gets error div.
        /// </summary>
        public HtmlDiv ErrorDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=has-error");
            }
        }

        /// <summary>
        /// Gets load more link.
        /// </summary>
        public HtmlAnchor LoadMoreLink
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "InnerText=Load more comments");
            }
        }
    }
}
