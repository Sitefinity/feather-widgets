using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for content block frontend wrappers.
    /// </summary>
    public class ContentBlockWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the ContentBlockWrapper
        /// </summary>
        /// <returns>Returns the ContentBlockWrapper</returns>
        public ContentBlockWrapper ContentBlockWrapper()
        {
            return new ContentBlockWrapper();
        }
    }
}
