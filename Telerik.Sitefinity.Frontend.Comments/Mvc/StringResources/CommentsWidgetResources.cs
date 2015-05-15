using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Comments widget
    /// </summary>
    [ObjectInfo(typeof(CommentsWidgetResources), ResourceClassId = "CommentsResources", Title = "CommentsResourcesTitle", Description = "CommentsResourcesDescription")]
    public class CommentsWidgetResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsWidgetResources"/> class. 
        /// Initializes new instance of <see cref="CommentsWidgetResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public CommentsWidgetResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsWidgetResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public CommentsWidgetResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        #region Meta resources

        /// <summary>
        /// Gets Comments widget resources title.
        /// </summary>
        [ResourceEntry("CommentsResourcesTitle",
            Value = "Comments widget resources",
            Description = "Title for the comments widget resources class.",
            LastModified = "2015/04/29")]
        public string CommentsResourcesTitle
        {
            get
            {
                return this["CommentsResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Comments widget resources description.
        /// </summary>
        [ResourceEntry("CommentsResourcesDescription",
            Value = "Localizable strings for the Comments widget.",
            Description = "Description for the comments widget resources class.",
            LastModified = "2015/04/29")]
        public string CommentsResourcesDescription
        {
            get
            {
                return this["CommentsResourcesDescription"];
            }
        }

        #endregion

        #region Frontend resources

        /// <summary>
        /// Gets phrase : Read full comment
        /// </summary>
        [ResourceEntry("ReadFullComment",
            Value = "Read full comment",
            Description = "phrase : Read full comment",
            LastModified = "2015/05/05")]
        public string ReadFullComment
        {
            get
            {
                return this["ReadFullComment"];
            }
        }

        /// <summary>
        /// Gets word : comment
        /// </summary>
        [ResourceEntry("Comment",
            Value = "comment",
            Description = "word : comment",
            LastModified = "2015/05/11")]
        public string Comment
        {
            get
            {
                return this["Comment"];
            }
        }

        /// <summary>
        /// Gets word : comments
        /// </summary>
        [ResourceEntry("CommentsPlural",
            Value = "comments",
            Description = "word : comments",
            LastModified = "2015/05/05")]
        public string CommentsPlural
        {
            get
            {
                return this["CommentsPlural"];
            }
        }

        /// <summary>
        /// Gets phrase : Leave a comment
        /// </summary>
        [ResourceEntry("LeaveComment",
            Value = "Leave a comment",
            Description = "phrase : Leave a comment",
            LastModified = "2015/05/05")]
        public string LeaveComment
        {
            get
            {
                return this["LeaveComment"];
            }
        }

        /// <summary>
        /// Gets phrase : Newest on top
        /// </summary>
        [ResourceEntry("NewestOnTop",
            Value = "Newest on top",
            Description = "phrase : Newest on top",
            LastModified = "2015/05/05")]
        public string NewestOnTop
        {
            get
            {
                return this["NewestOnTop"];
            }
        }

        /// <summary>
        /// Gets phrase : Oldest on top
        /// </summary>
        [ResourceEntry("OldestOnTop",
            Value = "Oldest on top",
            Description = "phrase : Oldest on top",
            LastModified = "2015/05/05")]
        public string OldestOnTop
        {
            get
            {
                return this["OldestOnTop"];
            }
        }

        /// <summary>
        /// Gets phrase : Load more comments
        /// </summary>
        [ResourceEntry("LoadMoreComments",
            Value = "Load more comments",
            Description = "phrase : Load more comments",
            LastModified = "2015/05/05")]
        public string LoadMoreComments
        {
            get
            {
                return this["LoadMoreComments"];
            }
        }

        /// <summary>
        /// Gets word : Submit
        /// </summary>
        [ResourceEntry("Submit",
            Value = "Submit",
            Description = "word : Submit",
            LastModified = "2015/05/05")]
        public string Submit
        {
            get
            {
                return this["Submit"];
            }
        }

        /// <summary>
        /// Gets phrase : Your name
        /// </summary>
        [ResourceEntry("YourName",
            Value = "Your name",
            Description = "phrase: Your name",
            LastModified = "2015/05/05")]
        public string YourName
        {
            get
            {
                return this["YourName"];
            }
        }

        /// <summary>
        /// Gets phrase : Email (optional)
        /// </summary>
        [ResourceEntry("EmailOptional",
            Value = "Email (optional)",
            Description = "phrase: Email (optional)",
            LastModified = "2015/05/05")]
        public string EmailOptional
        {
            get
            {
                return this["EmailOptional"];
            }
        }

        /// <summary>
        /// Gets phrase : Subscribe
        /// </summary>
        [ResourceEntry("SubscribeLink",
            Value = "Subscribe",
            Description = "phrase: Subscribe",
            LastModified = "2015/05/12")]
        public string SubscribeLink
        {
            get
            {
                return this["SubscribeLink"];
            }
        }

        /// <summary>
        /// Gets phrase : Unsubscribe
        /// </summary>
        [ResourceEntry("UnsubscribeLink",
            Value = "Unsubscribe",
            Description = "phrase: Unsubscribe",
            LastModified = "2015/05/12")]
        public string UnsubscribeLink
        {
            get
            {
                return this["UnsubscribeLink"];
            }
        }

        /// <summary>
        /// Gets phrase : Subscribe to new comments
        /// </summary>
        [ResourceEntry("SubscribeToNewComments",
            Value = "Subscribe to new comments",
            Description = "phrase: Subscribe to new comments",
            LastModified = "2015/05/12")]
        public string SubscribeToNewComments
        {
            get
            {
                return this["SubscribeToNewComments"];
            }
        }

        /// <summary>
        /// Gets phrase : You are subscribed to new comments
        /// </summary>
        [ResourceEntry("YouAreSubscribedToNewComments",
            Value = "You are subscribed to new comments",
            Description = "phrase: You are subscribed to new comments",
            LastModified = "2015/05/12")]
        public string YouAreSubscribedToNewComments
        {
            get
            {
                return this["YouAreSubscribedToNewComments"];
            }
        }

        /// <summary>
        /// Gets phrase : You are successfully subscribed to new comments
        /// </summary>
        [ResourceEntry("SuccessfullySubscribedToNewComments",
            Value = "You are successfully subscribed to new comments",
            Description = "phrase: You are successfully subscribed to new comments",
            LastModified = "2015/05/12")]
        public string SuccessfullySubscribedToNewComments
        {
            get
            {
                return this["SuccessfullySubscribedToNewComments"];
            }
        }

        /// <summary>
        /// Gets phrase : You are successfully unsubscribed
        /// </summary>
        [ResourceEntry("SuccessfullyUnsubscribedFromNewComments",
            Value = "You are successfully unsubscribed",
            Description = "phrase: You are successfully unsubscribed",
            LastModified = "2015/05/12")]
        public string SuccessfullyUnsubscribedFromNewComments
        {
            get
            {
                return this["SuccessfullyUnsubscribedFromNewComments"];
            }
        }

        /// <summary>
        /// Gets phrase : Comments are not allowed anymore
        /// </summary>
        [ResourceEntry("CommentsThreadIsClosedMessage",
            Value = "Comments are not allowed anymore",
            Description = "phrase: Comments are not allowed anymore",
            LastModified = "2015/05/14")]
        public string CommentsThreadIsClosedMessage
        {
            get
            {
                return this["CommentsThreadIsClosedMessage"];
            }
        }
               
        /// <summary>
        /// Gets phrase : New code
        /// </summary>
        [ResourceEntry("NewCode",
            Value = "New code",
            Description = "phrase : New code",
            LastModified = "2015/05/05")]
        public string NewCode
        {
            get
            {
                return this["NewCode"];
            }
        }

        /// <summary>
        /// Gets phrase : Please type the code above
        /// </summary>
        [ResourceEntry("TypeCodeAbove",
            Value = "Please type the code above",
            Description = "phrase : Please type the code above",
            LastModified = "2015/05/05")]
        public string TypeCodeAbove
        {
            get
            {
                return this["TypeCodeAbove"];
            }
        }

        /// <summary>
        /// Gets phrase : Login
        /// </summary>
        [ResourceEntry("Login",
            Value = "Login",
            Description = "phrase : Login",
            LastModified = "2015/05/08")]
        public string Login
        {
            get
            {
                return this["Login"];
            }
        }

        /// <summary>
        /// Gets phrase : Author name is required!
        /// </summary>
        [ResourceEntry("NameIsRequired",
            Value = "Author name is required!",
            Description = "phrase : Author name is required!",
            LastModified = "2015/05/07")]
        public string NameIsRequired
        {
            get
            {
                return this["NameIsRequired"];
            }
        }

        /// <summary>
        /// Gets phrase :  to be able to comment
        /// </summary>
        [ResourceEntry("ToBeAbleToComment",
            Value = " to be able to comment",
            Description = "phrase : to be able to comment",
            LastModified = "2015/05/12")]
        public string ToBeAbleToComment
        {
            get
            {
                return this["ToBeAbleToComment"];
            }
        }
             
        /// <summary>
        /// Gets phrase : Message is required!
        /// </summary>
        [ResourceEntry("MessageIsRequired",
            Value = "Message is required!",
            Description = "phrase : Message is required!",
            LastModified = "2015/05/07")]
        public string MessageIsRequired
        {
            get
            {
                return this["MessageIsRequired"];
            }
        }
        
        /// <summary>
        /// Gets phrase : Thank you for the comment! Your comment must be approved first
        /// </summary>
        [ResourceEntry("CommentPendingApproval",
            Value = "Thank you for the comment! Your comment must be approved first",
            Description = "phrase : Thank you for the comment! Your comment must be approved first",
            LastModified = "2015/05/12")]
        public string CommentPendingApproval
        {
            get
            {
                return this["CommentPendingApproval"];
            }
        }

        #endregion

        #region Reviews

        /// <summary>
        /// Gets phrase : Rating
        /// </summary>
        [ResourceEntry("Rating",
            Value = "Rating",
            Description = "phrase : Rating",
            LastModified = "2015/05/14")]
        public string Rating
        {
            get
            {
                return this["Rating"];
            }
        }

        /// <summary>
        /// Gets phrase : Rating is required!
        /// </summary>
        [ResourceEntry("RatingIsRequired",
            Value = "Rating is required!",
            Description = "phrase : Rating is required!",
            LastModified = "2015/05/14")]
        public string RatingIsRequired
        {
            get
            {
                return this["RatingIsRequired"];
            }
        }

        /// <summary>
        /// Gets phrase : Write a review
        /// </summary>
        [ResourceEntry("WriteReview",
            Value = "Write a review",
            Description = "phrase : Write a review",
            LastModified = "2015/05/14")]
        public string WriteReview
        {
            get
            {
                return this["WriteReview"];
            }
        }

        /// <summary>
        /// Gets phrase : Reviews are not allowed anymore
        /// </summary>
        [ResourceEntry("ReviewsThreadIsClosedMessage",
            Value = "Reviews are not allowed anymore",
            Description = "phrase: Reviews are not allowed anymore",
            LastModified = "2015/05/14")]
        public string ReviewsThreadIsClosedMessage
        {
            get
            {
                return this["ReviewsThreadIsClosedMessage"];
            }
        }

        /// <summary>
        /// Gets phrase : Load more reviews
        /// </summary>
        [ResourceEntry("LoadMoreReviews",
            Value = "Load more reviews",
            Description = "phrase : Load more reviews",
            LastModified = "2015/05/14")]
        public string LoadMoreReviews
        {
            get
            {
                return this["LoadMoreReviews"];
            }
        }

        /// <summary>
        /// Gets phrase : Thank you for the review! Your review must be approved first
        /// </summary>
        [ResourceEntry("ReviewPendingApproval",
            Value = "Thank you for the review! Your review must be approved first",
            Description = "phrase : Thank you for the review! Your review must be approved first",
            LastModified = "2015/05/14")]
        public string ReviewPendingApproval
        {
            get
            {
                return this["ReviewPendingApproval"];
            }
        }

        /// <summary>
        /// Gets word : review
        /// </summary>
        [ResourceEntry("Review",
            Value = "review",
            Description = "word : review",
            LastModified = "2015/05/14")]
        public string Review
        {
            get
            {
                return this["Review"];
            }
        }

        /// <summary>
        /// Gets word : review
        /// </summary>
        [ResourceEntry("ReviewsPlural",
            Value = "reviews",
            Description = "word : reviews",
            LastModified = "2015/05/14")]
        public string ReviewsPlural
        {
            get
            {
                return this["ReviewsPlural"];
            }
        }

        /// <summary>
        /// Gets phrase : Read full review
        /// </summary>
        [ResourceEntry("ReadFullReview",
            Value = "Read full review",
            Description = "phrase : Read full review",
            LastModified = "2015/05/14")]
        public string ReadFullReview
        {
            get
            {
                return this["ReadFullReview"];
            }
        }
        
        /// <summary>
        /// Gets phrase : Subscribe to new reviews
        /// </summary>
        [ResourceEntry("SubscribeToNewReviews",
            Value = "Subscribe to new reviews",
            Description = "phrase: Subscribe to new reviews",
            LastModified = "2015/05/14")]
        public string SubscribeToNewReviews
        {
            get
            {
                return this["SubscribeToNewReviews"];
            }
        }

        /// <summary>
        /// Gets phrase : You are subscribed to new reviews
        /// </summary>
        [ResourceEntry("YouAreSubscribedToNewReviews",
            Value = "You are subscribed to new reviews",
            Description = "phrase: You are subscribed to new reviews",
            LastModified = "2015/05/14")]
        public string YouAreSubscribedToNewReviews
        {
            get
            {
                return this["YouAreSubscribedToNewReviews"];
            }
        }

        /// <summary>
        /// Gets phrase : You are successfully subscribed to new reviews
        /// </summary>
        [ResourceEntry("SuccessfullySubscribedToNewReviews",
            Value = "You are successfully subscribed to new reviews",
            Description = "phrase: You are successfully subscribed to new reviews",
            LastModified = "2015/05/14")]
        public string SuccessfullySubscribedToNewReviews
        {
            get
            {
                return this["SuccessfullySubscribedToNewReviews"];
            }
        }

        /// <summary>
        /// Gets phrase :  to be able to write a review
        /// </summary>
        [ResourceEntry("ToBeAbleToReview",
            Value = " to be able to write a review",
            Description = "phrase : to be able to write a review",
            LastModified = "2015/05/14")]
        public string ToBeAbleToReview
        {
            get
            {
                return this["ToBeAbleToReview"];
            }
        }

         /// <summary>
        /// Gets phrase : Thank you! Your review has been submitted successfully
        /// </summary>
        [ResourceEntry("ThankYouReviewSubmited",
            Value = "Thank you! Your review has been submitted successfully",
            Description = "phrase : Thank you! Your review has been submitted successfully",
            LastModified = "2015/05/14")]
        public string ThankYouReviewSubmited
        {
            get
            {
                return this["ThankYouReviewSubmited"];
            }
        }

         /// <summary>
        /// Gets phrase : You've already submitted a review for this item
        /// </summary>
        [ResourceEntry("UserAlreadySubmitedReview",
            Value = "You've already submitted a review for this item",
            Description = "phrase : You've already submitted a review for this item",
            LastModified = "2015/05/14")]
        public string UserAlreadySubmitedReview
        {
            get
            {
                return this["UserAlreadySubmitedReview"];
            }
        }

        #endregion

        #region Designer resources

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/05/04")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/05/04")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets phrase: Template
        /// </summary>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "phrase : Template",
            LastModified = "2015/05/04")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        #endregion
    }
}
