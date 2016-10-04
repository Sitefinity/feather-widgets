using System;
using System.Web.UI;
using System.Web.Hosting;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript
{
    public class JavaScriptModel : IJavaScriptModel
    {
        /// <summary>
        /// Gets or sets the javascript code entered by the user which will be inlined in the page.
        /// </summary>
        /// <value>The inline code.</value>
        public string InlineCode { get; set; }

        /// <summary>
        /// Gets or sets the URL of the file where the javascript is stored.
        /// </summary>
        /// <value>The file URL.</value>
        public string FileUrl { get; set; }

        /// <summary>
        /// Gets or sets the position in the document where the code will be inserted.
        /// </summary>
        /// <value>The embed position.</value>
        public EmbedPosition Position { get; set; }

        /// <summary>
        /// Gets or sets whether the widget will use entered by the user custom code or the url to a file.
        /// </summary>
        /// <value>The mode.</value>
        public ResourceMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the description of the used code.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets the view model of the current model that will be displayed by the view.
        /// </summary>
        /// <returns></returns>
        public virtual JavaScriptViewModel GetViewModel()
        {
            var script = this.BuildScriptTag();

            return new JavaScriptViewModel()
            {
                Position = this.Position,
                JavaScriptCode = script
            };
        }

        /// <summary>
        /// Builds the script tag with url pointing to the script file or containig the script in its content.
        /// </summary>
        /// <returns></returns>
        public virtual string BuildScriptTag()
        {
            var scriptTag = string.Empty;
            if (this.Mode == ResourceMode.Reference && !string.IsNullOrEmpty(this.FileUrl))
            {
                var scriptUrl = this.FileUrl;
                if (scriptUrl.StartsWith("~/"))
				{
                    scriptUrl = HostingEnvironment.ApplicationVirtualPath + scriptUrl.Substring(1);
                    if (scriptUrl.StartsWith(@"//"))
                        scriptUrl = scriptUrl.Substring(1);
				}

				scriptTag = string.Format(@"<script type=""text/javascript"" src=""{0}""></script>", scriptUrl);
            }
            else if(this.Mode == ResourceMode.Inline && !string.IsNullOrEmpty(this.InlineCode))
            {
                scriptTag = string.Format(@"<script type=""text/javascript"">{0}{1}{0}</script>",
                                                    Environment.NewLine, this.InlineCode);
            }

            return scriptTag;
        }

        /// <summary>
        /// Places the script before the end of the body.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="script">The script.</param>
        public virtual void PlaceScriptBeforeBodyEnd(Page page, string script)
        {
            page.PlaceScriptBeforeBodyEnd(script);
        }
    }
}