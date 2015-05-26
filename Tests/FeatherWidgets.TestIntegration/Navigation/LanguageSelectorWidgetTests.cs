using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Web;
using Telerik.WebTestRunner.Server.Attributes;

namespace FeatherWidgets.TestIntegration.Navigation
{
    /// <summary>
    /// This is a class with language selector tests
    /// </summary>
    [TestFixture]
    [Description("This is a class with language selector tests.")]
    public class LanguageSelectorWidgetTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        { 
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.EndsWith(System.String)"), Test]
        [Multilingual]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies language selector current language not included")]
        public void LanguageSelectorWidget_CurrentLanguageIncluded()
        {           
            var languageSelectorControl = new MvcControllerProxy();
            languageSelectorControl.ControllerName = typeof(LanguageSelectorController).FullName;
            var languageSelectorController = new LanguageSelectorController();
            languageSelectorControl.Settings = new ControllerSettings(languageSelectorController);

            languageSelectorController.Model.IncludeCurrentLanguage = true;

            var controls = new List<System.Web.UI.Control>();
            controls.Add(languageSelectorControl);

            Guid currentID;
            var pageId = CreateLocalizedPage("TestPage", out currentID, Guid.Empty);
            PageContentGenerator.AddControlsToPage(pageId, controls);
            string url = UrlPath.ResolveAbsoluteUrl("~/" + "TestPage" + "en-US");
            var pageContent = PageInvoker.ExecuteWebRequest(url);
            Assert.IsNotNull(pageContent);

            var expectedLinkes = new Dictionary<string, string>()
            {
                { "English (United States)", "/TestPageen-US" },
                { "Türkçe (Türkiye)", "/tr-tr/TestPagetr-TR" }              
            };

            this.AssertContainsLanguageLinks(pageContent, expectedLinkes);       
        }
  
        #region Helper methods

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
        private static Guid CreateLocalizedPage(string pageTitle, out Guid pageId, Guid parentId)
        {
            pageId = new Guid();

            var defaultCulture = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.DefinedFrontendLanguages[1]; ////CultureInfo.GetCultureInfo("en-US");
            var secondCulture = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.DefinedFrontendLanguages[2]; ////CultureInfo.GetCultureInfo("Tr");

            CreateLocalizedPage(ref pageId, pageTitle + defaultCulture.Name, parentId, LocalizationStrategy.Split, defaultCulture, NodeType.Standard);
            CreateLocalizedPage(ref pageId, pageTitle + secondCulture.Name, parentId, LocalizationStrategy.Split, secondCulture, NodeType.Standard);
            return pageId;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        private static IList<KeyValuePair<Guid, CultureInfo>> CreateLocalizedPage(string pageTitle, CultureInfo[] cultures)
        {
            var pageID = new Guid();
            var result = new List<KeyValuePair<Guid, CultureInfo>>();
            foreach (var culture in cultures)
            {
                CreateLocalizedPage(ref pageID, pageTitle + culture.LCID, Guid.Empty, LocalizationStrategy.Split, culture, NodeType.Standard);
                result.Add(new KeyValuePair<Guid, CultureInfo>(pageID, culture));
            }

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        private static bool CreateLocalizedPage(ref Guid pageId, string pageName, Guid parentPageId, LocalizationStrategy localizationStrategy, CultureInfo cultureInfo, NodeType nodeType)
        {
            var currentCulture = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;

            var manager = PageManager.GetManager();

            PageNode pageNode = null;

            var result = false;

            var id = pageId;
            pageNode = manager.GetPageNodes()
                              .Where(n => n.Id == id)
                              .SingleOrDefault();

            if (pageNode != null && pageNode.AvailableCultures.Contains(cultureInfo))
            {
                pageId = pageNode.Id;
                return result;
            }

            result = true;

            if (pageNode == null)
            {
                var parentId = parentPageId;

                if (parentId == Guid.Empty)
                {
                    parentId = SiteInitializer.CurrentFrontendRootNodeId;
                }
                ////Create Page
                PageNode parent = manager.GetPageNode(parentId);
                pageNode = manager.CreatePage(parent, pageId, nodeType);

                pageNode.Name = pageName;
                pageNode.UrlName = pageName;
                pageNode.Description = pageName;
                pageNode.Title = pageName;
                pageNode.ShowInNavigation = true;
                pageNode.DateCreated = DateTime.UtcNow;
                pageNode.LastModified = DateTime.UtcNow;
                pageNode.ShowInNavigation = true;
                pageId = pageNode.Id;
            }
            else
            {
                ////TranslatePage
                pageNode.UrlName = pageName;
                pageNode.Description = pageName;
                pageNode.Title = pageName;

                manager.InitializePageLocalizationStrategy(pageNode, localizationStrategy, false);
            }

            var pageData = pageNode.GetPageData();
            if (pageData != null)
            {
                pageData.HtmlTitle[cultureInfo] = pageName;
                pageData.Title[cultureInfo] = pageName;
                pageData.Description[cultureInfo] = pageName;
                var draft = manager.EditPage(pageData.Id, true);
                manager.PublishPageDraft(draft, true, null);
            }

            manager.RecompileItemUrls<PageNode>(pageNode);
            manager.SaveChanges();

            Thread.CurrentThread.CurrentUICulture = currentCulture;
            Thread.CurrentThread.CurrentCulture = currentCulture;

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.EndsWith(System.String)")]
        private void AssertContainsLanguageLinks(string pageContent, Dictionary<string, string> links)
        {
            using (HtmlParser parser = new HtmlParser(pageContent))
            {
                HtmlChunk chunk = null;
                parser.SetChunkHashMode(false);
                parser.AutoExtractBetweenTagsOnly = true;
                parser.CompressWhiteSpaceBeforeTag = true;
                parser.KeepRawHTML = true;

                while ((chunk = parser.ParseNext()) != null)
                {
                    if (chunk.TagName.Equals("a") && !chunk.IsClosure)
                    {
                        var foundLanguage = string.Empty;

                        foreach (var link in links)
                        {
                            if (chunk.GetParamValue("href").EndsWith(link.Value))
                            {
                                var contentChunk = parser.ParseNext();
                                var encodedString = HttpUtility.HtmlEncode(link.Key);
                                Assert.AreEqual(encodedString, contentChunk.Html);                               
                                foundLanguage = link.Key;
                                break;
                            }
                        }

                        Assert.IsFalse(string.IsNullOrEmpty(foundLanguage));
                        links.Remove(foundLanguage);
                    }
                }
            }
        }

        #endregion
    }
}