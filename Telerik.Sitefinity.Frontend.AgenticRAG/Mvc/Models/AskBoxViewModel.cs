using System.Collections.Generic;

namespace Telerik.Sitefinity.Frontend.AgenticRAG.Mvc.Models
{
    public class AskBoxViewModel
    {
        public string KnowledgeBoxName { get; set; }

        public string SearchConfigurationName { get; set; }

        public string ResultsPageUrl { get; set; }

        public string CssClass { get; set; }

        public string Placeholder { get; set; }

        public string ButtonLabel { get; set; }

        public string SuggestionsLabel { get; set; }

        public string Suggestions { get; set; }

        public string ContentTypes { get; set; }

        public string LastModified { get; set; }
    }
}
