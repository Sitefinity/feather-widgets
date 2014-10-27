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
    }
}
