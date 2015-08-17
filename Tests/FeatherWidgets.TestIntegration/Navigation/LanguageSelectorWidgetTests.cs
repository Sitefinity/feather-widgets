using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Web;
using Telerik.WebTestRunner.Server.Attributes;

namespace FeatherWidgets.TestIntegration.Navigation
{
    /// <summary>
    /// This is a class with language selector tests
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), TestFixture]
    [Description("This is a class with language selector tests.")]
    public class LanguageSelectorWidgetTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.InitializeSitefinityLanguages();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            this.serverOperationsNews.DeleteAllNews();
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies language selector current language included")]
        public void LanguageSelectorWidget_CurrentLanguageIncluded()
        {
            var languageSelectorControl = this.CreateLanguageSelectorControl();
            var languageSelectorModel = languageSelectorControl.Settings.Controller.Model;
            languageSelectorModel.IncludeCurrentLanguage = true;

            var controls = new List<System.Web.UI.Control>();
            controls.Add(languageSelectorControl);

            var pageLanguages = new[] 
            {
                this.sitefinityLanguages["English"],
                this.sitefinityLanguages["Turkish"]
            };

            var createdPages = this.CreateLocalizedPage(PageName, pageLanguages);

            // Add language selector widget to the en-US page
            var currentPage = createdPages.First();
            PageContentGenerator.AddControlsToPage(currentPage.Key, controls);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + currentPage.Value.Name);
            var pageContent = PageInvoker.ExecuteWebRequest(url);
            Assert.IsNotNull(pageContent);

            var expectedLinks = new Dictionary<CultureInfo, string>()
            {
                { this.sitefinityLanguages["English"], this.GetPageUrl(PageName, this.sitefinityLanguages["English"], true) },
                { this.sitefinityLanguages["Turkish"], this.GetPageUrl(PageName, this.sitefinityLanguages["Turkish"]) }
            };

            var notExpectedLinks = new Dictionary<CultureInfo, string>()
            {
                { this.sitefinityLanguages["Arabic"], this.GetPageUrl(PageName, this.sitefinityLanguages["Arabic"]) },
                { this.sitefinityLanguages["Serbian"], this.GetPageUrl(PageName, this.sitefinityLanguages["Serbian"]) },
                { this.sitefinityLanguages["Bulgarian"], this.GetPageUrl(PageName, this.sitefinityLanguages["Bulgarian"]) }
            };

            this.AssertLanguageLinks(pageContent, expectedLinks, notExpectedLinks);
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies language selector current language not included")]
        public void LanguageSelectorWidget_CurrentLanguageNotIncluded()
        {
            var languageSelectorControl = this.CreateLanguageSelectorControl();
            var controls = new List<System.Web.UI.Control>();
            controls.Add(languageSelectorControl);

            var pageLanguages = new[]
            {
                this.sitefinityLanguages["English"],
                this.sitefinityLanguages["Turkish"]
            };

            var createdPages = this.CreateLocalizedPage(PageName, pageLanguages);

            // Add language selector widget to the en-US page
            var currentPage = createdPages.First();
            PageContentGenerator.AddControlsToPage(currentPage.Key, controls);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + currentPage.Value.Name);
            var pageContent = PageInvoker.ExecuteWebRequest(url);
            Assert.IsNotNull(pageContent);

            var expectedLinks = new Dictionary<CultureInfo, string>()
            {
                { this.sitefinityLanguages["Turkish"], this.GetPageUrl(PageName, this.sitefinityLanguages["Turkish"]) }
            };

            var notExpectedLinks = new Dictionary<CultureInfo, string>()
            {
                { this.sitefinityLanguages["English"], this.GetPageUrl(PageName, this.sitefinityLanguages["English"], true) },
                { this.sitefinityLanguages["Arabic"], this.GetPageUrl(PageName, this.sitefinityLanguages["Arabic"]) },
                { this.sitefinityLanguages["Serbian"], this.GetPageUrl(PageName, this.sitefinityLanguages["Serbian"]) },
                { this.sitefinityLanguages["Bulgarian"], this.GetPageUrl(PageName, this.sitefinityLanguages["Bulgarian"]) }
            };

            this.AssertLanguageLinks(pageContent, expectedLinks, notExpectedLinks);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Multilingual]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies language selector, with current language included, is included in detail view of content items")]
        public void LanguageSelectorWidget_DetailsViewOfNewsItemWithENTranslationOnly()
        {
            var languageSelectorControl = this.CreateLanguageSelectorControl();
            var languageSelectorModel = languageSelectorControl.Settings.Controller.Model;
            languageSelectorModel.IncludeCurrentLanguage = true;

            var controls = new List<System.Web.UI.Control>();
            controls.Add(languageSelectorControl);

            this.serverOperationsNews.CreateNewsItem("TestNewsItem");
            var newsControl = this.CreateNewsControl();
            controls.Add(newsControl);

            var pageLanguages = new[]
            {
                this.sitefinityLanguages["English"],
                this.sitefinityLanguages["Turkish"]
            };

            var createdPages = this.CreateLocalizedPage(PageName, pageLanguages);

            // Add language selector widget to the en-US page
            var currentPage = createdPages.First();
            PageContentGenerator.AddControlsToPage(currentPage.Key, controls);

            var items = newsControl.Settings.Controller.Model.CreateListViewModel(null, 1).Items.ToArray();
            var expectedDetailNews = (NewsItem)items[0].DataItem;

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + currentPage.Value.Name);
            string detailNewsUrl = url + expectedDetailNews.ItemDefaultUrl;

            var pageContent = PageInvoker.ExecuteWebRequest(detailNewsUrl);
            Assert.IsNotNull(pageContent);

            var expectedLinks = new Dictionary<CultureInfo, string>()
            {
                { this.sitefinityLanguages["English"], this.GetPageUrl(PageName, this.sitefinityLanguages["English"], true) + expectedDetailNews.ItemDefaultUrl },
                { this.sitefinityLanguages["Turkish"], this.GetPageUrl(PageName, this.sitefinityLanguages["Turkish"]) }
            };

            var notExpectedLinks = new Dictionary<CultureInfo, string>()
            {
                { this.sitefinityLanguages["Arabic"], this.GetPageUrl(PageName, this.sitefinityLanguages["Arabic"]) },
                { this.sitefinityLanguages["Serbian"], this.GetPageUrl(PageName, this.sitefinityLanguages["Serbian"]) },
                { this.sitefinityLanguages["Bulgarian"], this.GetPageUrl(PageName, this.sitefinityLanguages["Bulgarian"]) }
            };

            this.AssertLanguageLinks(pageContent, expectedLinks, notExpectedLinks);
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies language selector with redirect option")]
        public void LanguageSelectorWidget_RedirectToHomePageOfTheMissingTranslations()
        {
            var languageSelectorControl = this.CreateLanguageSelectorControl();
            var languageSelectorModel = languageSelectorControl.Settings.Controller.Model;
            languageSelectorModel.MissingTranslationAction = NoTranslationAction.RedirectToPage;
            var controls = new List<System.Web.UI.Control>();
            controls.Add(languageSelectorControl);

            var pageLanguages = new[]
            {
                this.sitefinityLanguages["English"],
                this.sitefinityLanguages["Turkish"]
            };

            var createdPages = this.CreateLocalizedPage(PageName, pageLanguages);

            // Add language selector widget to the en-US page
            var currentPage = createdPages.First();
            PageContentGenerator.AddControlsToPage(currentPage.Key, controls);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + currentPage.Value.Name);
            var pageContent = PageInvoker.ExecuteWebRequest(url);
            Assert.IsNotNull(pageContent);

            var expectedLinks = new Dictionary<CultureInfo, string>()
            {
                { this.sitefinityLanguages["Turkish"], this.GetPageUrl(PageName, this.sitefinityLanguages["Turkish"]) },
                { this.sitefinityLanguages["Arabic"], this.GetPageUrlOfNotTranslatedPage(PageName + currentPage.Value.Name, this.sitefinityLanguages["Arabic"]) },
                { this.sitefinityLanguages["Serbian"], this.GetPageUrlOfNotTranslatedPage(PageName + currentPage.Value.Name, this.sitefinityLanguages["Serbian"]) },
                { this.sitefinityLanguages["Bulgarian"], this.GetPageUrlOfNotTranslatedPage(PageName + currentPage.Value.Name, this.sitefinityLanguages["Bulgarian"]) }
            };

            var notExpectedLinks = new Dictionary<CultureInfo, string>()
            {
                { this.sitefinityLanguages["English"], this.GetPageUrl(PageName, this.sitefinityLanguages["English"], true) }
            };

            this.AssertLanguageLinks(pageContent, expectedLinks, notExpectedLinks);
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

        private string GetPageUrlOfNotTranslatedPage(string defaultCultureHomePageName, CultureInfo culture)
        {
            // returns the url of the home page for the given culture
            return string.Format(CultureInfo.InvariantCulture, "/{0}/{1}", culture.Name.ToLower(), defaultCultureHomePageName);
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

        private void InitializeSitefinityLanguages()
        {
            var english = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "en-US").FirstOrDefault();
            var turkish = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "tr-TR").FirstOrDefault();
            var arabic = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "ar-MA").FirstOrDefault();
            var serbian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "sr-Cyrl-BA").FirstOrDefault();
            var bulgarian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "bg-BG").FirstOrDefault();

            this.sitefinityLanguages.Add("English", english);
            this.sitefinityLanguages.Add("Turkish", turkish);
            this.sitefinityLanguages.Add("Arabic", arabic);
            this.sitefinityLanguages.Add("Serbian", serbian);
            this.sitefinityLanguages.Add("Bulgarian", bulgarian);
        }

        private void AssertLanguageLinks(string pageContent, Dictionary<CultureInfo, string> links, Dictionary<CultureInfo, string> notVisiblelinks)
        {
            using (HtmlParser parser = new HtmlParser(pageContent))
            {
                HtmlChunk chunk = null;
                parser.SetChunkHashMode(false);
                parser.AutoExtractBetweenTagsOnly = true;
                parser.CompressWhiteSpaceBeforeTag = true;
                parser.KeepRawHTML = true;
                var initialLinks = new Dictionary<CultureInfo, string>(links);

                while ((chunk = parser.ParseNext()) != null)
                {
                    if (chunk.TagName.Equals("a") && !chunk.IsClosure && chunk.GetParamValue("onclick") != null)
                    {
                        var linkOnClickAttribute = chunk.GetParamValue("onclick");
                        chunk = parser.ParseNext();
                        var linkText = chunk.Html;

                        foreach (var link in notVisiblelinks)
                        {
                            Assert.IsFalse(
                                linkOnClickAttribute.Contains(link.Key.Name),
                                string.Format(CultureInfo.InvariantCulture, "The anchor tag for culture {0} is found, but is not expected.", linkOnClickAttribute));

                            Assert.AreNotEqual(
                                link.Key.NativeName,
                                linkText,
                                string.Format(CultureInfo.InvariantCulture, "The link display name {0} is found, but is not expected.", linkText));
                        }

                        var foundlanguageCulture = new CultureInfo(string.Empty);

                        foreach (var link in links)
                        {
                            Assert.IsTrue(
                                linkOnClickAttribute.Contains(link.Key.Name),
                                string.Format(CultureInfo.InvariantCulture, "The expected link's culture {0} is not found.", link.Value));

                            Assert.AreEqual(
                                HttpUtility.HtmlEncode(link.Key.NativeName),
                                linkText,
                                string.Format(CultureInfo.InvariantCulture, "The link display name {0} is not correct.", linkText));

                            foundlanguageCulture = link.Key;
                            break;
                        }

                        Assert.IsFalse(
                            string.IsNullOrEmpty(foundlanguageCulture.Name),
                            string.Format(CultureInfo.InvariantCulture, "The anchor tag for culture {0} is not expected.", linkOnClickAttribute));

                        links.Remove(foundlanguageCulture);
                    }
                    else if (chunk.TagName.Equals("input") && chunk.GetParamValue("type") == "hidden" && chunk.GetParamValue("value") != null && chunk.GetParamValue("value").StartsWith("http://", StringComparison.Ordinal))
                    {
                        var dataSfRole = chunk.GetParamValue("data-sf-role");
                        if (dataSfRole != null)
                        {
                            var currentCulture = new CultureInfo(dataSfRole);
                            Assert.IsTrue(initialLinks.ContainsKey(currentCulture), string.Format(CultureInfo.InvariantCulture, "The found hidden input field {0} is not expected", currentCulture));
                            var expectedLink = initialLinks[currentCulture];

                            var hiddenInputValue = chunk.GetParamValue("value");

                            Assert.IsTrue(hiddenInputValue.EndsWith(expectedLink, StringComparison.Ordinal));

                            initialLinks.Remove(currentCulture);
                        }
                    }
                }

                Assert.AreEqual(0, links.Count(), "Not all expected languages are found.");
                Assert.AreEqual(0, initialLinks.Count(), "Not all expected hidden fields of languages are found.");
            }
        }

        private MvcControllerProxy CreateNewsControl()
        {
            var newsSelectorControl = new MvcControllerProxy();
            newsSelectorControl.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsSelectorControl.Settings = new ControllerSettings(newsController);
            return newsSelectorControl;
        }

        private MvcControllerProxy CreateLanguageSelectorControl()
        {
            var languageSelectorControl = new MvcControllerProxy();
            languageSelectorControl.ControllerName = typeof(LanguageSelectorController).FullName;
            var languageSelectorController = new LanguageSelectorController();
            languageSelectorControl.Settings = new ControllerSettings(languageSelectorController);

            return languageSelectorControl;
        }

        #endregion

        #region Private fields and constants

        private const string PageName = "TestPage";
        private readonly Dictionary<string, CultureInfo> sitefinityLanguages = new Dictionary<string, CultureInfo>();
        private readonly NewsOperations serverOperationsNews = ServerOperations.News();

        #endregion
    }
}