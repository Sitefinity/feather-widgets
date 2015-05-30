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

        [Test]
        [Multilingual]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies language selector current language included")]
        public void LanguageSelectorWidget_CurrentLanguageIncluded()
        {
            var languageSelectorControl = new MvcControllerProxy();
            languageSelectorControl.ControllerName = typeof(LanguageSelectorController).FullName;
            var languageSelectorController = new LanguageSelectorController();
            languageSelectorControl.Settings = new ControllerSettings(languageSelectorController);
            languageSelectorController.Model.IncludeCurrentLanguage = true;

            var controls = new List<System.Web.UI.Control>();
            controls.Add(languageSelectorControl);

            var sitefinityLanguages = AppSettings.CurrentSettings.DefinedFrontendLanguages;

            var pageLanguages = new[] 
            {
                sitefinityLanguages[1], ////en-US
                sitefinityLanguages[2]   ////tr-TR
            };

            var pageName = "TestPage";
            
            var createdPages = this.CreateLocalizedPage(pageName, pageLanguages);

            // Add language selector widget to the en-US page
            var currentPage = createdPages.First();
            PageContentGenerator.AddControlsToPage(currentPage.Key, controls);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + pageName + currentPage.Value.Name);
            var pageContent = PageInvoker.ExecuteWebRequest(url);
            Assert.IsNotNull(pageContent);

            var expectedLinkes = new Dictionary<string, string>()
            {
                { sitefinityLanguages[1].NativeName, this.GetPageUrl(pageName, sitefinityLanguages[1], true) }, // en-US
                { sitefinityLanguages[2].NativeName, this.GetPageUrl(pageName, sitefinityLanguages[2]) } // tr-TR
            };

            var notExpectedLinkes = new Dictionary<string, string>()
            {
                { sitefinityLanguages[3].NativeName, this.GetPageUrl(pageName, sitefinityLanguages[3]) }, // ar-MA
                { sitefinityLanguages[4].NativeName, this.GetPageUrl(pageName, sitefinityLanguages[4]) } // sr-cyrl-ba
            };

            this.AssertLanguageLinks(pageContent, expectedLinkes, notExpectedLinkes);           
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies language selector current language not included")]
        public void LanguageSelectorWidget_CurrentLanguageNotIncluded()
        {
            var languageSelectorControl = new MvcControllerProxy();
            languageSelectorControl.ControllerName = typeof(LanguageSelectorController).FullName;
            var languageSelectorController = new LanguageSelectorController();
            languageSelectorControl.Settings = new ControllerSettings(languageSelectorController);
            var controls = new List<System.Web.UI.Control>();
            controls.Add(languageSelectorControl);

            var sitefinityLanguages = AppSettings.CurrentSettings.DefinedFrontendLanguages;

            var pageLanguages = new[]
            {
                sitefinityLanguages[1], ////en-US
                sitefinityLanguages[2]   ////tr-TR
            };

            var pageName = "TestPage";

            var createdPages = this.CreateLocalizedPage(pageName, pageLanguages);

            // Add language selector widget to the en-US page
            var currentPage = createdPages.First();
            PageContentGenerator.AddControlsToPage(currentPage.Key, controls);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + pageName + currentPage.Value.Name);
            var pageContent = PageInvoker.ExecuteWebRequest(url);
            Assert.IsNotNull(pageContent);

            var expectedLinkes = new Dictionary<string, string>()
            {
                { sitefinityLanguages[2].NativeName, this.GetPageUrl(pageName, sitefinityLanguages[2]) } // tr-TR
            };

            var notExpectedLinkes = new Dictionary<string, string>()
            {
                { sitefinityLanguages[1].NativeName, this.GetPageUrl(pageName, sitefinityLanguages[1], true) }, // en-US
                { sitefinityLanguages[3].NativeName, this.GetPageUrl(pageName, sitefinityLanguages[3]) }, // ar-MA
                { sitefinityLanguages[4].NativeName, this.GetPageUrl(pageName, sitefinityLanguages[4]) } // sr-cyrl-ba
            };

            this.AssertLanguageLinks(pageContent, expectedLinkes, notExpectedLinkes);
        }

        #region Helper methods

        private string GetPageUrl(string pageName, CultureInfo culture, bool isDefaultCulture = false)
        {
            if (isDefaultCulture)
            {
                // returns /TestPageen-US
                return string.Format(CultureInfo.InvariantCulture, "/{0}{1}", pageName, culture.Name);
            }
            else
            {
                // returns /tr-tr/TestPagetr-TR
                return string.Format(CultureInfo.InvariantCulture, "/{0}/{1}{2}", culture.Name.ToLower(), pageName, culture.Name);
            }
        }

        private IList<KeyValuePair<Guid, CultureInfo>> CreateLocalizedPage(string pageTitle, CultureInfo[] cultures)
        {
            var pageID = new Guid();
            var result = new List<KeyValuePair<Guid, CultureInfo>>();
            foreach (var culture in cultures)
            {
                this.CreateLocalizedPage(ref pageID, pageTitle + culture.Name, Guid.Empty, LocalizationStrategy.Split, culture, NodeType.Standard);
                result.Add(new KeyValuePair<Guid, CultureInfo>(pageID, culture));
            }

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        private bool CreateLocalizedPage(ref Guid pageId, string pageName, Guid parentPageId, LocalizationStrategy localizationStrategy, CultureInfo cultureInfo, NodeType nodeType)
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
                pageData.NavigationNode.Title[cultureInfo] = pageName;
                pageData.Description[cultureInfo] = pageName;
                var draft = manager.EditPage(pageData.Id);
                manager.PublishPageDraft(draft, cultureInfo);
            }

            manager.RecompileItemUrls<PageNode>(pageNode);
            manager.SaveChanges();

            Thread.CurrentThread.CurrentUICulture = currentCulture;
            Thread.CurrentThread.CurrentCulture = currentCulture;

            return result;
        }

        private void AssertLanguageLinks(string pageContent, Dictionary<string, string> links, Dictionary<string, string> notVisiblelinks)
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
                        foreach (var link in notVisiblelinks)
                        {
                            if (chunk.GetParamValue("href") != null)
                            {
                                Assert.IsFalse(
                                    chunk.GetParamValue("href").EndsWith(link.Value, StringComparison.Ordinal), 
                                    string.Format(CultureInfo.InvariantCulture, "The link {0} is found but is not expected.", chunk.GetParamValue("href")));
                            }
                        }

                        var foundLanguage = string.Empty;

                        foreach (var link in links)
                        {
                            if (chunk.GetParamValue("href").EndsWith(link.Value, StringComparison.Ordinal))
                            {
                                var contentChunk = parser.ParseNext();
                                Assert.AreEqual(
                                    HttpUtility.HtmlEncode(link.Key), 
                                    contentChunk.Html,
                                    string.Format(CultureInfo.InvariantCulture, "The link display name {0} is not correct.", contentChunk.Html));
                                foundLanguage = link.Key;
                                break;
                            }
                        }

                        Assert.IsFalse(
                            string.IsNullOrEmpty(foundLanguage), 
                            string.Format(CultureInfo.InvariantCulture, "Current link {0} is not expected.", chunk.GetParamValue("href")));                
                    }
                }
            }
        }
    }
    
    #endregion
}