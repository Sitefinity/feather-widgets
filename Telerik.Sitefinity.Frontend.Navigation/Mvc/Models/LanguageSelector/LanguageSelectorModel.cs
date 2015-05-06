using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector
{
    /// <summary>
    /// This class represents model used for Language selector widget.
    /// </summary>
    public class LanguageSelectorModel : ILanguageSelectorModel
    {
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

        #region Private methods

        /// <summary>
        /// Gets the languages to display.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        private IEnumerable<CultureInfo> GetLanguagesToDisplay()
        {
            var settings = Telerik.Sitefinity.Services.SystemManager.CurrentContext.AppSettings;

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

            CultureInfo currentLanguage = Thread.CurrentThread.CurrentUICulture;

            ////Get used languages for the page, excluding invariant language
            this.usedLanguages = new List<CultureInfo>();

            if (actualSitemapNode != null)
            {
                availableLanguages.ToList().ForEach(ci =>
                {
                    if (!ci.Equals(CultureInfo.InvariantCulture))
                    {
                        bool isHidden = actualSitemapNode.IsHidden(ci);

                        if (!isHidden)
                        {
                            this.usedLanguages.Add(ci);
                        }
                    }
                });
            }

            ////Get languages to list
            List<CultureInfo> languages = new List<CultureInfo>();
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
                        url = this.urlService.ResolveUrl("~/", culture);
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
                return this.urlService.ResolvePageUrl(this.homePageNode, culture, true);
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Private fields and constants

        private UrlLocalizationService urlService = ObjectFactory.Resolve<UrlLocalizationService>();
        private List<CultureInfo> usedLanguages;
        private Pages.Model.PageNode node;
        private Pages.Model.PageNode homePageNode;
        #endregion
    }
}
