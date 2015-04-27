
namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.StyleSheet
{
    /// <summary>
    /// This is a contract for the model of the StyleSheet widget.
    /// </summary>
    public class IStyleSheetModel
    {
        /// <summary>
        /// Custom CSS code for linking style sheets.
        /// </summary>
        string CustomCssCode { get; set; }

        /// <summary>
        /// Gets or sets the URL to the StyleSheet.
        /// </summary>
        string ResourceUrl { get; set; }

        /// <summary>
        /// Description of the CSS resource.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the media type.
        /// </summary>
        string MediaType { get; set; }

        /// <summary>
        /// Gets or sets how the css will be referenced.
        /// </summary>
        ResourceMode Mode { get; set; }
    }
}
