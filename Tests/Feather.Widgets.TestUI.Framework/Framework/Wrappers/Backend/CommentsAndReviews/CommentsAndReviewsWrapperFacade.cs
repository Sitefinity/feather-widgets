using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.CommentsAndReviews
{
    /// <summary>
    /// This is an entry point for comments and reviews wrappers for the backend.
    /// </summary>
    public class CommentsAndReviewsWrapperFacade
    {
        /// <summary>
        /// Provides access to CommentsWrapper
        /// </summary>
        /// <returns>New instance of CommentsWrapper</returns>
        public CommentsWrapper CommentsWrapper()
        {
            return new CommentsWrapper();
        }

        /// <summary>
        /// Provides access to ReviewsWrapper
        /// </summary>
        /// <returns>New instance of ReviewsWrapper</returns>
        public ReviewsWrapper ReviewsWrapper()
        {
            return new ReviewsWrapper();
        }
    }
}
