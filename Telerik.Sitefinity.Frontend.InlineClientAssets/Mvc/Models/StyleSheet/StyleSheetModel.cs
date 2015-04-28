
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Web;

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
        public string ResourceUrl { get; set; }

        /// <inheritDocs/>
        public string Description { get; set; }

        /// <inheritDocs/>
        public string MediaType
        {
            get
            {
                return this.mediaType;
            }
            set
            {
                this.mediaType = value;
            }
        }

        /// <inheritDocs/>
        public ResourceMode Mode { get; set; }

        #endregion

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
                markup = this.GetReferenceMarkup();
            }

            return markup;
        }

        /// <inheritDocs/>
        public virtual string GetShortInlineStyles()
        {
            var inlineStyles = this.InlineStyles;
            var inlineStylesByLine = inlineStyles.Split(new[] { '\r', '\n' });
            string firstLinesContent;
            if (inlineStylesByLine.Length > 1)
            {
                firstLinesContent = inlineStylesByLine[0] + inlineStylesByLine[1] + "...";
            }
            else
            {
                firstLinesContent = inlineStyles;
            }

            return firstLinesContent;
        }

        /// <summary>
        /// Gets the markup for inline CSS.
        /// </summary>
        /// <returns>HTML markup for inline CSS.</returns>
        protected virtual string GetInlineMarkup()
        {
            string markup;
            if (!string.IsNullOrEmpty(this.InlineStyles))
            {
                markup = string.Format(@"<style type=""text/css"" media=""{1}"">{0}</style>",
                                      HttpUtility.HtmlEncode(this.InlineStyles), HttpUtility.HtmlEncode(this.MediaType));
            }
            else
            {
                markup = null;
            }

            return markup;
        }

        /// <summary>
        /// Gets the reference markup.
        /// </summary>
        /// <returns>HTML markup for referenced CSS.</returns>
        protected virtual string GetReferenceMarkup()
        {
            if (this.ResourceUrl == null)
                return null;

            var url = this.ResourceUrl.StartsWith("/") ? RouteHelper.ResolveUrl("~" + this.ResourceUrl, UrlResolveOptions.Rooted) : this.ResourceUrl;

            var tag = new TagBuilder("link");
            tag.Attributes["href"] = url;
            tag.Attributes["rel"] = "stylesheet";
            tag.Attributes["type"] = "text/css";
            tag.Attributes["media"] = this.MediaType;

            return tag.ToString(TagRenderMode.SelfClosing);
        }

        private string mediaType = "all";
    }
}
