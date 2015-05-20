using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.CommentsAndReviews
{
    /// <summary>
    /// This is an entry point for coments and reviews wrappers for the frontend.
    /// </summary>
    public class CommentsAndReviewsWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the CommentsWrapper
        /// </summary>
        /// <returns>Returns the CommentsWrapper</returns>
        public CommentsWrapper CommentsWrapper()
        {
            return new CommentsWrapper();
        }

        /// <summary>
        /// Provides unified access to the ReviewsWrapper
        /// </summary>
        /// <returns>Returns the ReviewsWrapper</returns>
        public ReviewsWrapper ReviewsWrapper()
        {
            return new ReviewsWrapper();
        }
    }
}
