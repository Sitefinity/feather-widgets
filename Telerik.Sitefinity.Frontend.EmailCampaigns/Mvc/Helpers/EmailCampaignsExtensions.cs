using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Helpers
{
    public static class EmailCampaignsExtensions
    {
        /// <summary>
        /// Gets the merged tags.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MergeTag> GetMergedTags()
        {
            var newslettersManager = NewslettersManager.GetManager();
            foreach (var mergeTag in newslettersManager.GetMergeTags())
            {
                if (mergeTag.PropertyName == "UnsubscribeLink")
                {
                    continue;
                }
                yield return mergeTag;
            }
            yield return new MergeTag("Subscribe link", "SubscribeLink", "MergeContextItems");
        }

        /// <summary>
        /// Determines whether the newsletters module is activated.
        /// </summary>
        /// <returns></returns>
        public static bool IsModuleActivated()
        {
            var module = SystemManager.GetModule(NewslettersModule.ModuleName);

            return module != null;
        }
    }
}
