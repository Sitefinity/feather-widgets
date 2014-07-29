using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace ContentBlock.Mvc.Helpers
{
    /// <summary>
    /// This class contains helper methods for use in the views of the ContentBlock widget and its designer.
    /// </summary>
    public static class ContentBlockHelpers
    {
        /// <summary>
        /// Gets a blank content item.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>Json representation of a blank content item.</returns>
        public static string GetBlankContentItem(this HtmlHelper html)
        {
            ContentItem blankContentItem;
            var manager = ContentManager.GetManager();
            using (new ElevatedModeRegion(manager))
            {
                blankContentItem = manager.CreateContent(Guid.Empty);
            }

            var serialized = JsonConvert.SerializeObject(blankContentItem);
            return serialized;
        }
    }
}
