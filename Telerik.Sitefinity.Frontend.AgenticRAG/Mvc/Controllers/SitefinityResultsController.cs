using Newtonsoft.Json;
using Progress.Sitefinity.AgenticRAGConnector.Clients.Builders;
using Progress.Sitefinity.AgenticRAGConnector.Clients.Contracts;
using Progress.Sitefinity.AgenticRAGConnector.Clients.Models.Requests;
using Progress.Sitefinity.AgenticRAGConnector.Clients.Models.Response;
using Progress.Sitefinity.AgenticRAGConnector.Configuration;
using Progress.Sitefinity.AgenticRAGConnector.Controllers;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.AgenticRAG.Mvc.Models;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.AttributeConfigurator.Attributes;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.AgenticRAG.Mvc.Controllers
{
    [Telerik.Sitefinity.Mvc.ControllerToolboxItem(
        Name = WidgetName,
        Title = "AI results",
        SectionName = SitefinityAskBoxController.SectionName,
        Ordinal = 3,
        CssClass = WidgetIconCssClass)]
    public class SitefinityResultsController : Controller
    {
        internal const string WidgetName = "AIResults_MVC";
        private const string WidgetIconCssClass = "sfSearchResultIcn sfMvcIcn";
        private const string ResultsListSettings = "AI results list settings";
        private const string DisplaySettingsSectionName = "Display settings";
        private const string LabelsSectionName = "Labels and messages";
        private readonly IKnowledgeBoxClient knowledgeBoxClient;

        public SitefinityResultsController()
        {
            this.knowledgeBoxClient = ObjectFactory.Resolve<IKnowledgeBoxClient>();
        }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(ResultsListSettings, 0)]
        [DisplayName("Results per page")]
        [DefaultValue(20)]
        [Range(1, 200)]
        public int? PageSize { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(DisplaySettingsSectionName, 0)]
        [ViewSelector]
        [DisplayName("AI results template")]
        [DefaultValue("Default")]
        public string SfViewName { get; set; }

        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 0)]
        [DisplayName("Search results header")]
        [DefaultValue("Results for \"{0}\"")]
        public string SearchResultsHeader { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 1)]
        [DisplayName("No results header")]
        [DefaultValue("No results for \"{0}\"")]
        public string NoResultsHeader { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 2)]
        [DisplayName("Results number label")]
        [DefaultValue("results")]
        public string ResultsNumberLabel { get; set; }

        public ActionResult Index()
        {
            var searchQuery = HttpContext.Request.QueryStringGet("searchQuery");
            var knowledgeBoxName = HttpContext.Request.QueryStringGet("knowledgeBoxName");

            var model = new SearchResultsViewModel();
            model.CssClass = this.CssClass;
            model.ResultsHeader = string.Format(CultureInfo.InvariantCulture, this.NoResultsHeader ?? "No results for \"{0}\"", searchQuery);
            model.ResultsNumberLabel = this.ResultsNumberLabel ?? "results";
            model.PageSize = this.PageSize ?? 20;

            if (!string.IsNullOrEmpty(searchQuery) && !string.IsNullOrEmpty(knowledgeBoxName))
            {
                model.SearchResults = new List<SearchResultModel>();
                FindResponse response = this.PerformSearch();

                if (response != null && response.Resources != null && response.Resources.Count > 0)
                {
                    foreach (var resourceEntry in response.Resources)
                    {
                        var resource = resourceEntry.Value;
                        var result = new SearchResultModel
                        {
                            Title = resource.Title,
                            Link = resource.Origin.Url,
                        };

                        var allParagraphs = new List<Progress.Sitefinity.AgenticRAGConnector.Clients.Models.Paragraph>();
                        if (resource.Fields != null)
                        {
                            foreach (var fieldEntry in resource.Fields)
                            {
                                if (fieldEntry.Value?.Paragraphs != null)
                                {
                                    foreach (var paraEntry in fieldEntry.Value.Paragraphs)
                                    {
                                        allParagraphs.Add(paraEntry.Value);
                                    }
                                }
                            }
                        }

                        allParagraphs.Sort((a, b) => a.Order.CompareTo(b.Order));
                        result.Order = allParagraphs.FirstOrDefault()?.Order ?? 0;
                        model.SearchResults.Add(result);
                    }

                    model.SearchResults.Sort((a, b) => a.Order.CompareTo(b.Order));

                    if (model.SearchResults.Count > 0)
                    {
                        model.ResultsHeader = string.Format(CultureInfo.InvariantCulture, this.SearchResultsHeader ?? "Results for \"{0}\"", searchQuery);
                    }
                }
            }

            return View(this.SfViewName ?? "Default", model);
        }

        private FindResponse PerformSearch()
        {
            var searchQuery = HttpContext.Request.QueryStringGet("searchQuery");
            var knowledgeBoxName = HttpContext.Request.QueryStringGet("knowledgeBoxName");
            var searchConfigurationName = HttpContext.Request.QueryStringGet("searchConfigurationName");
            var contentTypes = HttpContext.Request.QueryStringGet("contentTypes");
            var lastModified = HttpContext.Request.QueryStringGet("lastModified");

            if (!string.IsNullOrEmpty(knowledgeBoxName) && !string.IsNullOrEmpty(searchQuery))
            {
                var config = Config.Get<AgenticRAGConfig>();

                if (!config.KnowledgeBoxes.TryGetValue(knowledgeBoxName, out var kbSettings))
                {
                    throw new ArgumentException($"Knowledge box with name {knowledgeBoxName} does not exist");
                }

                var filterBuilder = new ARAGFilterBuilder()
                    .And(ARAGFilterBuilder.OriginPathFilter(null))
                    .And(ARAGFilterBuilder.LanguageFilter(SystemManager.CurrentContext.Culture.Name))
                    .And(ARAGFilterBuilder.LabelFilter("SiteIds", SystemManager.CurrentContext.CurrentSite.Id.ToString("N")));

                var requestBuilder = new FindRequestBuilder()
                    .WithQuery(searchQuery)
                    .WithSearchConfiguration(searchConfigurationName)
                    .WithShow(new[] { "basic", "extra", "origin" });

                if (!string.IsNullOrEmpty(contentTypes))
                {
                    var contentTypeValues = contentTypes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(contentType => contentType.Trim())
                        .Where(contentType => !string.IsNullOrEmpty(contentType))
                        .ToList();

                    if (contentTypeValues.Count > 0)
                    {
                        var contentTypesFilterBuilder = new ARAGFilterBuilder();
                        foreach (var contentType in contentTypeValues)
                        {
                            contentTypesFilterBuilder.Or(ARAGFilterBuilder.LabelFilter("ContentType", contentType));
                        }

                        filterBuilder.And(contentTypesFilterBuilder);
                    }
                }

                if (!string.IsNullOrEmpty(lastModified))
                {
                    filterBuilder.And(ARAGFilterBuilder.ModifiedDateFilter("modified", lastModified, null));
                }

                var request = requestBuilder
                    .WithFilter(filterBuilder)
                    .WithTake(200)
                    .Build();

                var response = this.knowledgeBoxClient.FindAsync(kbSettings.KnowledgeBoxId, request).GetAwaiter().GetResult();

                return response;
            }

            return null;
        }
    }
}
