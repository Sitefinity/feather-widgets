using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// Provides common server operations
    /// </summary>
    public class ServerOperationsFeather
    {
        /// <summary>
        /// Entry point for ContentBlocks operations.
        /// </summary>
        /// <returns>ContentBlockOperations instance</returns>
        public static ContentBlockOperations ContentBlocks()
        {
            return new ContentBlockOperations();
        }

        /// <summary>
        /// Entry point for Pages operations.
        /// </summary>
        /// <returns>PagesOperations instance</returns>
        public static PagesOperations Pages()
        {
            return new PagesOperations();
        }
    }
}
