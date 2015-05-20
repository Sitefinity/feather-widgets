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
                return this.Get<HtmlSpan>("InnerText=Leave a comment");
            }
        }
    }
}
