using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Model;
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

            ServerOperations.Multilingual().Templates().CreateLocalizedPageTemplate(templateIdB, PageTemplateNameB, culture, site, framework: PageTemplateFramework.Mvc);
            ServerOperations.Multilingual().Templates().CreateLocalizedPageTemplate(templateIdF, PageTemplateNameF, culture, site, framework: PageTemplateFramework.Mvc);
            ServerOperations.Multilingual().Templates().CreateLocalizedPageTemplate(templateIdS, PageTemplateNameS, culture, site, framework: PageTemplateFramework.Mvc);
          
            var cultureInfo = new CultureInfo(culture);
            this.CreatePureMVCPageTemplate(PageTemplateNameB1, templateIdB, cultureInfo);
            this.CreatePureMVCPageTemplate(PageTemplateNameS1, templateIdS, cultureInfo);
            this.CreatePureMVCPageTemplate(PageTemplateNameF1, templateIdF, cultureInfo);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MVC")]
        private Guid CreatePureMVCPageTemplate(string templateTitle, Guid parentTemplateId, CultureInfo cultureInfo)
        {
            Guid templateId = Guid.Empty;
            var fluent = App.WorkWith();
            var parentTemplate = fluent.Page().PageManager.GetTemplate(parentTemplateId);
            templateId = fluent.PageTemplate()
                               .CreateNew()
                               .Do(t =>
                               {
                                   t.Title[cultureInfo] = templateTitle;
                                   t.Name = new Lstring(Regex.Replace(templateTitle, ArrangementConstants.UrlNameCharsToReplace, string.Empty).ToLower());
                                   t.Description = templateTitle + " MVC";
                                   t.ParentTemplate = parentTemplate;
                                   t.ShowInNavigation = true;
                                   t.Framework = PageTemplateFramework.Mvc;
                                   t.Visible = true;
                               })
                               .SaveAndContinue()
                               .Get()
                               .Id;
            var pageManager = PageManager.GetManager();
            var template = pageManager.GetTemplates().Where(t => t.Id == templateId).SingleOrDefault();
            var master = pageManager.TemplatesLifecycle.Edit(template);
            pageManager.TemplatesLifecycle.Publish(master, cultureInfo);
            pageManager.SaveChanges();

            return templateId;
        }

        ////private Guid RenamePageTemplate(string templateName, string newName)
        ////{
        ////    var pageManager = PageManager.GetManager();

        ////        var template = pageManager.GetTemplates().Where(t => t.Title == templateName).SingleOrDefault();

        ////        if (template != null)
        ////        {
        ////            template.Title = newName;
        ////        }

        ////        pageManager.SaveChanges();

        ////        return template.Id;
        ////}

        private const string SiteName = "SecondSite";
        private const string Url = "http://localhost:83/";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private static readonly List<string> Cultures = new List<string>() { "en", "bg-BG" };
        private const string Admin = "admin";
        private const string Password = "admin@2";
        private const string PageTemplateNameB = "Bootstrap.default";
        private const string PageTemplateNameS = "Foundation.default";
        private const string PageTemplateNameF = "SemanticUI.default";

        private const string PageTemplateNameB1 = "Bootstrap1.default";
        private const string PageTemplateNameS1 = "Foundation1.default";
        private const string PageTemplateNameF1 = "SemanticUI1.default";
    }
}
