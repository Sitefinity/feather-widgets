using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.SiteSelector
{
    /// <summary>
    /// The model of the site selector widget.
    /// </summary>
    public class SiteSelectorModel : ISiteSelectorModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteSelectorModel" /> class.
        /// </summary>
        public SiteSelectorModel()
        {
            this.currentSiteId = SystemManager.CurrentContext.CurrentSite.Id;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the Site selector widget (if such is presented).
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }

        /// <summary>
        /// Determines whether to include the current site in the list of sites.
        /// </summary>
        /// <value>The include current site.</value>
        public bool IncludeCurrentSite
        {
            get
            {
                return this.includeCurrentSite;
            }

            set
            {
                this.includeCurrentSite = value;
            }
        }

        /// <summary>
        /// Determines whether to display each language version as a separate site.
        /// </summary>
        /// <value></value>
        public bool EachLanguageAsSeparateSite { get; set; }

        /// <summary>
        /// Determines how to display each language version of the site.
        /// </summary>
        /// <value>The site languages display mode.</value>
        public SiteLanguagesDisplayMode SiteLanguagesDisplayMode { get; set; }

        /// <summary>
        /// Determines whether to use the live or the staging URLs of the sites.
        /// </summary>
        public bool UseLiveUrl
        {
            get
            {
                return this.useLiveUrl;
            }

            set
            {
                this.useLiveUrl = value;
            }
        }

        /// <summary>
        /// Gets the URL service.
        /// </summary>
        protected UrlLocalizationService UrlService
        {
            get
            {
                if (this.urlService == null)
                {
                    this.urlService = ObjectFactory.Resolve<UrlLocalizationService>();
                }

                return this.urlService;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Creates the site selector's view model from this model.
        /// </summary>
        /// <returns></returns>
        public SiteSelectorViewModel CreateViewModel()
        {
            var vm = new SiteSelectorViewModel();

            vm.CssClass = this.CssClass;
            vm.IncludeCurrentSite = this.IncludeCurrentSite;
            vm.EachLanguageAsSeparateSite = this.EachLanguageAsSeparateSite;
            vm.SiteLanguagesDisplayMode = this.SiteLanguagesDisplayMode;
            vm.Sites = this.GetSitesViewModels();

            return vm;
        }

        #endregion

        #region Virtual methods
        /// <summary>
        /// Gets the view models for each site that will be displayed.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected virtual IList<SiteViewModel> GetSitesViewModels()
        {
            var sites = ((IMultisiteContext)SystemManager.CurrentContext).GetSites();

            if (!this.IncludeCurrentSite)
            {
                sites = sites.Where(s => s.Id != currentSiteId);
            }

            var result = new List<SiteViewModel>();
            foreach (var site in sites)
            {
                // Site is in multilingual and its url should be rendered according to settings
                if (site.PublicContentCultures.Count() > 1 && this.EachLanguageAsSeparateSite)
                {
                    result.AddRange(this.GetMultilingualSiteViewModels(site));
                }
                else
                {
                    CultureInfo cultureInfo = new CultureInfo(site.DefaultCulture);
                    bool isCurrentSite = this.currentSiteId == site.Id;

                    result.Add(this.GetSiteViewModel(site, cultureInfo, isCurrentSite));
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the view models for each culture of the given site.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns></returns>
        protected virtual IList<SiteViewModel> GetMultilingualSiteViewModels(ISite site)
        {
            var result = new List<SiteViewModel>();
            bool addToDataSource = true;
            foreach (var culture in site.PublicContentCultures)
            {
                string siteUrl = string.Empty;
                bool isCurrentSite = false;

                // Handle current site URLs from page node
                if (site.Id == this.currentSiteId)
                {
                    var actualSitemapNode = SiteMapBase.GetActualCurrentNode();
                    if (actualSitemapNode != null)
                    {
                        var actualPageNode = PageManager.GetManager().GetPageNode(actualSitemapNode.Id);
                        addToDataSource =
                            actualPageNode.AvailableCultures.Contains(culture);

                        if (addToDataSource)
                        {
                            if (actualSitemapNode.UiCulture == culture.Name)
                                isCurrentSite = true;

                            siteUrl = this.ResolveDefaultSiteUrl(actualPageNode, culture);
                        }
                    }
                }
                else
                {
                    // Remove the reflection when SiteRegion become public.
                    var sitefinityAssembly = typeof(ISite).Assembly;
                    var siteRegionType = sitefinityAssembly.GetType("Telerik.Sitefinity.Multisite.SiteRegion");
                    var siteRegionConstructor = siteRegionType.GetConstructor(new Type[] { typeof(ISite) });
                    using ((IDisposable)siteRegionConstructor.Invoke(new object[] { site }))
                    {
                        siteUrl = this.UrlService.ResolveUrl(this.GetSiteUrl(site), culture);
                    }
                }

                if (addToDataSource)
                    result.Add(this.GetSiteViewModel(site, culture, isCurrentSite, siteUrl));
            }

            return result;
        }

        /// <summary>
        /// Resolves the default site's URL.
        /// </summary>
        /// <param name="cultureInfo">The culture info.</param>
        /// <param name="siteUrl">The site URL.</param>
        /// <param name="actualPageNode">The actual page node.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        protected virtual string ResolveDefaultSiteUrl(PageNode actualPageNode, CultureInfo cultureInfo)
        {
            var fullUrl = actualPageNode.GetFullUrl(cultureInfo, false, false);
            var siteUrl = this.UrlService.ResolveUrl(fullUrl, cultureInfo);
            siteUrl = RouteHelper.ResolveUrl(siteUrl, UrlResolveOptions.Absolute);
            return siteUrl;
        }

        /// <summary>
        /// Gets the site url based on UseLiveUrl setting of the model.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        protected virtual string GetSiteUrl(ISite site)
        {
            string url = site.StagingUrl;

            if (this.UseLiveUrl)
                url = site.LiveUrl;

            if (!url.EndsWith("/", StringComparison.OrdinalIgnoreCase))
                url = string.Concat(url, "/");

            return url;
        }

        /// <summary>
        /// Gets the view model of the given site.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <param name="isCurrentSite">Determines if the site is the current one.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "3#"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        protected virtual SiteViewModel GetSiteViewModel(ISite site, CultureInfo cultureInfo, bool isCurrentSite, string siteUrl = null)
        {
            if (siteUrl == null)
            {
                siteUrl = this.GetSiteUrl(site);
            }

            if (!string.IsNullOrEmpty(siteUrl) && !siteUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                if (site.RequiresSsl)
                {
                    siteUrl = string.Concat("https://", siteUrl);
                }
                else
                {
                    siteUrl = string.Concat("http://", siteUrl);
                }
            }

            return new SiteViewModel()
            {
                Name = site.Name,
                Language = cultureInfo == null ? site.Name : this.GetDisplayedLanguageName(cultureInfo),
                Url = siteUrl,
                IsCurrent = isCurrentSite
            };
        }

        /// <summary>
        /// Gets the name of the displayed language.
        /// </summary>
        protected virtual string GetDisplayedLanguageName(CultureInfo lang)
        {
            return lang.NativeName;
        }
        #endregion

        #region Private fields and constants
        private readonly Guid currentSiteId;
        private UrlLocalizationService urlService;
        private bool includeCurrentSite = true;
        private bool useLiveUrl = true;
        #endregion
    }
}
