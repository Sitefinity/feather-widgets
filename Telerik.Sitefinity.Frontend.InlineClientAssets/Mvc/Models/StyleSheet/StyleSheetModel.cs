
namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.StyleSheet
{
    /// <summary>
    /// This class represents the model of the StyleSheet widget.
    /// </summary>
    public class StyleSheetModel : IStyleSheetModel
    {
        /// <inheritDocs/>
        public string CustomCssCode { get; set; }

        /// <inheritDocs/>
        public string Description { get; set; }

        /// <inheritDocs/>
        public string MediaType { get; set; }
    }
}
