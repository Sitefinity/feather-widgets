using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Modules.Newsletters.Web;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Helpers
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "html")]
        public static string GetBlankContentItem(this HtmlHelper html)
        {
            ContentItem blankContentItem;

            var manager = ContentManager.GetManager();

            using (new ElevatedModeRegion(manager))
            {
                blankContentItem = manager.CreateContent(Guid.Empty);

                var serialized = JsonConvert.SerializeObject(blankContentItem);
                return serialized;
            }
        }

        /// <summary>
        /// Gets a list of merge tags.
        /// </summary>
        /// <returns>Json representation of a list of merge tags.</returns>
        public static string GetMergeTags()
        {
            var serialized = JsonConvert.SerializeObject(ContentBlockHelpers.GetMergeTagsInternal());
            return serialized;
        }

        /// <summary>
        /// Gets a value that determines if merge tags should be shown.
        /// </summary>
        /// <returns>True if merge tags should be shown.</returns>
        public static bool ShowMergeTags()
        {
            var httpContext = SystemManager.CurrentHttpContext;
            if (httpContext != null)
            {
                var virtualPath = httpContext.Request.UrlReferrer.AbsolutePath;

                return virtualPath.StartsWith(newslettersPath, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        internal static IList<MergeTag> GetMergeTagsInternal()
        {
            IList<MergeTag> mergeTags = null;
            Newsletters.Model.MailingList mailingList = null;

            var newslettersManager = NewslettersManager.GetManager();

            var httpContext = SystemManager.CurrentHttpContext;
            if (httpContext.Items["NewslettersDescriptionProxy"] is StandardMessageDraftProxy)
            {
                var messageBody = httpContext.Items["NewslettersDescriptionProxy"] as StandardMessageDraftProxy;

                var campaign = newslettersManager.GetCampaigns().Where(c => c.MessageBody.Id == messageBody.PageDraftId).SingleOrDefault();
                mailingList = (campaign == null) ? null : campaign.List;
            }

            mergeTags = newslettersManager.GetMergeTags(mailingList);

            return mergeTags;
        }

        private static string newslettersPath = "/Sitefinity/SFNwslttrs/";
    }
}
