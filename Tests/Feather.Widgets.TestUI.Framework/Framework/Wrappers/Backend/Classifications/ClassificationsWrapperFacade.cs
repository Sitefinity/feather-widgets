using System;
using System.Linq;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications
{
    /// <summary>
    /// This is the entry point class for classifications widget backend wrappers.
    /// </summary>
    public class ClassificationsWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the TagsWrapper 
        /// </summary>
        /// <returns>Returns the TagsWrapper</returns>
        public TagsWrapper TagsWrapper()
        {
            return new TagsWrapper();
        }
    }
}
