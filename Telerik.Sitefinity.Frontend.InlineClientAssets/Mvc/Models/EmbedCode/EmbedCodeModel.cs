using System;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.EmbedCode
{
    public class EmbedCodeModel : IEmbedCodeModel
    {
        /// <inheritDocs/>
        public string InlineCode { get; set; }

        /// <inheritDocs/>
        public string Description { get; set; }

        /// <inheritDocs/>
        public EmbedCodeViewModel GetViewModel()
        {
            return new EmbedCodeViewModel
            {
                EmbedCode = this.InlineCode,
                Description = this.Description
            };
        }
    }
}