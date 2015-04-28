namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models
{
    /// <summary>
    /// Specifies where to embed the asset
    /// </summary>
    public enum EmbedPosition
    {
        /// <summary>
        /// Embeded in the head tag
        /// </summary>
        Head,
        /// <summary>
        /// Emeded in the same place where it is positioned in the page
        /// </summary>
        InPlace,
        /// <summary>
        /// Embed before the closing body tag
        /// </summary>
        BeforeBodyEndTag
    };
}