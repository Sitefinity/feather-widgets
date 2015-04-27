
using System.Web;
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
        protected virtual string GetInlineMarkup()
        {
            var markup = string.Empty;
            if (!string.IsNullOrEmpty(this.InlineStyles))
            {
                markup = string.Format(@"<style type=""text/css"">{0}</style>",
                                      HttpContext.Current.Server.HtmlEncode(this.InlineStyles));
            }

            return markup;
        }
    }
}
