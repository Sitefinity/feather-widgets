namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript
{
    /// <summary>
    /// Specifies where to embed the script
    /// </summary>
    public enum ScriptEmbedPosition
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