using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.CommentsAndReviews
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather comments and reviews widgets.
    /// </summary>
    public class CommentsAndReviewsMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsAndReviewsMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public CommentsAndReviewsMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the comments widget frontend
        /// </summary>
        public CommentsFrontend CommentsFrontend
        {
            get
            {
                return new CommentsFrontend(this.find);
            }
        }

        /// <summary>
        /// Gets the comments widget backend
        /// </summary>
        public CommentsEditScreen CommentsEditScreen
        {
            get
            {
                return new CommentsEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the comments widget frontend
        /// </summary>
        public ReviewsFrontend ReviewsFrontend
        {
            get
            {
                return new ReviewsFrontend(this.find);
            }
        }

        /// <summary>
        /// Gets the comments widget backend
        /// </summary>
        public ReviewsEditScreen ReviewsEditScreen
        {
            get
            {
                return new ReviewsEditScreen(this.find);
            }
        }

        private Find find;
    }
}
