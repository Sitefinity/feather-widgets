using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Assistant.DTOs;
using Telerik.Sitefinity.Frontend.Assistant.Mvc.Models;
using Telerik.Sitefinity.Frontend.Assistant.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Assistant.Mvc.Controllers
{
    [Sitefinity.Mvc.ControllerToolboxItem(
        Name = WidgetName,
        Title = nameof(SitefinityAssistantWidgetResources.AIAssistant),
        ResourceClassId = nameof(SitefinityAssistantWidgetResources),
        SectionName = "Marketing",
        CssClass = WidgetIconCssClass)]
    [Localization(typeof(SitefinityAssistantWidgetResources))]
    public class SitefinityAssistantController : Controller, ICustomWidgetVisualizationExtended, IPersonalizable
    {
        internal const string WidgetName = "SitefinityAssistant_MVC";
        private const string WidgetIconCssClass = "sfForumsViewIcn sfMvcIcn";
        private const string LabelsSectionName = "Labels and messages";
        private readonly HttpClient httpClient;

        public SitefinityAssistantController()
        {
            this.httpClient = new HttpClient();
        }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 0)]
        [DisplayName("AI assistant type")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetAvailableAssistantModules()", ServiceWarningMessage = "No AI assistants are found.")]
        [Placeholder("Select assistant type")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Sitefinity AI Assistant: \",\"Presentation\":[0]},{\"Value\":\"Answers from your site's published content only.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Progress Agentic RAG: \",\"Presentation\":[0]},{\"Value\":\"Answers from the selected Agentic RAG connection.\",\"Presentation\":[]}]}]")]
        public string AssistantType { get; set; }

        #region PARAG properties

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 1)]
        [DisplayName("Agentic RAG connection")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"A connection to a specific knowledge box in Progress Agentic RAG. Select which connection this widget should use to search and answer questions.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Manage connections in \",\"Presentation\":[]},{\"Value\":\"Administration > Progress Agentic Rag connections\",\"Presentation\":[3]}]}]")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetConfiguredKnowledgeBoxes()", ServiceWarningMessage = "No Agentic RAG connections are found.")]
        [Placeholder("Select connection")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\"}]}")]
        public string KnowledgeBoxName { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 2)]
        [DisplayName("Search configuration")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"A saved set of search settings that the AI uses to find content.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Can be found in Progress Agentic Rag portal \",\"Presentation\":[]},{\"Value\":\"Search > Saved configurations\",\"Presentation\":[3]}]}]")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetSearchConfigurations(knowledgeBoxName=\'{0}\')", ServiceCallParameters = "[{ \"knowledgeBoxName\" : \"{0}\"}]")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\"}]}")]
        public string ConfigurationName { get; set; }

        #endregion

        #region SAIA properties

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 1)]
        [DisplayName("Select an AI assistant")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"AI assistants are created and managed in\",\"Presentation\":[]},{\"Value\":\"Administration > AI assistants\",\"Presentation\":[2]}]}]")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetAiAssistantChoices()", ServiceWarningMessage = "No AI assistants are found.")]
        [Placeholder("Select")]
        [DefaultValue("")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"SAIA\"}]}")]
        public string AssistantApiKey { get; set; }

        #endregion

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 3)]
        [DisplayName("Nickname of the assistant")]
        [Description("Name displayed before assistant's messages in the chat.")]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\" },{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"SAIA\" }]}")]
        public string Nickname { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 4)]
        [DisplayName("Greeting message")]
        [Description("You can customize the bot's initial words by adding a phrase that triggers conversation on a specific topic.")]
        [DataType(customDataType: KnownFieldTypes.TextArea)]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\" },{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"SAIA\" }]}")]
        public string GreetingMessage { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 5)]
        [DisplayName("Avatar of the assistant")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false, LiveData = false)]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\" },{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"SAIA\" }]}")]
        public MixedContentContext AssistantAvatar { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 6)]
        [DisplayName("Display sources")]
        [Description("In answers, display links to sources of information.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\"}]}")]
        public bool? ShowSources { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 7)]
        [DisplayName("Enable visitor feedback")]
        [Description("If enabled, site visitors can provide feedback on the assistant's answer in the chat window.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\"}]}")]
        public bool? ShowFeedback { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 0)]
        [DisplayName("Chat window mode")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Display overlay: \",\"Presentation\":[0]},{\"Value\":\"Chat appears in a small window, usually in the bottom right corner of the screen. It requires user interaction to open and overlays parts of the page content.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Display inline: \",\"Presentation\":[0]},{\"Value\":\"Chat area is integrated into the page layout and does not overlay other elements. Suitable for long assistant responses and prompts.\",\"Presentation\":[]}]}]")]
        public AssistantDisplayMode DisplayMode { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 1)]
        [DisplayName("Opening chat icon")]
        [Description("Select a custom icon for opening chat window. If left empty, default icon will be displayed.")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"DisplayMode\",\"operator\":\"Equals\",\"value\":\"modal\"}]}")]
        public MixedContentContext OpeningChatIcon { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 2)]
        [DisplayName("Closing chat icon")]
        [Description("Select a custom icon for closing chat window. If left empty, default icon will be displayed.")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"DisplayMode\",\"operator\":\"Equals\",\"value\":\"modal\"}]}")]
        public MixedContentContext ClosingChatIcon { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 3)]
        [DisplayName("Container ID")]
        [Description("ID of the HTML element that will host the chat widget.")]
        [DefaultValue("sf-assistant-chat-container")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"DisplayMode\",\"operator\":\"Equals\",\"value\":\"inline\"}]}")]
        public string ContainerId { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 0)]
        [DisplayName("Placeholder text in the message box")]
        [DefaultValue("Ask anything...")]
        public string Placeholder { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 1)]
        [DisplayName("Notice")]
        [Description("Text displayed under the message box, informing users that they are interacting with AI.")]
        [DefaultValue("You are interacting with an AI-powered assistant and the responses are generated by AI.")]
        [DataType(customDataType: KnownFieldTypes.TextArea)]
        public string Notice { get; set; }

        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS for custom design")]
        [Placeholder("type URL or path to file...")]
        public string CustomCss { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 0)]
        [DefaultValue("Helpful")]
        [DisplayName("Positive feedback tooltip")]
        public string PositiveFeedbackTooltip { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 1)]
        [DefaultValue("Not helpful")]
        [DisplayName("Negative feedback tooltip")]
        public string NegativeFeedbackTooltip { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 2)]
        [DefaultValue("Thank you for your feedback!")]
        [DisplayName("Thank you message")]
        public string ThankYouMessage { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection(LabelsSectionName, 3)]
        [DefaultValue("Sources")]
        [DisplayName("Sources header")]
        public string SourcesHeader { get; set; }

        [Browsable(false)]
        public string EmptyLinkText
        {
            get
            {
                return Res.Get<SitefinityAssistantWidgetResources>().EmptyWidgetText;
            }
        }

        /// <inheritdoc />
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return AssistantType == Sitefinity.Assistant.Constants.PARAG ? KnowledgeBoxName.IsNullOrEmpty() : AssistantApiKey.IsNullOrEmpty();
            }
        }

        /// <summary>
        /// Gets the widget CSS class.
        /// </summary>
        /// <value>
        /// The widget CSS class.
        /// </value>
        [Browsable(false)]
        public string WidgetCssClass
        {
            get
            {
                return WidgetIconCssClass;
            }
        }

        public ActionResult Index()
        {
            var cdnUrlFormatString = BuildCdnUrlFormatString();
            var viewModel = new SitefinityAssistantViewModel(
                // SAIA
                this.AssistantApiKey,

                // PARAG
                AssistantType == Sitefinity.Assistant.Constants.PARAG ? this.KnowledgeBoxName : null,
                this.ConfigurationName,
                this.ShowFeedback.HasValue ? this.ShowFeedback.Value : true,
                this.ShowSources.HasValue ? this.ShowSources.Value : true,

                // Common
                this.Nickname,
                this.GreetingMessage,
                this.AssistantAvatar,
                this.DisplayMode,
                this.OpeningChatIcon,
                this.ClosingChatIcon,
                this.ContainerId,
                this.Placeholder,
                this.Notice,
                this.CustomCss,
                this.CssClass,
                AssistantType == Sitefinity.Assistant.Constants.PARAG ?
                RouteHelper.ResolveUrl("/api/default/AgenticRag/", UrlResolveOptions.Rooted) : 
                RouteHelper.ResolveUrl("/api/default/SitefinityAssistantChatService/", UrlResolveOptions.Rooted),
                cdnUrlFormatString,
                AssistantType == Sitefinity.Assistant.Constants.PARAG ? "ProgressARAGChatService" : "AzureAssistantChatService",
                this.PositiveFeedbackTooltip,
                this.NegativeFeedbackTooltip,
                this.ThankYouMessage,
                this.SourcesHeader);

            return View("Index", viewModel);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        private VersionInfoDto RetrieveVersionInfo()
        {
            var functionName = AssistantType == Sitefinity.Assistant.Constants.PARAG
                ? "Default.GetPARAGAssistantVersionInfo()"
                : "Default.GetAiAssistantVersionInfo()";
            var relativePath = $"/api/default/{functionName}";
            var requestUri = System.Web.HttpContext.Current.Request.Url;
            var baseUri = new Uri(requestUri.GetLeftPart(UriPartial.Authority));
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(baseUri, relativePath));

            var httpResponseMessage = this.httpClient.SendAsync(request).GetAwaiter().GetResult();
            httpResponseMessage.EnsureSuccessStatusCode();

            var result = httpResponseMessage.Content.ReadAsAsync<VersionInfoDto>().GetAwaiter().GetResult();

            return result;
        }

        private string BuildCdnUrlFormatString()
        {
            using (var config = AssistantType == Sitefinity.Assistant.Constants.PARAG ? 
                new SitefinityAssistantConfigAccessor("AgenticRAGConfig", "assistant") : 
                new SitefinityAssistantConfigAccessor("SitefinityAssistantConfig"))
            {
                string version = null;
                try
                {
                    var versionInfo = this.RetrieveVersionInfo();
                    version = versionInfo?.ProductVersion;
                }
                catch (Exception ex)
                {
                    string logMessage = $"Error retrieving assistant version info. Please check the assistant configuration details: {ex.Message}";
                    Log.Write(logMessage, ConfigurationPolicy.Trace);
                }

                string cdnHostName = config.CdnHostName;
                if (string.IsNullOrEmpty(cdnHostName))
                    throw new ArgumentException("CdnHostName is not configured in AgenticRAGConfig -> Assistant.");

                string versionSuffix = string.IsNullOrEmpty(version) ? string.Empty : $"?ver={version}";
                string placeholder = "{0}";

                return $"https://{cdnHostName}/{placeholder}{versionSuffix}";
            }
        }

        public enum AssistantDisplayMode
        {
            [Description("Display modal")]
            [EnumDisplayName("Display overlay")]
            modal,
            [Description("Display inline")]
            [EnumDisplayName("Display inline")]
            inline
        }
    }
}
