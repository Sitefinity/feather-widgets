using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector
{
    /// <summary>
    /// This class represents model used for Language selector widget.
    /// </summary>
    public class LanguageSelectorModel : ILanguageSelectorModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageSelectorModel" /> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LanguageSelectorModel()
        {
            this.urlService = this.InitializeUrlLocalizationService();
        }

        #endregion

        #region Properties
        /// <inheritdoc />
        public bool IncludeCurrentLanguage { get; set; }

        /// <inheritdoc />
        public NoTranslationAction MissingTranslationAction { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        #endregion

        #region Public methods

        /// <inheritdoc />
        public LanguageSelectorViewModel CreateViewModel()
        {
            var viewModel = new LanguageSelectorViewModel();
            viewModel.IncludeCurrentLanguage = this.IncludeCurrentLanguage;
            viewModel.CurrentLanguage = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            IEnumerable<CultureInfo> shownLanguages = this.GetLanguagesToDisplay();

            foreach (var lang in shownLanguages)
            {
                var langName = this.GetDisplayedLanguageName(lang);
                var url = this.GetUrlForLanguage(lang);
                url = RouteHelper.ResolveUrl(url, UrlResolveOptions.Absolute);

                viewModel.Languages.Add(new LanguageSelectorItem(langName, url, lang.TwoLetterISOLanguageName));
            }

            return viewModel;
        }

        #endregion

        #region Virtual methods

        /// <summary>
        /// Initializes the URL localization service.
        /// </summary>
        /// <returns></returns>
        protected virtual UrlLocalizationService InitializeUrlLocalizationService()
        {
            return ObjectFactory.Resolve<UrlLocalizationService>();
        }

        /// <summary>
        /// Resolves the page URL.
        /// </summary>
        /// <param name="pageNode">The page node.</param>
        /// <param name="targetCulture">The target culture.</param>
        /// <param name="useNewImplementation">The use new implementation.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        protected virtual string ResolvePageUrl(PageNode pageNode, CultureInfo targetCulture, bool useNewImplementation)
        {
            return this.urlService.ResolvePageUrl(pageNode, targetCulture, true);
        }

        /// <summary>
        /// Resolves the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="targetCulture">The target culture.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        protected virtual string ResolveUrl(string url, CultureInfo targetCulture)
        {
            return this.urlService.ResolveUrl(url, targetCulture);
        }

        /// <summary>
        /// Gets the languages to display.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected virtual IEnumerable<CultureInfo> GetLanguagesToDisplay()
        {
            ////This is the current node - may be a group page node
            ////var sitemapNode = SiteMapBase.GetCurrentNode();
            ////This is the real node - may be the same as sitemapNode, but is never a group page node
            var actualSitemapNode = SiteMapBase.GetActualCurrentNode();

            PageManager pm = PageManager.GetManager();

            var homePageId = SystemManager.CurrentContext.CurrentSite.HomePageId;
            IEnumerable<CultureInfo> availableLanguages = null;

            Guid nodeId;
            if (actualSitemapNode != null)
            {
                nodeId = actualSitemapNode.Id;
            }
            else
            {
                nodeId = homePageId;
            }

            ////This is used for generating links to language versions - we want it to be the current node, not the "real node".
            this.node = pm.GetPageNode(nodeId);

            if (actualSitemapNode != null)
            {
                availableLanguages = actualSitemapNode.AvailableLanguages;
            }
            else
            {
                availableLanguages = this.node.AvailableCultures;
            }
            
            this.usedLanguages = this.GetLanguagesForPage(actualSitemapNode, availableLanguages);

            IEnumerable<CultureInfo> shownLanguages = this.GetLanguagesList(pm, homePageId);

            return shownLanguages;
        }

        #endregion
        
        #region Private methods

        /// <summary>
        /// Gets used languages for the page, excluding invariant language
        /// </summary>
        /// <param name="sitemapNode">The sitemap node.</param>
        /// <param name="availableLanguages">The available languages.</param>
        private List<CultureInfo> GetLanguagesForPage(PageSiteNode sitemapNode, IEnumerable<CultureInfo> availableLanguages)
        {
            var usedLanguages = new List<CultureInfo>();

            if (sitemapNode != null)
            {
                availableLanguages.ToList().ForEach(ci =>
                {
                    if (!ci.Equals(CultureInfo.InvariantCulture))
                    {
                        bool isHidden = sitemapNode.IsHidden(ci);

                        if (!isHidden)
                        {
                            usedLanguages.Add(ci);
                        }
                    }
                });
            }

            return usedLanguages;
        }

        private IEnumerable<CultureInfo> GetLanguagesList(PageManager pm, Guid homePageId)
        {
            ////Get languages to list
            List<CultureInfo> languages = new List<CultureInfo>();
            var settings = Telerik.Sitefinity.Services.SystemManager.CurrentContext.AppSettings;
            if (this.MissingTranslationAction == NoTranslationAction.HideLink)
            {
                languages.AddRange(this.usedLanguages);
            }
            else
            {
                languages.AddRange(settings.DefinedFrontendLanguages);
                if (homePageId != Guid.Empty)
                {
                    this.homePageNode = pm.GetPageNode(homePageId);
                }
            }

            ////Remove current language, if necessary
            IEnumerable<CultureInfo> shownLanguages;
            CultureInfo currentLanguage = Thread.CurrentThread.CurrentUICulture;
            if (!this.IncludeCurrentLanguage)
            {
                shownLanguages = languages.Where(ci => !ci.Equals(currentLanguage));
            }
            else
            {
                shownLanguages = languages;
            }

            return shownLanguages;
        }

        /// <summary>
        /// Gets the display name of the language.
        /// </summary>
        /// <param name="lang">The lang.</param>
        private string GetDisplayedLanguageName(CultureInfo lang)
        {
            return lang.NativeName;
        }

        /// <summary>
        /// Gets the URL used for links to language version.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        private string GetUrlForLanguage(CultureInfo culture)
        {
            string url = null;

            if (this.MissingTranslationAction == NoTranslationAction.RedirectToPage)
            {
                ////If the current page has no translation in the current language, set the URL to the home page in this language
                if (this.usedLanguages.Contains(culture) == false)
                {
                    url = this.GetMissingLanguageUrl(culture);
                }
            }

            if (url == null)
            {
                var currentUiCulture = Thread.CurrentThread.CurrentUICulture;
                Thread.CurrentThread.CurrentUICulture = culture;
                try
                {
                    var siteNode = SiteMapBase.GetCurrentProvider().FindSiteMapNodeFromKey(this.node.Id.ToString());
                    if (siteNode != null)
                        url = siteNode.Url;
                    else
                        url = this.ResolveUrl("~/", culture);
                }
                finally
                {
                    Thread.CurrentThread.CurrentUICulture = currentUiCulture;
                }
            }

            return url;
        }

        /// <summary>
        /// Returns the URL used for links to missing language version.
        /// </summary>
        /// <param name="culture">The language that is missing.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        private string GetMissingLanguageUrl(CultureInfo culture)
        {
            if (this.homePageNode != null)
            {
                return this.ResolvePageUrl(this.homePageNode, culture, true);
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Private fields and constants

        private UrlLocalizationService urlService;
        private List<CultureInfo> usedLanguages;
        private Pages.Model.PageNode node;
        private Pages.Model.PageNode homePageNode;
        #endregion
    }
}
