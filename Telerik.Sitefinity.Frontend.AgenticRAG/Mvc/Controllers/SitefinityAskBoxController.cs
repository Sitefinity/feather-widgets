using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.Renderer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.AgenticRAG.Mvc.Models;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.AttributeConfigurator.Attributes;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.AgenticRAG.Mvc.Controllers
{
    [Telerik.Sitefinity.Mvc.ControllerToolboxItem(
        Name = WidgetName,
        Title = "AI ask box",
        SectionName = SectionName,
        CssClass = WidgetIconCssClass,
        Ordinal = 1)]
    public class SitefinityAskBoxController : Controller, IPersonalizable, ICustomWidgetVisualizationExtended
    {
        internal const string WidgetName = "AIAskBox_MVC";
        internal const string SectionName = "AI search";
        private const string WidgetIconCssClass = "sfSearchBoxIcn sfMvcIcn";
        private const string SetupSectionName = "AI ask box setup";
        private const string LabelsSectionName = "Labels and messages";
        private const string DisplaySettingsSectionName = "Display settings"; 
        private const string KnowledgeScopeSectionName = "Knowledge scope";

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(SetupSectionName, 0)]
        [DisplayName("Agentic RAG connection")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"A connection to a specific knowledge box in Progress Agentic RAG. Select which connection this widget should use to search and answer questions.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Manage connections in \",\"Presentation\":[]},{\"Value\":\"Administration > Progress Agentic Rag connections\",\"Presentation\":[3]}]}]")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetConfiguredKnowledgeBoxes()", ServiceWarningMessage = "No Agentic RAG connections are found.")]
        [Placeholder("Select connection")]
        public string KnowledgeBoxName { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(SetupSectionName, 1)]
        [DisplayName("Search configuration")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"A saved set of search settings that the AI uses to find content.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Can be found in Progress Agentic Rag portal \",\"Presentation\":[]},{\"Value\":\"Search > Saved configurations\",\"Presentation\":[3]}]}]")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetSearchConfigurations(knowledgeBoxName=\'{0}\')", ServiceCallParameters = "[{ \"knowledgeBoxName\" : \"{0}\"}]")]
        public string ConfigurationName { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(SetupSectionName, 2)]
        [DisplayName("After search is submitted...")]
        [Description("This is the page where you have dropped the AI answer and/or AI results widgets.")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        [DefaultValue("stay")]
        [Choice("[{\"Title\":\"Stay on the same page\",\"Name\":\"stay\",\"Value\":\"stay\"},{\"Title\":\"Redirect to page...\",\"Name\":\"redirect\",\"Value\":\"redirect\"}]")]
        public string RedirectPageMode { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(SetupSectionName, 3)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"RedirectPageMode\",\"operator\":\"Equals\",\"value\":\"redirect\"}],\"inline\":\"true\"}")]
        [Required(ErrorMessage = "Please select a search results page")]
        public MixedContentContext SearchResultsPage { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(SetupSectionName, 4)]
        [DisplayName("Suggestions")]
        [Description("Suggestions are example questions or phrases displayed under the AI ask box.")]
        [TableView(Reorderable = true)]
        [DataType("enumerable")]
        public string Suggestions { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(KnowledgeScopeSectionName, 0)]
        [DisplayName("Base answers on...")]
        [Description("Choose which content types this widget can use as knowledge when searching or answering questions.")]
        [ContentTypeSelector(CanSelectAll = true, IsMultipleSelection = true, IsLifecycleContentType = true, UseFullTypeName = true)]
        [PropertyPersistence(PersistAsJson = true)]
        public IList<string> ContentTypes { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(KnowledgeScopeSectionName, 1)]
        [DisplayName("Content modified")]
        [DatePeriodSelector(DateTimeMode = DateTimeMode.DateOnly, FromToMode = FromToMode.FromOnly)]
        public DateTimeFilterValue ModifiedDateFilter { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(DisplaySettingsSectionName, 0)]
        [ViewSelector]
        [DisplayName("AI ask box template")]
        public string SfViewName { get; set; }

        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")] 
        public string CssClass { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 0)]
        [DisplayName("AI ask box placeholder text")]
        [DefaultValue("Search...")]
        public string Placeholder { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 1)]
        [DisplayName("Submit button label")]
        [DefaultValue("Search")]
        public string ButtonLabel { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 2)]
        [DisplayName("Suggestions label")]
        [DefaultValue("Try searching for:")]
        public string SuggestionsLabel { get; set; }

        public ActionResult Index()
        {
            var viewModel = new AskBoxViewModel()
            {
                KnowledgeBoxName = this.KnowledgeBoxName,
                SearchConfigurationName = this.ConfigurationName,
                ResultsPageUrl = this.RedirectPageMode == "redirect" ? GetPageNodeUrl(this.SearchResultsPage) : null,
                CssClass = this.CssClass,
                Placeholder = this.Placeholder,
                ButtonLabel = this.ButtonLabel,
                SuggestionsLabel = this.SuggestionsLabel,
                Suggestions = this.Suggestions,
                ContentTypes = this.ContentTypes != null ? string.Join(",", this.ContentTypes) : null,
                LastModified = ResolveDate(this.ModifiedDateFilter)
            };

            if (!this.IsEmpty)
            {
                var query = this.GetSearchQueryFromQueryString(this.KnowledgeBoxName);
                this.ViewBag.SearchQuery = query;
            }

            return View(this.SfViewName ?? "Default", viewModel);
        }

        [Browsable(false)]
        public string EmptyLinkText
        {
            get
            {
                return "Set where to search";
            }
        }

        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return KnowledgeBoxName.IsNullOrEmpty();
            }
        }

        [Browsable(false)]
        public string WidgetCssClass
        {
            get
            {
                return WidgetIconCssClass;
            }
        }

        private static string GetPageNodeUrl(MixedContentContext context)
        {
            var resultsUrl = string.Empty;
            if (context?.Content?[0]?.Variations?.Length != 0 && context?.ItemIdsOrdered?.Length == 1)
            {
                var provider = SiteMapBase.GetSiteMapProvider(SiteMapBase.DefaultSiteMapProviderName);
                var resultsPageId = context.ItemIdsOrdered[0];

                if (provider != null)
                {
                    var sitemapBase = provider as SiteMapBase;
                    var node = sitemapBase == null ? provider.FindSiteMapNodeFromKey(resultsPageId) : sitemapBase.FindSiteMapNodeFromKey(resultsPageId, false);

                    if (node != null)
                    {
                        resultsUrl = node.Url;
                    }
                }

                if (string.IsNullOrEmpty(resultsUrl))
                {
                    var node = SiteMapBase.GetActualCurrentNode();
                    if (node != null)
                        resultsUrl = node.Url;
                }

                // If ML is using different domains, the url does not need to be resolved
                if (!RouteHelper.IsCompleteUrl(resultsUrl))
                {
                    return RouteHelper.ResolveUrl(resultsUrl, UrlResolveOptions.Rooted);
                }
                else
                {
                    return resultsUrl;
                }
            }

            return resultsUrl;
        }

        private string GetSearchQueryFromQueryString(string currentKnowledgeBoxName)
        {
            var searchQuery = string.Empty;

            // Set the search text if searchQuery exists in the QueryString and the knowledgeBoxName matches the current one.
            var context = SystemManager.CurrentHttpContext;
            if (context != null)
            {
                string knowledgeBox = context.Request.QueryStringGet("knowledgeBoxName");
                if (!string.IsNullOrEmpty(knowledgeBox) &&
                    knowledgeBox.Equals(currentKnowledgeBoxName))
                {
                    searchQuery = context.Request.QueryStringGet("searchQuery") ?? string.Empty;
                    searchQuery = searchQuery.Trim();
                }
            }

            return searchQuery;
        }

        private static string ResolveDate(DateTimeFilterValue filter)
        {
            if (filter == null)
                return null;

            switch (filter.PeriodType)
            {
                case PeriodType.Last:
                    var now = DateTime.UtcNow.Date;
                    DateTime? date = null;
                    switch (filter.TimeSpanInterval)
                    {
                        case TimeSpanInterval.Days: date = now.AddDays(-filter.TimeSpanValue); break;
                        case TimeSpanInterval.Weeks: date = now.AddDays(-filter.TimeSpanValue * 7); break;
                        case TimeSpanInterval.Months: date = now.AddMonths(-filter.TimeSpanValue); break;
                        case TimeSpanInterval.Years: date = now.AddYears(-filter.TimeSpanValue); break;
                        default: date = null; break;
                    };

                    return date?.ToString("O", DateTimeFormatInfo.InvariantInfo);

                case PeriodType.Period:
                    return filter.FromDate?.Date.ToString("O", DateTimeFormatInfo.InvariantInfo);

                default:
                    return null;
            }
        }
    }
}
