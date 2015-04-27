
namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.StyleSheet
{
    /// <summary>
    /// This class represents the model of the StyleSheet widget.
    /// </summary>
    public class StyleSheetModel : IStyleSheetModel
    {
        #region Properties

        /// <inheritDocs/>
        public string InlineStyles { get; set; }

        /// <inheritDocs/>
        public string Description { get; set; }

        /// <inheritDocs/>
        public string MediaType { get; set; }

        /// <inheritDocs/>
        public ResourceMode Mode { get; set; }

        /// <inheritDocs/>
        public virtual string GetMarkup()
        {
            string markup;

            if (this.Mode == ResourceMode.Inline)
            {
                markup = this.GetInlineMarkup();
            }
            else
            {
                markup = string.Empty;
            }

            return markup;
        }

        #endregion

        /// <summary>
        /// Gets the markup for inline CSS.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual string GetInlineMarkup()
        {
            throw new System.NotImplementedException();
        }
    }
}
