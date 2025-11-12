using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Assistant.DTOs;
using Telerik.Sitefinity.Frontend.Assistant.Mvc.Models;
using Telerik.Sitefinity.Frontend.Assistant.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Web.Api.Configuration;
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
        private readonly HttpClient httpClient;

        public SitefinityAssistantController()
        {
            this.httpClient = new HttpClient();
        }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 0)]
        [DisplayName("Select an AI assistant")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"AI assitants are created and managed in\",\"Presentation\":[]},{\"Value\":\"Administration > AI assistants\",\"Presentation\":[2]}]}]")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetAiAssistantChoices()", ServiceWarningMessage = "No AI assistants are found.")]
        [Placeholder("Select")]
        [DefaultValue("")]
        public string AssistantApiKey { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 1)]
        [DisplayName("Nickname of the assistant")]
        [Description("Name displayed before assistant's messages in the chat.")]
        [DefaultValue("AI assistant")]
        public string Nickname { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 2)]
        [DisplayName("Greeting message")]
        [Description("You can customize the bot's initial words by adding a phrase that triggers conversation on a specific topic.")]
        [DataType(customDataType: KnownFieldTypes.TextArea)]
        public string GreetingMessage { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 3)]
        [DisplayName("Avatar of the assistant")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false, LiveData = false)]
        public MixedContentContext AssistantAvatar { get; set; }

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
                return AssistantApiKey.IsNullOrEmpty();
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
            var serviceUrl = "/api/default/SitefinityAssistantChatService/";
            var viewModel = new SitefinityAssistantViewModel(
                this.AssistantApiKey,
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
                serviceUrl,
                cdnUrlFormatString);

            return View("Index", viewModel);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        private VersionInfoDto RetrieveVersionInfo(string adminAPIBaseUrl)
        {
            var pathUrl = "/Version";
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri(new Uri(adminAPIBaseUrl),
                pathUrl)
            );

            var httpResponseMessage = this.httpClient.SendAsync(request).GetAwaiter().GetResult();
            httpResponseMessage.EnsureSuccessStatusCode();

            var result = httpResponseMessage.Content.ReadAsAsync<VersionInfoDto>().GetAwaiter().GetResult();

            return result;
        }

        private string BuildCdnUrlFormatString()
        {
            using (var config = new SitefinityAssistantConfigAccessor())
            {
                string version = null;
                try
                {
                    var versionInfo = this.RetrieveVersionInfo(config.AdminApiBaseUrl);
                    version = versionInfo?.ProductVersion;
                }
                catch (Exception ex)
                {
                    string logMessage = $"Error retrieving assistant version info. Please check the assistant configuration details: {ex.Message}";
                    Log.Write(logMessage, ConfigurationPolicy.Trace);
                }

                string cdnHostName = config.CdnHostName;
                string rootRelativePath = config.CdnRootFolderRelativePath == null ?
                   "staticfiles/" :
                   (string.IsNullOrEmpty(config.CdnRootFolderRelativePath) ? string.Empty : $"{config.CdnRootFolderRelativePath.Trim('/')}/");
                string versionSuffix = string.IsNullOrEmpty(version) ? string.Empty : $"?ver={version}";
                string placeholder = "{0}";

                return $"https://{cdnHostName}/{rootRelativePath}{placeholder}{versionSuffix}";
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
