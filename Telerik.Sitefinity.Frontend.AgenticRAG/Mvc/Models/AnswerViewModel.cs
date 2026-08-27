using Progress.Sitefinity.Renderer.Entities.Content;

namespace Telerik.Sitefinity.Frontend.AgenticRAG.Mvc.Models
{
    public class AnswerViewModel
    {
        public string Title { get; set; }

        public string AssistantAvatarUrl { get; set; }

        public bool ShowSources { get; set; }

        public string Notice { get; set; }

        public bool ShowFeedback { get; set; }

        public string CssClass { get; set; }

        public string SearchedPhraseLabel { get; set; }

        public string PositiveFeedbackTooltip { get; set; }

        public string NegativeFeedbackTooltip { get; set; }

        public string ThankYouMessage { get; set; }

        public string ExpandAnswerLabel { get; set; }

        public string CollapseAnswerLabel { get; set; }

        public string LoadingLabel { get; set; }

        public string ConfigName { get; set; }

        public string KnowledgeBoxName { get; set; }

        public string SearchQuery { get; set; }

        public string ContentTypes { get; set; }

        public string LastModified { get; set; }

        public string ServiceUrl { get; set; }
    }
}
