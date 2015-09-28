using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
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
            var site = new SiteModel(SiteName, Url, SiteName + "Provider", true) { Cultures = siteCultures, SourcePagesSiteId = Guid.Empty };
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
        internal void SharePageTemplateWithSite(string site, string culture)
        {
            ServerOperations.Templates().SharePageTemplateWithSite(PageTemplateNameB, site);
            ServerOperations.Templates().SharePageTemplateWithSite(PageTemplateNameS, site);
            ServerOperations.Templates().SharePageTemplateWithSite(PageTemplateNameF, site);

            Guid templateIdB = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateNameB);
            Guid templateIdS = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateNameS);
            Guid templateIdF = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateNameF);

            var cultureInfo = new CultureInfo(culture);
            ServerOperationsFeather.TemplateOperations().CreatePureMVCPageTemplate(PageTemplateNameB, templateIdB, cultureInfo);
            ServerOperationsFeather.TemplateOperations().CreatePureMVCPageTemplate(PageTemplateNameS, templateIdS, cultureInfo);
            ServerOperationsFeather.TemplateOperations().CreatePureMVCPageTemplate(PageTemplateNameF, templateIdF, cultureInfo);
        }        

        private const string SiteName = "SecondSite";
        private const string Url = "http://localhost:83/";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private static readonly List<string> Cultures = new List<string>() { "en", "bg-bg" };
        private const string Admin = "admin";
        private const string Password = "admin@2";
        private const string PageTemplateNameB = "Bootstrap.default";
        private const string PageTemplateNameS = "Foundation.default";
        private const string PageTemplateNameF = "SemanticUI.default";
    }
}
