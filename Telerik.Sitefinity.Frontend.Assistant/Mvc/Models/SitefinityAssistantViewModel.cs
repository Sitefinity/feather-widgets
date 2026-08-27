using Progress.Sitefinity.Renderer.Entities.Content;
using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Libraries;
using static Telerik.Sitefinity.Frontend.Assistant.Mvc.Controllers.SitefinityAssistantController;

namespace Telerik.Sitefinity.Frontend.Assistant.Mvc.Models
{
    public class SitefinityAssistantViewModel
    {
        private readonly string cdnUrlFormatString;

        public SitefinityAssistantViewModel(
            string assistantApiKey,
            string knowledgeBoxName,
            string configName,
            bool showFeedback,
            bool showSources,
            string nickname,
            string greetingMessage,
            MixedContentContext assistantAvatar,
            AssistantDisplayMode displayMode,
            MixedContentContext openingChatIcon,
            MixedContentContext closingChatIcon,
            string containerId,
            string placeholder,
            string notice,
            string customCss,
            string cssClass,
            string serviceUrl,
            string cdnUrlFormatString,
            string chatServiceName,
            string positiveFeedbackTooltip,
            string negativeFeedbackTooltip,
            string thankYouMessage,
            string sourcesHeader)
        {
            this.AssistantApiKey = assistantApiKey;
            this.KnowledgeBoxName = knowledgeBoxName;
            this.ConfigName = configName;
            this.ShowFeedback = showFeedback;
            this.ShowSources = showSources;
            this.ServiceUrl = serviceUrl;
            this.AssistantGreetingMessage = greetingMessage;
            this.DisplayMode = displayMode;
            this.Placeholder = placeholder;
            this.CustomCss = customCss;
            this.CssClass = cssClass;
            this.cdnUrlFormatString = cdnUrlFormatString;
            this.AssistantDisplayName = string.IsNullOrWhiteSpace(nickname) ? NicknameDefaultValue : nickname;
            this.ContainerId = string.IsNullOrWhiteSpace(containerId) ? DefaultContainerId : containerId;
            this.Notice = string.IsNullOrWhiteSpace(notice) ? NoticeDefaultValue : notice;

            if (assistantAvatar == null)
            {
                this.AssistantAvatarUrl = this.GetImageUrl(DefaultAssistantIcon);
            }
            else
            {
                this.SetAssistantAvatarImageUrl(assistantAvatar, "AssistantAvatarUrl");
            }

            this.SetImageUrl(openingChatIcon, "OpeningChatIconUrl");
            this.SetImageUrl(closingChatIcon, "ClosingChatIconUrl");

            this.ChatServiceName = chatServiceName;
            this.PositiveFeedbackTooltip = positiveFeedbackTooltip;
            this.NegativeFeedbackTooltip = negativeFeedbackTooltip;
            this.ThankYouMessage = thankYouMessage;
            this.SourcesHeader = sourcesHeader;
        }

        #region SAIA properties

        public string AssistantApiKey { get; set; }

        #endregion

        #region PARAG properties

        public string KnowledgeBoxName { get; set; }

        public string ConfigName { get; set; }

        public bool ShowFeedback { get; set; }

        public bool ShowSources { get; set; }

        #endregion

        public string ServiceUrl { get; set; }

        public string AssistantDisplayName { get; set; }

        public string AssistantGreetingMessage { get; set; }

        public MixedContentContext AssistantAvatar { get; set; }

        public AssistantDisplayMode DisplayMode { get; set; }

        public string ChatServiceName { get; set; }

        public MixedContentContext OpeningChatIcon { get; set; }

        public MixedContentContext ClosingChatIcon { get; set; }

        public string Placeholder { get; set; }

        public string Notice { get; set; }

        public string ContainerId { get; set; }

        public string OpeningChatIconUrl { get; set; }

        public string ClosingChatIconUrl { get; set; }

        public string AssistantAvatarUrl { get; set; }

        public string CustomCss { get; set; }

        public string CssClass { get; set; }

        public string PositiveFeedbackTooltip { get; set; }

        public string NegativeFeedbackTooltip { get; set; }

        public string ThankYouMessage { get; set; }

        public string SourcesHeader { get; set; }

        public string GetCdnUrl(string cdnFile)
        {
            return string.Format(this.cdnUrlFormatString, cdnFile);
        }

        public string GetImageUrl(string filePath, string version = null)
        {
            using (var config = !string.IsNullOrEmpty(this.KnowledgeBoxName) ? new SitefinityAssistantConfigAccessor("AgenticRAGConfig", "assistant") :
                         !string.IsNullOrEmpty(this.AssistantApiKey) ? new SitefinityAssistantConfigAccessor("SitefinityAssistantConfig") :
                         null)
            {
                if (config != null)
                {
                    string versionSuffix = string.IsNullOrEmpty(version) ? string.Empty : $"?ver={version}";
                    string hostName = config.CdnHostName;
                    if (string.IsNullOrEmpty(hostName))
                        throw new ArgumentException("CdnHostName is not configured in AgenticRAGConfig -> Assistant.");

                    return $"https://{hostName}/{filePath}{versionSuffix}";
                }
            }

            return null;
        }

        private void SetImageUrl(MixedContentContext image, string propName)
        {
            if (image != null)
            {
                var imageProvider = image.Content[0].Variations[0].Source;
                var imageId = new Guid(image.ItemIdsOrdered[0]);
                var librariesManager = LibrariesManager.GetManager(imageProvider);
                var imageUrl = librariesManager.GetMediaItem(imageId).Url;
                var prop = this.GetType().GetProperty(propName);
                prop.SetValue(this, imageUrl);
            }
        }

        private void SetAssistantAvatarImageUrl(MixedContentContext image, string propName)
        {
            try
            {
                SetImageUrl(image, propName);
            }
            catch (UnauthorizedAccessException ex)
            {
                this.AssistantAvatarUrl = this.GetImageUrl(DefaultAssistantIcon);

                string logMessage = $"Error retrieving assistant avatar image. Please check the image permissions: {ex.Message}";
                Log.Write(logMessage, ConfigurationPolicy.Trace);
            }
        }

        private const string DefaultAssistantIcon = "chat-avatar.svg";
        private const string DefaultContainerId = "sf-assistant-chat-container";
        private const string NicknameDefaultValue = "AI assistant";
        private const string NoticeDefaultValue = "You are interacting with an AI-powered assistant and the responses are generated by AI.";
    }
}
