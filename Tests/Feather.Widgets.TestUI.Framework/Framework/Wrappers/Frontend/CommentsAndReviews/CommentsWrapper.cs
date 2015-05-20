using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.CommentsAndReviews
{
    /// <summary>
    /// This is the entry point class for comments on the frontend.
    /// </summary>
    public class CommentsWrapper : BaseWrapper
    {
        /// <summary>
        /// Asserts that Leave a comment message is present.
        /// </summary>
        public void AssertLeaveACommentMessage()
        {
            EM.CommentsAndReviews.CommentsFrontend.CommentsHeader.AssertIsPresent("Leave a comment message");
        }
    }
}
