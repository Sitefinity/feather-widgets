using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestArrangementService.Core;
using Telerik.Sitefinity.TestIntegration.Helpers;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.Helpers.Models;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SetupSitefinityForMultisiteMultilingual Arrangement
    /// </summary>
    public class SetupSitefinityForMultisiteMultilingual : ITestArrangement
    {
        /// <summary>
        /// Creates website with localization.
        /// </summary>
        [ServerArrangement]
        public void CreateMultilingualSite()
        {
            AuthenticationHelper.AuthenticateUser(Admin, Password);

            var siteCultures = ArrangementConfig.GetArrangementSiteCultures();
            var site = new SiteModel(SiteName, Url, SiteName + "Provider", true) { Cultures = siteCultures };
            MultisiteHelper.CreateSite(site);

            this.SharePageTemplateWithSite(SetupSitefinityForMultisiteMultilingual.SiteName, SetupSitefinityForMultisiteMultilingual.Cultures[1]);
        }

        /// <summary>
        /// Deletes the additionally created site.
        /// </summary>
        [ServerArrangement]
        public void DeleteSite()
        {
            ServerOperations.MultiSite().DeleteSite(SiteName);
        }

        /// <summary>
        /// Shares the page template with site.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="culture">The culture.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "site"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "culture")]
        internal void SharePageTemplateWithSite(string site, string culture)
        {
            var pageManager = PageManager.GetManager();
            var templateInfos = pageManager.GetTemplates().Where(t => t.Framework == PageTemplateFramework.Mvc).Select(t => new KeyValuePair<Guid, string>(t.Id, t.Title));

            var siteManager = MultisiteManager.GetManager();
            var allSites = siteManager.GetSites().Select(s => s.Id.ToString()).ToArray();

            var pageTemplatesService = new PageTemplatesService();

            foreach (var templateInfo in templateInfos)
            {
                if (templateInfo.Key != Guid.Empty)
                {
                    pageTemplatesService.SaveSharedSites(templateInfo.Key.ToString(), allSites);

                    ServerOperations.Multilingual().Templates().CreateLocalizedPageTemplate(templateInfo.Key, templateInfo.Value, culture, site, framework: PageTemplateFramework.Mvc);
                }
            }
        }

        private const string SiteName = "SecondSite";
        private const string Url = "http://localhost:83/";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private static readonly List<string> Cultures = new List<string>() { "en", "bg-bg" };
        private const string Admin = "admin";
        private const string Password = "admin@2";
    }
}
