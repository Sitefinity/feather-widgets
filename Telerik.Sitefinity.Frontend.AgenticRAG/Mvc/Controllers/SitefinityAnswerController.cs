using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.AgenticRAG.Mvc.Models;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.AttributeConfigurator.Attributes;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.AgenticRAG.Mvc.Controllers
{
    [Telerik.Sitefinity.Mvc.ControllerToolboxItem(
        Name = WidgetName,
        Title = "AI answer",
        SectionName = SitefinityAskBoxController.SectionName,
        CssClass = WidgetIconCssClass,
        Ordinal = 2)]
    public class SitefinityAnswerController : Controller
    {
        internal const string WidgetName = "AIAnswer_MVC";
        private const string WidgetIconCssClass = "sfContentBlockIcn sfMvcIcn";
        private const string DisplaySettingsSectionName = "Display settings";
        private const string SetupSectionName = "AI answer setup";
        private const string LabelsSectionName = "Labels and messages";

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(SetupSectionName, 0)]
        [DefaultValue("AI answer")]
        [DisplayName("Answer label")]
        [Description("Label shown above the AI-generated text.")]
        public string Title { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(SetupSectionName, 1)]
        [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false)]
        [DisplayName("Icon")]
        public MixedContentContext AssistantAvatar { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(SetupSectionName, 2)]
        [DisplayName("Searched phrase")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Include...")]
        public bool? ShowSearchedPhrase { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(SetupSectionName, 3)]
        [DisplayName("Sources")]
        [Description("In AI-generated answer, display links to sources of information.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Include...")]
        public bool? ShowSources { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(SetupSectionName, 4)]
        [DisplayName("Notice")]
        [Description("Text displayed under the answer, informing users that they are interacting with AI.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Include...")]
        public bool? ShowNotice { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(SetupSectionName, 6)]
        [DisplayName("Enable visitor feedback")]
        [Description("If enabled, site visitors can provide feedback on the generated answer.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        public bool? ShowFeedback { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(DisplaySettingsSectionName, 0)]
        [ViewSelector]
        [DisplayName("AI answer template")]
        [DefaultValue("Default")]
        public string SfViewName { get; set; }

        [Category(PropertyCategory.Advanced)]
        [DisplayName("Custom CSS class")]
        public string CssClass { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 0)]
        [DefaultValue("Answer for \"{0}\"")]
        [DisplayName("Searched phrase label")]
        public string SearchedPhraseLabel { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 1)]
        [DefaultValue("AI answer may contain mistakes.")]
        public string Notice { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 2)]
        [DefaultValue("Helpful")]
        [DisplayName("Positive feedback tooltip")]
        public string PositiveFeedbackTooltip { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 3)]
        [DefaultValue("Not helpful")]
        [DisplayName("Negative feedback tooltip")]
        public string NegativeFeedbackTooltip { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 4)]
        [DefaultValue("Thank you for your feedback!")]
        [DisplayName("Thank you message")]
        public string ThankYouMessage { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 5)]
        [DefaultValue("Show more")]
        [DisplayName("Expand answer")]
        public string ExpandAnswerLabel { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 6)]
        [DefaultValue("Show less")]
        [DisplayName("Collapse answer")]
        public string CollapseAnswerLabel { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 7)]
        [DefaultValue("Putting together an answer")]
        [DisplayName("Loading text")]
        public string LoadingLabel { get; set; }

        public ActionResult Index()
        {
            var knowledgeBoxName = HttpContext.Request.QueryStringGet("knowledgeBoxName");
            var searchConfiguarionName = HttpContext.Request.QueryStringGet("searchConfigurationName");
            var searchQuery = HttpContext.Request.QueryStringGet("searchQuery");
            var contentTypes = HttpContext.Request.QueryStringGet("contentTypes");
            var lastModified = HttpContext.Request.QueryStringGet("lastModified");

            var viewModel = new AnswerViewModel
            {
                Title = !string.IsNullOrEmpty(this.Title) ? this.Title : "AI answer",
                AssistantAvatarUrl = this.GetImageUrl(this.AssistantAvatar),
                ShowSources = this.ShowSources.HasValue ? this.ShowSources.Value : true,
                Notice = this.ShowNotice.HasValue && this.ShowNotice.Value ?
                    !string.IsNullOrEmpty(this.Notice) ? this.Notice : "AI answer may contain mistakes." :
                    null,
                ShowFeedback = this.ShowFeedback.HasValue ? this.ShowFeedback.Value : true,
                CssClass = this.CssClass,
                SearchedPhraseLabel = this.ShowSearchedPhrase.HasValue && this.ShowSearchedPhrase.Value ? 
                    !string.IsNullOrEmpty(this.SearchedPhraseLabel) ? this.SearchedPhraseLabel : "Answer for \"{0}\"" :
                    null,
                PositiveFeedbackTooltip = !string.IsNullOrEmpty(this.PositiveFeedbackTooltip) ? this.PositiveFeedbackTooltip : "Helpful",
                NegativeFeedbackTooltip = !string.IsNullOrEmpty(this.NegativeFeedbackTooltip) ? this.NegativeFeedbackTooltip : "Not helpful",
                ThankYouMessage = !string.IsNullOrEmpty(this.ThankYouMessage) ? this.ThankYouMessage : "Thank you for your feedback!",
                ExpandAnswerLabel = !string.IsNullOrEmpty(this.ExpandAnswerLabel) ? this.ExpandAnswerLabel : "Show more",
                CollapseAnswerLabel = !string.IsNullOrEmpty(this.CollapseAnswerLabel) ? this.CollapseAnswerLabel : "Show less",
                LoadingLabel = !string.IsNullOrEmpty(this.LoadingLabel) ? this.LoadingLabel : "Putting together an answer",
                ConfigName = searchConfiguarionName,
                KnowledgeBoxName = knowledgeBoxName,
                SearchQuery = searchQuery,
                LastModified = lastModified,
                ContentTypes = contentTypes,
                ServiceUrl = RouteHelper.ResolveUrl("/api/default/AgenticRag/", UrlResolveOptions.Rooted)
            };

            return View(this.SfViewName ?? "Default", viewModel);
        }

        private string GetImageUrl(MixedContentContext image)
        {
            if (image != null)
            {
                var imageProvider = image.Content[0].Variations[0].Source;
                var imageId = new Guid(image.ItemIdsOrdered[0]);
                var librariesManager = LibrariesManager.GetManager(imageProvider);
                return librariesManager.GetMediaItem(imageId).Url;
            }

            return null;
        }
    }
}
