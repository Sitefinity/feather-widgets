using FeatherWidgets.TestUtilities.CommonOperations.Blogs;
using FeatherWidgets.TestUtilities.CommonOperations.Forms;
using FeatherWidgets.TestUtilities.CommonOperations.Pages;
using FeatherWidgets.TestUtilities.CommonOperations.Templates;
using FeatherWidgets.TestUtilities.CommonOperations.Users;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// Provides common server operations
    /// </summary>
    public static class ServerOperationsFeather
    {
        /// <summary>
        /// Entry point for Pages operations.
        /// </summary>
        /// <returns>PagesOperations instance</returns>
        public static PagesOperations Pages()
        {
            return new PagesOperations();
        }

        /// <summary>
        /// Contents the block operations.
        /// </summary>
        /// <returns></returns>
        public static ContentBlockOperations ContentBlockOperations()
        {
            return new ContentBlockOperations();
        }

        /// <summary>
        /// Medias the operations.
        /// </summary>
        /// <returns></returns>
        public static MediaOperations MediaOperations()
        {
            return new MediaOperations();
        }

        /// <summary>
        /// Entry point for Templates operations.
        /// </summary>
        /// <returns>TemplateOperations instance</returns>
        public static TemplateOperations TemplateOperations()
        {
            return new TemplateOperations();
        }

        /// <summary>
        /// Newses the operations.
        /// </summary>
        /// <returns></returns>
        public static NewsOperations NewsOperations()
        {
            return new NewsOperations();
        }

        /// <summary>
        /// Entry point for Users operations.
        /// </summary>
        /// <returns>UsersOperations instance.</returns>
        public static UsersOperations Users()
        {
            return new UsersOperations();
        }

        /// <summary>
        /// Entry point for Navigation operations.
        /// </summary>
        /// <returns>NavigationOperations instance.</returns>
        public static NavigationOperations Navigation()
        {
            return new NavigationOperations();
        }

        /// <summary>
        /// Entry point for Dynamic modules operations.
        /// </summary>
        /// <returns>DynamicModulesOperations instance.</returns>
        public static DynamicModulesOperations DynamicModules()
        {
            return new DynamicModulesOperations();
        }

        /// <summary>
        /// Entry point for Dynamic modules Press Article operations.
        /// </summary>
        /// <returns>DynamicModulePressArticleOperations instance.</returns>
        public static DynamicModulePressArticleOperations DynamicModulePressArticle()
        {
            return new DynamicModulePressArticleOperations();
        }

        /// <summary>
        /// Entry point for Dynamic modules All types operations.
        /// </summary>
        /// <returns>DynamicModuleAllTypesOperations instance.</returns>
        public static DynamicModuleAllTypesOperations DynamicModuleAllTypes()
        {
            return new DynamicModuleAllTypesOperations();
        }

        /// <summary>
        /// Entry point for Booking dynamic module common operations.
        /// </summary>
        /// <returns>DynamicModuleBookingOperations instance.</returns>
        public static DynamicModuleBookingOperations DynamicModuleBooking()
        {
            return new DynamicModuleBookingOperations();
        }

        /// <summary>
        /// Entry point for Tags operations.
        /// </summary>
        /// <returns>TagsOperations instance.</returns>
        public static TagsOperations Tags()
        {
            return new TagsOperations();
        }

        /// <summary>
        /// Entry point for common operations related to Module1 dynamic module
        /// </summary>
        /// <returns></returns>
        public static DynamicModuleModule1Operations DynamicModule1Operations()
        {
            return new DynamicModuleModule1Operations();
        }

        /// <summary>
        /// Entry point for common operations related to Module2 dynamic module
        /// </summary>
        /// <returns></returns>
        public static DynamicModuleModule2Operations DynamicModule2Operations()
        {
            return new DynamicModuleModule2Operations();
        }

        /// <summary>
        /// Entry point for common operations related to Lists
        /// </summary>
        /// <returns></returns>
        public static ListsOperations ListsOperations()
        {
            return new ListsOperations();
        }

        /// <summary>
        /// Entry point for common operations related to Blogs and blog posts.
        /// </summary>
        /// <returns></returns>
        public static BlogsOperations Blogs()
        {
            return new BlogsOperations();
        }

        /// <summary>
        /// Entry point for common operations related to comments and reviews.
        /// </summary>
        /// <returns></returns>
        public static CommentsAndReviews CommentsAndReviews()
        {
            return new CommentsAndReviews();
        }

        /// <summary>
        /// Entry point for common operations related to forms.
        /// </summary>
        /// <returns></returns>
        public static FormsOperations Forms()
        {
            return new FormsOperations();
        }
    }
}
