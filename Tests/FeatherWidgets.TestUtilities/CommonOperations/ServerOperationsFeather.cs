using FeatherWidgets.TestUtilities.CommonOperations.Pages;

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
        /// Newses the operations.
        /// </summary>
        /// <returns></returns>
        public static NewsOperations NewsOperations()
        {
            return new NewsOperations();
        }
    }
}
